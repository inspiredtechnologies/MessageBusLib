using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MessageBusLib;
using SystemConfiguration;

namespace NetworkEqptMonitorClient
{
  public partial class FormClient : Form
  {
    private const int NmsHealthStatusTimeOut = 30;
    private const int TreeLevelEquipment = 1;

    private const int EqptStateUnknown = 1;
    private const int EqptStateNormal = 2;
    private const int EqptStateDegraded = 3;
    private const int EqptStateFault = 4;

    private Collection<NetworkEqpt> _Equipment = null;
    private string _MessageBusBrokerUri = NetworkEqptMonitorConfig.MessageBusBrokerUri;
    private TopicConnectionFactory _MessageBus = null;
    private string _MessageBusClientId = string.Empty;

    private Topic _EquipmentConfigurationTopic = null;
    private Topic _EquipmentStatusUpdateTopic = null;
    private Topic _ConfigurationUpdateRequestTopic = null;
    private Topic _NmsHeartBeatTopic = null;

    private NetworkEqpt _SelectedEquipment = null;
    private TreeNode _SelectedEquipmentNode = null;
    private FormSetup _SetupForm = null;

    private HealthState _NmsHealthStatus = HealthState.Unknown;
    private DateTime _NmsHealthStatusLastUpdatedTime = DateTime.MinValue;

    public FormClient()
    {
      InitializeComponent();
    }

    private void FormClient_Load(object sender, EventArgs e)
    {
      try
      {
        if (!ConnectToMessageBus())
        {
          Console.WriteLine("Error: Could not connect to the Message Bus");
          Environment.Exit(1);
        }
        else
        {
          if (!PublishSubscribeTopics())
          {
            Console.WriteLine("Error: Topics subscription failure");
            Environment.Exit(1);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_MessageBus != null)
      {
        _MessageBus.TopicUpdatedEvent -= MessageBus_TopicUpdatedEvent;
        _MessageBus.ConnectionExceptionEvent -= MessageBus_ConnectionExceptionEvent;
        UnpublishUnsubscribeTopics();
        _MessageBus.Dispose();
        _MessageBus = null;
      }
    }

    private bool ConnectToMessageBus()
    {
      try
      {
        _MessageBusClientId = Guid.NewGuid().ToString();
        _MessageBus = new TopicConnectionFactory(_MessageBusBrokerUri, _MessageBusClientId, MessageBusType.ActiveMQ);
        _MessageBus.TopicUpdatedEvent += MessageBus_TopicUpdatedEvent;
        _MessageBus.ConnectionExceptionEvent += MessageBus_ConnectionExceptionEvent;
        return _MessageBus.Connect();
      }
      catch
      {
        throw;
      }
    }

    private bool PublishSubscribeTopics()
    {
      _EquipmentConfigurationTopic = new Topic(NetworkEqptMonitorConfig.EquipmentConfigurationTopicName, string.Empty);
      _EquipmentStatusUpdateTopic = new Topic(NetworkEqptMonitorConfig.EquipmentStatusUpdateTopicName, string.Empty);
      _ConfigurationUpdateRequestTopic = new Topic(NetworkEqptMonitorConfig.ConfigurationUpdateRequestTopicName, string.Empty);
      _NmsHeartBeatTopic = new Topic(NetworkEqptMonitorConfig.NmsHeartBeatTopicName, string.Empty);

      return _MessageBus.Subscribe(_EquipmentConfigurationTopic, true) && _MessageBus.Subscribe(_EquipmentStatusUpdateTopic) && 
        _MessageBus.Subscribe(_NmsHeartBeatTopic, true) && _MessageBus.Publish(_ConfigurationUpdateRequestTopic);
    }

    private void UnpublishUnsubscribeTopics()
    {
      try
      {
        _MessageBus.UnPublish(_ConfigurationUpdateRequestTopic);
        _MessageBus.UnSubscribe(_NmsHeartBeatTopic);
        _MessageBus.UnSubscribe(_EquipmentStatusUpdateTopic);
        _MessageBus.UnSubscribe(_EquipmentConfigurationTopic);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    private void MessageBus_TopicUpdatedEvent(object sender, TopicUpdatedEventArg e)
    {
      ThreadPool.QueueUserWorkItem(delegate
      {
        if (e.UpdatedTopic.Name.Equals(NetworkEqptMonitorConfig.EquipmentConfigurationTopicName,
          StringComparison.InvariantCulture))
        {
          HandleEquipmentConfigurationUpdate(e.UpdatedTopic.Value);
        }
        else if (e.UpdatedTopic.Name.Equals(NetworkEqptMonitorConfig.EquipmentStatusUpdateTopicName,
          StringComparison.InvariantCulture))
        {
          HandleEquipmentStatusUpdate(e.UpdatedTopic.Value);
        }
        else if (e.UpdatedTopic.Name.Equals(NetworkEqptMonitorConfig.NmsHeartBeatTopicName,
          StringComparison.InvariantCulture))
        {
          HandleNmsStatusUpdate(e.UpdatedTopic.Value);
        }
      }, null);
    }

    private void HandleEquipmentConfigurationUpdate(string value)
    {
      try
      {
        try
        {
          _Equipment = (Collection<NetworkEqpt>)Helper.DeserializeObject(typeof(Collection<NetworkEqpt>), value);
          this.Invoke((MethodInvoker)delegate
          {
            RefreshEquipmentInventory();
            UpdateAllEquipmentListView();
          });
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void HandleEquipmentStatusUpdate(string value)
    {
      try
      {
        NetworkEqpt updatedEquipment = (NetworkEqpt)Helper.DeserializeObject(typeof(NetworkEqpt), value);
        if (updatedEquipment != null && _Equipment != null)
        {
          tvEqpt.Invoke((MethodInvoker)delegate
          {
            UpdateEqptStatus(updatedEquipment);
          });
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    private void HandleNmsStatusUpdate(string statusMessage)
    {
      if (string.IsNullOrWhiteSpace(statusMessage))
        return;

      HealthState serverStatus = HealthState.Unknown;
      if (Enum.TryParse<HealthState>(statusMessage, out serverStatus))
      {
        _NmsHealthStatus = serverStatus;
        _NmsHealthStatusLastUpdatedTime = DateTime.Now;
        UpdateServerStatus();
      }
    }

    private void RefreshEquipmentInventory()
    {
      tvEqpt.Nodes[0].Nodes.Clear();

      if (_Equipment != null)
      {
        foreach (NetworkEqpt eqpt in _Equipment)
        {
          TreeNode newEqptNode = tvEqpt.Nodes[0].Nodes.Add(eqpt.Name);
          newEqptNode.Tag = eqpt;
          newEqptNode.ImageIndex = newEqptNode.SelectedImageIndex = EqptStateUnknown;
        }
      }
      tvEqpt.SelectedNode = tvEqpt.Nodes[0];
      tvEqpt.Nodes[0].ExpandAll();
    }

    private void UpdateAllEquipmentListView()
    {
      lvAllEqpt.Invoke((MethodInvoker)delegate
      {
        lvAllEqpt.Items.Clear();
        if (_Equipment != null)
        {
          foreach (NetworkEqpt eqpt in _Equipment)
          {
            ListViewItem item = lvAllEqpt.Items.Add(eqpt.Name);
            item.SubItems.Add(eqpt.IpAddress);
            ListViewItem.ListViewSubItem subItem = item.SubItems.Add(eqpt.Health.ToString());
            item.UseItemStyleForSubItems = false;
            subItem.ForeColor = GetColorForHealthState(eqpt.Health);
          }
        }
      });
    }

    private Color GetColorForHealthState(HealthState healthState)
    {
      switch (healthState)
      {
        case HealthState.Unknown:
          return Color.Gray;

        case HealthState.Normal:
          return Color.LimeGreen;

        case HealthState.Degraded:
          return Color.Orange;

        case HealthState.Fault:
          return Color.Red;

        default:
          return Color.Black;
      }
    }

    private void UpdateServerStatus()
    {
      statusStripMain.Invoke((MethodInvoker)delegate
      {
        tsslServerStatus.Text = _NmsHealthStatus.ToString();

        switch (_NmsHealthStatus)
        {
          case HealthState.Normal:
            tsslServerStatus.ForeColor = Color.Green;
            break;

          case HealthState.Degraded:
            tsslServerStatus.ForeColor = Color.Orange;
            break;

          case HealthState.Unknown:
            tsslServerStatus.ForeColor = Color.Gray;
            break;

          default:
            tsslServerStatus.ForeColor = Color.Red;
            break;
        }
      });
    }

    private void MessageBus_ConnectionExceptionEvent(object sender, ConnectionExceptionEventArg e)
    {
      Console.WriteLine(e.ErrorMessage);
    }

    private void UpdateEqptStatus(NetworkEqpt updatedEqpt)
    {
      try
      {
        if (updatedEqpt != null && _Equipment != null)
        {
          foreach (TreeNode assetNode in tvEqpt.Nodes[0].Nodes)
          {
            NetworkEqpt eqpt = (NetworkEqpt)assetNode.Tag;
            if (eqpt.Equals(updatedEqpt))
            {
              if (eqpt.Health != updatedEqpt.Health)
              {
                eqpt.Health = updatedEqpt.Health;
                UpdateSiteNodeVisualState(assetNode, updatedEqpt.Health);
              }
              eqpt.Copy(updatedEqpt);
              break;
            }
          }
          foreach (ListViewItem item in lvAllEqpt.Items)
          {
            if (item.Text.Equals(updatedEqpt.Name, StringComparison.InvariantCulture))
            {
              item.SubItems[2].Text = updatedEqpt.Health.ToString();
              item.SubItems[2].ForeColor = GetColorForHealthState(updatedEqpt.Health);
              break;
            }
          }
        }
      }
      catch
      {
        throw;
      }
    }

    private void UpdateSiteNodeVisualState(TreeNode assetNode, HealthState health)
    {
      string eventMessage = string.Empty;
      NetworkEqpt eqpt = null;
      Color messageTextColor = SystemColors.ControlText;

      if (assetNode != null && assetNode.Tag != null && assetNode.Tag is NetworkEqpt)
      {
        eqpt = (NetworkEqpt)assetNode.Tag;

        switch (health)
        {
          case HealthState.Normal:
            assetNode.ForeColor = Color.Green;
            messageTextColor = Color.Green;
            assetNode.ImageIndex = assetNode.SelectedImageIndex = EqptStateNormal;
            eventMessage = eqpt.Name + " is online";
            break;

          case HealthState.Fault:
            assetNode.ForeColor = Color.Red;
            messageTextColor = Color.Red;
            assetNode.ImageIndex = assetNode.SelectedImageIndex = EqptStateFault;
            eventMessage = eqpt.Name + " is offline";
           break;

          case HealthState.Degraded:
            assetNode.ForeColor = Color.Orange;
            messageTextColor = Color.Orange;
            assetNode.ImageIndex = assetNode.SelectedImageIndex = EqptStateDegraded;
            eventMessage = eqpt.Name + " health has degraded";
            break;

          case HealthState.Unknown:
          default:
            assetNode.ForeColor = Color.DarkGray;
            assetNode.ImageIndex = assetNode.SelectedImageIndex = EqptStateUnknown;
            break;
        }
      }

      if (!string.IsNullOrEmpty(eventMessage) && eqpt != null)
      {
        InsertLiveEvent(DateTime.Now.ToString(), "Health Check Service", eqpt.Name, health.ToString(),
          eventMessage, messageTextColor);
      }
    }

    private void InsertLiveEvent(string timestamp, string source, string site, string severity, string status, Color messageTextColor)
    {
      ListViewItem item = lvLiveEvents.Items.Insert(0, timestamp);
      item.SubItems.Add(source);
      item.SubItems.Add(site);
      item.SubItems.Add(severity);
      ListViewItem.ListViewSubItem subItem = item.SubItems.Add(status);
      item.UseItemStyleForSubItems = false;
      subItem.ForeColor = messageTextColor;
      item.Tag = DateTime.Now;

      // Perform list maintenance to prevent memory usage buildup.
      if (lvLiveEvents.Items.Count > 1000)
      {
        if (lvLiveEvents.Items[lvLiveEvents.Items.Count - 1].Tag is DateTime)
        {
          DateTime eventTimestamp = (DateTime)lvLiveEvents.Items[lvLiveEvents.Items.Count - 1].Tag;
          if (DateTime.Now - eventTimestamp > TimeSpan.FromHours(6.0))
          {
            lvLiveEvents.Items.RemoveAt(lvLiveEvents.Items.Count - 1);
          }
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void setupToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (_SetupForm == null)
          _SetupForm = new FormSetup();
        _SetupForm.SetCurrentConfiguration(_Equipment);
        _SetupForm.EqptConfigUpdated = false;

        if (_SetupForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          tvEqpt.SelectedNode = null;
          Application.DoEvents();

          if (_SetupForm.EqptConfigUpdated)
          {
            SubmitEqptConfigChanges();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void SubmitEqptConfigChanges()
    {
      try
      {
        Collection<NetworkEqpt> newConfig = _SetupForm.TempEqptCfg;
        if (newConfig != null)
        {
          _ConfigurationUpdateRequestTopic.Value = Helper.SerializeObject(newConfig);
          _MessageBus.PublishUpdate(_ConfigurationUpdateRequestTopic);
        }
      }
      catch
      {
        throw;
      }
    }

    private void tvEqpt_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      try
      {
        if (e.Node.Level == TreeLevelEquipment)
        {
          _SelectedEquipment = (NetworkEqpt)e.Node.Tag;
          _SelectedEquipmentNode = e.Node;
        }
        else
        {
          _SelectedEquipment = null;
          _SelectedEquipmentNode = null;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (FormAbout aboutForm = new FormAbout())
      {
        aboutForm.ShowDialog();
      }
    }

  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MessageBusLib;

namespace MessageBusLibDemo
{
  public partial class FormTest : Form
  {
    private const string TopicNameA = "Chat Group A";
    private const string TopicNameB = "Chat Group B";

    private TopicConnectionFactory _MessageBus = null;
    private string _BrokerUri = string.Empty;
    private string _ClientId = Guid.NewGuid().ToString();
    private readonly StringBuilder _Builder = new StringBuilder();
    private Topic _TestTopicA = null;
    private Topic _TestTopicB = null;

    public FormTest()
    {
      InitializeComponent();
    }

    private void FormTest_Load(object sender, EventArgs e)
    {
      chkSubscribeTopicA.Text = "Subscribe " + TopicNameA;
      chkSubscribeTopicB.Text = "Subscribe " + TopicNameB;
    }

    private void FormTest_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_MessageBus != null)
      {
        if (btnConnect.Text.StartsWith("Disconnect"))
        {
          btnConnect.PerformClick();
        }
      }
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(txtServerIp.Text))
      {
        MessageBox.Show("Please specify the messaage bus server IP address");
        txtServerIp.Focus();
        return;
      }

      try
      {
        if (btnConnect.Text.StartsWith("Connect"))
        {
          _BrokerUri = @"failover:(tcp://" + txtServerIp.Text + @":61616)?transport.timeout=3000";
          _MessageBus = new TopicConnectionFactory(_BrokerUri, _ClientId, MessageBusType.ActiveMQ);
          _MessageBus.TopicUpdatedEvent += MessageBus_TopicUpdatedEvent;
          _MessageBus.ConnectionExceptionEvent += MessageBus_ConnectionExceptionEvent;

          if (!_MessageBus.Connect())
          {
            MessageBox.Show("Could not establish a connection to the message bus");
            return;
          }

          txtMessage.Enabled = txtMessageHistory.Enabled = btnSendMessage.Enabled = 
            cmbActiveTopic.Enabled = chkSubscribeTopicA.Enabled = chkSubscribeTopicB.Enabled = true;

          _TestTopicA = new Topic(TopicNameA, string.Empty);
          _TestTopicB = new Topic(TopicNameB, string.Empty);

          if (!_MessageBus.Publish(_TestTopicA) || !_MessageBus.Publish(_TestTopicB))
          {
            MessageBox.Show("Failed to publish one or more topics");
            return;
          }

          cmbActiveTopic.Items.Clear();
          cmbActiveTopic.Items.Add(_TestTopicA);
          cmbActiveTopic.Items.Add(_TestTopicB);
          cmbActiveTopic.SelectedIndex = 0;

          btnConnect.Text = "Disconnect";
        }
        else
        {
          if (_MessageBus != null)
          {
            _MessageBus.TopicUpdatedEvent -= MessageBus_TopicUpdatedEvent;
            if (!_MessageBus.UnPublish(_TestTopicA) || !_MessageBus.UnPublish(_TestTopicB))
            {
              MessageBox.Show("Failed to unpublish one or more topics");
            }
            // Unsubscribe topics
            if (chkSubscribeTopicA.Checked)
            {
              chkSubscribeTopicA.Checked = false;
            }
            if (chkSubscribeTopicB.Checked)
            {
              chkSubscribeTopicB.Checked = false;
            }

            if (!_MessageBus.Disconnect())
            {
              MessageBox.Show("Could not disconnect from the message bus");
            }
            _MessageBus.Dispose();
            _MessageBus = null;
            _TestTopicA = null;
            _TestTopicB = null;
          }

          txtMessage.Enabled = txtMessageHistory.Enabled = btnSendMessage.Enabled = cmbActiveTopic.Enabled =
            chkSubscribeTopicA.Enabled = chkSubscribeTopicB.Enabled = false;
          btnConnect.Text = "Connect";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void chkSubscribeTopicA_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (chkSubscribeTopicA.Checked)
        {
          if (!_MessageBus.Subscribe(_TestTopicA, false))
          {
            MessageBox.Show("Subscribe " + TopicNameA + " failed");
          }
          else
            chkSubscribeTopicA.Text = "Unsubscribe " + TopicNameA;
        }
        else
        {
          if (!_MessageBus.UnSubscribe(_TestTopicA))
          {
            MessageBox.Show("UnSubscribe " + TopicNameA + " failed");
          }
          else
            chkSubscribeTopicA.Text = "Subscribe " + TopicNameA;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void chkSubscribeTopicB_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (chkSubscribeTopicB.Checked)
        {
          if (!_MessageBus.Subscribe(_TestTopicB, false))
          {
            MessageBox.Show("Subscribe " + TopicNameB + " failed");
          }
          else
            chkSubscribeTopicB.Text = "Unsubscribe " + TopicNameB;
        }
        else
        {
          if (!_MessageBus.UnSubscribe(_TestTopicB))
          {
            MessageBox.Show("UnSubscribe " + TopicNameB + " failed");
          }
          else
            chkSubscribeTopicB.Text = "Subscribe " + TopicNameB;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void MessageBus_TopicUpdatedEvent(object sender, TopicUpdatedEventArg e)
    {
      _Builder.AppendLine(e.UpdatedTopic.Name + ": " + e.UpdatedTopic.Value);
      txtMessageHistory.Invoke((MethodInvoker)delegate
      {
        txtMessageHistory.Text = _Builder.ToString();
      });
    }

    private void MessageBus_ConnectionExceptionEvent(object sender, ConnectionExceptionEventArg e)
    {
      _Builder.AppendLine(">>> Connection Exception: " + e.ErrorMessage);
      txtMessageHistory.Invoke((MethodInvoker)delegate
      {
        txtMessageHistory.Text = _Builder.ToString();
      });
    }

    private void btnSendMessage_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtMessage.Text))
        return;

      try
      {
        Topic selectedTopic = (Topic)cmbActiveTopic.SelectedItem;
        selectedTopic.Value = txtMessage.Text;
        if (!_MessageBus.PublishUpdate(selectedTopic))
        {
          MessageBox.Show("Submit message failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
          txtMessage.Text = string.Empty;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        e.Handled = true;
        btnSendMessage.PerformClick();
      }
    }

    private void cmbActiveTopic_Format(object sender, ListControlConvertEventArgs e)
    {
      if (e.ListItem is Topic)
      {
        Topic topic = (Topic)e.ListItem;
        e.Value = topic.Name;
      }
    }

  }
}

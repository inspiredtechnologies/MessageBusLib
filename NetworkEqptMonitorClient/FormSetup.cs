using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemConfiguration;

namespace NetworkEqptMonitorClient
{
  public partial class FormSetup : Form
  {
    private const int TreeLevelEquipment = 1;

    private TreeNode _SelectedEquipmentNode = null;

    // ----------------------------------------------------

    #region Public Properties

    public Collection<NetworkEqpt> TempEqptCfg { get; set; }
    public bool EqptConfigUpdated { get; set; }

    #endregion

    // ----------------------------------------------------

    #region Public Methods

    public void SetCurrentConfiguration(Collection<NetworkEqpt> equipment)
    {
      try
      {
        if (TempEqptCfg != null)
        {
          TempEqptCfg.Clear();
          TempEqptCfg = null;
        }
        TempEqptCfg = new Collection<NetworkEqpt>();
        if (equipment != null)
        {
          foreach (NetworkEqpt eqpt in equipment)
          {
            TempEqptCfg.Add(eqpt.Clone());
          }
        }
        RefreshEqptTreeView();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    #endregion

    // ----------------------------------------------------

    public FormSetup()
    {
      InitializeComponent();
    }

    private void FormSetup_Load(object sender, EventArgs e)
    {

    }

    private void FormSetup_FormClosing(object sender, FormClosingEventArgs e)
    {
      tsbOk.Enabled = false;
      tvSetup.SelectedNode = null;
    }

    private void tsbOk_Click(object sender, EventArgs e)
    {
      this.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.Close();
    }

    private void tsbCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void RefreshEqptTreeView()
    {
      tvSetup.Nodes[0].Nodes.Clear();

      if (TempEqptCfg != null)
      {
        foreach (NetworkEqpt eqptRecord in TempEqptCfg)
        {
          TreeNode newEqptNode = tvSetup.Nodes[0].Nodes.Add(eqptRecord.Name);
          newEqptNode.Tag = eqptRecord;
        }
      }
      tvSetup.ExpandAll();
    }

    private void tvSetup_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Node.Level == TreeLevelEquipment && e.Node.Tag is NetworkEqpt)
      {
        propertyGridEquipment.SelectedObject = (NetworkEqpt)e.Node.Tag;
        _SelectedEquipmentNode = e.Node;
      }
      else
      {
        _SelectedEquipmentNode = null;
      }
    }

    private void tvSetup_MouseClick(object sender, MouseEventArgs e)
    {
      TreeNode selectedNode = tvSetup.GetNodeAt(e.X, e.Y);
      if (selectedNode == null)
        return;

      if (e.Button == System.Windows.Forms.MouseButtons.Right)
      {
        tvSetup.SelectedNode = selectedNode;

        if (selectedNode.Level == TreeLevelEquipment)
        {
          _SelectedEquipmentNode = selectedNode;
          deleteEquipmentToolStripMenuItem.Enabled = true;
        }
        else
        {
          _SelectedEquipmentNode = null;
          deleteEquipmentToolStripMenuItem.Enabled = false;
        }
        contextMenuStripEquipment.Show(System.Windows.Forms.Cursor.Position);
      }
    }

    private void tvSetup_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      if (e.Label != null)
      {
        if (e.Label.Trim().Length > 0)
        {
          if (e.Label.IndexOfAny(new char[] { '/', '\\' }) == -1)
          {
            // Accept changes.
            e.Node.EndEdit(false);

            if (e.Node.Level == TreeLevelEquipment)
            {
              NetworkEqpt editedEqpt = (NetworkEqpt)_SelectedEquipmentNode.Tag;
              if (editedEqpt != null && !e.Label.Equals(editedEqpt.Name, StringComparison.InvariantCulture))
              {
                AllowSaveConfig(true, true);
                editedEqpt.Name = e.Label;
                propertyGridEquipment.SelectedObject = editedEqpt;
              }
            }
          }
          else
          {
            /* Cancel the label edit action, inform the user, and 
               place the node in edit mode again. */
            e.CancelEdit = true;
            MessageBox.Show("Invalid Name." + Environment.NewLine + "The invalid characters are: '/','\\'",
               "Rename Equipment");
            e.Node.BeginEdit();
          }
        }
        else
        {
          /* Cancel the label edit action, inform the user, and 
             place the node in edit mode again. */
          e.CancelEdit = true;
          MessageBox.Show("Invalid Equipment name." + Environment.NewLine + "The name cannot be blank",
             "Rename Equipment");
          e.Node.BeginEdit();
        }
      }
    }

    private void tvSetup_ItemDrag(object sender, ItemDragEventArgs e)
    {
      if (e.Item is TreeNode)
      {
        TreeNode draggedNode = (TreeNode)e.Item;
        if (draggedNode.Level == TreeLevelEquipment)
        {
          DoDragDrop(e.Item, DragDropEffects.Move);
        }
      }
    }

    private void tvSetup_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Move;
    }

    private void tvSetup_DragOver(object sender, DragEventArgs e)
    {
      Point targetPoint = tvSetup.PointToClient(new Point(e.X, e.Y));
      TreeNode targetNode = tvSetup.GetNodeAt(targetPoint);
      TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

      if (targetNode == null || targetNode.Level != TreeLevelEquipment)
        e.Effect = DragDropEffects.None;
      else
        e.Effect = DragDropEffects.Move;
    }

    private void tvSetup_DragDrop(object sender, DragEventArgs e)
    {
      try
      {
        // Retrieve the client coordinates of the drop location.
        Point targetPoint = tvSetup.PointToClient(new Point(e.X, e.Y));
        // Retrieve the node at the drop location.
        TreeNode targetNode = tvSetup.GetNodeAt(targetPoint);
        // Retrieve the node that was dragged.
        TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

        // Confirm that the node at the drop location is not 
        // the dragged node and that target node isn't null
        // (for example if you drag outside the control)
        if (!draggedNode.Equals(targetNode) && targetNode != null)
        {
          // Remove the node from its current location and add it to the node at the drop location.
          draggedNode.Remove();

          if (targetNode.Tag is NetworkEqpt)
          {
            int indexToInsert = targetNode.Parent.Nodes.IndexOf(targetNode);
            targetNode.Parent.Nodes.Insert(indexToInsert, draggedNode);
          }

          TempEqptCfg.Clear();
          foreach (TreeNode eqptNode in tvSetup.Nodes[0].Nodes)
          {
            NetworkEqpt eqpt = (NetworkEqpt)eqptNode.Tag;
            TempEqptCfg.Add(eqpt);
          }
          AllowSaveConfig(true, true);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void AllowSaveConfig(bool allow, bool eqptConfigChanged)
    {
      tsbOk.Enabled = allow;

      if (eqptConfigChanged)
        EqptConfigUpdated = true;
    }

    private void addEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (TempEqptCfg == null)
        {
          TempEqptCfg = new Collection<NetworkEqpt>();
        }
        NetworkEqpt newEqpt = new NetworkEqpt();
        newEqpt.Name = "New Equipment";
        newEqpt.IpAddress = "127.0.0.1";
        newEqpt.Enabled = true;

        TreeNode newNode = tvSetup.Nodes[0].Nodes.Add(newEqpt.Name);
        newNode.Tag = newEqpt;
        TempEqptCfg.Add(newEqpt);
        tvSetup.SelectedNode = _SelectedEquipmentNode = newNode;
        AllowSaveConfig(true, true);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void deleteEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (_SelectedEquipmentNode != null)
        {
          if (MessageBox.Show("Are you sure you want to delete the equipment, " + Environment.NewLine +
            "'" + _SelectedEquipmentNode.Text + "' ?",
            "Delete Equipment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
          {
            NetworkEqpt eqptToDelete = (NetworkEqpt)_SelectedEquipmentNode.Tag;
            TempEqptCfg.Remove(eqptToDelete);

            tvSetup.Nodes[0].Nodes.Remove(_SelectedEquipmentNode);
            AllowSaveConfig(true, true);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void renameEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (_SelectedEquipmentNode != null)
        {
          tvSetup.SelectedNode = _SelectedEquipmentNode;
          tvSetup.LabelEdit = true;
          if (!_SelectedEquipmentNode.IsEditing)
          {
            _SelectedEquipmentNode.BeginEdit();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void propertyGridEquipment_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      tsbOk.Enabled = true;
    }

  }
}

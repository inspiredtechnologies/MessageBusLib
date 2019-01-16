namespace NetworkEqptMonitorClient
{
  partial class FormSetup
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Equipment");
      this.toolStripSetup = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.tsbOk = new System.Windows.Forms.ToolStripButton();
      this.tsbCancel = new System.Windows.Forms.ToolStripButton();
      this.splitContainerSetup = new System.Windows.Forms.SplitContainer();
      this.tvSetup = new System.Windows.Forms.TreeView();
      this.contextMenuStripEquipment = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addEquipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteEquipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripProperties = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.propertyGridEquipment = new System.Windows.Forms.PropertyGrid();
      this.renameEquipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSetup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerSetup)).BeginInit();
      this.splitContainerSetup.Panel1.SuspendLayout();
      this.splitContainerSetup.Panel2.SuspendLayout();
      this.splitContainerSetup.SuspendLayout();
      this.contextMenuStripEquipment.SuspendLayout();
      this.toolStripProperties.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripSetup
      // 
      this.toolStripSetup.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripSetup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsbCancel,
            this.tsbOk});
      this.toolStripSetup.Location = new System.Drawing.Point(0, 0);
      this.toolStripSetup.Name = "toolStripSetup";
      this.toolStripSetup.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStripSetup.Size = new System.Drawing.Size(790, 25);
      this.toolStripSetup.TabIndex = 0;
      this.toolStripSetup.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(165, 22);
      this.toolStripLabel1.Text = "Equipment Configuration";
      // 
      // tsbOk
      // 
      this.tsbOk.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.tsbOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.tsbOk.Enabled = false;
      this.tsbOk.Image = ((System.Drawing.Image)(resources.GetObject("tsbOk.Image")));
      this.tsbOk.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbOk.Name = "tsbOk";
      this.tsbOk.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.tsbOk.Size = new System.Drawing.Size(37, 22);
      this.tsbOk.Text = "OK";
      this.tsbOk.Click += new System.EventHandler(this.tsbOk_Click);
      // 
      // tsbCancel
      // 
      this.tsbCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
      this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbCancel.Name = "tsbCancel";
      this.tsbCancel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
      this.tsbCancel.Size = new System.Drawing.Size(57, 22);
      this.tsbCancel.Text = "Cancel";
      this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
      // 
      // splitContainerSetup
      // 
      this.splitContainerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerSetup.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainerSetup.Location = new System.Drawing.Point(0, 25);
      this.splitContainerSetup.Name = "splitContainerSetup";
      // 
      // splitContainerSetup.Panel1
      // 
      this.splitContainerSetup.Panel1.Controls.Add(this.tvSetup);
      // 
      // splitContainerSetup.Panel2
      // 
      this.splitContainerSetup.Panel2.Controls.Add(this.propertyGridEquipment);
      this.splitContainerSetup.Panel2.Controls.Add(this.toolStripProperties);
      this.splitContainerSetup.Size = new System.Drawing.Size(790, 386);
      this.splitContainerSetup.SplitterDistance = 233;
      this.splitContainerSetup.TabIndex = 1;
      // 
      // tvSetup
      // 
      this.tvSetup.AllowDrop = true;
      this.tvSetup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvSetup.LabelEdit = true;
      this.tvSetup.Location = new System.Drawing.Point(0, 0);
      this.tvSetup.Name = "tvSetup";
      treeNode2.Name = "Node0";
      treeNode2.Text = "Equipment";
      this.tvSetup.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
      this.tvSetup.Size = new System.Drawing.Size(233, 386);
      this.tvSetup.TabIndex = 0;
      this.tvSetup.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvSetup_AfterLabelEdit);
      this.tvSetup.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvSetup_ItemDrag);
      this.tvSetup.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSetup_NodeMouseClick);
      this.tvSetup.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvSetup_DragDrop);
      this.tvSetup.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvSetup_DragEnter);
      this.tvSetup.DragOver += new System.Windows.Forms.DragEventHandler(this.tvSetup_DragOver);
      this.tvSetup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvSetup_MouseClick);
      // 
      // contextMenuStripEquipment
      // 
      this.contextMenuStripEquipment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEquipmentToolStripMenuItem,
            this.deleteEquipmentToolStripMenuItem,
            this.renameEquipmentToolStripMenuItem});
      this.contextMenuStripEquipment.Name = "contextMenuStripEnclosure";
      this.contextMenuStripEquipment.Size = new System.Drawing.Size(179, 70);
      // 
      // addEquipmentToolStripMenuItem
      // 
      this.addEquipmentToolStripMenuItem.Name = "addEquipmentToolStripMenuItem";
      this.addEquipmentToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.addEquipmentToolStripMenuItem.Text = "Add Equipment";
      this.addEquipmentToolStripMenuItem.ToolTipText = "Create a new equipment record";
      this.addEquipmentToolStripMenuItem.Click += new System.EventHandler(this.addEquipmentToolStripMenuItem_Click);
      // 
      // deleteEquipmentToolStripMenuItem
      // 
      this.deleteEquipmentToolStripMenuItem.Name = "deleteEquipmentToolStripMenuItem";
      this.deleteEquipmentToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.deleteEquipmentToolStripMenuItem.Text = "Delete Equipment...";
      this.deleteEquipmentToolStripMenuItem.ToolTipText = "Delete equipment record";
      this.deleteEquipmentToolStripMenuItem.Click += new System.EventHandler(this.deleteEquipmentToolStripMenuItem_Click);
      // 
      // toolStripProperties
      // 
      this.toolStripProperties.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
      this.toolStripProperties.Location = new System.Drawing.Point(0, 0);
      this.toolStripProperties.Name = "toolStripProperties";
      this.toolStripProperties.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStripProperties.Size = new System.Drawing.Size(553, 25);
      this.toolStripProperties.TabIndex = 1;
      this.toolStripProperties.Text = "toolStrip1";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(89, 22);
      this.toolStripLabel2.Text = "Properties        ";
      // 
      // propertyGridEquipment
      // 
      this.propertyGridEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGridEquipment.HelpVisible = false;
      this.propertyGridEquipment.Location = new System.Drawing.Point(0, 25);
      this.propertyGridEquipment.Name = "propertyGridEquipment";
      this.propertyGridEquipment.Size = new System.Drawing.Size(553, 361);
      this.propertyGridEquipment.TabIndex = 2;
      this.propertyGridEquipment.ToolbarVisible = false;
      this.propertyGridEquipment.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridEquipment_PropertyValueChanged);
      // 
      // renameEquipmentToolStripMenuItem
      // 
      this.renameEquipmentToolStripMenuItem.Name = "renameEquipmentToolStripMenuItem";
      this.renameEquipmentToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
      this.renameEquipmentToolStripMenuItem.Text = "Rename Equipment";
      this.renameEquipmentToolStripMenuItem.Click += new System.EventHandler(this.renameEquipmentToolStripMenuItem_Click);
      // 
      // FormSetup
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(790, 411);
      this.Controls.Add(this.splitContainerSetup);
      this.Controls.Add(this.toolStripSetup);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "FormSetup";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Setup";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetup_FormClosing);
      this.Load += new System.EventHandler(this.FormSetup_Load);
      this.toolStripSetup.ResumeLayout(false);
      this.toolStripSetup.PerformLayout();
      this.splitContainerSetup.Panel1.ResumeLayout(false);
      this.splitContainerSetup.Panel2.ResumeLayout(false);
      this.splitContainerSetup.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerSetup)).EndInit();
      this.splitContainerSetup.ResumeLayout(false);
      this.contextMenuStripEquipment.ResumeLayout(false);
      this.toolStripProperties.ResumeLayout(false);
      this.toolStripProperties.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStripSetup;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripButton tsbCancel;
    private System.Windows.Forms.ToolStripButton tsbOk;
    private System.Windows.Forms.SplitContainer splitContainerSetup;
    private System.Windows.Forms.TreeView tvSetup;
    private System.Windows.Forms.PropertyGrid propertyGridEquipment;
    private System.Windows.Forms.ToolStrip toolStripProperties;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ContextMenuStrip contextMenuStripEquipment;
    private System.Windows.Forms.ToolStripMenuItem addEquipmentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteEquipmentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem renameEquipmentToolStripMenuItem;
  }
}
namespace NetworkEqptMonitorClient
{
  partial class FormClient
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
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Equipment");
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClient));
      this.menuStripMain = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStripMain = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.tsslServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.splitContainerMain = new System.Windows.Forms.SplitContainer();
      this.tvEqpt = new System.Windows.Forms.TreeView();
      this.imageListEqpt = new System.Windows.Forms.ImageList(this.components);
      this.toolStripEquipment = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.splitContainerEquipment = new System.Windows.Forms.SplitContainer();
      this.lvLiveEvents = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.toolStripEvents = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.lvAllEqpt = new System.Windows.Forms.ListView();
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.menuStripMain.SuspendLayout();
      this.statusStripMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
      this.splitContainerMain.Panel1.SuspendLayout();
      this.splitContainerMain.Panel2.SuspendLayout();
      this.splitContainerMain.SuspendLayout();
      this.toolStripEquipment.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerEquipment)).BeginInit();
      this.splitContainerEquipment.Panel1.SuspendLayout();
      this.splitContainerEquipment.Panel2.SuspendLayout();
      this.splitContainerEquipment.SuspendLayout();
      this.toolStripEvents.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStripMain
      // 
      this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStripMain.Location = new System.Drawing.Point(0, 0);
      this.menuStripMain.Name = "menuStripMain";
      this.menuStripMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
      this.menuStripMain.Size = new System.Drawing.Size(946, 24);
      this.menuStripMain.TabIndex = 0;
      this.menuStripMain.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // setupToolStripMenuItem
      // 
      this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
      this.setupToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
      this.setupToolStripMenuItem.Text = "Setup...";
      this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.aboutToolStripMenuItem.Text = "About...";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // statusStripMain
      // 
      this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.tsslServerStatus});
      this.statusStripMain.Location = new System.Drawing.Point(0, 451);
      this.statusStripMain.Name = "statusStripMain";
      this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
      this.statusStripMain.Size = new System.Drawing.Size(946, 22);
      this.statusStripMain.TabIndex = 1;
      this.statusStripMain.Text = "statusStrip1";
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(794, 17);
      this.toolStripStatusLabel2.Spring = true;
      this.toolStripStatusLabel2.Text = "        ";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 17);
      this.toolStripStatusLabel1.Text = "Server Status:";
      // 
      // tsslServerStatus
      // 
      this.tsslServerStatus.Name = "tsslServerStatus";
      this.tsslServerStatus.Size = new System.Drawing.Size(58, 17);
      this.tsslServerStatus.Text = "Unknown";
      // 
      // splitContainerMain
      // 
      this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
      this.splitContainerMain.Name = "splitContainerMain";
      // 
      // splitContainerMain.Panel1
      // 
      this.splitContainerMain.Panel1.Controls.Add(this.tvEqpt);
      this.splitContainerMain.Panel1.Controls.Add(this.toolStripEquipment);
      // 
      // splitContainerMain.Panel2
      // 
      this.splitContainerMain.Panel2.Controls.Add(this.splitContainerEquipment);
      this.splitContainerMain.Size = new System.Drawing.Size(946, 427);
      this.splitContainerMain.SplitterDistance = 228;
      this.splitContainerMain.TabIndex = 2;
      // 
      // tvEqpt
      // 
      this.tvEqpt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvEqpt.ImageIndex = 0;
      this.tvEqpt.ImageList = this.imageListEqpt;
      this.tvEqpt.Location = new System.Drawing.Point(0, 25);
      this.tvEqpt.Name = "tvEqpt";
      treeNode2.Name = "Node0";
      treeNode2.Text = "Equipment";
      this.tvEqpt.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
      this.tvEqpt.SelectedImageIndex = 0;
      this.tvEqpt.Size = new System.Drawing.Size(228, 402);
      this.tvEqpt.TabIndex = 1;
      this.tvEqpt.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvEqpt_NodeMouseClick);
      // 
      // imageListEqpt
      // 
      this.imageListEqpt.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListEqpt.ImageStream")));
      this.imageListEqpt.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListEqpt.Images.SetKeyName(0, "Eqpt16.png");
      this.imageListEqpt.Images.SetKeyName(1, "Eqpt.png");
      this.imageListEqpt.Images.SetKeyName(2, "Eqpt_Green.png");
      this.imageListEqpt.Images.SetKeyName(3, "Eqpt_Amber.png");
      this.imageListEqpt.Images.SetKeyName(4, "Eqpt_Red.png");
      // 
      // toolStripEquipment
      // 
      this.toolStripEquipment.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripEquipment.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripEquipment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
      this.toolStripEquipment.Location = new System.Drawing.Point(0, 0);
      this.toolStripEquipment.Name = "toolStripEquipment";
      this.toolStripEquipment.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStripEquipment.Size = new System.Drawing.Size(228, 25);
      this.toolStripEquipment.TabIndex = 0;
      this.toolStripEquipment.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(138, 22);
      this.toolStripLabel1.Text = "Equipment Inventory";
      // 
      // splitContainerEquipment
      // 
      this.splitContainerEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainerEquipment.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainerEquipment.Location = new System.Drawing.Point(0, 0);
      this.splitContainerEquipment.Name = "splitContainerEquipment";
      this.splitContainerEquipment.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainerEquipment.Panel1
      // 
      this.splitContainerEquipment.Panel1.Controls.Add(this.lvAllEqpt);
      // 
      // splitContainerEquipment.Panel2
      // 
      this.splitContainerEquipment.Panel2.Controls.Add(this.lvLiveEvents);
      this.splitContainerEquipment.Panel2.Controls.Add(this.toolStripEvents);
      this.splitContainerEquipment.Size = new System.Drawing.Size(714, 427);
      this.splitContainerEquipment.SplitterDistance = 203;
      this.splitContainerEquipment.TabIndex = 0;
      // 
      // lvLiveEvents
      // 
      this.lvLiveEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader17,
            this.columnHeader4});
      this.lvLiveEvents.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvLiveEvents.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lvLiveEvents.FullRowSelect = true;
      this.lvLiveEvents.Location = new System.Drawing.Point(0, 25);
      this.lvLiveEvents.Name = "lvLiveEvents";
      this.lvLiveEvents.Size = new System.Drawing.Size(714, 195);
      this.lvLiveEvents.TabIndex = 2;
      this.lvLiveEvents.UseCompatibleStateImageBehavior = false;
      this.lvLiveEvents.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Time Stamp";
      this.columnHeader1.Width = 148;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Source";
      this.columnHeader2.Width = 147;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Equipment";
      this.columnHeader3.Width = 130;
      // 
      // columnHeader17
      // 
      this.columnHeader17.Text = "Severity";
      this.columnHeader17.Width = 90;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Status";
      this.columnHeader4.Width = 243;
      // 
      // toolStripEvents
      // 
      this.toolStripEvents.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripEvents.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
      this.toolStripEvents.Location = new System.Drawing.Point(0, 0);
      this.toolStripEvents.Name = "toolStripEvents";
      this.toolStripEvents.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStripEvents.Size = new System.Drawing.Size(714, 25);
      this.toolStripEvents.TabIndex = 1;
      this.toolStripEvents.Text = "toolStrip1";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(77, 22);
      this.toolStripLabel2.Text = "Live Events";
      // 
      // lvAllEqpt
      // 
      this.lvAllEqpt.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
      this.lvAllEqpt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvAllEqpt.FullRowSelect = true;
      this.lvAllEqpt.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvAllEqpt.Location = new System.Drawing.Point(0, 0);
      this.lvAllEqpt.MultiSelect = false;
      this.lvAllEqpt.Name = "lvAllEqpt";
      this.lvAllEqpt.Size = new System.Drawing.Size(714, 203);
      this.lvAllEqpt.TabIndex = 0;
      this.lvAllEqpt.UseCompatibleStateImageBehavior = false;
      this.lvAllEqpt.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Name";
      this.columnHeader5.Width = 120;
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "IP Address";
      this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader6.Width = 100;
      // 
      // columnHeader7
      // 
      this.columnHeader7.Text = "Health";
      this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader7.Width = 80;
      // 
      // FormClient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(946, 473);
      this.Controls.Add(this.splitContainerMain);
      this.Controls.Add(this.statusStripMain);
      this.Controls.Add(this.menuStripMain);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MainMenuStrip = this.menuStripMain;
      this.Name = "FormClient";
      this.Text = "Network Equipment Monitor Client";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClient_FormClosing);
      this.Load += new System.EventHandler(this.FormClient_Load);
      this.menuStripMain.ResumeLayout(false);
      this.menuStripMain.PerformLayout();
      this.statusStripMain.ResumeLayout(false);
      this.statusStripMain.PerformLayout();
      this.splitContainerMain.Panel1.ResumeLayout(false);
      this.splitContainerMain.Panel1.PerformLayout();
      this.splitContainerMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
      this.splitContainerMain.ResumeLayout(false);
      this.toolStripEquipment.ResumeLayout(false);
      this.toolStripEquipment.PerformLayout();
      this.splitContainerEquipment.Panel1.ResumeLayout(false);
      this.splitContainerEquipment.Panel2.ResumeLayout(false);
      this.splitContainerEquipment.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainerEquipment)).EndInit();
      this.splitContainerEquipment.ResumeLayout(false);
      this.toolStripEvents.ResumeLayout(false);
      this.toolStripEvents.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStripMain;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusStripMain;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.ToolStripStatusLabel tsslServerStatus;
    private System.Windows.Forms.SplitContainer splitContainerMain;
    private System.Windows.Forms.TreeView tvEqpt;
    private System.Windows.Forms.ToolStrip toolStripEquipment;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.SplitContainer splitContainerEquipment;
    private System.Windows.Forms.ListView lvLiveEvents;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader17;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ToolStrip toolStripEvents;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ImageList imageListEqpt;
    private System.Windows.Forms.ListView lvAllEqpt;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ColumnHeader columnHeader7;
  }
}


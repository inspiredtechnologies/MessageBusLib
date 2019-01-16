namespace MessageBusLibDemo
{
  partial class FormTest
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtServerIp = new System.Windows.Forms.TextBox();
      this.btnConnect = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtMessageHistory = new System.Windows.Forms.TextBox();
      this.btnSendMessage = new System.Windows.Forms.Button();
      this.txtMessage = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.cmbActiveTopic = new System.Windows.Forms.ComboBox();
      this.chkSubscribeTopicA = new System.Windows.Forms.CheckBox();
      this.chkSubscribeTopicB = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Server IP:";
      // 
      // txtServerIp
      // 
      this.txtServerIp.Location = new System.Drawing.Point(72, 13);
      this.txtServerIp.Name = "txtServerIp";
      this.txtServerIp.Size = new System.Drawing.Size(100, 20);
      this.txtServerIp.TabIndex = 1;
      this.txtServerIp.Text = "127.0.0.1";
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(178, 11);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(75, 23);
      this.btnConnect.TabIndex = 2;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkSubscribeTopicB);
      this.groupBox1.Controls.Add(this.chkSubscribeTopicA);
      this.groupBox1.Location = new System.Drawing.Point(12, 44);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(333, 82);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Topics";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtMessageHistory);
      this.groupBox2.Location = new System.Drawing.Point(9, 229);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(337, 154);
      this.groupBox2.TabIndex = 14;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Received Messages";
      // 
      // txtMessageHistory
      // 
      this.txtMessageHistory.Enabled = false;
      this.txtMessageHistory.Location = new System.Drawing.Point(15, 19);
      this.txtMessageHistory.Multiline = true;
      this.txtMessageHistory.Name = "txtMessageHistory";
      this.txtMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtMessageHistory.Size = new System.Drawing.Size(311, 121);
      this.txtMessageHistory.TabIndex = 5;
      // 
      // btnSendMessage
      // 
      this.btnSendMessage.Enabled = false;
      this.btnSendMessage.Location = new System.Drawing.Point(262, 150);
      this.btnSendMessage.Name = "btnSendMessage";
      this.btnSendMessage.Size = new System.Drawing.Size(84, 62);
      this.btnSendMessage.TabIndex = 13;
      this.btnSendMessage.Text = "Send";
      this.btnSendMessage.UseVisualStyleBackColor = true;
      this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
      // 
      // txtMessage
      // 
      this.txtMessage.Enabled = false;
      this.txtMessage.Location = new System.Drawing.Point(65, 180);
      this.txtMessage.Multiline = true;
      this.txtMessage.Name = "txtMessage";
      this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtMessage.Size = new System.Drawing.Size(188, 32);
      this.txtMessage.TabIndex = 12;
      this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(9, 183);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 13);
      this.label4.TabIndex = 11;
      this.label4.Text = "Message:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(25, 153);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(70, 13);
      this.label5.TabIndex = 15;
      this.label5.Text = "Select Topic:";
      // 
      // cmbActiveTopic
      // 
      this.cmbActiveTopic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbActiveTopic.Enabled = false;
      this.cmbActiveTopic.FormattingEnabled = true;
      this.cmbActiveTopic.Items.AddRange(new object[] {
            "Topic A",
            "Topic B"});
      this.cmbActiveTopic.Location = new System.Drawing.Point(101, 150);
      this.cmbActiveTopic.Name = "cmbActiveTopic";
      this.cmbActiveTopic.Size = new System.Drawing.Size(152, 21);
      this.cmbActiveTopic.TabIndex = 16;
      this.cmbActiveTopic.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cmbActiveTopic_Format);
      // 
      // chkSubscribeTopicA
      // 
      this.chkSubscribeTopicA.Appearance = System.Windows.Forms.Appearance.Button;
      this.chkSubscribeTopicA.Enabled = false;
      this.chkSubscribeTopicA.Location = new System.Drawing.Point(60, 16);
      this.chkSubscribeTopicA.Name = "chkSubscribeTopicA";
      this.chkSubscribeTopicA.Size = new System.Drawing.Size(181, 24);
      this.chkSubscribeTopicA.TabIndex = 4;
      this.chkSubscribeTopicA.Text = "Subscribe Topic A";
      this.chkSubscribeTopicA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.chkSubscribeTopicA.UseVisualStyleBackColor = true;
      this.chkSubscribeTopicA.CheckedChanged += new System.EventHandler(this.chkSubscribeTopicA_CheckedChanged);
      // 
      // chkSubscribeTopicB
      // 
      this.chkSubscribeTopicB.Appearance = System.Windows.Forms.Appearance.Button;
      this.chkSubscribeTopicB.Enabled = false;
      this.chkSubscribeTopicB.Location = new System.Drawing.Point(60, 46);
      this.chkSubscribeTopicB.Name = "chkSubscribeTopicB";
      this.chkSubscribeTopicB.Size = new System.Drawing.Size(181, 24);
      this.chkSubscribeTopicB.TabIndex = 5;
      this.chkSubscribeTopicB.Text = "Subscribe Topic B";
      this.chkSubscribeTopicB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.chkSubscribeTopicB.UseVisualStyleBackColor = true;
      this.chkSubscribeTopicB.CheckedChanged += new System.EventHandler(this.chkSubscribeTopicB_CheckedChanged);
      // 
      // FormTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(357, 398);
      this.Controls.Add(this.cmbActiveTopic);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.btnSendMessage);
      this.Controls.Add(this.txtMessage);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnConnect);
      this.Controls.Add(this.txtServerIp);
      this.Controls.Add(this.label1);
      this.MaximizeBox = false;
      this.Name = "FormTest";
      this.Text = "MessageBusLib Demo";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTest_FormClosing);
      this.Load += new System.EventHandler(this.FormTest_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtServerIp;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtMessageHistory;
    private System.Windows.Forms.Button btnSendMessage;
    private System.Windows.Forms.TextBox txtMessage;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbActiveTopic;
    private System.Windows.Forms.CheckBox chkSubscribeTopicB;
    private System.Windows.Forms.CheckBox chkSubscribeTopicA;
  }
}


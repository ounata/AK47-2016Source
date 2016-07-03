namespace MCS.Services.Messages
{
	partial class SendMailControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxLogOnName = new System.Windows.Forms.TextBox();
            this.labelLogonName = new System.Windows.Forms.Label();
            this.labelAuthType = new System.Windows.Forms.Label();
            this.comboBoxAuthenticateType = new System.Windows.Forms.ComboBox();
            this.textBoxDest = new System.Windows.Forms.TextBox();
            this.labelDest = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelCaption = new System.Windows.Forms.Label();
            this.textBoxSignInAddress = new System.Windows.Forms.TextBox();
            this.labelSignInName = new System.Windows.Forms.Label();
            this.buttonResetFromConfig = new System.Windows.Forms.Button();
            this.buttonSendOneCandidate = new System.Windows.Forms.Button();
            this.checkBoxEnableSSL = new System.Windows.Forms.CheckBox();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.textBoxPassword);
            this.groupBoxAuth.Controls.Add(this.labelPassword);
            this.groupBoxAuth.Controls.Add(this.textBoxLogOnName);
            this.groupBoxAuth.Controls.Add(this.labelLogonName);
            this.groupBoxAuth.Location = new System.Drawing.Point(132, 153);
            this.groupBoxAuth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxAuth.Size = new System.Drawing.Size(448, 102);
            this.groupBoxAuth.TabIndex = 47;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "认证";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(107, 62);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(319, 25);
            this.textBoxPassword.TabIndex = 21;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPassword.Location = new System.Drawing.Point(43, 65);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(48, 15);
            this.labelPassword.TabIndex = 20;
            this.labelPassword.Text = "口令:";
            // 
            // textBoxLogOnName
            // 
            this.textBoxLogOnName.Location = new System.Drawing.Point(107, 25);
            this.textBoxLogOnName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxLogOnName.Name = "textBoxLogOnName";
            this.textBoxLogOnName.Size = new System.Drawing.Size(319, 25);
            this.textBoxLogOnName.TabIndex = 19;
            // 
            // labelLogonName
            // 
            this.labelLogonName.AutoSize = true;
            this.labelLogonName.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLogonName.Location = new System.Drawing.Point(21, 28);
            this.labelLogonName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLogonName.Name = "labelLogonName";
            this.labelLogonName.Size = new System.Drawing.Size(64, 15);
            this.labelLogonName.TabIndex = 18;
            this.labelLogonName.Text = "登录名:";
            // 
            // labelAuthType
            // 
            this.labelAuthType.AutoSize = true;
            this.labelAuthType.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAuthType.Location = new System.Drawing.Point(36, 117);
            this.labelAuthType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAuthType.Name = "labelAuthType";
            this.labelAuthType.Size = new System.Drawing.Size(80, 15);
            this.labelAuthType.TabIndex = 45;
            this.labelAuthType.Text = "认证方式:";
            // 
            // comboBoxAuthenticateType
            // 
            this.comboBoxAuthenticateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAuthenticateType.Location = new System.Drawing.Point(132, 114);
            this.comboBoxAuthenticateType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxAuthenticateType.Name = "comboBoxAuthenticateType";
            this.comboBoxAuthenticateType.Size = new System.Drawing.Size(233, 23);
            this.comboBoxAuthenticateType.TabIndex = 46;
            this.comboBoxAuthenticateType.SelectionChangeCommitted += new System.EventHandler(this.comboBoxAuthenticateType_SelectionChangeCommitted);
            // 
            // textBoxDest
            // 
            this.textBoxDest.Location = new System.Drawing.Point(132, 345);
            this.textBoxDest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxDest.Name = "textBoxDest";
            this.textBoxDest.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDest.Size = new System.Drawing.Size(319, 25);
            this.textBoxDest.TabIndex = 44;
            this.textBoxDest.Text = "zhaodan@msoatest.com";
            // 
            // labelDest
            // 
            this.labelDest.AutoSize = true;
            this.labelDest.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDest.Location = new System.Drawing.Point(57, 347);
            this.labelDest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDest.Name = "labelDest";
            this.labelDest.Size = new System.Drawing.Size(64, 15);
            this.labelDest.TabIndex = 42;
            this.labelDest.Text = "收件人:";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(132, 308);
            this.textBoxMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(319, 25);
            this.textBoxMessage.TabIndex = 41;
            this.textBoxMessage.Text = "Hello world";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.Location = new System.Drawing.Point(36, 310);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(80, 15);
            this.labelMessage.TabIndex = 40;
            this.labelMessage.Text = "邮件标题:";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(464, 344);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(100, 27);
            this.buttonSend.TabIndex = 43;
            this.buttonSend.Text = "发送(&S)";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPort.Location = new System.Drawing.Point(388, 80);
            this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(48, 15);
            this.labelPort.TabIndex = 37;
            this.labelPort.Text = "端口:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(448, 77);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(110, 25);
            this.textBoxPort.TabIndex = 38;
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(132, 77);
            this.textBoxServer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(233, 25);
            this.textBoxServer.TabIndex = 34;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelServer.Location = new System.Drawing.Point(57, 80);
            this.labelServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(64, 15);
            this.labelServer.TabIndex = 33;
            this.labelServer.Text = "服务器:";
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCaption.Location = new System.Drawing.Point(15, 33);
            this.labelCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(177, 20);
            this.labelCaption.TabIndex = 36;
            this.labelCaption.Text = "测试发送邮件通知";
            // 
            // textBoxSignInAddress
            // 
            this.textBoxSignInAddress.Location = new System.Drawing.Point(132, 271);
            this.textBoxSignInAddress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxSignInAddress.Name = "textBoxSignInAddress";
            this.textBoxSignInAddress.Size = new System.Drawing.Size(319, 25);
            this.textBoxSignInAddress.TabIndex = 39;
            // 
            // labelSignInName
            // 
            this.labelSignInName.AutoSize = true;
            this.labelSignInName.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSignInName.Location = new System.Drawing.Point(25, 273);
            this.labelSignInName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSignInName.Name = "labelSignInName";
            this.labelSignInName.Size = new System.Drawing.Size(96, 15);
            this.labelSignInName.TabIndex = 35;
            this.labelSignInName.Text = "发送人地址:";
            // 
            // buttonResetFromConfig
            // 
            this.buttonResetFromConfig.Location = new System.Drawing.Point(391, 113);
            this.buttonResetFromConfig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonResetFromConfig.Name = "buttonResetFromConfig";
            this.buttonResetFromConfig.Size = new System.Drawing.Size(168, 27);
            this.buttonResetFromConfig.TabIndex = 48;
            this.buttonResetFromConfig.Text = "从配置文件重置(&R)";
            this.buttonResetFromConfig.Click += new System.EventHandler(this.buttonResetFromConfig_Click);
            // 
            // buttonSendOneCandidate
            // 
            this.buttonSendOneCandidate.Location = new System.Drawing.Point(572, 344);
            this.buttonSendOneCandidate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSendOneCandidate.Name = "buttonSendOneCandidate";
            this.buttonSendOneCandidate.Size = new System.Drawing.Size(135, 27);
            this.buttonSendOneCandidate.TabIndex = 49;
            this.buttonSendOneCandidate.Text = "发送一条候选";
            this.buttonSendOneCandidate.Click += new System.EventHandler(this.buttonSendOneCandidate_Click);
            // 
            // checkBoxEnableSSL
            // 
            this.checkBoxEnableSSL.AutoSize = true;
            this.checkBoxEnableSSL.Location = new System.Drawing.Point(572, 80);
            this.checkBoxEnableSSL.Name = "checkBoxEnableSSL";
            this.checkBoxEnableSSL.Size = new System.Drawing.Size(83, 19);
            this.checkBoxEnableSSL.TabIndex = 50;
            this.checkBoxEnableSSL.Text = "启用SSL";
            this.checkBoxEnableSSL.UseVisualStyleBackColor = true;
            // 
            // SendMailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxEnableSSL);
            this.Controls.Add(this.buttonSendOneCandidate);
            this.Controls.Add(this.buttonResetFromConfig);
            this.Controls.Add(this.groupBoxAuth);
            this.Controls.Add(this.labelAuthType);
            this.Controls.Add(this.comboBoxAuthenticateType);
            this.Controls.Add(this.textBoxDest);
            this.Controls.Add(this.labelDest);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.textBoxSignInAddress);
            this.Controls.Add(this.labelSignInName);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SendMailControl";
            this.Size = new System.Drawing.Size(711, 404);
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxAuth;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.TextBox textBoxLogOnName;
		private System.Windows.Forms.Label labelLogonName;
		private System.Windows.Forms.Label labelAuthType;
		private System.Windows.Forms.ComboBox comboBoxAuthenticateType;
		private System.Windows.Forms.TextBox textBoxDest;
		private System.Windows.Forms.Label labelDest;
		private System.Windows.Forms.TextBox textBoxMessage;
		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.Label labelPort;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.TextBox textBoxServer;
		private System.Windows.Forms.Label labelServer;
		private System.Windows.Forms.Label labelCaption;
		private System.Windows.Forms.TextBox textBoxSignInAddress;
		private System.Windows.Forms.Label labelSignInName;
		private System.Windows.Forms.Button buttonResetFromConfig;
		private System.Windows.Forms.Button buttonSendOneCandidate;
        private System.Windows.Forms.CheckBox checkBoxEnableSSL;
    }
}

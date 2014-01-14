namespace DatabaseComparer
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gBoxSource = new System.Windows.Forms.GroupBox();
            this.pSourceType = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSTip = new System.Windows.Forms.Label();
            this.cmboxSourceServer = new System.Windows.Forms.ComboBox();
            this.cmboxSDatabase = new System.Windows.Forms.ComboBox();
            this.txtSPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSPassword = new System.Windows.Forms.TextBox();
            this.txtSUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.gBoxDest = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pDestType = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.lblDTip = new System.Windows.Forms.Label();
            this.cmboxDestServer = new System.Windows.Forms.ComboBox();
            this.cmboxDDatabase = new System.Windows.Forms.ComboBox();
            this.txtDPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDPassword = new System.Windows.Forms.TextBox();
            this.txtDUserId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ckBoxIgnoreType = new System.Windows.Forms.CheckBox();
            this.ckBoxIgnoreLength = new System.Windows.Forms.CheckBox();
            this.gBoxSource.SuspendLayout();
            this.pSourceType.SuspendLayout();
            this.gBoxDest.SuspendLayout();
            this.pDestType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBoxSource
            // 
            this.gBoxSource.Controls.Add(this.pSourceType);
            this.gBoxSource.Controls.Add(this.label11);
            this.gBoxSource.Controls.Add(this.lblSTip);
            this.gBoxSource.Controls.Add(this.cmboxSourceServer);
            this.gBoxSource.Controls.Add(this.cmboxSDatabase);
            this.gBoxSource.Controls.Add(this.txtSPort);
            this.gBoxSource.Controls.Add(this.label10);
            this.gBoxSource.Controls.Add(this.label4);
            this.gBoxSource.Controls.Add(this.txtSPassword);
            this.gBoxSource.Controls.Add(this.txtSUserId);
            this.gBoxSource.Controls.Add(this.label3);
            this.gBoxSource.Controls.Add(this.label2);
            this.gBoxSource.Controls.Add(this.label1);
            this.gBoxSource.Location = new System.Drawing.Point(2, 2);
            this.gBoxSource.Name = "gBoxSource";
            this.gBoxSource.Size = new System.Drawing.Size(273, 197);
            this.gBoxSource.TabIndex = 0;
            this.gBoxSource.TabStop = false;
            this.gBoxSource.Text = "源头";
            // 
            // pSourceType
            // 
            this.pSourceType.Controls.Add(this.radioButton2);
            this.pSourceType.Controls.Add(this.radioButton1);
            this.pSourceType.Location = new System.Drawing.Point(94, 147);
            this.pSourceType.Name = "pSourceType";
            this.pSourceType.Size = new System.Drawing.Size(145, 26);
            this.pSourceType.TabIndex = 16;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(80, 6);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(53, 16);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Mysql";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(1, 6);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 16);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Sql Server";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "数据库类型：";
            // 
            // lblSTip
            // 
            this.lblSTip.AutoSize = true;
            this.lblSTip.Location = new System.Drawing.Point(92, 175);
            this.lblSTip.Name = "lblSTip";
            this.lblSTip.Size = new System.Drawing.Size(29, 12);
            this.lblSTip.TabIndex = 13;
            this.lblSTip.Text = "提示";
            // 
            // cmboxSourceServer
            // 
            this.cmboxSourceServer.FormattingEnabled = true;
            this.cmboxSourceServer.Location = new System.Drawing.Point(94, 23);
            this.cmboxSourceServer.Name = "cmboxSourceServer";
            this.cmboxSourceServer.Size = new System.Drawing.Size(173, 20);
            this.cmboxSourceServer.TabIndex = 12;
            this.cmboxSourceServer.SelectedIndexChanged += new System.EventHandler(this.cmboxSourceServer_SelectedIndexChanged);
            // 
            // cmboxSDatabase
            // 
            this.cmboxSDatabase.FormattingEnabled = true;
            this.cmboxSDatabase.Location = new System.Drawing.Point(94, 121);
            this.cmboxSDatabase.Name = "cmboxSDatabase";
            this.cmboxSDatabase.Size = new System.Drawing.Size(121, 20);
            this.cmboxSDatabase.TabIndex = 11;
            this.cmboxSDatabase.Click += new System.EventHandler(this.cmboxDatabase_Click);
            // 
            // txtSPort
            // 
            this.txtSPort.Location = new System.Drawing.Point(94, 97);
            this.txtSPort.Name = "txtSPort";
            this.txtSPort.Size = new System.Drawing.Size(100, 21);
            this.txtSPort.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "数据库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "端口：";
            // 
            // txtSPassword
            // 
            this.txtSPassword.Location = new System.Drawing.Point(94, 73);
            this.txtSPassword.Name = "txtSPassword";
            this.txtSPassword.Size = new System.Drawing.Size(125, 21);
            this.txtSPassword.TabIndex = 5;
            // 
            // txtSUserId
            // 
            this.txtSUserId.Location = new System.Drawing.Point(94, 48);
            this.txtSUserId.Name = "txtSUserId";
            this.txtSUserId.Size = new System.Drawing.Size(125, 21);
            this.txtSUserId.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器：";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(237, 204);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 21);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // gBoxDest
            // 
            this.gBoxDest.Controls.Add(this.label12);
            this.gBoxDest.Controls.Add(this.pDestType);
            this.gBoxDest.Controls.Add(this.lblDTip);
            this.gBoxDest.Controls.Add(this.cmboxDestServer);
            this.gBoxDest.Controls.Add(this.cmboxDDatabase);
            this.gBoxDest.Controls.Add(this.txtDPort);
            this.gBoxDest.Controls.Add(this.label9);
            this.gBoxDest.Controls.Add(this.label5);
            this.gBoxDest.Controls.Add(this.txtDPassword);
            this.gBoxDest.Controls.Add(this.txtDUserId);
            this.gBoxDest.Controls.Add(this.label6);
            this.gBoxDest.Controls.Add(this.label7);
            this.gBoxDest.Controls.Add(this.label8);
            this.gBoxDest.Location = new System.Drawing.Point(275, 2);
            this.gBoxDest.Name = "gBoxDest";
            this.gBoxDest.Size = new System.Drawing.Size(274, 197);
            this.gBoxDest.TabIndex = 8;
            this.gBoxDest.TabStop = false;
            this.gBoxDest.Text = "目标";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "数据库类型：";
            // 
            // pDestType
            // 
            this.pDestType.Controls.Add(this.radioButton3);
            this.pDestType.Controls.Add(this.radioButton4);
            this.pDestType.Location = new System.Drawing.Point(94, 146);
            this.pDestType.Name = "pDestType";
            this.pDestType.Size = new System.Drawing.Size(145, 27);
            this.pDestType.TabIndex = 17;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(80, 6);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(53, 16);
            this.radioButton3.TabIndex = 16;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Mysql";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(1, 6);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(83, 16);
            this.radioButton4.TabIndex = 15;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Sql Server";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // lblDTip
            // 
            this.lblDTip.AutoSize = true;
            this.lblDTip.Location = new System.Drawing.Point(92, 175);
            this.lblDTip.Name = "lblDTip";
            this.lblDTip.Size = new System.Drawing.Size(29, 12);
            this.lblDTip.TabIndex = 14;
            this.lblDTip.Text = "提示";
            // 
            // cmboxDestServer
            // 
            this.cmboxDestServer.FormattingEnabled = true;
            this.cmboxDestServer.Location = new System.Drawing.Point(94, 23);
            this.cmboxDestServer.Name = "cmboxDestServer";
            this.cmboxDestServer.Size = new System.Drawing.Size(173, 20);
            this.cmboxDestServer.TabIndex = 13;
            this.cmboxDestServer.SelectedIndexChanged += new System.EventHandler(this.cmboxSourceServer_SelectedIndexChanged);
            // 
            // cmboxDDatabase
            // 
            this.cmboxDDatabase.FormattingEnabled = true;
            this.cmboxDDatabase.Location = new System.Drawing.Point(94, 121);
            this.cmboxDDatabase.Name = "cmboxDDatabase";
            this.cmboxDDatabase.Size = new System.Drawing.Size(121, 20);
            this.cmboxDDatabase.TabIndex = 9;
            this.cmboxDDatabase.Click += new System.EventHandler(this.cmboxDatabase_Click);
            // 
            // txtDPort
            // 
            this.txtDPort.Location = new System.Drawing.Point(94, 97);
            this.txtDPort.Name = "txtDPort";
            this.txtDPort.Size = new System.Drawing.Size(100, 21);
            this.txtDPort.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "数据库：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "端口：";
            // 
            // txtDPassword
            // 
            this.txtDPassword.Location = new System.Drawing.Point(94, 73);
            this.txtDPassword.Name = "txtDPassword";
            this.txtDPassword.Size = new System.Drawing.Size(125, 21);
            this.txtDPassword.TabIndex = 5;
            // 
            // txtDUserId
            // 
            this.txtDUserId.Location = new System.Drawing.Point(94, 48);
            this.txtDUserId.Name = "txtDUserId";
            this.txtDUserId.Size = new System.Drawing.Size(125, 21);
            this.txtDUserId.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "密码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "用户名：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "服务器：";
            // 
            // ckBoxIgnoreType
            // 
            this.ckBoxIgnoreType.AutoSize = true;
            this.ckBoxIgnoreType.Location = new System.Drawing.Point(326, 208);
            this.ckBoxIgnoreType.Name = "ckBoxIgnoreType";
            this.ckBoxIgnoreType.Size = new System.Drawing.Size(72, 16);
            this.ckBoxIgnoreType.TabIndex = 9;
            this.ckBoxIgnoreType.Text = "忽略类型";
            this.ckBoxIgnoreType.UseVisualStyleBackColor = true;
            // 
            // ckBoxIgnoreLength
            // 
            this.ckBoxIgnoreLength.AutoSize = true;
            this.ckBoxIgnoreLength.Location = new System.Drawing.Point(403, 208);
            this.ckBoxIgnoreLength.Name = "ckBoxIgnoreLength";
            this.ckBoxIgnoreLength.Size = new System.Drawing.Size(96, 16);
            this.ckBoxIgnoreLength.TabIndex = 10;
            this.ckBoxIgnoreLength.Text = "忽略字段长度";
            this.ckBoxIgnoreLength.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 224);
            this.Controls.Add(this.ckBoxIgnoreLength);
            this.Controls.Add(this.ckBoxIgnoreType);
            this.Controls.Add(this.gBoxDest);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.gBoxSource);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "两数据库比较";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gBoxSource.ResumeLayout(false);
            this.gBoxSource.PerformLayout();
            this.pSourceType.ResumeLayout(false);
            this.pSourceType.PerformLayout();
            this.gBoxDest.ResumeLayout(false);
            this.gBoxDest.PerformLayout();
            this.pDestType.ResumeLayout(false);
            this.pDestType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBoxSource;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtSPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSPassword;
        private System.Windows.Forms.TextBox txtSUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gBoxDest;
        private System.Windows.Forms.TextBox txtDPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDPassword;
        private System.Windows.Forms.TextBox txtDUserId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmboxSDatabase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmboxDDatabase;
        private System.Windows.Forms.ComboBox cmboxSourceServer;
        private System.Windows.Forms.ComboBox cmboxDestServer;
        private System.Windows.Forms.Label lblSTip;
        private System.Windows.Forms.Label lblDTip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel pSourceType;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pDestType;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.CheckBox ckBoxIgnoreType;
        private System.Windows.Forms.CheckBox ckBoxIgnoreLength;
    }
}


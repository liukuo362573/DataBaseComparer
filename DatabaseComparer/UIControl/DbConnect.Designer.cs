namespace DatabaseComparer.UIControl
{
    partial class DbConnect
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gBox = new System.Windows.Forms.GroupBox();
            this.pDbType = new System.Windows.Forms.Panel();
            this.rBtnMysql = new System.Windows.Forms.RadioButton();
            this.rBtnSqlServer = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.cmboxServer = new System.Windows.Forms.ComboBox();
            this.cmboxDatabase = new System.Windows.Forms.ComboBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gBox.SuspendLayout();
            this.pDbType.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBox
            // 
            this.gBox.Controls.Add(this.pDbType);
            this.gBox.Controls.Add(this.label11);
            this.gBox.Controls.Add(this.lblTip);
            this.gBox.Controls.Add(this.cmboxServer);
            this.gBox.Controls.Add(this.cmboxDatabase);
            this.gBox.Controls.Add(this.txtPort);
            this.gBox.Controls.Add(this.label10);
            this.gBox.Controls.Add(this.label4);
            this.gBox.Controls.Add(this.txtPassword);
            this.gBox.Controls.Add(this.txtUserId);
            this.gBox.Controls.Add(this.label3);
            this.gBox.Controls.Add(this.label2);
            this.gBox.Controls.Add(this.label1);
            this.gBox.Location = new System.Drawing.Point(3, 3);
            this.gBox.Name = "gBox";
            this.gBox.Size = new System.Drawing.Size(273, 197);
            this.gBox.TabIndex = 1;
            this.gBox.TabStop = false;
            this.gBox.Text = "源头";
            // 
            // pDbType
            // 
            this.pDbType.Controls.Add(this.rBtnMysql);
            this.pDbType.Controls.Add(this.rBtnSqlServer);
            this.pDbType.Location = new System.Drawing.Point(94, 147);
            this.pDbType.Name = "pDbType";
            this.pDbType.Size = new System.Drawing.Size(145, 26);
            this.pDbType.TabIndex = 16;
            // 
            // rBtnMysql
            // 
            this.rBtnMysql.AutoSize = true;
            this.rBtnMysql.Location = new System.Drawing.Point(80, 6);
            this.rBtnMysql.Name = "rBtnMysql";
            this.rBtnMysql.Size = new System.Drawing.Size(14, 13);
            this.rBtnMysql.TabIndex = 7;
            this.rBtnMysql.TabStop = true;
            this.rBtnMysql.UseVisualStyleBackColor = true;
            this.rBtnMysql.Click += new System.EventHandler(this.rBtn_Click);
            // 
            // rBtnSqlServer
            // 
            this.rBtnSqlServer.AutoSize = true;
            this.rBtnSqlServer.Location = new System.Drawing.Point(1, 6);
            this.rBtnSqlServer.Name = "rBtnSqlServer";
            this.rBtnSqlServer.Size = new System.Drawing.Size(14, 13);
            this.rBtnSqlServer.TabIndex = 6;
            this.rBtnSqlServer.TabStop = true;
            this.rBtnSqlServer.UseVisualStyleBackColor = true;
            this.rBtnSqlServer.Click += new System.EventHandler(this.rBtn_Click);
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
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(92, 175);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(29, 12);
            this.lblTip.TabIndex = 13;
            this.lblTip.Text = "提示";
            // 
            // cmboxServer
            // 
            this.cmboxServer.FormattingEnabled = true;
            this.cmboxServer.Location = new System.Drawing.Point(94, 23);
            this.cmboxServer.Name = "cmboxServer";
            this.cmboxServer.Size = new System.Drawing.Size(173, 20);
            this.cmboxServer.TabIndex = 1;
            this.cmboxServer.SelectedIndexChanged += new System.EventHandler(this.cmboxServer_SelectedIndexChanged);
            // 
            // cmboxDatabase
            // 
            this.cmboxDatabase.FormattingEnabled = true;
            this.cmboxDatabase.Location = new System.Drawing.Point(94, 121);
            this.cmboxDatabase.Name = "cmboxDatabase";
            this.cmboxDatabase.Size = new System.Drawing.Size(121, 20);
            this.cmboxDatabase.TabIndex = 5;
            this.cmboxDatabase.Click += new System.EventHandler(this.cmboxDatabase_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(94, 97);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 4;
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
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(94, 73);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(125, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(94, 48);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(125, 21);
            this.txtUserId.TabIndex = 2;
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
            // DbConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gBox);
            this.Name = "DbConnect";
            this.Size = new System.Drawing.Size(283, 204);
            this.Load += new System.EventHandler(this.DbConnect_Load);
            this.gBox.ResumeLayout(false);
            this.gBox.PerformLayout();
            this.pDbType.ResumeLayout(false);
            this.pDbType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBox;
        private System.Windows.Forms.Panel pDbType;
        private System.Windows.Forms.RadioButton rBtnMysql;
        private System.Windows.Forms.RadioButton rBtnSqlServer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.ComboBox cmboxServer;
        private System.Windows.Forms.ComboBox cmboxDatabase;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

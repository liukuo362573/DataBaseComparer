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
            this.btnNext = new System.Windows.Forms.Button();
            this.ckBoxIgnoreType = new System.Windows.Forms.CheckBox();
            this.ckBoxIgnoreLength = new System.Windows.Forms.CheckBox();
            this.dbConnectDestination = new DatabaseComparer.UIControl.DbConnect();
            this.dbConnectSource = new DatabaseComparer.UIControl.DbConnect();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(235, 213);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 21);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ckBoxIgnoreType
            // 
            this.ckBoxIgnoreType.AutoSize = true;
            this.ckBoxIgnoreType.Location = new System.Drawing.Point(324, 217);
            this.ckBoxIgnoreType.Name = "ckBoxIgnoreType";
            this.ckBoxIgnoreType.Size = new System.Drawing.Size(72, 16);
            this.ckBoxIgnoreType.TabIndex = 9;
            this.ckBoxIgnoreType.Text = "忽略类型";
            this.ckBoxIgnoreType.UseVisualStyleBackColor = true;
            // 
            // ckBoxIgnoreLength
            // 
            this.ckBoxIgnoreLength.AutoSize = true;
            this.ckBoxIgnoreLength.Location = new System.Drawing.Point(401, 217);
            this.ckBoxIgnoreLength.Name = "ckBoxIgnoreLength";
            this.ckBoxIgnoreLength.Size = new System.Drawing.Size(96, 16);
            this.ckBoxIgnoreLength.TabIndex = 10;
            this.ckBoxIgnoreLength.Text = "忽略字段长度";
            this.ckBoxIgnoreLength.UseVisualStyleBackColor = true;
            // 
            // dbConnectDestination
            // 
            this.dbConnectDestination.ConnectString = null;
            this.dbConnectDestination.Location = new System.Drawing.Point(294, 2);
            this.dbConnectDestination.Name = "dbConnectDestination";
            this.dbConnectDestination.Size = new System.Drawing.Size(283, 204);
            this.dbConnectDestination.TabIndex = 12;
            // 
            // dbConnectSource
            // 
            this.dbConnectSource.ConnectString = null;
            this.dbConnectSource.Location = new System.Drawing.Point(3, 2);
            this.dbConnectSource.Name = "dbConnectSource";
            this.dbConnectSource.Size = new System.Drawing.Size(283, 204);
            this.dbConnectSource.TabIndex = 11;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 245);
            this.Controls.Add(this.dbConnectDestination);
            this.Controls.Add(this.dbConnectSource);
            this.Controls.Add(this.ckBoxIgnoreLength);
            this.Controls.Add(this.ckBoxIgnoreType);
            this.Controls.Add(this.btnNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "两数据库比较";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.CheckBox ckBoxIgnoreType;
        private System.Windows.Forms.CheckBox ckBoxIgnoreLength;
        private UIControl.DbConnect dbConnectSource;
        private UIControl.DbConnect dbConnectDestination;
    }
}


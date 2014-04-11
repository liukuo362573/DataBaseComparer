namespace DatabaseComparer
{
    partial class FrmDifferentList
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
            this.gBoxSource = new System.Windows.Forms.GroupBox();
            this.pBarSource = new System.Windows.Forms.ProgressBar();
            this.gBoxDest = new System.Windows.Forms.GroupBox();
            this.pBarDest = new System.Windows.Forms.ProgressBar();
            this.txtDestResult = new System.Windows.Forms.TextBox();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.btnDest = new System.Windows.Forms.Button();
            this.txtSourceResult = new System.Windows.Forms.TextBox();
            this.btnSource = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.gBoxSource.SuspendLayout();
            this.gBoxDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBoxSource
            // 
            this.gBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gBoxSource.Controls.Add(this.pBarSource);
            this.gBoxSource.Location = new System.Drawing.Point(130, 1);
            this.gBoxSource.Name = "gBoxSource";
            this.gBoxSource.Size = new System.Drawing.Size(301, 429);
            this.gBoxSource.TabIndex = 1;
            this.gBoxSource.TabStop = false;
            this.gBoxSource.Text = "源头";
            // 
            // pBarSource
            // 
            this.pBarSource.Location = new System.Drawing.Point(6, 17);
            this.pBarSource.Name = "pBarSource";
            this.pBarSource.Size = new System.Drawing.Size(288, 21);
            this.pBarSource.TabIndex = 1;
            // 
            // gBoxDest
            // 
            this.gBoxDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gBoxDest.Controls.Add(this.pBarDest);
            this.gBoxDest.Location = new System.Drawing.Point(431, 1);
            this.gBoxDest.Name = "gBoxDest";
            this.gBoxDest.Size = new System.Drawing.Size(301, 429);
            this.gBoxDest.TabIndex = 2;
            this.gBoxDest.TabStop = false;
            this.gBoxDest.Text = "目标";
            // 
            // pBarDest
            // 
            this.pBarDest.Location = new System.Drawing.Point(6, 18);
            this.pBarDest.Name = "pBarDest";
            this.pBarDest.Size = new System.Drawing.Size(288, 21);
            this.pBarDest.TabIndex = 2;
            // 
            // txtDestResult
            // 
            this.txtDestResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtDestResult.Location = new System.Drawing.Point(738, 34);
            this.txtDestResult.Multiline = true;
            this.txtDestResult.Name = "txtDestResult";
            this.txtDestResult.Size = new System.Drawing.Size(121, 395);
            this.txtDestResult.TabIndex = 8;
            // 
            // txtDest
            // 
            this.txtDest.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDest.Location = new System.Drawing.Point(738, 8);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(73, 21);
            this.txtDest.TabIndex = 7;
            // 
            // btnDest
            // 
            this.btnDest.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDest.Location = new System.Drawing.Point(817, 7);
            this.btnDest.Name = "btnDest";
            this.btnDest.Size = new System.Drawing.Size(42, 21);
            this.btnDest.TabIndex = 6;
            this.btnDest.Text = "搜索";
            this.btnDest.UseVisualStyleBackColor = true;
            this.btnDest.Click += new System.EventHandler(this.FieldSearch_Click);
            // 
            // txtSourceResult
            // 
            this.txtSourceResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtSourceResult.Location = new System.Drawing.Point(3, 32);
            this.txtSourceResult.Multiline = true;
            this.txtSourceResult.Name = "txtSourceResult";
            this.txtSourceResult.Size = new System.Drawing.Size(120, 398);
            this.txtSourceResult.TabIndex = 11;
            // 
            // btnSource
            // 
            this.btnSource.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSource.Location = new System.Drawing.Point(81, 6);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(42, 21);
            this.btnSource.TabIndex = 9;
            this.btnSource.Text = "搜索";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.FieldSearch_Click);
            // 
            // txtSource
            // 
            this.txtSource.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSource.Location = new System.Drawing.Point(2, 7);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(73, 21);
            this.txtSource.TabIndex = 10;
            // 
            // FrmDifferentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 433);
            this.Controls.Add(this.txtSourceResult);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.txtDestResult);
            this.Controls.Add(this.txtDest);
            this.Controls.Add(this.btnDest);
            this.Controls.Add(this.gBoxDest);
            this.Controls.Add(this.gBoxSource);
            this.Name = "FrmDifferentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库差异";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDifferentList_FormClosing);
            this.Load += new System.EventHandler(this.FrmDifferentList_Load);
            this.gBoxSource.ResumeLayout(false);
            this.gBoxDest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBoxSource;
        private System.Windows.Forms.GroupBox gBoxDest;
        private System.Windows.Forms.ProgressBar pBarSource;
        private System.Windows.Forms.ProgressBar pBarDest;
        private System.Windows.Forms.TextBox txtDestResult;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.Button btnDest;
        private System.Windows.Forms.TextBox txtSourceResult;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.TextBox txtSource;
    }
}
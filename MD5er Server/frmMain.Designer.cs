namespace MD5er_Server
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnCount = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblIpaddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbHash = new System.Windows.Forms.TextBox();
            this.btnHash = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.rtbLog = new MD5er_Server.ScrollingRichTextBox();
            this.btnCRestore = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(12, 39);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(86, 23);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnCount
            // 
            this.btnCount.Location = new System.Drawing.Point(12, 68);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(86, 23);
            this.btnCount.TabIndex = 1;
            this.btnCount.Text = "Count Clients";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblIpaddress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 308);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(599, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblIpaddress
            // 
            this.lblIpaddress.Name = "lblIpaddress";
            this.lblIpaddress.Size = new System.Drawing.Size(68, 17);
            this.lblIpaddress.Text = "IP: 127.0.0.1";
            // 
            // tbHash
            // 
            this.tbHash.Location = new System.Drawing.Point(12, 12);
            this.tbHash.Name = "tbHash";
            this.tbHash.Size = new System.Drawing.Size(169, 20);
            this.tbHash.TabIndex = 4;
            // 
            // btnHash
            // 
            this.btnHash.Location = new System.Drawing.Point(187, 11);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(75, 23);
            this.btnHash.TabIndex = 5;
            this.btnHash.Text = "Start";
            this.btnHash.UseVisualStyleBackColor = true;
            this.btnHash.Click += new System.EventHandler(this.btnHash_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(268, 11);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(349, 11);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 7;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(104, 41);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(483, 258);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // btnCRestore
            // 
            this.btnCRestore.Location = new System.Drawing.Point(430, 11);
            this.btnCRestore.Name = "btnCRestore";
            this.btnCRestore.Size = new System.Drawing.Size(80, 23);
            this.btnCRestore.TabIndex = 8;
            this.btnCRestore.Text = "Clear Restore";
            this.btnCRestore.UseVisualStyleBackColor = true;
            this.btnCRestore.Click += new System.EventHandler(this.btnCRestore_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 330);
            this.Controls.Add(this.btnCRestore);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnHash);
            this.Controls.Add(this.tbHash);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnCount);
            this.Controls.Add(this.btnStartServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "MD5er Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnCount;
        internal ScrollingRichTextBox rtbLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblIpaddress;
        private System.Windows.Forms.TextBox tbHash;
        private System.Windows.Forms.Button btnHash;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCRestore;
    }
}
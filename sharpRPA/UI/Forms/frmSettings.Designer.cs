namespace sharpRPA.UI.Forms
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkRetryOnDisconnect = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutomaticallyConnect = new System.Windows.Forms.CheckBox();
            this.chkServerEnabled = new System.Windows.Forms.CheckBox();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnConnect.Location = new System.Drawing.Point(8, 183);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Raleway", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStatus.Location = new System.Drawing.Point(6, 212);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(215, 19);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Current Status: Not Enabled";
            // 
            // chkRetryOnDisconnect
            // 
            this.chkRetryOnDisconnect.AutoSize = true;
            this.chkRetryOnDisconnect.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkRetryOnDisconnect.Location = new System.Drawing.Point(8, 113);
            this.chkRetryOnDisconnect.Name = "chkRetryOnDisconnect";
            this.chkRetryOnDisconnect.Size = new System.Drawing.Size(172, 19);
            this.chkRetryOnDisconnect.TabIndex = 15;
            this.chkRetryOnDisconnect.Text = "Retry If Connection Fails";
            this.chkRetryOnDisconnect.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Raleway", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(452, 58);
            this.label1.TabIndex = 14;
            this.label1.Text = "Optionally, the server component allows for remote execution of tasks as well as " +
    "visiblity and tracking into the sharpRPA workforce.\r\n";
            // 
            // chkAutomaticallyConnect
            // 
            this.chkAutomaticallyConnect.AutoSize = true;
            this.chkAutomaticallyConnect.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAutomaticallyConnect.Location = new System.Drawing.Point(7, 93);
            this.chkAutomaticallyConnect.Name = "chkAutomaticallyConnect";
            this.chkAutomaticallyConnect.Size = new System.Drawing.Size(231, 19);
            this.chkAutomaticallyConnect.TabIndex = 13;
            this.chkAutomaticallyConnect.Text = "Automatically Connect on Startup";
            this.chkAutomaticallyConnect.UseVisualStyleBackColor = true;
            // 
            // chkServerEnabled
            // 
            this.chkServerEnabled.AutoSize = true;
            this.chkServerEnabled.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkServerEnabled.Location = new System.Drawing.Point(7, 74);
            this.chkServerEnabled.Name = "chkServerEnabled";
            this.chkServerEnabled.Size = new System.Drawing.Size(195, 19);
            this.chkServerEnabled.TabIndex = 12;
            this.chkServerEnabled.Text = "Server Connection Enabled";
            this.chkServerEnabled.UseVisualStyleBackColor = true;
            // 
            // txtServerURL
            // 
            this.txtServerURL.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtServerURL.Location = new System.Drawing.Point(7, 154);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(371, 23);
            this.txtServerURL.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Raleway", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(4, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Server URL ex. ws://localhost:port/ws)\r";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.btnConnect);
            this.groupBox3.Controls.Add(this.lblStatus);
            this.groupBox3.Controls.Add(this.chkRetryOnDisconnect);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.chkAutomaticallyConnect);
            this.groupBox3.Controls.Add(this.chkServerEnabled);
            this.groupBox3.Controls.Add(this.txtServerURL);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Raleway", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox3.Location = new System.Drawing.Point(3, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 256);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "sharpRPA Server Settings";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundChangeIndex = 166;
            this.ClientSize = new System.Drawing.Size(476, 285);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerURL;
        private System.Windows.Forms.CheckBox chkAutomaticallyConnect;
        private System.Windows.Forms.CheckBox chkServerEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkRetryOnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}
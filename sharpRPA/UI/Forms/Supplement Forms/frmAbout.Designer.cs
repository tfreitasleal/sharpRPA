namespace sharpRPA.UI.Forms.Supplemental
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblAppVersion = new System.Windows.Forms.Label();
            this.lblIconCredit = new System.Windows.Forms.Label();
            this.lblTaskSchedulerCredit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.BackColor = System.Drawing.Color.Transparent;
            this.lblProjectName.Font = new System.Drawing.Font("Raleway ExtraBold", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectName.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblProjectName.Location = new System.Drawing.Point(10, 1);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(179, 41);
            this.lblProjectName.TabIndex = 1;
            this.lblProjectName.Text = "sharpRPA";
            // 
            // lblAppVersion
            // 
            this.lblAppVersion.AutoSize = true;
            this.lblAppVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblAppVersion.Font = new System.Drawing.Font("Raleway Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppVersion.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblAppVersion.Location = new System.Drawing.Point(14, 43);
            this.lblAppVersion.Name = "lblAppVersion";
            this.lblAppVersion.Size = new System.Drawing.Size(90, 19);
            this.lblAppVersion.TabIndex = 2;
            this.lblAppVersion.Text = "sharpRPA";
            // 
            // lblIconCredit
            // 
            this.lblIconCredit.AutoSize = true;
            this.lblIconCredit.BackColor = System.Drawing.Color.Transparent;
            this.lblIconCredit.Font = new System.Drawing.Font("Raleway Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIconCredit.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblIconCredit.Location = new System.Drawing.Point(12, 64);
            this.lblIconCredit.Name = "lblIconCredit";
            this.lblIconCredit.Size = new System.Drawing.Size(518, 19);
            this.lblIconCredit.TabIndex = 3;
            this.lblIconCredit.Text = "Icons by SmashIcons (https://www.flaticon.com/authors/smashicons)";
            this.lblIconCredit.Click += new System.EventHandler(this.lblIconCredit_Click);
            // 
            // lblTaskSchedulerCredit
            // 
            this.lblTaskSchedulerCredit.AutoSize = true;
            this.lblTaskSchedulerCredit.BackColor = System.Drawing.Color.Transparent;
            this.lblTaskSchedulerCredit.Font = new System.Drawing.Font("Raleway Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskSchedulerCredit.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTaskSchedulerCredit.Location = new System.Drawing.Point(12, 87);
            this.lblTaskSchedulerCredit.Name = "lblTaskSchedulerCredit";
            this.lblTaskSchedulerCredit.Size = new System.Drawing.Size(537, 19);
            this.lblTaskSchedulerCredit.TabIndex = 4;
            this.lblTaskSchedulerCredit.Text = "Task Scheduler API by dahall (https://github.com/dahall/TaskScheduler)";
            this.lblTaskSchedulerCredit.Click += new System.EventHandler(this.lblTaskSchedulerCredit_Click);
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundChangeIndex = 110;
            this.ClientSize = new System.Drawing.Size(558, 124);
            this.Controls.Add(this.lblTaskSchedulerCredit);
            this.Controls.Add(this.lblAppVersion);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.lblIconCredit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAbout";
            this.Text = "About";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblAppVersion;
        private System.Windows.Forms.Label lblIconCredit;
        private System.Windows.Forms.Label lblTaskSchedulerCredit;
    }
}
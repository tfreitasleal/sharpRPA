using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sharpRPA.UI.Forms.Supplemental
{
    public partial class frmAbout : UIForm
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblAppVersion.Text = "v." + System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        private void lblIconCredit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.flaticon.com/authors/smashicons");
        }

        private void lblTaskSchedulerCredit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dahall/TaskScheduler");
        }


    }
}

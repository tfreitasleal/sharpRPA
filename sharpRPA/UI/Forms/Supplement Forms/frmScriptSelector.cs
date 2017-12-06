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

    public partial class frmScriptSelector : UIForm
    {
        public string selectedScript;
        private string rpaScriptsFolder;
        public frmScriptSelector()
        {
            InitializeComponent();
        }

        private void frmScriptSelector_Load(object sender, EventArgs e)
        {

            rpaScriptsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\sharpRPA\\My Scripts\\";
            var files = System.IO.Directory.GetFiles(rpaScriptsFolder);

            foreach (var fil in files)
            {
               System.IO.FileInfo newFileInfo = new System.IO.FileInfo(fil);
               cboSelectFile.Items.Add(newFileInfo.Name);
            }


         

        }

        private void btnRun_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void uiBtnOpen_Click(object sender, EventArgs e)
        {

            if (cboSelectFile.Text == "")
            {

                return;
            }

            selectedScript = System.IO.Path.Combine(rpaScriptsFolder, cboSelectFile.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void uiBtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

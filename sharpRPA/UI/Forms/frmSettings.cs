using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace sharpRPA.UI.Forms
{
    public partial class frmSettings : UI.Forms.UIForm
    {
        Core.ApplicationSettings newAppSettings;
        public frmScriptBuilder scriptBuilderForm;
        public frmSettings(frmScriptBuilder sender)
        {
            scriptBuilderForm = sender;
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            newAppSettings = new Core.ApplicationSettings();
            newAppSettings = newAppSettings.GetOrCreateApplicationSettings();

            var serverSettings = newAppSettings.ServerSettings;
            chkServerEnabled.DataBindings.Add("Checked", serverSettings, "ServerConnectionEnabled");
            chkAutomaticallyConnect.DataBindings.Add("Checked", serverSettings, "ConnectToServerOnStartup");
            chkRetryOnDisconnect.DataBindings.Add("Checked", serverSettings, "RetryServerConnectionOnFail");
            txtServerURL.DataBindings.Add("Text", serverSettings, "ServerURL");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            scriptBuilderForm.CreateSocketConnection(txtServerURL.Text);
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            newAppSettings.Save(newAppSettings);
        }

        private void btnAddAssembly_Click(object sender, EventArgs e)
        {
            //allow importing custom command assemblies
            OpenFileDialog newOpenDialog = new OpenFileDialog();

            if (newOpenDialog.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
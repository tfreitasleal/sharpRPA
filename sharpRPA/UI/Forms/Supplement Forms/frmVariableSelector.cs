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
    public partial class frmVariableSelector : UIForm
    {
        public frmVariableSelector()
        {
            InitializeComponent();
        }

        private void frmVariableSelector_Load(object sender, EventArgs e)
        {

        }

        private void uiBtnOk_Click(object sender, EventArgs e)
        {
            if (lstVariables.SelectedItem == null)
            {
                MessageBox.Show("There are no variables selected! Select a variable and Ok or select Cancel");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void uiBtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

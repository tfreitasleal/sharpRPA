using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sharpRPA.UI.CustomControls
{
    public partial class CommandGroupControl : UserControl
    {
        public CommandGroupControl()
        {
            InitializeComponent();
            this.GroupName = "Category";
        }

        private void CommandSelectionControl_Load(object sender, EventArgs e)
        {
        }
        private string groupName;
        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                groupName = value;
                this.Invalidate();
            }
        }

        private void CommandSelectionControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.folder, 5, 0, 32, 32);
            e.Graphics.DrawString(this.GroupName, this.Font, new SolidBrush(this.ForeColor), 39, 10);
        }

        private void CommandSelectionControl_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void CommandSelectionControl_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
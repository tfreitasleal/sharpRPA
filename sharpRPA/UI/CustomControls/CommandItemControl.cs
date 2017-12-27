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
    public partial class CommandItemControl : UserControl
    {
        public CommandItemControl()
        {
            InitializeComponent();
            this.CommandImage = Properties.Resources.comment;
        }

        public string FunctionalDescription { get; set; }
        public string ImplementationDescription { get; set; }
        private string commandDisplay;
        public string CommandDisplay
        {
            get
            {
                return commandDisplay;
            }
            set
            {
                commandDisplay = value;
                this.Invalidate();
            }
        }
        private Image commandImage;
        public Image CommandImage
        {
            get
            {
                return commandImage;
            }
            set
            {
                commandImage = value;
                this.Invalidate();
            }
        }

        private void CommandItemControl_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void CommandItemControl_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void CommandItemControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.CommandImage, 0, 0, 16, 16);
            e.Graphics.DrawString(this.CommandDisplay, this.Font, new SolidBrush(this.ForeColor), 18, 0);
        }

        private void CommandItemControl_Load(object sender, EventArgs e)
        {
        }
    }
}
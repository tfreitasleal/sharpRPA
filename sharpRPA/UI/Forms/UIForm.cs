using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sharpRPA.UI.Forms
{
    public class UIForm : System.Windows.Forms.Form
    {
        private int backgroundChangeIndex;
        public int BackgroundChangeIndex
        {
            get
            {
                if (backgroundChangeIndex <= 0)
                {
                    return (this.Height / 2);
                }
                else
                {
                    return backgroundChangeIndex;
                }
            }
            set
            {
                this.backgroundChangeIndex = value;
                this.Invalidate();
            }
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            //using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush
            //      (new Rectangle(0, 0, this.Width, BackgroundChangeIndex), Color.SteelBlue, Color.FromArgb(106, 160, 204), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            //{
            //    e.Graphics.FillRectangle(brush, 0, 0, this.Width, BackgroundChangeIndex);
            //}

            //using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush

            //(new Rectangle(0, BackgroundChangeIndex, this.Width, this.Height), Color.FromArgb(255, 214, 88), Color.Orange, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            //{
            //    e.Graphics.FillRectangle(brush, 0, BackgroundChangeIndex, this.Width, this.Height);
            //}

            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush
                  (new Rectangle(0, 0, this.Width, BackgroundChangeIndex), Color.White, Color.WhiteSmoke, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, 0, 0, this.Width, BackgroundChangeIndex);
            }

            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush

            (new Rectangle(0, BackgroundChangeIndex, this.Width, this.Height), Color.SteelBlue, Color.LightSteelBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, 0, BackgroundChangeIndex, this.Width, this.Height);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.SuspendLayout();
            //
            // UIForm
            //
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UIForm";
            this.Load += new System.EventHandler(this.UIForm_Load);
            this.ResumeLayout(false);
        }

        private void UIForm_Load(object sender, EventArgs e)
        {
        }
        public static void MoveFormToBottomRight(Form sender)
        {
            sender.Location = new Point(Screen.FromPoint(sender.Location).WorkingArea.Right - sender.Width, Screen.FromPoint(sender.Location).WorkingArea.Bottom - sender.Height);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace brachIOplexus
{
    internal partial class unityAddCamera : Form
    {
        public unityAddCamera()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "Save Camera Position";
            this.AcceptButton = this.EnterButton;
            this.CancelButton = this.ExitButton;

            // https://stackoverflow.com/questions/2022660/how-to-get-the-size-of-a-winforms-form-titlebar-height
            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;

            int midX = (this.Width / 2);
            int midY = this.Height / 2 - titleHeight;

            this.NameLabel.Location = new Point(midX - (NameLabel.Width / 2), (int)(midY - (NameLabel.Height / 2) - this.PostitionText.Height - 10));
            this.PostitionText.Location = new Point(midX - (this.PostitionText.Width / 2), (int)(midY - (this.PostitionText.Height / 2) + 10));
            this.EnterButton.Location = new Point(midX - this.EnterButton.Width - 15 , (int)(midY - (this.EnterButton.Height / 2) + 40));
            this.ExitButton.Location = new Point(midX + 15, (int)(midY - (this.ExitButton.Height / 2) + 40));

            this.PostitionText.Text = $"Position{mainForm.unityCameraPositions.Count}";
        }

        private void enter(object sender, EventArgs e)
        {
            string name = this.PostitionText.Text;
            mainForm.unityCameraPositions.Add(name);
            this.Close();
        }
        private void exit(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

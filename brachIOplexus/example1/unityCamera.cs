using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace brachIOplexus
{
    partial class unityCamera : Form
    {
        private Point origin = new Point(10, 10);
        private Size textSize = new Size(150, 5);

        public unityCamera()
        {
            InitializeComponent();
        }
        private void deletePosition(object sender, EventArgs e)
        {
            Button delete = (Button)sender;
            if (System.Windows.Forms.Application.OpenForms["mainForm"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["mainForm"] as mainForm).deletePosition(int.Parse(delete.Name)); ;
            }
        }
        private void renamePosition(object sender, EventArgs e)
        {
            Button rename = (Button)sender;
            var matches = this.Controls.Find("textBox" + rename.Name.ToString(),true);
            string name = matches[0].Text;
            mainForm.unityCameraPositions[int.Parse(rename.Name)] = name;
        }
    }
}

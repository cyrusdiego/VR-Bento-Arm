using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
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

            //https://stackoverflow.com/questions/4734116/find-and-extract-a-number-from-a-string
            string index = Regex.Match(delete.Name.ToString(), @"\d+").Value;
            if (System.Windows.Forms.Application.OpenForms["mainForm"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["mainForm"] as mainForm).deletePosition(int.Parse(index)); 
            }

            Control[] matches = new Control[4];
            matches[0] = this.Controls.Find("textBox" + index, true)[0];
            matches[1] = this.Controls.Find("delete" + index, true)[0];
            matches[2] = this.Controls.Find("slot" + index, true)[0];
            matches[3] = this.Controls.Find("rename" + index, true)[0];

            for(int i = 0; i < 4; i++)
            {
                matches[i].Enabled = false;
            }


        }
        private void renamePosition(object sender, EventArgs e)
        {
            Button rename = (Button)sender;
            string index = Regex.Match(rename.Name.ToString(), @"\d+").Value;
            var matches = this.Controls.Find("textBox" + index,true);
            string name = matches[0].Text;
            mainForm.unityCameraPositions[int.Parse(index)] = name;
            if (System.Windows.Forms.Application.OpenForms["mainForm"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["mainForm"] as mainForm).updateCurrentPosition();
            }
        }
    }
}

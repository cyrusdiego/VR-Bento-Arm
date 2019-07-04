using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;

namespace brachIOplexus
{
    partial class unityCamera : Form
    {
        private Point origin = new Point(10, 10);
        private Size textSize = new Size(150, 5);
        private List<Button> deleteList = new List<Button>();
        private List<Button> renameList = new List<Button>();
        private List<TextBox> textList = new List<TextBox>();
        private List<Label> labelList = new List<Label>();
        private string jsonStoragePath = @"C:\Users\Trillian\Documents\VR-Bento-Arm\brachIOplexus\Example1\resources\unityCameraPositions";

        public unityCamera()
        {
            InitializeComponent();
        }
        private void deletePosition(object sender, EventArgs e)
        {
            int index = deleteList.FindIndex(btn => btn == (Button)sender);

            if (System.Windows.Forms.Application.OpenForms["mainForm"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["mainForm"] as mainForm).deletePosition(index);
            }
            deleteList[index].Enabled = false;
            renameList[index].Enabled = false;
            textList[index].Enabled = false;
            labelList[index].Enabled = false;

            deleteList.RemoveAt(index);
            textList.RemoveAt(index);
            labelList.RemoveAt(index);
            renameList.RemoveAt(index);
        }

        private void renamePosition(object sender, EventArgs e)
        {
            int index = renameList.FindIndex(btn => btn == (Button)sender);
            string name = textList[index].Text;
            mainForm.unityCameraPositions[index] = name;
            if (System.Windows.Forms.Application.OpenForms["mainForm"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["mainForm"] as mainForm).updateCurrentPosition();
            }

            string oldName = System.IO.Path.Combine(jsonStoragePath, $"{index}");
            string newName = System.IO.Path.Combine(jsonStoragePath, $"{name}");
            System.IO.File.Move(oldName,newName);
        }
    }
}

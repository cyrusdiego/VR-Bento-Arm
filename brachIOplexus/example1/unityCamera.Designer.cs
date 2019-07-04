using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace brachIOplexus
{
    partial class unityCamera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // unityCamera
            // 
            this.Name = "unityCamera";
            this.ResumeLayout(false);
            this.AutoSize = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Edit - Camera Positions";
            for (int i = 0; i < mainForm.unityCameraPositions.Count; i++)
            {
                Label slot = new Label();
                TextBox name = new TextBox();
                Button delete = new Button();
                Button rename = new Button();

                slot.Text = i.ToString();
                slot.Name = "slot";
                slot.AutoSize = true;

                name.Text = mainForm.unityCameraPositions[i];
                name.Name = "textBox";
                name.Size = textSize;

                delete.Text = "Delete";
                delete.Name = "delete";
                delete.AutoSize = true;
                delete.Click += new EventHandler(this.deletePosition);

                rename.Text = "Rename";
                rename.Name = "rename";
                rename.AutoSize = true;
                rename.Click += new EventHandler(this.renamePosition);

                slot.Location = new Point(origin.X, origin.Y + (i * 22));
                name.Location = new Point(origin.X + 15, origin.Y + (i * 20));
                delete.Location = new Point(origin.X + 15 + textSize.Width, origin.Y + (i * 20));
                rename.Location = new Point(origin.X + 15 + textSize.Width + delete.Width, origin.Y + (i * 20));

                this.Controls.Add(slot);
                this.Controls.Add(name);
                this.Controls.Add(delete);
                this.Controls.Add(rename);

                this.labelList.Add(slot);
                this.textList.Add(name);
                this.deleteList.Add(delete);
                this.renameList.Add(rename);

            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace brachIOplexus
{
    internal partial class unitySave : Form
    {
        public string fileName { get; set; }

        public unitySave()
        {
            InitializeComponent();
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Enter_Click(object sender, EventArgs e)
        {
            fileName = this.textBox1.Text;
            this.Close();
        }
    }
}

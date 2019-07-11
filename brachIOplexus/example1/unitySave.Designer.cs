namespace brachIOplexus
{
    partial class unitySave
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.unityProfileName = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Enter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 117);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(143, 20);
            this.textBox1.TabIndex = 0;
            // 
            // unityProfileName
            // 
            this.unityProfileName.AutoSize = true;
            this.unityProfileName.Location = new System.Drawing.Point(83, 101);
            this.unityProfileName.Name = "unityProfileName";
            this.unityProfileName.Size = new System.Drawing.Size(110, 13);
            this.unityProfileName.TabIndex = 1;
            this.unityProfileName.Text = "Save as: ";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(56, 143);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            this.CancelButton = Cancel;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            // 
            // Enter
            // 
            this.Enter.Location = new System.Drawing.Point(143, 143);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(75, 23);
            this.Enter.TabIndex = 3;
            this.Enter.Text = "Enter";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Enter_Click);
            this.AcceptButton = Enter;
            this.Enter.DialogResult = System.Windows.Forms.DialogResult.OK;
            // 
            // unityAddCameraProfile
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Enter);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.unityProfileName);
            this.Controls.Add(this.textBox1);
            this.Name = "unityAddCameraProfile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label unityProfileName;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Enter;
    }
}

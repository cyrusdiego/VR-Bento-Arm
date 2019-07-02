namespace brachIOplexus
{
    partial class unityAddCamera
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
            this.NameLabel = new System.Windows.Forms.Label();
            this.PostitionText = new System.Windows.Forms.TextBox();
            this.EnterButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Name
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 9);
            this.NameLabel.Name = "Name";
            this.NameLabel.Size = new System.Drawing.Size(35, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name of Camera Position";
            // 
            // PostitionText
            // 
            this.PostitionText.Location = new System.Drawing.Point(12, 25);
            this.PostitionText.Name = "PostitionText";
            this.PostitionText.Size = new System.Drawing.Size(100, 20);
            this.PostitionText.TabIndex = 1;
            // 
            // EnterButton
            // 
            this.EnterButton.Location = new System.Drawing.Point(12, 51);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(75, 23);
            this.EnterButton.TabIndex = 2;
            this.EnterButton.Text = "Enter";
            this.EnterButton.UseVisualStyleBackColor = true;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(93, 51);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.Text = "Cancel";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // unityAddCamera
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.PostitionText);
            this.Controls.Add(this.NameLabel);
            this.Name = "unityAddCamera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox PostitionText;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.Button ExitButton;
    }
}

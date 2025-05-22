namespace IntegralTable
{
    partial class Start
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
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.changeLabel = new System.Windows.Forms.LinkLabel();
            this.openButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.pathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathTextBox.Font = new System.Drawing.Font("Arial", 8F);
            this.pathTextBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.pathTextBox.Location = new System.Drawing.Point(13, 16);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(0, 5, 1, 0);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(372, 20);
            this.pathTextBox.TabIndex = 5;
            // 
            // changeLabel
            // 
            this.changeLabel.ActiveLinkColor = System.Drawing.Color.Silver;
            this.changeLabel.AutoSize = true;
            this.changeLabel.LinkColor = System.Drawing.SystemColors.HighlightText;
            this.changeLabel.Location = new System.Drawing.Point(11, 39);
            this.changeLabel.Name = "changeLabel";
            this.changeLabel.Size = new System.Drawing.Size(44, 13);
            this.changeLabel.TabIndex = 6;
            this.changeLabel.TabStop = true;
            this.changeLabel.Text = "Change";
            this.changeLabel.VisitedLinkColor = System.Drawing.SystemColors.HighlightText;
            // 
            // openButton
            // 
            this.openButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openButton.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.openButton.Location = new System.Drawing.Point(300, 78);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(85, 23);
            this.openButton.TabIndex = 21;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(397, 114);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.changeLabel);
            this.Controls.Add(this.pathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integral Table";
            this.Load += new System.EventHandler(this.Start_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.LinkLabel changeLabel;
        private System.Windows.Forms.Button openButton;
    }
}


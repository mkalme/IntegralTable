using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using IntegralTablesModel;
using IntegralTableEngine;

namespace IntegralTable
{
    public partial class PasswordForm : Form
    {
        private static Base BaseForm;
        private Password Password { get; set; }
        private bool AlreadyExists { get; set; }

        public PasswordForm(Base baseFarm, Password password, bool alreadyExists)
        {
            InitializeComponent();

            this.Password = password;
            this.AlreadyExists = alreadyExists;
            BaseForm = baseFarm;
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            SetPassword();
        }

        //Set
        private void SetPassword() {
            passwordNameTextBox.Text = Password.Name;
            websiteNameTextBox.Text = Password.Website;
            passwordTextBox.Text = Password.MainPassword;
            emailTextBox.Text = Password.Email;
            notesRichtextBox.Text = Password.Notes;

            entry1HeaderLabel.Text = Password.Entry1.HeaderName;
            entry1NameTextBox.Text = Password.Entry1.Name;
            entry1PasswordTextBox.Text = Password.Entry1.Password;

            entry2HeaderLabel.Text = Password.Entry2.HeaderName;
            entry2NameTextBox.Text = Password.Entry2.Name;
            entry2PasswordTextBox.Text = Password.Entry2.Password;

            enableEditingCheckBox.Checked = Password.EditingEnabled;
        }

        //CheckBox
        private void EnableEditingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = enableEditingCheckBox.Checked;

            TextBox[] textBoxes = {passwordNameTextBox, websiteNameTextBox, emailTextBox, passwordTextBox, entry1NameTextBox,
            entry1PasswordTextBox, entry2NameTextBox, entry2PasswordTextBox, generatedPasswordTextBox};

            //Change state
            for (int i = 0; i < textBoxes.Length; i++) {
                textBoxes[i].ReadOnly = !enabled;
            }

            notesRichtextBox.ReadOnly = !enabled;

            entry1NameLink.Visible = enabled;
            entry1NameLinkLabel.Visible = !enabled;

            entry2NameLink.Visible = enabled;
            entry2NameLinkLabel.Visible = !enabled;

            numberOfCharsMaskedTextBox.Enabled = enabled;

            //Change color
            Color TextBoxColor = enabled ? ColorTranslator.FromHtml("#232323") : ColorTranslator.FromHtml("#2D2D2D");
            Color TextBoxForeColor = enabled ? ColorTranslator.FromHtml("#E0E0E0") : ColorTranslator.FromHtml("#898989");

            //TextBox Color
            for (int i = 0; i < textBoxes.Length; i++) {
                textBoxes[i].BackColor = TextBoxColor;
                textBoxes[i].ForeColor = TextBoxForeColor;
            }

            notesRichtextBox.BackColor = TextBoxColor;
            notesPanel.BackColor = TextBoxColor;
            numberOfCharsMaskedTextBox.BackColor = TextBoxColor;

            notesRichtextBox.ForeColor = TextBoxForeColor;
            numberOfCharsMaskedTextBox.ForeColor = TextBoxForeColor;

            //Button Color
            generatePasswordButton.ForeColor = enabled ? SystemColors.HighlightText : Color.Gray;
        }

        //Paint GroupBox
        private static Color BackgroundColor = ColorTranslator.FromHtml("#2D2D2D");
        private static Color TextColor = ColorTranslator.FromHtml("#E0E0E0");
        private static Color BorderColor = ColorTranslator.FromHtml("#999999");

        private void PasswordNameGroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void GroupBox2_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void PasswordGroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void EmailGroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void NotesGroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void Entry1GroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void Entry2GroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }
        private void GeneratedPasswordGroupBox_Paint(object sender, PaintEventArgs e) {
            Draw(sender, e);
        }

        private void Draw(object sender, PaintEventArgs e) {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, TextColor, BorderColor);
        }
        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null) {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(BackgroundColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

        //Buttons
        private void GeneratePasswordButton_Click(object sender, EventArgs e) {
            if (enableEditingCheckBox.Checked) {
                int characters = 0;
                bool parsed = Int32.TryParse(numberOfCharsMaskedTextBox.Text, out characters);

                //Generate
                string chars = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

                Random rand = new Random();

                string text = "";
                for (int i = 0; i < characters; i++) {
                    text += chars[rand.Next(chars.Length)];
                }

                generatedPasswordTextBox.Text = text;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            UpdatePassword();

            if (AlreadyExists){
                XmlMethods.EditPassword(Password);
            }else {
                XmlMethods.CreatePassword(Password, BaseForm.ListToPath());
            }

            BaseForm.Load_Base();

            Close();
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Scripts
        private void UpdatePassword() {
            if (!AlreadyExists){
                Password.Properties.CreationDate = DateTime.Now;
                Password.Properties.LastModificationDate = DateTime.Now;
                Password.Properties.LastPasswordChange = DateTime.Now;

            }else if (Password.MainPassword != passwordTextBox.Text ||
                Password.Entry1.Password != entry1PasswordTextBox.Text || 
                Password.Entry2.Password != entry2PasswordTextBox.Text){

                Password.Properties.LastModificationDate = DateTime.Now;
                Password.Properties.LastPasswordChange = DateTime.Now;
            }

            Password.Name = passwordNameTextBox.Text;
            Password.Website = websiteNameTextBox.Text;
            Password.MainPassword = passwordTextBox.Text;
            Password.Email = emailTextBox.Text;
            Password.Notes = notesRichtextBox.Text;

            Password.Entry1.HeaderName = entry1HeaderLabel.Text;
            Password.Entry1.Name = entry1NameTextBox.Text;
            Password.Entry1.Password = entry1PasswordTextBox.Text;

            Password.Entry2.HeaderName = entry2HeaderLabel.Text;
            Password.Entry2.Name = entry2NameTextBox.Text;
            Password.Entry2.Password = entry2PasswordTextBox.Text;

            Password.EditingEnabled = enableEditingCheckBox.Checked;
        }

        //Copy to clipboard
        private void CopyPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            Clipboard.SetText(string.IsNullOrEmpty(passwordTextBox.Text) ? " " : passwordTextBox.Text);
        }
        private void CopyEmailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            Clipboard.SetText(string.IsNullOrEmpty(emailTextBox.Text) ? " " : emailTextBox.Text);
        }
        private void CopyNotesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            Clipboard.SetText(string.IsNullOrEmpty(notesRichtextBox.Text) ? " " : notesRichtextBox.Text);
        }
        private void copyPasswordEntry1Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            Clipboard.SetText(string.IsNullOrEmpty(entry1PasswordTextBox.Text) ? " " : entry1PasswordTextBox.Text);
        }
        private void copyPasswordEntry2Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            Clipboard.SetText(string.IsNullOrEmpty(entry2PasswordTextBox.Text) ? " " : entry2PasswordTextBox.Text);
        }
        private void CopyGeneratedPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            Clipboard.SetText(string.IsNullOrEmpty(generatedPasswordTextBox.Text) ? " " : generatedPasswordTextBox.Text);
        }
    }
}

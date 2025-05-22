using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntegralTableEngine;
using IntegralTablesModel;

namespace IntegralTable
{
    public partial class Rename : Form
    {
        private Base BaseForm { get; set; }
        private EntityType EntityType { get; set; }
        private string XmlPath { get; set; }
        private string ExistingName { get; set; }

        public Rename(Base baseForm, EntityType entityType, string xmlPath, string existingName)
        {
            InitializeComponent();

            this.BaseForm = baseForm;
            this.EntityType = entityType;
            this.XmlPath = xmlPath;
            this.ExistingName = existingName;
        }

        private void Rename_Load(object sender, EventArgs e){
            nameTextBox.Text = ExistingName;

            SetPictureBox();
        }

        private void SaveButton_Click(object sender, EventArgs e){
            XmlMethods.Rename(XmlPath, nameTextBox.Text);

            BaseForm.Load_Base();

            Close();
        }

        private void SetPictureBox() {
            if (EntityType == EntityType.Group) {
                nameLabel.Text = "Name of the group";
                pictureBox1.Location = new Point(2, 68);
                pictureBox1.BackgroundImage = Properties.Resources.Group;
            } else if (EntityType == EntityType.Password) {
                nameLabel.Text = "Name of the password";
                pictureBox1.Location = new Point(-1, 64);
                pictureBox1.BackgroundImage = Properties.Resources.Password;
            } else if (EntityType == EntityType.Document) {
                nameLabel.Text = "Name of the document";
                pictureBox1.Location = new Point(0, 68);
                pictureBox1.BackgroundImage = Properties.Resources.Document;
            }
        }

    }
}

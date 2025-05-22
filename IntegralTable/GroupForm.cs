using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntegralTablesModel;
using IntegralTableEngine;

namespace IntegralTable
{
    public partial class GroupForm : Form
    {
        private Base BaseForm { get; set; }
        private Group Group { get; set; }
        private bool AlreadyExists { get; set; }

        public GroupForm(Base baseForm, Group group, bool alreadyExists)
        {
            InitializeComponent();

            this.BaseForm = baseForm;
            this.Group = group;
            this.AlreadyExists = alreadyExists;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            UpdateGroup();

            if (AlreadyExists){
                XmlMethods.EditGroup(Group);
            }else {
                XmlMethods.CreateGroup(Group, BaseForm.ListToPath());
            }

            BaseForm.Load_Base();

            Close();
        }

        private void UpdateGroup() {
            if (!AlreadyExists) {
                Group.Properties.CreationDate = DateTime.Now;
                Group.Properties.LastModificationDate = DateTime.Now;
                Group.Properties.LastPasswordChange = DateTime.Now;
            } else if (Group.Name != nameTextBox.Text) {
                Group.Properties.LastModificationDate = DateTime.Now;
            }

            Group.Name = nameTextBox.Text;
        }
    }
}

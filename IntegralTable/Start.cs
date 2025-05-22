using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IntegralTable
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            CreateBase();
        }

        private void CreateBase() {
            if (!Directory.Exists(Storage.Path)) {
                Directory.CreateDirectory(Storage.Path);
            }

            if (!File.Exists(Storage.FilePath)) {
                File.WriteAllText(Storage.FilePath, "<manager></manager>");
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            Base baseForm = new Base();

            Hide();

            baseForm.ShowDialog();

            Close();
        }
    }
}

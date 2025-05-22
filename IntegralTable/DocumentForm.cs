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
    public partial class DocumentForm : Form
    {
        private static Base BaseForm;
        private Document Document { get; set; }
        private bool AlreadyExists { get; set; }

        public DocumentForm(Base baseFarm, Document document, bool alreadyExists)
        {
            InitializeComponent();

            this.Document = document;
            this.AlreadyExists = alreadyExists;
            BaseForm = baseFarm;
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {
            SetDocument();
        }

        private void SetDocument() {
            richTextBox1.Text = Document.Text;

            richTextBox1.Font = Document.DefaultFont;
            richTextBox1.ForeColor = Document.ForeColor;

            for (int i = 0; i < Document.Changes.Count; i++) {
                Change change = Document.Changes[i];

                SetChange(change.StartIndex,
                          change.Characters,
                          change.Font,
                          change.ForeColor,
                          change.BackgroundColor
                );
            }
        }
        private void SetChange(int index, int characters, Font font, Color foreColor, Color backgrounColor) {
            richTextBox1.SelectionStart = index;
            richTextBox1.SelectionLength = characters;

            richTextBox1.SelectionFont = font;
            richTextBox1.SelectionColor = foreColor;
            richTextBox1.SelectionBackColor = backgrounColor;
        }
    }
}

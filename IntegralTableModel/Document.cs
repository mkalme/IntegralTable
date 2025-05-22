using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntegralTablesModel
{
    public class Document
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Font DefaultFont { get; set; }
        public Color ForeColor { get; set; }
        public Color BackgroundColor { get; set; }

        public List<Change> Changes { get; set; }
        public Properties Properties { get; set; }

        public Document() {
            Name = "";
            Text = "";
            DefaultFont = new Font("Arial", 12);
            ForeColor = new Color();
            BackgroundColor = new Color();
            Changes = new List<Change>();
            Properties = new Properties();
        }
        public Document(string name, string text, Font defaultFont, Color foreColor, Color backgroundColor, List<Change> changes) {
            this.Name = name;
            this.Text = text;
            this.DefaultFont = defaultFont;
            this.ForeColor = foreColor;
            this.BackgroundColor = backgroundColor;
            this.Changes = changes;
        }
    }
}

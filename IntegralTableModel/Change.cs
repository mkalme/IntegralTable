using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IntegralTablesModel
{
    public class Change
    {
        public int StartIndex { get; set; }
        public int Characters { get; set; }

        public Font Font { get; set; }
        public Color ForeColor { get; set; }
        public Color BackgroundColor { get; set; }

        public Change() {
            StartIndex = 0;
            Characters = 0;
            Font = new Font("Arial", 12);
            ForeColor = new Color();
            BackgroundColor = new Color();
        }
        public Change(int startIndex, int characters, Font font, Color foreColor, Color backgroundColor) {
            this.StartIndex = startIndex;
            this.Characters = characters;
            this.Font = font;
            this.ForeColor = foreColor;
            this.BackgroundColor = backgroundColor;
        }
    }
}

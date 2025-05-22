
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralTablesModel
{
    public class Password
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string MainPassword { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public Entry Entry1 { get; set; }
        public Entry Entry2 { get; set; }

        public bool EditingEnabled { get; set; }
        public Properties Properties { get; set; }

        public Password() {
            Name = "";
            Website = "";
            MainPassword = "";
            Email = "";
            Notes = "";

            Entry1 = new Entry();
            Entry2 = new Entry();

            EditingEnabled = true;
            Properties = new Properties();
        }
        public Password(string name, string website, string mainPassword, string email, string notes, Entry entry1, Entry entry2, bool editingEnabled) {
            this.Name = name;
            this.Website = website;
            this.MainPassword = mainPassword;
            this.Email = email;
            this.Notes = notes;

            this.Entry1 = entry1;
            this.Entry2 = entry2;

            this.EditingEnabled = editingEnabled;
        }
    }
}

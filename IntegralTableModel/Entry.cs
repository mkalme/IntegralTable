using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralTablesModel
{
    public class Entry
    {
        public string HeaderName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Entry() {
            HeaderName = "Name";
            Name = "";
            Password = "";
        }
        public Entry(string headerName, string name, string password) {
            this.HeaderName = headerName;
            this.Name = name;
            this.Password = password;
        }
    }
}

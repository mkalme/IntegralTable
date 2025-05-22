using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralTablesModel
{
    public class Group
    {
        public string Name { get; set; }
        public Properties Properties { get; set; }

        public Group() {
            Name = "";
            Properties = new Properties();
        }

        public Group(string name, Properties properties) {
            this.Name = name;
            this.Properties = properties;
        }
    }
}

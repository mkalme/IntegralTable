using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralTablesModel
{
    public class Properties
    {
        public EntityType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public DateTime LastPasswordChange { get; set; }
        public int Size { get; set; }
        public string XmlPath { get; set; }

        public Properties() {
            Type = EntityType.Group;
            CreationDate = new DateTime();
            LastModificationDate = new DateTime();
            LastPasswordChange = new DateTime();
            Size = 0;
            XmlPath = "";
        }
        public Properties(EntityType type, DateTime creationDate, DateTime lastModificationDate, DateTime lastPasswordChange, int size, string xmlPath) {
            this.Type = type;
            this.CreationDate = creationDate;
            this.LastModificationDate = lastModificationDate;
            this.LastPasswordChange = lastPasswordChange;
            this.Size = size;
            this.XmlPath = xmlPath;
        }
    }
}

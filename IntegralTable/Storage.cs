using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegralTablesModel;

namespace IntegralTable
{
    class Storage
    {
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IntegralTable\Storage";
        public static string FilePath = Path + @"\Storage.xml";

        public static Profile Profile = new Profile();
    }
}

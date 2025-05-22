using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegralTablesModel
{
    public class Profile
    {
        public List<string> Path { get; set; }
        public int CurrentIndex { get; set; }

        public Dictionary<string, Group> Groups { get; set; }
        public Dictionary<string, Password> Passwords { get; set; }
        public Dictionary<string, Document> Documents { get; set; }

        public List<List<string>> SelectedPaths { get; set; }

        public Profile() {
            Path = new List<string>();
            CurrentIndex = 0;

            Groups = new Dictionary<string, Group>();
            Passwords = new Dictionary<string, Password>();
            Documents = new Dictionary<string, Document>();

            SelectedPaths = new List<List<string>>() { new List<string>() {"", ""} };
        }
        public Profile(List<string> path, int currentIndex, Dictionary<string, Group> groups, Dictionary<string,
            Password> passwords, Dictionary<string, Document> documents, List<List<string>> selectedPaths) {

            this.Path = path;
            this.CurrentIndex = currentIndex;
            this.Groups = groups;
            this.Passwords = passwords;
            this.Documents = documents;
            this.SelectedPaths = selectedPaths;
        }
    }
}

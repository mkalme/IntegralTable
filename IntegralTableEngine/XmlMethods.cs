using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Drawing;
using IntegralTablesModel;

namespace IntegralTableEngine
{
    public class XmlMethods
    {
        private static XmlDocument Document = new XmlDocument();
        private static string FilePath = "";

        //Load & Save
        public static void LoadXmlDocument(string filePath) {
            Document.LoadXml(File.ReadAllText(filePath));
            FilePath = filePath;
        }
        public static void SaveXmlDocument() {
            Document.Save(FilePath);
        }

        //Get all entities
        public static Dictionary<string, Group> GetAllGroups(string path) {
            string xmlPath = PathToXml(path);
            XmlNodeList allNodes = Document.SelectSingleNode(xmlPath).SelectNodes("entity[@type=\"group\"]");

            Dictionary<string, Group> allGroups = new Dictionary<string, Group>();
            for (int i = 0; i < allNodes.Count; i++) {
                Group group = GetGroup(allNodes[i], path);
                allGroups.Add(group.Properties.XmlPath, group);
            }

            return allGroups;
        }
        public static Dictionary<string, Password> GetAllPasswords(string path){
            string xmlPath = PathToXml(path);
            XmlNodeList allNodes = Document.SelectSingleNode(xmlPath).SelectNodes("entity[@type=\"password\"]");

            Dictionary<string, Password> allPasswords = new Dictionary<string, Password>();
            for (int i = 0; i < allNodes.Count; i++)
            {
                Password password = GetPassword(allNodes[i], path);
                allPasswords.Add(password.Properties.XmlPath, password);
            }

            return allPasswords;
        }
        public static Dictionary<string, Document> GetAllDocuments(string path){
            string xmlPath = PathToXml(path);
            XmlNodeList allNodes = Document.SelectSingleNode(xmlPath).SelectNodes("entity[@type=\"document\"]");

            Dictionary<string, Document> allDocuments = new Dictionary<string, Document>();
            for (int i = 0; i < allNodes.Count; i++)
            {
                Document document = GetDocument(allNodes[i], path);
                allDocuments.Add(document.Properties.XmlPath, document);
            }

            return allDocuments;
        }

        private static Group GetGroup(XmlNode node, string path) {
            Group group = new Group
            {
                Name = node.Attributes["name"].Value,
                Properties = GetProperties(node.SelectSingleNode("properties"), AddToPath(path, node.Attributes["name"].Value))
            };

            return group;
        }
        private static Password GetPassword(XmlNode node, string path) {
            Password password = new Password
            {
                Name = node.Attributes["name"].Value,
                Website = node.SelectSingleNode("website").InnerText,
                MainPassword = node.SelectSingleNode("main-password").InnerText,
                Email = node.SelectSingleNode("email").InnerText,
                Notes = System.Text.RegularExpressions.Regex.Unescape(node.SelectSingleNode("notes").InnerText),
                Entry1 = GetEntry(node.SelectSingleNode("entry1")),
                Entry2 = GetEntry(node.SelectSingleNode("entry2")),
                EditingEnabled = bool.Parse(node.SelectSingleNode("editing-enabled").Attributes["value"].Value),
                Properties = GetProperties(node.SelectSingleNode("properties"), AddToPath(path, node.Attributes["name"].Value))
            };

            return password;
        }
        private static Document GetDocument(XmlNode node, string path) {
            Document document = new Document
            {
                Name = node.Attributes["name"].Value,
                Text = System.Text.RegularExpressions.Regex.Unescape(node.SelectSingleNode("text").InnerText),
                DefaultFont = GetFont(node.SelectSingleNode("default-font")),
                ForeColor = ColorTranslator.FromHtml(node.SelectSingleNode("fore-color").Attributes["hex"].Value),
                BackgroundColor = ColorTranslator.FromHtml(node.SelectSingleNode("background-color").Attributes["hex"].Value),
                Properties = GetProperties(node.SelectSingleNode("properties"), AddToPath(path, node.Attributes["name"].Value))
            };

            XmlNodeList changeNodes = node.SelectNodes("change");
            for (int i = 0; i < changeNodes.Count; i++) {
                document.Changes.Add(GetChange(changeNodes[i]));
            }

            return document;
        }

        private static Font GetFont(XmlNode node) {
            //Font-Family
            string fontFamily = node.SelectSingleNode("font-family").InnerText;

            //Size
            int size = Int32.Parse(node.SelectSingleNode("size").InnerText);

            //Font-Style
            string[] styles = node.SelectSingleNode("font-style").InnerText.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            FontStyle fontStyles = FontStyle.Regular;
            for (int i = 0; i < styles.Length; i++) {
                switch (styles[i])
                {
                    case "regular":
                        fontStyles = fontStyles | FontStyle.Regular;
                        break;
                    case "bold":
                        fontStyles = fontStyles | FontStyle.Bold;
                        break;
                    case "italic":
                        fontStyles = fontStyles | FontStyle.Italic;
                        break;
                    case "underline":
                        fontStyles = fontStyles | FontStyle.Underline;
                        break;
                    case "strikeout":
                        fontStyles = fontStyles | FontStyle.Strikeout;
                        break;
                    default:

                        break;
                }
            }

            return new Font(fontFamily, size, fontStyles);
        }
        private static Change GetChange(XmlNode node) {
            Change change = new Change
            {
                StartIndex = Int32.Parse(node.Attributes["index"].Value),
                Characters = Int32.Parse(node.Attributes["characters"].Value),
                Font = GetFont(node.SelectSingleNode("font")),
                ForeColor = ColorTranslator.FromHtml(node.SelectSingleNode("fore-color").Attributes["hex"].Value),
                BackgroundColor = ColorTranslator.FromHtml(node.SelectSingleNode("background-color").Attributes["hex"].Value)
            };

            return change;
        }
        private static Entry GetEntry(XmlNode node) {
            Entry entry = new Entry
            {
                HeaderName = node.SelectSingleNode("header-name").InnerText,
                Name = node.SelectSingleNode("name").InnerText,
                Password = node.SelectSingleNode("password").InnerText
            };

            return entry;
        }
        private static Properties GetProperties(XmlNode node, string path) {
            Properties properties = new Properties
            {
                CreationDate = DateTime.FromFileTime(long.Parse(node.SelectSingleNode("creation-date").Attributes["value"].Value.ToString())),
                LastModificationDate = DateTime.FromFileTime(long.Parse(node.SelectSingleNode("last-modification-date").Attributes["value"].Value.ToString())),
                LastPasswordChange = DateTime.FromFileTime(long.Parse(node.SelectSingleNode("last-password-change").Attributes["value"].Value.ToString())),
                XmlPath = path
            };

            return properties;
        }

        //Create
        public static void CreateGroup(Group group, string parentGroup) {
            string parentXmlPath = PathToXml(parentGroup);

            //Get element
            XmlElement groupElement = Document.CreateElement("entity");
            groupElement.SetAttribute("type", "group");
            groupElement.SetAttribute("name", group.Name);

            XmlElement propertiesElement = GetPropertiesElement(group.Properties);

            groupElement.AppendChild(propertiesElement);

            //Add Element
            Document.SelectSingleNode(parentXmlPath).AppendChild(groupElement);

            //Save
            SaveXmlDocument();
        }
        public static void CreatePassword(Password password, string parentGroup) {
            string parentXmlPath = PathToXml(parentGroup);

            //Create element
            XmlElement passwordElement = Document.CreateElement("entity");
            passwordElement.SetAttribute("type", "password");
            passwordElement.SetAttribute("name", password.Name);

            XmlElement websiteElement = Document.CreateElement("website");
            websiteElement.InnerText = password.Website;

            XmlElement mainPasswordElement = Document.CreateElement("main-password");
            mainPasswordElement.InnerText = password.MainPassword;

            XmlElement emailElement = Document.CreateElement("email");
            emailElement.InnerText = password.Email;

            XmlElement notesElement = Document.CreateElement("notes");
            notesElement.InnerText = System.Text.RegularExpressions.Regex.Escape(password.Notes);

            XmlElement entry1Element = GetEntryElement(password.Entry1, "entry1");
            XmlElement entry2Element = GetEntryElement(password.Entry2, "entry2");

            XmlElement editingEnabledElement = Document.CreateElement("editing-enabled");
            editingEnabledElement.SetAttribute("value", password.EditingEnabled.ToString().ToLower());

            XmlElement propertiesElement = GetPropertiesElement(password.Properties);

            passwordElement.AppendChild(mainPasswordElement);
            passwordElement.AppendChild(websiteElement);
            passwordElement.AppendChild(emailElement);
            passwordElement.AppendChild(notesElement);
            passwordElement.AppendChild(entry1Element);
            passwordElement.AppendChild(entry2Element);
            passwordElement.AppendChild(editingEnabledElement);
            passwordElement.AppendChild(propertiesElement);

            //Add element
            Document.SelectSingleNode(parentXmlPath).AppendChild(passwordElement);

            //Save
            SaveXmlDocument();
        }
        public static void CreateDocument(Document document, string parentGroup) {
        }

        private static XmlElement GetEntryElement(Entry entry, string name) {
            XmlElement entryElement = Document.CreateElement(name);

            XmlElement headerNameElement = Document.CreateElement("header-name");
            headerNameElement.InnerText = entry.HeaderName;

            XmlElement nameElement = Document.CreateElement("name");
            nameElement.InnerText = entry.Name;

            XmlElement passwordElement = Document.CreateElement("password");
            passwordElement.InnerText = entry.Password;

            entryElement.AppendChild(headerNameElement);
            entryElement.AppendChild(nameElement);
            entryElement.AppendChild(passwordElement);

            return entryElement;
        }
        private static XmlElement GetPropertiesElement(Properties properties) {
            XmlElement propertiesElement = Document.CreateElement("properties");

            XmlElement creationDateElement = Document.CreateElement("creation-date");
            creationDateElement.SetAttribute("value", properties.CreationDate.ToFileTime().ToString());

            XmlElement lastModificationDate = Document.CreateElement("last-modification-date");
            lastModificationDate.SetAttribute("value", properties.LastModificationDate.ToFileTime().ToString());

            XmlElement lastPasswordChangeElement = Document.CreateElement("last-password-change");
            lastPasswordChangeElement.SetAttribute("value", properties.LastPasswordChange.ToFileTime().ToString());

            propertiesElement.AppendChild(creationDateElement);
            propertiesElement.AppendChild(lastModificationDate);
            propertiesElement.AppendChild(lastPasswordChangeElement);

            return propertiesElement;
        }

        //Edit
        public static void EditGroup(Group group) {
            XmlNode node = Document.SelectSingleNode(group.Properties.XmlPath);

            //Edit
            node.Attributes["name"].Value = group.Name;
            EditProperties(group.Properties, node.SelectSingleNode("properties"));

            //Save
            SaveXmlDocument();
        }
        public static void EditPassword(Password password) {
            string xmlPath = PathToXml(password.Properties.XmlPath);
            XmlNode node = Document.SelectSingleNode(xmlPath);

            //Edit
            node.Attributes["name"].Value = password.Name;
            node.SelectSingleNode("website").InnerText = password.Website;
            node.SelectSingleNode("main-password").InnerText = password.MainPassword;
            node.SelectSingleNode("email").InnerText = password.Email;
            node.SelectSingleNode("notes").InnerText = System.Text.RegularExpressions.Regex.Escape(password.Notes);

            EditEntry(password.Entry1, node.SelectSingleNode("entry1"));
            EditEntry(password.Entry2, node.SelectSingleNode("entry2"));

            node.SelectSingleNode("editing-enabled").Attributes["value"].Value = password.EditingEnabled.ToString().ToLower();

            EditProperties(password.Properties, node.SelectSingleNode("properties"));

            //Save
            SaveXmlDocument();
        }
        public static void EditDocument(Document document) {

        }

        private static void EditEntry(Entry entry, XmlNode node) {
            node.SelectSingleNode("header-name").InnerText = entry.HeaderName;
            node.SelectSingleNode("name").InnerText = entry.Name;
            node.SelectSingleNode("password").InnerText = entry.Password;
        }
        private static void EditProperties(Properties properties, XmlNode node) {
            node.SelectSingleNode("creation-date").Attributes["value"].Value = properties.CreationDate.ToFileTime().ToString();
            node.SelectSingleNode("last-modification-date").Attributes["value"].Value = properties.LastModificationDate.ToFileTime().ToString();
            node.SelectSingleNode("last-password-change").Attributes["value"].Value = properties.LastPasswordChange.ToFileTime().ToString();
        }

        //Rename
        public static void Rename(string path, string name) {
            string xmlPath = PathToXml(path);

            //Rename
            Document.SelectSingleNode(xmlPath).Attributes["name"].Value = name;

            //Save
            SaveXmlDocument();
        }

        //Delete
        public static void Delete(string path) {
            string xmlPath = PathToXml(path);

            //Delete
            Document.SelectSingleNode(xmlPath).ParentNode.RemoveChild(Document.SelectSingleNode(xmlPath));

            //Save
            SaveXmlDocument();
        }

        //Scripts
        private static string PathToXml(string path) {
            string xmlPath = "/manager";

            string[] split = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < split.Length; i++) {
                xmlPath += "/entity[@name=\"" + split[i] + "\"]";
            }

            return xmlPath;
        }
        public static EntityType GetEntityType(string path) {
            string xmlPath = PathToXml(path);

            string type = Document.SelectSingleNode(xmlPath).Attributes["type"].Value;
            if (type == "group"){
                return EntityType.Group;
            }else if (type == "password"){
                return EntityType.Password;
            }else if(type == "document"){
                return EntityType.Document;
            }

            return EntityType.Group;
        }
        private static string AddToPath(string path, string value) {
            if (string.IsNullOrEmpty(path.Trim())){
                return value;
            }else {
                return path + "/" + value;
            }
        }
    }
}

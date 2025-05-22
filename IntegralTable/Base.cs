using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntegralTableEngine;
using IntegralTablesModel;
using System.Diagnostics;

namespace IntegralTable
{
    public partial class Base : Form
    {
        public Base()
        {
            InitializeComponent();

            contextMenuStrip1.Renderer = new MyRenderer();
        }

        private void Base_Load(object sender, EventArgs e)
        {
            SetStyle();

            Load_Base();

            ArrowTimer.Start();
        }

        //Set
        private void SetStyle(){
            //DataGridView
            dataGridView.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;

            dataGridView.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

            //Button
            leftArrowButton.FlatAppearance.BorderColor = BackColor;
            rightArrowButton.FlatAppearance.BorderColor = BackColor;

            refreshButton.FlatAppearance.BorderColor = BackColor;
        }

        //Load Base
        public void Load_Base() {
            XmlMethods.LoadXmlDocument(Storage.FilePath);

            Storage.Profile.Groups = XmlMethods.GetAllGroups(ListToPath());
            Storage.Profile.Passwords = XmlMethods.GetAllPasswords(ListToPath());
            Storage.Profile.Documents = XmlMethods.GetAllDocuments(ListToPath());

            //Load DataGridView
            CanUpdate = false;
            dataGridView.Rows.Clear();

            //Group
            for (int i = 0; i < Storage.Profile.Groups.Count; i++) {
                Group group = Storage.Profile.Groups.ElementAt(i).Value;

                dataGridView.Rows.Add(
                    group.Properties.XmlPath,
                    Properties.Resources.Group, 
                    group.Name,
                    "",
                    "",
                    "",
                    GetTimeString(group.Properties.LastModificationDate));
            }
            //Password
            for (int i = 0; i < Storage.Profile.Passwords.Count; i++){
                Password password = Storage.Profile.Passwords.ElementAt(i).Value;

                dataGridView.Rows.Add(
                    password.Properties.XmlPath,
                    Properties.Resources.Password,
                    password.Name,
                    password.Website,
                    password.MainPassword.Length.ToString() + " Length",
                    password.Email,
                    GetTimeString(password.Properties.LastPasswordChange));
            }

            //Document
            for (int i = 0; i < Storage.Profile.Documents.Count; i++)
            {
                Document document = Storage.Profile.Documents.ElementAt(i).Value;

                dataGridView.Rows.Add(
                    document.Properties.XmlPath,
                    Properties.Resources.Document,
                    document.Name,
                    "",
                    "",
                    "",
                    GetTimeString(document.Properties.LastModificationDate));
            }

            dataGridView.ClearSelection();

            SelectRowBasedOnValue(Storage.Profile.SelectedPaths[Storage.Profile.CurrentIndex][1]);
            CanUpdate = true;

            //TextBox
            pathTextBox.Text = ListToTextBoxPath();
        }

        private void SelectRowBasedOnValue(string value) {
            for (int i = 0; i < dataGridView.Rows.Count; i++) {
                if (dataGridView.Rows[i].Cells[0].Value.ToString() == value) {
                    dataGridView.Rows[i].Selected = true;

                    i = dataGridView.Rows.Count;
                }
            }
        }

        private string GetTimeString(DateTime time) {
            if (time == DateTime.MaxValue) {
                return "Never";
            } else if (time == DateTime.MinValue) {
                return "";
            }

            TimeSpan timeSpan = DateTime.Now - time;
            double seconds = timeSpan.TotalSeconds;

            if (seconds > -1 && seconds < 60) { //Seconds
                if (seconds == 1){
                    return (int)seconds + " second ago";
                }else {
                    return (int)seconds + " seconds ago";
                }
            } else if (seconds > 59 && seconds < 3600) { //Minutes
                if (seconds < 120){
                    return (int)(seconds / 60) + " minute ago";
                }else{
                    return (int)(seconds / 60) + " minutes ago";
                }
            } else if (seconds > 3599 && seconds < 86400) { //Hours
                if (seconds < 7200){
                    return (int)(seconds / 3600) + " hour ago";
                }else{
                    return (int)(seconds / 3600) + " hours ago";
                }
            } else{ //Days
                if (seconds < 172800){
                    return (int)(seconds / 86400) + " day ago";
                }else{
                    return (int)(seconds / 86400) + " days ago";
                }
            }
        }
        private string ListToTextBoxPath() {
            return " " + String.Join(" > ", Storage.Profile.Path);
        }

        //Scripts
        public string ListToPath() {
            return String.Join("/", Storage.Profile.Path);
        }
        private string SelectedPath() {
            if (dataGridView.SelectedRows.Count > 0){
                return dataGridView.SelectedRows[0].Cells[0].Value.ToString();
            }else {
                return "";
            }
        }

        private void OpenEntity(string selectedPath) {
            EntityType entityType = XmlMethods.GetEntityType(selectedPath);

            if (entityType == EntityType.Group){
                Storage.Profile.Path.Add(Storage.Profile.Groups[selectedPath].Name);
                Storage.Profile.CurrentIndex++;

                //Selected Paths
                if (Storage.Profile.SelectedPaths.Count - 1 < Storage.Profile.CurrentIndex){ //If not created
                    Storage.Profile.SelectedPaths.Add(new List<string> { Storage.Profile.Groups[selectedPath].Properties.XmlPath, "" });
                }else if (Storage.Profile.SelectedPaths[Storage.Profile.CurrentIndex][0] != Storage.Profile.Groups[selectedPath].Properties.XmlPath){ //If different path

                    Storage.Profile.SelectedPaths.RemoveRange(Storage.Profile.CurrentIndex, Storage.Profile.SelectedPaths.Count - Storage.Profile.CurrentIndex);
                    Storage.Profile.SelectedPaths.Add(new List<string> { Storage.Profile.Groups[selectedPath].Properties.XmlPath, "" });
                }

                Load_Base();
            }else if (entityType == EntityType.Password){
                PasswordForm passwordForm = new PasswordForm(this, Storage.Profile.Passwords[selectedPath], true);
                passwordForm.ShowDialog();
            }else if (entityType == EntityType.Document){
                DocumentForm documentForm = new DocumentForm(this, Storage.Profile.Documents[selectedPath], true);
                documentForm.ShowDialog();
            }
        }

        //DataGridView Action
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e){
            if (dataGridView.SelectedRows.Count > 0) {
                OpenEntity(SelectedPath());
            }
        }
        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dataGridView.HitTest(e.X, e.Y);
            if (hit.Type != DataGridViewHitTestType.Cell)
            {
                dataGridView.ClearSelection();
            }
        }

        private static bool CanUpdate = true;
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CanUpdate) {
                if (dataGridView.SelectedRows.Count > 0) {
                    Storage.Profile.SelectedPaths[Storage.Profile.CurrentIndex][1] = SelectedPath();
                }else {
                    Storage.Profile.SelectedPaths[Storage.Profile.CurrentIndex][1] = "";
                }
            }
        }

        //Refresh Button
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            Load_Base();
        }

        //Left & Right Buttons
        private void LeftArrowButton_Click(object sender, EventArgs e)
        {
            if (Storage.Profile.Path.Count > 0) {
                Storage.Profile.Path.RemoveAt(Storage.Profile.Path.Count - 1);
                Storage.Profile.CurrentIndex--;

                Load_Base();
            }
        }
        private void RightArrowButton_Click(object sender, EventArgs e)
        {
            if (Storage.Profile.CurrentIndex < Storage.Profile.SelectedPaths.Count - 1) {
                Storage.Profile.Path.Add(Storage.Profile.SelectedPaths[Storage.Profile.CurrentIndex + 1][0]);
                Storage.Profile.CurrentIndex++;

                Load_Base();
            }
        }

        //Timer
        private void ArrowTimer_Tick(object sender, EventArgs e)
        {
            //Left Button
            if (Storage.Profile.Path.Count > 0){
                leftArrowButton.Enabled = true;
            }else {
                leftArrowButton.Enabled = false;
            }

            //Right Button
            if (Storage.Profile.CurrentIndex < Storage.Profile.SelectedPaths.Count - 1){
                rightArrowButton.Enabled = true;
            }else {
                rightArrowButton.Enabled = false;
            }
        }

        //Context Menu Strip
        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string selectedPath = SelectedPath();

            ToolStripItem[] menuStripItems = {openToolStripMenuItem, addToolStripMenuItem, renameToolStripMenuItem, deleteToolStripMenuItem,
                                              groupToolStripMenuItem, passwordToolStripMenuItem, documentToolStripMenuItem};

            bool[] enabled = {true, true, true, true, true, true, true};

            if (string.IsNullOrEmpty(selectedPath)) {
                enabled = new bool[] { false, true, false, false, true, true, true };
            }

            //Set
            for (int i = 0; i < menuStripItems.Length; i++) {
                menuStripItems[i].Enabled = enabled[i];
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e){
            OpenEntity(SelectedPath());
        }
        private void RenameToolStripMenuItem_Click(object sender, EventArgs e){
            string selectedPath = SelectedPath();

            EntityType entityType = XmlMethods.GetEntityType(selectedPath);
            string existingName = "";

            if (entityType == EntityType.Group) {
                existingName = Storage.Profile.Groups[selectedPath].Name;
            } else if (entityType == EntityType.Password) {
                existingName = Storage.Profile.Passwords[selectedPath].Name;
            } else if (entityType == EntityType.Document) {
                existingName = Storage.Profile.Documents[selectedPath].Name;
            }

            Rename rename = new Rename(this, entityType, selectedPath, existingName);
            rename.ShowDialog();
        }
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e){
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this?\t\t\t\t\t\t", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes){
                XmlMethods.Delete(SelectedPath());

                Load_Base();
            }
        }

        private void GroupToolStripMenuItem_Click(object sender, EventArgs e){
            GroupForm groupForm = new GroupForm(this, new Group(), false);
            groupForm.ShowDialog();
        }
        private void PasswordToolStripMenuItem_Click(object sender, EventArgs e){
            PasswordForm passwordForm = new PasswordForm(this, new Password(), false);
            passwordForm.ShowDialog();
        }
        private void DocumentToolStripMenuItem_Click(object sender, EventArgs e){
            DocumentForm documentForm = new DocumentForm(this, new Document(), false);
            documentForm.ShowDialog();
        }

        private class MyRenderer : ToolStripProfessionalRenderer{
            public MyRenderer() : base(new MyColors()) { }
        }
        private class MyColors : ProfessionalColorTable{
            public override Color MenuItemSelected{
                get { return Color.Gray; }
            }

            public override Color MenuItemBorder{
                get { return Color.Gray; }
            }
        }
    }
}

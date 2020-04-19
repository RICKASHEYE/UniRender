using System;
using System.Windows.Forms;
using System.IO;

namespace SubrightEngineEditor
{
    public partial class FileSave : Form
    {
        public static string startingDir = "C:/";
        public static string backDir;

        string toUSE_;

        public FileSave(string toUse)
        {
            toUSE_ = toUse;
            InitializeComponent();
            LoadDialog();
        }

        public void LoadDialog()
        {
            //Load the Dialog
            listBox1.Items.Clear();
            string[] dirs = Directory.GetDirectories(startingDir);
            foreach(string m in dirs)
            {
                listBox1.Items.Add(m);
            }
            textBox1.Text = startingDir;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Use masturbation techniques ;)
            Main.destinationSave = startingDir;
            this.Text = "UniRender - " + Main.destinationSave;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Back
            startingDir = backDir;
            LoadDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //New Directory
            string newFolderName = "";
            if (!Directory.Exists(textBox1.Text))
            {
                //Create a new folder name as the new folder name
                newFolderName = "New Folder";
            }
            else
            {
                //Create a new folder from the name in the text box
                newFolderName = Path.GetDirectoryName(textBox1.Text);
            }
            LoadDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Directory.Delete(listBox1.SelectedItem.ToString(), true);
            LoadDialog();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            backDir = startingDir;
            startingDir = listBox1.SelectedItem.ToString();
            LoadDialog();
        }
    }
}

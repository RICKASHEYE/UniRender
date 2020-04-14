using GameEngine;
using GameEngineUtil;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GameEngineEditor
{
    public partial class Main : Form
    {
        public static Bitmap canvas;
        public static string destinationSave = "null";

        public Main()
        {
            InitializeComponent();
            //LoadToolbox();
            this.Text = "UniRender - " + destinationSave;
            canvas = new Bitmap(Width, Height);
            GameEngine.Axis.AssignNewKey(new KeyCode("Enter", Keys.Enter, Keys.Enter));
            MainGame.Run();
            Update();
            SecondaryLoadToolbox(true);
        }

        //Dont know if i wanted to reload the toolbox like this just yet
        public void SecondaryLoadToolbox(bool refresh)
        {
            try
            {
                if (refresh == true) { gameObjectBox.Items.Clear(); componentBox.Items.Clear(); }
                LoadGameObjects();
                LoadComponents();
            }
            catch(Exception e)
            {
                Debug.Error("Error when loading toolbox!");
                Debug.Error(e.Message);
            }
        }

        public void LoadGameObjects()
        {
            if (GameObjectHandler.gameObjects != null)
            {
                gameObjectBox.Items.Clear();
                foreach (GameObject gameObjects in GameObjectHandler.gameObjects)
                {
                    gameObjectBox.Items.Add(gameObjects.name_);
                }
            }
            else
            {
                Debug.Error("No GameObjects to list!");
            }
        }

        public void LoadComponents()
        {
            if (gameObjectBox.SelectedItem != null && GameObjectHandler.gameObjects != null)
            {
                componentBox.Items.Clear();
                GameObject objectNew = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
                componentBox.Items.Add("Position: " + objectNew.X + " " + objectNew.Y);
                foreach (Component coms in objectNew.components)
                {
                    componentBox.Items.Add(coms.name);
                }
            }
            else
            {
                Debug.Error("No GameObject Selected or no gameObjects in the handler!!!");
            }
            Update();
        }

        private void gameView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //Painting
                gameView.Refresh();
                Graphics g = e.Graphics;
                g.Clear(System.Drawing.Color.White);
                gameView.Image = canvas;
                /*foreach (Pixel pixel in GameEngine.Canvas.ScreenRender)
                {
                    if (pixel != null)
                    {
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, pixel.color.R, pixel.color.G, pixel.color.B);
                        g.FillRectangle(new SolidBrush(newColor), pixel.X, pixel.Y, 1, 1);
                    }
                }*/
                DrawCanvas();
                //g.Clear(System.Drawing.Color.White);
            }
            catch (System.Exception m)
            {
                Console.WriteLine("Error: " + m);
            }
        }

        public void DrawCanvas()
        {
            //Nothing to draw here yet!!!
            MainGame.Update();
        }

        private void gameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a gameObject
            GameObjectHandler.CreateGameObject("New GameObject");
            SecondaryLoadToolbox(true);
            GameEngine.Debug.Log("Added a new gameobject!");
        }

        private void rendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open or add the component Renderer
            GameObject reference = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
            RectangleRenderer renderer = new RectangleRenderer(new GameEngine.Rectangle(10, 10, reference.X, reference.Y), new GameEngine.Color(255, 255, 255));
            AddComponent(renderer);
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Delete the selected object
            GameObjectHandler.gameObjects.Remove(GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()));
            SecondaryLoadToolbox(true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load the gameobjects components in the component view
            if (gameObjectBox.SelectedItem != null)
            {
                LoadComponents();
            }
        }

        private void animatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Animator animator = new Animator();
            AddComponent(animator);
        }

        public void AddComponent(Component com)
        {
            GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()).RegisterComponent(com);
            GameEngine.Debug.Log("Added a new Component: " + com.name);
            LoadComponents();
        }

        private void boxColliderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoxCollider collider = new BoxCollider();
            AddComponent(collider);
        }

        private void imageRendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageRenderer renderer = new ImageRenderer(new GameEngine.Rectangle(new Vector2(0, 0), new Vector2(0, 0)), null);
            AddComponent(renderer);
        }

        private void gameObjectBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CreateEditBox(new System.Drawing.Point(gameObjectBox.Location.X, gameObjectBox.Location.Y * gameObjectBox.SelectedIndex), new Size(gameObjectBox.Size.Width, 10));
        }

        bool modified = false;

        public void CreateEditBox(System.Drawing.Point location, System.Drawing.Size size)
        {
            TextBox box = new TextBox();
            box.Location = new System.Drawing.Point(gameObjectBox.Location.X, gameObjectBox.Location.Y * gameObjectBox.SelectedIndex);
            box.Size = new System.Drawing.Size(gameObjectBox.Size.Width, 10);
            this.Controls.Add(box);

            while(modified == false)
            {
                if(GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Enter")) == 1)
                {
                    box.Dispose();
                    modified = true;
                }
            }
            modified = false;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void componentBox_DoubleClick(object sender, EventArgs e)
        {
            //Double click this!
            //Delete the component 
            GameObject reference = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
            reference.DeRegisterComponent(reference.GetComponent(componentBox.SelectedItem.ToString()));
            LoadComponents();
        }

        private void componentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            componentInspector.Items.Clear();
            //Change of the index
            //Load the component values into the component inspector!
            Component reference = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()).GetComponent(componentBox.SelectedItem.ToString());
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            var fieldValues = reference.GetType().GetFields(bindingFlags).Select(field => field.GetValue(reference)).ToList();

            foreach (var info in fieldValues)
            {
                if (info != null && info.ToString() != reference.name)
                {
                    componentInspector.Items.Add(info.ToString()); 
                }
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Save the project in a folder
            Save();
        }

        public void saveAs()
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                } 
            }
        }

        public void Save()
        {
            if(destinationSave == "null")
            {
                saveAs();
                Save();
            }
            else
            {
                //Save the files
                string savedString = JsonConvert.SerializeObject(GameObjectHandler.gameObjects);
                Debug.Log("Saved file as: " + savedString);
            }
        }

        private void saveAsProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
            Save();
        }
    }
}

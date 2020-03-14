using GameEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngineEditor
{
    public partial class Main : Form
    {
        public static Bitmap canvas;
        public List<GameObjectNode> nodes = new List<GameObjectNode>();

        public Main()
        {
            InitializeComponent();
            //LoadToolbox();
            canvas = new Bitmap(Width, Height);
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
                foreach (GameEngine.Organisation.GameObject gameObjects in GameObjectHandler.gameObjects)
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
                GameEngine.Organisation.GameObject objectNew = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
                componentBox.Items.Add("Position: " + objectNew.X + " " + objectNew.Y);
                foreach (GameEngine.Organisation.Component coms in objectNew.components)
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
                foreach (Pixel pixel in GameEngine.Canvas.ScreenRender)
                {
                    if (pixel != null)
                    {
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, pixel.color.R, pixel.color.G, pixel.color.B);
                        g.FillRectangle(new SolidBrush(newColor), pixel.X, pixel.Y, 1, 1);
                    }
                }
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
            GameEngine.Organisation.GameObject reference = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
            GameEngine.Organisation.RectangleRenderer renderer = new GameEngine.Organisation.RectangleRenderer(new GameEngine.Rectangle(10, 10, reference.X, reference.Y), new GameEngine.Color(255, 255, 255));
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
            GameEngine.Organisation.Animator animator = new GameEngine.Organisation.Animator();
            AddComponent(animator);
        }

        public void AddComponent(GameEngine.Organisation.Component com)
        {
            GameEngine.GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()).RegisterComponent(com);
            GameEngine.Debug.Log("Added a new Component: " + com.name);
            LoadComponents();
        }

        private void boxColliderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameEngine.Organisation.BoxCollider collider = new GameEngine.Organisation.BoxCollider();
            AddComponent(collider);
        }

        private void imageRendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameEngine.Organisation.ImageRenderer renderer = new GameEngine.Organisation.ImageRenderer(new GameEngine.Rectangle(new Vector2(0, 0), new Vector2(0, 0)), null);
            AddComponent(renderer);
        }

        private void gameObjectBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditProperty property = new EditProperty(GameEngine.GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()));
            property.ShowDialog();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void componentBox_DoubleClick(object sender, EventArgs e)
        {
            //Double click this!
            //Delete the component 
            GameEngine.Organisation.GameObject reference = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
            reference.DeRegisterComponent(reference.GetComponent(componentBox.SelectedItem.ToString()));
            LoadComponents();
        }

        private void componentBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            componentInspector.Items.Clear();
            //Change of the index
            //Load the component values into the component inspector!
            GameEngine.Organisation.Component reference = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()).GetComponent(componentBox.SelectedItem.ToString());
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
    }
}

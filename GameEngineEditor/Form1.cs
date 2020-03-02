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
    public partial class Form1 : Form
    {
        public static Bitmap canvas;
        public List<GameObjectNode> nodes = new List<GameObjectNode>();

        public Form1()
        {
            InitializeComponent();
            //LoadToolbox();
            canvas = new Bitmap(Width, Height);
            Update();
            SecondaryLoadToolbox();
        }

        /*public void LoadToolbox()
        {
            try
            {
                toolboxHireachy.Nodes.Clear();
                GameEngine.Debug.Log("Cleared Toolbox!");
                foreach (GameEngine.Organisation.GameObject objects in GameObjectHandler.gameObjects)
                {
                    GameEngine.Debug.Log("Loaded: " + objects.name_);
                    GameObjectNode gameObjectNode = new GameObjectNode(objects.name_);
                    gameObjectNode.objectParent = objects;
                    if (objects.components.Count >= 0 && objects.components != null)
                    {
                        foreach (GameEngine.Organisation.Component component in objects.components)
                        {
                            ComponentNode componentNode = new ComponentNode(component.name);
                            componentNode.com = component;
                            gameObjectNode.ComponentNodes.Add(componentNode);
                        }
                    }
                    nodes.Add(gameObjectNode);
                }

                //Pull out and display the gameobjects with components
                foreach (GameObjectNode node in nodes)
                {
                    TreeNode node_ = new TreeNode();
                    node_.Text = node.name;
                    GameEngine.Debug.Log("Creating a node: " + node_.Text);
                    foreach (ComponentNode nodeComponent in node.ComponentNodes)
                    {
                        TreeNode nodeCom = new TreeNode();
                        nodeCom.Text = nodeComponent.name;
                        node_.Nodes.Add(node_);
                        var fieldValues = nodeComponent.GetType().GetFields().Select(field => field.GetValue(nodeComponent)).ToList();
                        foreach (FieldInfo info in fieldValues)
                        {
                            TreeNode variableName = new TreeNode();
                            variableName.Text = info.Name;
                            nodeCom.Nodes.Add(variableName);
                        }
                    }
                    toolboxHireachy.Nodes.Add(node_);
                    GameEngine.Debug.Log("Added a node: " + node_.Text);
                }
                Update();
                GameEngine.Debug.Log("FINISHED: updated all toolbox references etc!!!");
            }
            catch (Exception d)
            {
                GameEngine.Debug.Error("Error when loading toolbox!");
                GameEngine.Debug.Error(d.Message);
            }
        }*/

        //Dont know if i wanted to reload the toolbox like this just yet
        public void SecondaryLoadToolbox()
        {
            try
            {
                if (GameObjectHandler.gameObjects != null)
                {
                    listBox1.Items.Clear();
                    if (currentGameObject == null)
                    {
                        foreach (GameEngine.Organisation.GameObject gameObjects in GameObjectHandler.gameObjects)
                        {
                            listBox1.Items.Add(gameObjects);
                        }
                    }
                    else
                    {
                        foreach (GameEngine.Organisation.Component coms in currentGameObject.components)
                        {
                            listBox1.Items.Add(coms);
                        }
                    }
                }
                else
                {
                    Debug.Error("No GameObjects to list!");
                }
            }catch(Exception e)
            {
                Debug.Error("Error when loading toolbox!");
                Debug.Error(e.Message);
            }
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
        }

        private void gameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a gameObject
            GameObjectHandler.CreateGameObject("New GameObject");
            SecondaryLoadToolbox();
            GameEngine.Debug.Log("Added a new gameobject!");
        }

        private void rendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open or add the component Renderer
            if (currentGameObject != null)
            {
                ComponentHandler.AddComponent(new GameEngine.Organisation.RectangleRenderer(new GameEngine.Rectangle(10, 10, 10, 10), new GameEngine.Color(255, 255, 255)), currentGameObject);
            }
            else
            {
                Debug.Error("No Object selected, may be in component selection!");
            }
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*TreeNode nodeSelected = toolboxHireachy.SelectedNode;
            foreach(GameObjectNode node in nodes)
            {
                if(node.name == nodeSelected.Text)
                {
                    //Delete!!! obv if there isnt any under this name it will skip it
                    nodes.Remove(node);
                }
            }*/

            //foreach(ComponentNode node in )
        }

        public void LoadGameObjects()
        {
            foreach (GameEngine.Organisation.GameObject objectRun in GameObjectHandler.gameObjects)
            {
                //Load them
                listBox1.Items.Add(objectRun);
            }
        }

        public GameEngine.Organisation.GameObject currentGameObject;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load the component menu and others
            //Beggining to press backwards
            if(currentGameObject == null)
            {
                //Load the main gameObjects
            }
        }
    }
}

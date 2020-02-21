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
        public List<GameEngine.Organisation.GameObject> gameObjects = new List<GameEngine.Organisation.GameObject>();
        public List<GameObjectNode> nodes = new List<GameObjectNode>();

        public Form1()
        {
            InitializeComponent();
            //LoadToolbox();
            canvas = new Bitmap(Width, Height);
            Update();
        }

        public void LoadToolbox()
        {
            toolboxHireachy.Nodes.Clear();
            foreach(GameEngine.Organisation.GameObject objects in gameObjects)
            {
                Console.WriteLine("Loaded: " + objects.name_);
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
            foreach(GameObjectNode node in nodes)
            {
                TreeNode node_ = new TreeNode();
                node_.Text = node.name;
                if (node.ComponentNodes.Count >= 0 && node.ComponentNodes != null)
                {
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
                }
                toolboxHireachy.Nodes.Add(node_);
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
        }

        private void gameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a gameObject
            gameObjects.Add(new GameEngine.Organisation.GameObject("New GameObject"));
            LoadToolbox();
        }

        private void rendererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open or add the component Renderer
            AddComponent(new GameEngine.Organisation.RectangleRenderer(new GameEngine.Rectangle(10, 10, 10, 10), new GameEngine.Color(255, 255, 255)));
        }

        public void AddComponent(GameEngine.Organisation.Component com)
        {
            //Add a component to this
            //first of all find the gameobject node of selected
            TreeNode selected = toolboxHireachy.SelectedNode;
            foreach(GameObjectNode node in nodes)
            {
                if(node.name == selected.Text)
                {
                    //Is a gameobject
                    node.objectParent.RegisterComponent(com);
                }
            }
            LoadToolbox();
        }
    }
}

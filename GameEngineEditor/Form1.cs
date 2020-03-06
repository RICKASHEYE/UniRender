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
            MainGame.Run();
            Update();
            SecondaryLoadToolbox(true);
        }

        //Dont know if i wanted to reload the toolbox like this just yet
        public void SecondaryLoadToolbox(bool refresh)
        {
            try
            {
                if (GameObjectHandler.gameObjects != null)
                {
                    if (refresh == true) { gameObjectBox.Items.Clear(); }
                    foreach (GameEngine.Organisation.GameObject gameObjects in GameObjectHandler.gameObjects)
                    {
                        gameObjectBox.Items.Add(gameObjects.name_);
                    }

                    if(gameObjectBox.SelectedItem != null)
                    {
                        componentBox.Items.Clear();
                        GameEngine.Organisation.GameObject objectNew = GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString());
                        componentBox.Items.Add("Position: " + objectNew.X + " " + objectNew.Y);
                        foreach (GameEngine.Organisation.Component coms in objectNew.components)
                        {
                            componentBox.Items.Add(coms.name);
                        }
                    }
                    Update();
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
            ComponentHandler.AddComponent(new GameEngine.Organisation.RectangleRenderer(new GameEngine.Rectangle(10, 10, 10, 10), new GameEngine.Color(255, 255, 255)), GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()));
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Delete the selected object
            GameObjectHandler.gameObjects.Remove(GameObjectHandler.getGameObject(gameObjectBox.SelectedItem.ToString()));
            SecondaryLoadToolbox(true);
        }

        int previouslySelected = 0;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load the gameobjects components in the component view
            if (gameObjectBox.SelectedItem != null)
            {
                previouslySelected = gameObjectBox.SelectedIndex;
                SecondaryLoadToolbox(true);
                gameObjectBox.SelectedIndex = previouslySelected;
            }
        }
    }
}

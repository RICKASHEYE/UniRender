using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SubrightEngine;

namespace DungeonCrawlerDemo
{
    public partial class Form1 : Form
    {
        public static Bitmap canvas;

        public Form1()
        {
            InitializeComponent();
            Axis.AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            Axis.AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            Axis.AssignNewKey(new KeyCode("Place", Keys.MButton));
            //GameEngine.Font.SetupFont();
            canvas = new Bitmap(Width, Height);
            playerPosition = new Vector2(Width / 2, Height / 2);
            Debug.Log("Initialised!");
        }

        public static Vector2 playerPosition = Vector2.zero;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //Painting
                pictureBox1.Refresh();
                Graphics g = e.Graphics;
                g.Clear(System.Drawing.Color.White);
                pictureBox1.Image = canvas;
                /*foreach (Pixel pixel in GameEngine.Canvas.ScreenRender)
                {
                    if (pixel != null && pixel.X >= 0 && pixel.X <= Width && pixel.Y >= 0 && pixel.Y <= Height)
                    {
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, pixel.color.R, pixel.color.G, pixel.color.B);
                        g.FillRectangle(new SolidBrush(newColor), pixel.X, pixel.Y, 1, 1);
                    }
                }*/
                DrawCanvas();
                //g.DrawString("Apples: " + GameEngine.PlayerValues.GetInteger("Apples"), new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(System.Drawing.Color.Black), 10, 10);
                //Text = "Camera Offset: " + Canvas.cameraOffset.x + ": " + Canvas.cameraOffset.y;
            }
            catch (System.Exception m)
            {
                Debug.Error("Error: " + m);
            }
        }

        public List<Prop> prop = new List<Prop>();
        public int clock;

        bool editorMode = true;
        public List<Node> points = new List<Node>();
        public Node lastPosition;

        public void DrawCanvas()
        {
            //Debug.Render(Width, Height);
            //GameEngine.Font.drawText("APPLES: " + GameEngine.PlayerValues.GetInteger("Apples"),10, 10, new GameEngine.Color(0, 0, 0), "APPLES");
            //playerPosition += new Vector2(GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Horizontal")), GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Vertical")));
            //GameEngine.Rectangle rect = new GameEngine.Rectangle((int)playerPosition.x, (int)playerPosition.y, 10, 10);
            //GameEngine.Canvas.DrawRect(rect, new GameEngine.Color(0, 0, 0), "Player");
            //Canvas.cameraOffset = playerPosition + Canvas.cameraOffset;
            //GameEngine.Canvas.MoveAllPixels(Canvas.cameraOffset, "Player");
            //playerPosition = Vector2.zero;

            if (editorMode)
            {
                /*if (GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Place")) > 0)
                {
                    System.Drawing.Point position = PointToClient(Cursor.Position);
                    Node newNode = new Node(lastPosition, new Vector2(position.X, position.Y));
                    points.Add(newNode);
                    lastPosition = newNode;
                }*/

                foreach (Node point in points)
                {
                    if (point != null)
                    {
                        //Render the point
                        if (point.lastNode != null) { Debug.Log("Point " + point.position.x + " " + point.position.y + "  Old Point " + point.lastNode.position.x + " " + point.lastNode.position.y); }
                        //Canvas.DrawRect(new GameEngine.Rectangle(new Vector2(10, 10), point.position), GameEngine.Color.Black, "Point" + point.position.x + point.position.y);
                        if (point.lastNode != null) { //Canvas.DrawLine(GameEngine.Color.Black, point.lastNode.position, point.position, "Line" + point.position.x + point.position.y); }
                        }
                    }
                }

                //Render the props
                foreach (Prop m in prop)
                {
                    m.Draw();
                }
            }
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameEngine;

namespace GameEngineWindow
{
    public partial class Form1 : Form
    {
        public static Bitmap canvas;

        public Form1()
        {
            InitializeComponent();
            GameEngine.Axis.AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            GameEngine.Axis.AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            //GameEngine.Font.SetupFont();
            canvas = new Bitmap(Width, Height);
            GameEngine.Debug.Log("Initialised!");
        }

        public static Vector2 playerPosition = Vector2.zero;
        int fps = 0;
        int cloc = 0;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //Painting
                pictureBox1.Refresh();
                Graphics g = e.Graphics;
                g.Clear(System.Drawing.Color.White);
                pictureBox1.Image = canvas;
                foreach (Pixel pixel in GameEngine.Canvas.ScreenRender)
                {
                    if (pixel != null && pixel.X >= 0 && pixel.X <= Width && pixel.Y >= 0 && pixel.Y <= Height)
                    {
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, pixel.color.R, pixel.color.G, pixel.color.B);
                        g.FillRectangle(new SolidBrush(newColor), pixel.X, pixel.Y, 1, 1);
                    }
                }
                DrawCanvas();
                g.DrawString("Apples: " + GameEngine.PlayerValues.GetInteger("Apples"), new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(System.Drawing.Color.Black), 10, 10);
                Text = "Camera Offset: " + Canvas.cameraOffset.x + ": " + Canvas.cameraOffset.y;
            }
            catch (System.Exception m)
            {
                GameEngine.Debug.Error("Error: " + m);
            }
        }

        public List<Apple> apples = new List<Apple>();
        public int clock;

        public void DrawCanvas()
        {
            //GameEngine.Font.drawText("APPLES: " + GameEngine.PlayerValues.GetInteger("Apples"),10, 10, new GameEngine.Color(0, 0, 0), "APPLES");
            playerPosition += new Vector2(GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Horizontal")), GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Vertical")));
            GameEngine.Rectangle rect = new GameEngine.Rectangle(Width / 2, Height / 2, 10, 10);
            GameEngine.Canvas.DrawRect(rect, new GameEngine.Color(0, 0, 0), "Player");
            Canvas.cameraOffset = playerPosition + Canvas.cameraOffset;
            GameEngine.Canvas.MoveAllPixels(Canvas.cameraOffset, "Player");
            playerPosition = Vector2.zero;

            if(clock >= 200)
            {
                //GameEngine.Debug.Log("Clock is 200");
                Random rand = new Random();
                Vector2 pos = new Vector2(rand.Next(Width), rand.Next(Height));
                apples.Add(new Apple(pos));
                //GameEngine.Debug.Log("Drawn Apple");
                clock = 0;
            }
            else
            {
                clock++;
            }

            //Render the apples
            foreach(Apple m in apples)
            {
                m.render();
            }
        }
    }
}

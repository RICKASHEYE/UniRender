using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SubrightEngine
{
    [System.Serializable]
    public class Command
    {
        public int xS;
        public int yS;
        public Color colorS;

        public Command(int x, int y, Color color)
        {
            xS = x;
            yS = y;
            colorS = color;
        }
    }

    public partial class SoftwareRender : Form
    {
        Canvas canvasRender = null;
        public static Bitmap canvasStored;
        public List<Command> commands = new List<Command>();
        public Graphics g;

        public SoftwareRender(Canvas canvas, AppConfiguration configuration)
        {
            InitializeComponent();
            canvasRender = canvas;
            Text = configuration.Title;
            Width = configuration.Width;
            Height = configuration.Height;
            canvasStored = new Bitmap(Width, Height);
            Debug.Log("Initialised Software Renderer");
        }

        public void DrawPixel(int x, int y, Color color)
        {
            Command command = new Command(x, y, color);
            commands.Add(command);
            Debug.Log("Drawn Pixel");
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //Painting
                pictureBox1.Refresh();
                g = e.Graphics;
                g.Clear(System.Drawing.Color.White);
                pictureBox1.Image = canvasStored;
                foreach (Command command in commands)
                {
                    if (command != null && command.xS >= 0 && command.xS <= Width && command.yS >= 0 && command.yS <= Height)
                    {
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, command.colorS.R, command.colorS.G, command.colorS.B);
                        SolidBrush brush = new SolidBrush(newColor);
                        g.FillRectangle(brush, command.xS, command.yS, 1, 1);
                        brush.Dispose();
                    }
                    commands.Remove(command);
                }
                //canvasRender.Draw();
            }
            catch (System.Exception m)
            {
                Debug.Error("Error: " + m);
            }
        }
    }
}

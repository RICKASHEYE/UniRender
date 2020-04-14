using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameEngine;
using System.IO;

namespace ArcadeGameInterface
{
    public partial class Form1 : Form
    {
        public static Bitmap canvas;
        bool romsInstalled = false;
        bool libretroInstalled = false;
        string[] pathNames;
        public string nesFileType = ".nes";
        public string snesFileType = ".smc";
        public string n64FileType = ".n64";
        public string gbcFileType = ".gbc";

        public Form1()
        {
            InitializeComponent();
            canvas = new Bitmap(Width, Height);
            if (!Directory.Exists(@"libretro/"))
            {
                Debug.Log("Libretro Path Missing...");
                Directory.CreateDirectory("libretro");
            }
            else
            {
                libretroInstalled = true;
            }

            if (!Directory.Exists(@"roms/") || IsDirectoryEmpty(@"roms/"))
            {
                Directory.CreateDirectory("roms");
            }
            else
            {
                pathNames = Directory.GetFiles("roms");
                romsInstalled = true;
            }
            GameEngine.Axis.AssignNewKey(new KeyCode("Move", Keys.D, Keys.A));
            GameEngine.Axis.AssignNewKey(new KeyCode("Open", Keys.Enter, Keys.Enter));
            GameEngine.Debug.Log("Initialised!");
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //Painting
                pictureBox1.Refresh();
                Graphics g = e.Graphics;
                g.Clear(System.Drawing.Color.Blue);
                pictureBox1.Image = canvas;
                /*foreach (Pixel pixel in //GameEngine.Canvas.ScreenRender)
                {
                    if (pixel != null && pixel.X >= 0 && pixel.X <= Width && pixel.Y >= 0 && pixel.Y <= Height)
                    {
                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(255, pixel.color.R, pixel.color.G, pixel.color.B);
                        g.DrawRectangle(new Pen(newColor), pixel.X, pixel.Y, 1, 1);
                    }
                }*/

                if(romsInstalled == false)
                {
                    g.DrawString("No roms installed.", new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(System.Drawing.Color.Black), 10, 10);
                }else if(libretroInstalled == false)
                {
                    g.DrawString("Libretro is not installed.", new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(System.Drawing.Color.Black), 10, 10);
                }
                DrawCanvas(g);
            }
            catch (System.Exception m)
            {
                GameEngine.Debug.Error("Error: " + m);
            }
        }

        int startX = 0;
        int startRoms = 0;
        int endRoms = 50;
        int index = 0;

        int timeReset = 0;
        public void DrawCanvas(Graphics g)
        {
            try
            {
                //Draw the interface.
                //g.DrawString(pathNames[index] + " " + index, new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(System.Drawing.Color.Black), 10, 25);
                for (int i = 0; i < 45; i++)
                {
                    System.Drawing.Color color = System.Drawing.Color.Black;
                    if(i == 0) { color = System.Drawing.Color.Gold; }
                    g.DrawString(pathNames[index + i], new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), new SolidBrush(color), 25, 25 * i);
                }

                //Change the index
                float indexChange = GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Move"));
                if (index > pathNames.Length)
                {
                    index = 0;
                }
                else if (index < 0)
                {
                    index = pathNames.Length;
                }

                float indexChangeEnter = GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Enter"));
                if(indexChangeEnter >= 0.5)
                {
                    //Trigger the enter function to launch

                }

                if(index > endRoms) { endRoms += 5; }

                if (timeReset == 0)
                {
                    if (indexChange > 0.9f)
                    {
                        index += 1;
                        timeReset = 1;
                    }
                    else if (indexChange < -0.9f)
                    {
                        index -= 1;
                        timeReset = 1;
                    }
                }
                else if (timeReset >= 1)
                {
                    timeReset++;
                    if (timeReset >= 25)
                    {
                        timeReset = 0;
                    }
                }
            }
            catch (Exception)
            {
                index = 0;
            }
        }
    }
}

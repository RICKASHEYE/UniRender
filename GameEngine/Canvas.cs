using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace SubrightEngine
{
    public class Pixel
    {
        public int X;
        public int Y;
        public Color color;
        public Vector2 previousDest;
        public string name_;
        public bool isCollidable;

        public Pixel(int x_, int y_, Color color_, string name)
        {
            X = x_;
            Y = y_;
            color = color_;
            name_ = name;
        }

        public void MovePixel(int x, int y)
        {
            //Move pixel to a certain destination
            previousDest = new Vector2(X, Y);
            X = x;
            Y = y;
        }
    }

    public class Text
    {
        public string text;
        public Vector2 position;
        public TextFormat TextFormat { get; private set; }
        public TextLayout TextLayout { get; private set; }
        public string font_stored;
        public int size_stored;
        public Color color;

        public Text(string textTitle, Vector2 positionPos, RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite, string font, int size, AppConfiguration Config, Color colorP)
        {
            Initialise(textTitle, positionPos, RenderTarget2D, FactoryDWrite, font, size, Config, colorP);
        }

        public void Initialise(string textTitle, Vector2 positionPos, RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite, string font, int size, AppConfiguration Config, Color colorP)
        {
            text = textTitle;
            position = positionPos;

            font_stored = font;
            size_stored = size;

            color = colorP;

            TextFormat = new TextFormat(FactoryDWrite, font, size) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };
            RenderTarget2D.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;
            TextLayout = new TextLayout(FactoryDWrite, textTitle, TextFormat, Config.Width, Config.Height);
        }

        public void Render(RenderTarget RenderTarget2D, SolidColorBrush SceneBrush)
        {
            if(TextFormat == null) { Debug.Error("Text Format has not been initialised as it is null!!!"); }
            if(TextLayout == null) { Debug.Error("Text Layout has not been initialised as it is null!!!"); }
            SharpDX.Mathematics.Interop.RawVector2 vector2 = new SharpDX.Mathematics.Interop.RawVector2(position.x, position.y);
            RenderTarget2D.DrawTextLayout(vector2, TextLayout, SceneBrush, DrawTextOptions.None);
        }
    }

    public enum DrawMode
    {
        DIRECT, ARRAY
    }

    public class Canvas : Axis
    {
        //Draw a rectangle or a screen
        public static List<Pixel> ScreenRender = new List<Pixel>();
        //public static RenderType currentRender;
        public static List<Text> textObjects = new List<Text>();
        public static string name = "Subright Engine";

        protected override void Draw(AppTime time)
        {
            //Clear(Color.Black);
            base.Draw(time);
            List<Vector2> duplicates = new List<Vector2>();
            foreach(Pixel m in ScreenRender)
            {
                var solidColorBrush = new SolidColorBrush(Direct2D1Handler.RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(m.color.R, m.color.G, m.color.B, 1));
                if (m.isCollidable)
                {
                    bool overlay = false;
                    foreach (Pixel p in ScreenRender)
                    {
                        if (p.name_ != m.name_)
                        {
                            Vector2 pixelPosition = new Vector2(p.X, p.Y);
                            Vector2 currentPixelPosition = new Vector2(m.X, m.Y);
                            if (pixelPosition == currentPixelPosition)
                            {
                                overlay = true;
                            } 
                        }
                    } 
                    if(overlay == true)
                    {
                        m.MovePixel((int)m.previousDest.x, (int)m.previousDest.y);
                    }
                }
                RenderTarget2D.FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(m.X, m.Y, m.X + 1, m.Y + 1), solidColorBrush);
                solidColorBrush.Dispose();
            }

            foreach(Text text in textObjects)
            {
                //if(text.TextFormat == null || text.TextLayout == null) { Debug.Log("Intialising Text..."); text.Initialise(text.text, text.position, RenderTarget2D, FactoryDWrite, text.font_stored, text.size_stored, Config); }
                var solidColorBrush = new SolidColorBrush(Direct2D1Handler.RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(text.color.R, text.color.G, text.color.B, 1));
                text.Render(RenderTarget2D, solidColorBrush);
                solidColorBrush.Dispose();
            }
        }

        protected override void Initialize(AppConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assetsFolder = Path.Combine(assemblyFolder, "Assets");
            if (!Directory.Exists(assetsFolder))
            {
                //Load an assembly assets folder
                Directory.CreateDirectory(assetsFolder);
                Initialize(demoConfiguration);
            }
            else
            {
                //Now initialise the assets into a registry and allow them to be loaded into the game.
            }
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            //Unload the content
            ScreenRender.Clear();
            textObjects.Clear();
        }

        /// <summary>
        /// Draws a pixel on the screen in Array Mode, by default.
        /// </summary>
        public static void DrawPixel(int x, int y, Color color, string name, DrawMode modeDraw)
        {
            //Debug.Log("Drawn Pixel");
            if(x == null || y == null)
            {
                Debug.Error("Unable to draw pixel no x or y specified.");
                return;
            }

            if(color == null)
            {
                Debug.Error("Unable to draw pixel no color defined.");
                return;
            }

            if (x > 0 && y > 0 && x < Config.Width && y < Config.Height)
            {
                if (modeDraw == DrawMode.ARRAY)
                {
                    if (!PixelExists(x, y, color, name))
                    {
                        Debug.Log("Adding this pixel to the screen render array!");
                        Pixel pixel = new Pixel(x, y, color, name);
                        ScreenRender.Add(pixel);
                    }
                }
                else
                {
                    DirectDrawPixel(x, y, color);
                    Debug.Log("Directly drawing the pixel");
                }
            }
            //RecalculatePixelObjects();
        }

        public static void DrawPixel(int x, int y, Color color, DrawMode modeDraw)
        {
            DrawPixel(x, y, color, "", modeDraw);
        }

        /// <summary>
        /// This function draws a pixel directly.
        /// </summary>
        private static void DirectDrawPixel(int x, int y, Color color)
        {
            var solidColorBrush = new SolidColorBrush(Direct2D1Handler.RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(color.R, color.G, color.B, 1));
            RenderTarget2D.FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(x, y, x + 1, y + 1), solidColorBrush);
            solidColorBrush.Dispose();
        }

        public static bool PixelExists(int x, int y, Color color, string name)
        {
            bool pixelexists = false;
            foreach(Pixel m in ScreenRender)
            {
                if(m.X == x && m.Y == y)
                {
                    if (m.color == color)
                    {
                        pixelexists = true;
                        Debug.Log("Pixel Exists");
                    }
                    else
                    {
                        m.color = color;
                        m.name_ = name;
                        Debug.Log("Theres a pixel there change the color and the name!");
                    }
                }
            }
            return pixelexists;
        }

        private static void DrawHorizontalLine(Color color, int dx, int x1, int y1, string name, DrawMode modeDraw)
        {
            for(int i = 0; i < dx; i++)
            {
                DrawPixel(x1 + i, y1, color, name, modeDraw);
            }
        }

        /// <summary>
        /// Still in development not recommended to use it just yet.
        /// </summary>
        public static void DrawText(string text, string font, int size, Vector2 position, Color color)
        {
            Text textObject = new Text(text, position, RenderTarget2D, FactoryDWrite, font, size, Config,color);
            textObjects.Add(textObject);
        }

        private void DrawVerticalLine(Color color, int dy, int x1, int y1, string name, DrawMode modeDraw)
        {
            for(int i = 0; i < dy; i++)
            {
                DrawPixel(x1, y1 + i, color, name, modeDraw);
            }
        }

        private void DrawDiagonalLine(Color color, int dx, int dy, int x1, int y1, string name, DrawMode modeDraw)
        {
            int i, sdx, sdy, dxabs, dyabs, x, y, px, py;

            dxabs = Math.Abs(dx);
            dyabs = Math.Abs(dy);
            sdx = Math.Sign(dx);
            sdy = Math.Sign(dy);
            x = dyabs >> 1;
            y = dxabs >> 1;
            px = x1;
            py = y1;

            if(dxabs >= dyabs)
            {
                for(i = 0; i < dxabs; i++)
                {
                    y += dyabs;
                    if(y >= dxabs)
                    {
                        y -= dxabs;
                        py += sdy;
                    }
                    px += sdx;
                    DrawPixel(px, py, color, name, modeDraw);
                }
            }
            else
            {
                for(i = 0; i < dyabs; i++)
                {
                    x += dyabs;
                    if(x >= dyabs)
                    {
                        x -= dyabs;
                        px += sdx;
                    }
                    py += sdy;
                    DrawPixel(px, py, color, name, modeDraw);
                }
            }
        }

        public virtual void DrawLine(Color color, int x1, int y1, int x2, int y2, string name, DrawMode modeDraw)
        {
            if(color == null)
            {
                Debug.Error("No colour defined");
            }

            int dx, dy;

            dx = x2 - x1;
            dy = y2 - y1;

            if(dy == 0)
            {
                DrawHorizontalLine(color, dx, x1, y1, name, modeDraw);
                return;
            }

            DrawDiagonalLine(color, dx, dy, x1, y1, name, modeDraw);
        }

        public static void DrawLine(Color color, Vector2 p1, Vector2 p2, DrawMode modeDraw)
        {
            DrawLine(color, p1, p2, "", modeDraw);
        }

        public static void DrawLine(Color color, Vector2 p1, Vector2 p2, string name, DrawMode modeDraw)
        {
            DrawLine(color, (int)p1.x, (int)p1.y, (int)p2.x, (int)p2.y, name, modeDraw);
        }

        public static void DrawLine(Color color, float x1, float y1, float x2, float y2, DrawMode modeDraw)
        {
            DrawLine(color, x1, y1, x2, y2, "", modeDraw);
        }

        public static void DrawLine(Color color, float x1, float y1, float x2, float y2, string name, DrawMode modeDraw)
        {
            DrawLine(color, new Vector2(x1, y1), new Vector2(x2, y2), name, modeDraw);
        }

        public static void DrawCircle(Color color, int x_center, int y_center, int radius, string name, DrawMode modeDraw)
        {
            int x = radius;
            int y = 0;
            int e = 0;
            
            while(x >= y)
            {
                DrawPixel(x_center + x, y_center + y, color, name, modeDraw);
                DrawPixel(x_center + y, y_center + x, color, name, modeDraw);
                DrawPixel(x_center - y, y_center + x, color, name, modeDraw);
                DrawPixel(x_center - x, y_center + y, color, name, modeDraw);
                DrawPixel(x_center - x, y_center - y, color, name, modeDraw);
                DrawPixel(x_center - y, y_center - x, color, name, modeDraw);
                DrawPixel(x_center + y, y_center - x, color, name, modeDraw);
                DrawPixel(x_center + x, y_center - y, color, name, modeDraw);

                y++;
                if(e <= 0)
                {
                    e += 2 * y + 1;
                }
                if(e > 0)
                {
                    x--;
                    e -= 2 * x + 1;
                }
            }
        }

        public static void DrawCircle(Color color, int x_center, int y_center, int radius, DrawMode modeDraw)
        {
            DrawCircle(color, x_center, y_center, radius, modeDraw);
        }

        public static void DrawCircle(Color color, Vector2 vector, int radius, DrawMode modeDraw)
        {
            DrawCircle(color, vector, radius, "", modeDraw);
        }

        public static void DrawCircle(Color color, Vector2 vector, int radius, string name, DrawMode modeDraw)
        {
            DrawCircle(color, (int)vector.x, (int)vector.y, radius, name, modeDraw);
        }

        public static void DrawFilledCircle(Color color, Vector2 vector, int radius, string name, DrawMode modeDraw)
        {
            for(int i = 0; i < radius; i++)
            {
                DrawCircle(color, vector, i, name, modeDraw);
            }
        }

        public static void DrawFilledCircle(Color color, Vector2 vector, int radius, DrawMode modeDraw)
        {
            DrawFilledCircle(color, vector, radius, "", modeDraw);
        }

        public static void DrawEllipse(Color color, int x_center, int y_center, int x_radius, int y_radius, string name, DrawMode modeDraw)
        {
            int a = 2 * x_radius;
            int b = 2 * y_radius;
            int b1 = b & 1;
            int dx = 4 * (1 - a) * b * b;
            int dy = 4 * (b1 + 1) * a * a;
            int err = dx + dy + b1 * a * a;
            int e2;
            int y = 0;
            int x = x_radius;
            a *= 8 * a;
            b1 = 8 * b * b;

            while(x >= 0)
            {
                DrawPixel(x_center + x, y_center + y, color, name, modeDraw);
                DrawPixel(x_center - x, y_center + y, color, name, modeDraw);
                DrawPixel(x_center - x, y_center - y, color, name, modeDraw);
                DrawPixel(x_center + x, y_center - y, color, name, modeDraw);
                e2 = 2 * err;
                if (e2 <= dy) { y++; err += dy += a; }
                if (e2 >= dx || 2 * err > dy) { x--; err += dx += b1; }
            }
        }

        public static void DrawEllipse(Color color, Vector2 vector, int x_radius, int y_radius, DrawMode modeDraw)
        {
            DrawEllipse(color, vector, x_radius, y_radius, "", modeDraw);
        }

        public static void DrawEllipse(Color color, Vector2 vector, int x_radius, int y_radius, string name, DrawMode modeDraw)
        {
            DrawEllipse(color, (int)vector.x, (int)vector.y, x_radius, y_radius, name, modeDraw);
        }

        public static void DrawPolygon(Vector2[] vectors, Color color, string name, DrawMode modeDraw)
        {
            //Read all of the vectors and draw a rectangle or point based on these three atleast.
            if(vectors.Length > 3)
            {
                //Continue
                for(int i = 0; i < vectors.Length - 1; i++)
                {
                    DrawLine(color, vectors[i], vectors[i + 1], name, modeDraw);
                }
            }
            else if(vectors.Length < 3 || vectors.Length > 4)
            {
                Debug.Error("Less or greater than three vertices found unable to create a polygon");
            }
        }

        public static void DrawPolygon(Vector2[] vectors, Color color)
        {
            DrawPolygon(vectors, color);
        }

        public static void DrawTriangle(Color color, int v1x, int v1y, int v2x, int v2y, int v3x, int v3y, string name, DrawMode modeDraw)
        {
            DrawLine(color, v1x, v1y, v2x, v2y, name, modeDraw);
            DrawLine(color, v1x, v1y, v3x, v3y, name, modeDraw);
            DrawLine(color, v2x, v2y, v3x, v3y, name, modeDraw);
        }

        public static void DrawTriangle(Color color, Vector2 vector01, Vector2 vector02, Vector2 vector03, string name, DrawMode modeDraw)
        {
            DrawTriangle(color, (int)vector01.x, (int)vector01.y, (int)vector02.x, (int)vector02.y, (int)vector03.x, (int)vector03.y, name, modeDraw);
        }

        public static void DrawTriangle(Color color, Vector2 vector01, Vector2 vector02, Vector2 vector3, DrawMode modeDraw)
        {
            DrawTriangle(color, vector01, vector02, vector3, "", modeDraw);
        }

        public static void DrawRect(Rectangle rect, Color color, string name, DrawMode modeDraw)
        {
            //ClearPixels(name);
            for(int x = rect.posx; x < rect.posx + rect.sizex; x++)
            {
                for(int y = rect.posy; y < rect.posy + rect.sizey; y++)
                {
                    DrawPixel(x, y, color, name, modeDraw);
                }
            }
        }

        public static void DrawRect(Rectangle rect, Color color, DrawMode modeDraw)
        {
            DrawRect(rect, color, "", modeDraw);
        }

        /*public static void DrawImage(Rectangle size, ParEngineImage image)
        {
            ClearPixels(name);
            Bitmap ima = image.map_;
            ima = ImageDrawer.ResizeImage(ima, size.sizex, size.sizey, 0, 0);
            for(int x = size.posx; x < size.posx + ima.Width; x++)
            {
                for(int y = size.posy; y < size.posy + ima.Height; y++)
                {
                    System.Drawing.Color imaColor = ima.GetPixel(x, y);
                    Color color = new Color(imaColor.R, imaColor.G, imaColor.B);
                    DrawPixel(x, y, color);
                }
            }
            ima.Dispose();
        }*/

        public static void Clear(Color color, DrawMode modeDraw)
        {
            if (modeDraw == DrawMode.DIRECT)
            {
                SharpDX.Mathematics.Interop.RawColor4 clearColor = new SharpDX.Mathematics.Interop.RawColor4(color.R, color.G, color.B, 1);
                RenderTarget2D.Clear(clearColor);
            }
            else
            {
                ScreenRender.Clear();
                Clear(color, DrawMode.DIRECT);
            }
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, Color color, string name, DrawMode modeDraw)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), color, name, modeDraw);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, int r, int g, int b, string name, DrawMode modeDraw)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), new Color(r, g, b), name, modeDraw);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, int r, int g, int b, DrawMode modeDraw)
        {
            DrawRect(x, y, sizex, sizey, r, g, b, "", modeDraw);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, Color color, DrawMode modeDraw)
        {
            DrawRect(x, y, sizex, sizey, color, modeDraw);
        }
    }
}

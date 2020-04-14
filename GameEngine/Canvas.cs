using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameEngine
{
    public class Pixel
    {
        public int X;
        public int Y;
        public Color color;
        public string namePixel;
        public Vector2 previousDest;

        public Pixel(int x_, int y_, Color color_, string name_)
        {
            X = x_;
            Y = y_;
            color = color_;
            namePixel = name_;
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

        public Text(string textTitle, Vector2 positionPos, RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite, string font, int size, AppConfiguration Config)
        {
            Initialise(textTitle, positionPos, RenderTarget2D, FactoryDWrite, font, size, Config);
        }

        public void Initialise(string textTitle, Vector2 positionPos, RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite, string font, int size, AppConfiguration Config)
        {
            text = textTitle;
            position = positionPos;

            font_stored = font;
            size_stored = size;

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

    public class PixelObjects
    {
        public List<Pixel> pixels = new List<Pixel>();
        public string name;

        public PixelObjects()
        {
            if(name == null)
            {
                name = pixels[0].namePixel;
            }
        }
    }

    public class Canvas : Direct2D1Handler
    {
        //Draw a rectangle or a screen
        public List<Pixel> ScreenRender = new List<Pixel>();
        //public RenderType currentRender;
        public List<Text> textObjects = new List<Text>();

        protected override void Draw(AppTime time)
        {
            RenderTarget2D.Clear(new SharpDX.Mathematics.Interop.RawColor4(0, 0, 0, 1));
            base.Draw(time);
            foreach (Pixel m in ScreenRender)
            {
                var solidColorBrush = new SolidColorBrush(Direct2D1Handler.RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(m.color.R, m.color.G, m.color.B, 1));
                RenderTarget2D.FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(m.X, m.Y, m.X + 1, m.Y + 1), solidColorBrush);
            }

            foreach(Text text in textObjects)
            {
                if(text.TextFormat == null || text.TextLayout == null) { Debug.Log("Intialising Text..."); text.Initialise(text.text, text.position, RenderTarget2D, FactoryDWrite, text.font_stored, text.size_stored, Config); }
                text.Render(RenderTarget2D, SceneColorBrush);
            }
        }

        public void Execute(EventHandler handler)
        {
            
        }

        protected override void Initialize(AppConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
        }

        public void RecalculatePixelObjects()
        {
            //Recalculate objects
            
        }

        public void DrawPixel(int x, int y, Color color, string name)
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

            if(name == null)
            {
                Debug.Error("No name found for the object not rendering");
                return;
            }

            Pixel addedPixel = new Pixel(x, y, color, name);
            ScreenRender.Add(addedPixel);
            //RecalculatePixelObjects();
        }

        public virtual void DrawArray(Color[] colors, Point point, int width, int height)
        {
            //Need to find a way to draw a array here!!!
        }

        private void DrawHorizontalLine(Color color, int dx, int x1, int y1, string name)
        {
            for(int i = 0; i < dx; i++)
            {
                DrawPixel(x1 + i, y1, color, name);
            }
        }

        public void DrawText(string text, string font, int size, Vector2 position)
        {
            Text textObject = new Text(text, position, RenderTarget2D, FactoryDWrite, font, size, Config);
            textObjects.Add(textObject);
        }

        private void DrawVerticalLine(Color color, int dy, int x1, int y1, string name)
        {
            for(int i = 0; i < dy; i++)
            {
                DrawPixel(x1, y1 + i, color, name);
            }
        }

        private void DrawDiagonalLine(Color color, int dx, int dy, int x1, int y1, string name)
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
                    DrawPixel(px, py, color, name);
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
                    DrawPixel(px, py, color, name);
                }
            }
        }

        public virtual void DrawLine(Color color, int x1, int y1, int x2, int y2, string name)
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
                DrawHorizontalLine(color, dx, x1, y1, name);
                return;
            }

            DrawDiagonalLine(color, dx, dy, x1, y1, name);
        }

        public void DrawLine(Color color, Vector2 p1, Vector2 p2, string name)
        {
            DrawLine(color, (int)p1.x, (int)p1.y, (int)p2.x, (int)p2.y, name);
        }

        public void DrawLine(Color color, float x1, float y1, float x2, float y2, string name)
        {
            DrawLine(color, new Vector2(x1, y1), new Vector2(x2, y2), name);
        }

        public void DrawCircle(Color color, int x_center, int y_center, int radius, string name)
        {
            int x = radius;
            int y = 0;
            int e = 0;
            
            while(x >= y)
            {
                DrawPixel(x_center + x, y_center + y, color, name);
                DrawPixel(x_center + y, y_center + x, color, name);
                DrawPixel(x_center - y, y_center + x, color, name);
                DrawPixel(x_center - x, y_center + y, color, name);
                DrawPixel(x_center - x, y_center - y, color, name);
                DrawPixel(x_center - y, y_center - x, color, name);
                DrawPixel(x_center + y, y_center - x, color, name);
                DrawPixel(x_center + x, y_center - y, color, name);

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

        public void DrawCircle(Color color, Vector2 vector, int radius, string name)
        {
            DrawCircle(color, (int)vector.x, (int)vector.y, radius, name);
        }

        public void DrawFilledCircle(Color color, Vector2 vector, int radius, string name)
        {
            for(int i = 0; i < radius; i++)
            {
                DrawCircle(color, vector, i, name);
            }
        }

        public void DrawEllipse(Color color, int x_center, int y_center, int x_radius, int y_radius, string name)
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
                DrawPixel(x_center + x, y_center + y, color, name);
                DrawPixel(x_center - x, y_center + y, color, name);
                DrawPixel(x_center - x, y_center - y, color, name);
                DrawPixel(x_center + x, y_center - y, color, name);
                e2 = 2 * err;
                if (e2 <= dy) { y++; err += dy += a; }
                if (e2 >= dx || 2 * err > dy) { x--; err += dx += b1; }
            }
        }

        public void DrawEllipse(Color color, Vector2 vector, int x_radius, int y_radius, string name)
        {
            DrawEllipse(color, (int)vector.x, (int)vector.y, x_radius, y_radius, name);
        }

        public void DrawPolygon(Vector2[] vectors, Color color, string name)
        {
            //Read all of the vectors and draw a rectangle or point based on these three atleast.
            if(vectors.Length > 3)
            {
                //Continue
                for(int i = 0; i < vectors.Length - 1; i++)
                {
                    DrawLine(color, vectors[i], vectors[i + 1], name);
                }
            }
            else if(vectors.Length < 3 || vectors.Length > 4)
            {
                Debug.Error("Less or greater than three vertices found unable to create a polygon");
            }
        }

        public void DrawTriangle(Color color, int v1x, int v1y, int v2x, int v2y, int v3x, int v3y, string name)
        {
            DrawLine(color, v1x, v1y, v2x, v2y, name);
            DrawLine(color, v1x, v1y, v3x, v3y, name);
            DrawLine(color, v2x, v2y, v3x, v3y, name);
        }

        public void DrawTriangle(Color color, Vector2 vector01, Vector2 vector02, Vector2 vector03, string name)
        {
            DrawTriangle(color, (int)vector01.x, (int)vector01.y, (int)vector02.x, (int)vector02.y, (int)vector03.x, (int)vector03.y, name);
        }

        public void DrawRect(Rectangle rect, Color color, string name)
        {
            ClearPixels(name);
            for(int x = rect.posx; x < rect.posx + rect.sizex; x++)
            {
                for(int y = rect.posy; y < rect.posy + rect.sizey; y++)
                {
                    DrawPixel(x, y, color, name);
                }
            }
        }

        /*public void DrawImage(Rectangle size, ParEngineImage image, string name)
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
                    DrawPixel(x, y, color, name);
                }
            }
            ima.Dispose();
        }*/

        public void MoveAllPixels(Vector2 direction, string blackList)
        {
            //Debug.Log("Moving all pixels to " + direction);
            foreach (Pixel m in ScreenRender)
            {
                if (m.namePixel != blackList)
                {
                    m.MovePixel(m.X + (int)direction.x, m.Y + (int)direction.y);
                }
            }
        }

        public void ClearPixels(string name)
        {
            foreach(Pixel m in ScreenRender.ToArray())
            {
                if(m.namePixel == name)
                {
                    ScreenRender.Remove(m);
                }
            }
        }

        public void DrawRect(int x, int y, int sizex, int sizey, Color color, string name)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), color, name);
        }

        public void DrawRect(int x, int y, int sizex, int sizey, int r, int g, int b, string name)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), new Color(r, g, b), name);
        }
    }
}

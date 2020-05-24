using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SubrightEngine.Types;
using Color32 = SubrightEngine.Types.Color32;
using Rectangle = SubrightEngine.Types.Rectangle;
using Vector2 = SubrightEngine.Types.Vector2;

namespace SubrightEngine
{
    public enum DrawMode
    {
        DIRECT, ARRAY
    }

    public enum RenderMode
    {
        DirectX, Software, Vulkan
    }

    public class Canvas : RenderingLibraryManager
    {
        //public static RenderType currentRender;
        public static string name = "Subright Engine";

        /// <summary>
        /// Draws a pixel on the screen in Array Mode, by default.
        /// </summary>
        public static void DrawPixel(int x, int y, Color32 Color, string name, DrawMode modeDraw)
        {
            //Debug.Log("Drawn Pixel");
            if(x == null || y == null)
            {
                Debug.Error("Unable to draw pixel no x or y specified.");
                return;
            }

            if(Color == null)
            {
                Debug.Error("Unable to draw pixel no Color32 defined.");
                return;
            }

            if (modeDraw == DrawMode.ARRAY)
            {
                if (!PixelExists(x, y, Color, name))
                {
                    //Debug.Log("Adding this pixel to the screen render array!");
                    Pixel pixel = new Pixel(x, y, Color, name);
                    ScreenRender.Add(pixel);
                }
            }
            else
            {
                DirectDrawPixel(x, y, Color);
                //Debug.Log("Directly drawing the pixel");
            }
            //RecalculatePixelObjects();
        }

        public static void DrawPixel(int x, int y, Color32 Color32, DrawMode modeDraw)
        {
            DrawPixel(x, y, Color32, "", modeDraw);
        }

        public static void DrawImage(int x, int y, string imageName, DrawMode modeDraw)
        {
            //string filePath = GetImagePath(imageName);
            if (modeRender != RenderMode.DirectX)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageName);
                for (int xpos = 0; xpos < bitmap.Width; xpos++)
                {
                    for (int ypos = 0; ypos < bitmap.Height; ypos++)
                    {
                        System.Drawing.Color Color = bitmap.GetPixel(xpos, ypos);
                        Color32 gColor32 = new Color32((int)Color.R, (int)Color.G, (int)Color.B, 1);
                        DrawPixel(x + xpos, y + ypos, gColor32, modeDraw);
                    }
                }
                bitmap.Dispose();
            }
            else
            {
                RenderTarget RenderTarget2D = libraryGet("SharpDX").getRenderTarget();
                SharpDX.Direct2D1.Bitmap bitmap = LoadFromFile(RenderTarget2D, imageName);
                RenderTarget2D.DrawBitmap(bitmap, 1.0f, BitmapInterpolationMode.NearestNeighbor, new SharpDX.Mathematics.Interop.RawRectangleF(x, y, x + bitmap.Size.Width + 1, y + bitmap.Size.Height + 1));
                bitmap.Dispose();
            }
        }

        /*public static void DrawPixel(int x, int y, Color32 Color32, int Angle, DrawMode modeDraw)
        {
            double newX = Math.Cos(-Angle) * x - Math.Sin(-Angle) * y;
            double newY = Math.Sin(-Angle) * x + Math.Cos(-Angle) * y;
            DrawPixel((int)newX, (int)newY, Color32, "", modeDraw);
        }*/

        public static bool PixelExists(int x, int y, Color32 Color, string name)
        {
            bool pixelexists = false;
            foreach(Pixel m in ScreenRender)
            {
                if(m.X == x && m.Y == y)
                {
                    if (m.color == Color)
                    {
                        pixelexists = true;
                        Debug.Log("Pixel Exists");
                    }
                    else
                    {
                        m.color = Color;
                        m.name_ = name;
                        Debug.Log("Theres a pixel there change the Color32 and the name!");
                    }
                }
            }
            return pixelexists;
        }

        private static SharpDX.Direct2D1.Bitmap LoadFromFile(RenderTarget renderTarget, string file)
        {
            // Loads from file using System.Drawing.Image
            using (var bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(file))
            {
                var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapProperties = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));
                var size = new Size2(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof(int);
                using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
                {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            int rgba = R | (G << 8) | (B << 16) | (A << 24);
                            tempStream.Write(rgba);
                        }

                    }
                    bitmap.UnlockBits(bitmapData);
                    tempStream.Position = 0;

                    return new SharpDX.Direct2D1.Bitmap(renderTarget, size, tempStream, stride, bitmapProperties);
                }
            }
        }

        private static void DrawHorizontalLine(Color32 Color32, int dx, int x1, int y1, string name, DrawMode modeDraw)
        {
            for(int i = 0; i < dx; i++)
            {
                DrawPixel(x1 + i, y1, Color32, name, modeDraw);
            }
        }

        private void DrawVerticalLine(Color32 Color32, int dy, int x1, int y1, string name, DrawMode modeDraw)
        {
            for(int i = 0; i < dy; i++)
            {
                DrawPixel(x1, y1 + i, Color32, name, modeDraw);
            }
        }

        private void DrawDiagonalLine(Color32 Color32, int dx, int dy, int x1, int y1, string name, DrawMode modeDraw)
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
                    DrawPixel(px, py, Color32, name, modeDraw);
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
                    DrawPixel(px, py, Color32, name, modeDraw);
                }
            }
        }

        public virtual void DrawLine(Color32 Color32, int x1, int y1, int x2, int y2, string name, DrawMode modeDraw)
        {
            if(Color32 == null)
            {
                Debug.Error("No colour defined");
            }

            int dx, dy;

            dx = x2 - x1;
            dy = y2 - y1;

            if(dy == 0)
            {
                DrawHorizontalLine(Color32, dx, x1, y1, name, modeDraw);
                return;
            }

            DrawDiagonalLine(Color32, dx, dy, x1, y1, name, modeDraw);
        }

        public static void DrawLine(Color32 Color32, Vector2 p1, Vector2 p2, DrawMode modeDraw)
        {
            DrawLine(Color32, p1, p2, "", modeDraw);
        }

        public static void DrawLine(Color32 Color32, Vector2 p1, Vector2 p2, string name, DrawMode modeDraw)
        {
            DrawLine(Color32, (int)p1.x, (int)p1.y, (int)p2.x, (int)p2.y, name, modeDraw);
        }

        public static void DrawLine(Color32 Color32, float x1, float y1, float x2, float y2, DrawMode modeDraw)
        {
            DrawLine(Color32, x1, y1, x2, y2, "", modeDraw);
        }

        public static void DrawLine(Color32 Color32, float x1, float y1, float x2, float y2, string name, DrawMode modeDraw)
        {
            DrawLine(Color32, new Vector2(x1, y1), new Vector2(x2, y2), name, modeDraw);
        }

        public static void DrawCircle(Color32 Color32, int x_center, int y_center, int radius, string name, DrawMode modeDraw)
        {
            int x = radius;
            int y = 0;
            int e = 0;
            
            while(x >= y)
            {
                DrawPixel(x_center + x, y_center + y, Color32, name, modeDraw);
                DrawPixel(x_center + y, y_center + x, Color32, name, modeDraw);
                DrawPixel(x_center - y, y_center + x, Color32, name, modeDraw);
                DrawPixel(x_center - x, y_center + y, Color32, name, modeDraw);
                DrawPixel(x_center - x, y_center - y, Color32, name, modeDraw);
                DrawPixel(x_center - y, y_center - x, Color32, name, modeDraw);
                DrawPixel(x_center + y, y_center - x, Color32, name, modeDraw);
                DrawPixel(x_center + x, y_center - y, Color32, name, modeDraw);

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

        public static void DrawCircle(Color32 Color32, int x_center, int y_center, int radius, DrawMode modeDraw)
        {
            DrawCircle(Color32, x_center, y_center, radius, modeDraw);
        }

        public static void DrawCircle(Color32 Color32, Vector2 vector, int radius, DrawMode modeDraw)
        {
            DrawCircle(Color32, vector, radius, "", modeDraw);
        }

        public static void DrawCircle(Color32 Color32, Vector2 vector, int radius, string name, DrawMode modeDraw)
        {
            DrawCircle(Color32, (int)vector.x, (int)vector.y, radius, name, modeDraw);
        }

        public static void DrawFilledCircle(Color32 Color32, Vector2 vector, int radius, string name, DrawMode modeDraw)
        {
            for(int i = 0; i < radius; i++)
            {
                DrawCircle(Color32, vector, i, name, modeDraw);
            }
        }

        public static void DrawFilledCircle(Color32 Color32, Vector2 vector, int radius, DrawMode modeDraw)
        {
            DrawFilledCircle(Color32, vector, radius, "", modeDraw);
        }

        public static void DrawEllipse(Color32 Color32, int x_center, int y_center, int x_radius, int y_radius, string name, DrawMode modeDraw)
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
                DrawPixel(x_center + x, y_center + y, Color32, name, modeDraw);
                DrawPixel(x_center - x, y_center + y, Color32, name, modeDraw);
                DrawPixel(x_center - x, y_center - y, Color32, name, modeDraw);
                DrawPixel(x_center + x, y_center - y, Color32, name, modeDraw);
                e2 = 2 * err;
                if (e2 <= dy) { y++; err += dy += a; }
                if (e2 >= dx || 2 * err > dy) { x--; err += dx += b1; }
            }
        }

        public static void DrawEllipse(Color32 Color32, Vector2 vector, int x_radius, int y_radius, DrawMode modeDraw)
        {
            DrawEllipse(Color32, vector, x_radius, y_radius, "", modeDraw);
        }

        public static void DrawEllipse(Color32 Color32, Vector2 vector, int x_radius, int y_radius, string name, DrawMode modeDraw)
        {
            DrawEllipse(Color32, (int)vector.x, (int)vector.y, x_radius, y_radius, name, modeDraw);
        }

        public static void DrawPolygon(Vector2[] vectors, Color32 Color32, string name, DrawMode modeDraw)
        {
            //Read all of the vectors and draw a rectangle or point based on these three atleast.
            if(vectors.Length > 3)
            {
                //Continue
                for(int i = 0; i < vectors.Length - 1; i++)
                {
                    DrawLine(Color32, vectors[i], vectors[i + 1], name, modeDraw);
                }
            }
            else if(vectors.Length < 3 || vectors.Length > 4)
            {
                Debug.Error("Less or greater than three vertices found unable to create a polygon");
            }
        }

        public static void DrawPolygon(Vector2[] vectors, Color32 Color32)
        {
            DrawPolygon(vectors, Color32);
        }

        public static void DrawTriangle(Color32 Color32, int v1x, int v1y, int v2x, int v2y, int v3x, int v3y, string name, DrawMode modeDraw)
        {
            DrawLine(Color32, v1x, v1y, v2x, v2y, name, modeDraw);
            DrawLine(Color32, v1x, v1y, v3x, v3y, name, modeDraw);
            DrawLine(Color32, v2x, v2y, v3x, v3y, name, modeDraw);
        }

        public static void DrawTriangle(Color32 Color32, Vector2 vector01, Vector2 vector02, Vector2 vector03, string name, DrawMode modeDraw)
        {
            DrawTriangle(Color32, (int)vector01.x, (int)vector01.y, (int)vector02.x, (int)vector02.y, (int)vector03.x, (int)vector03.y, name, modeDraw);
        }

        public static void DrawTriangle(Color32 Color32, Vector2 vector01, Vector2 vector02, Vector2 vector3, DrawMode modeDraw)
        {
            DrawTriangle(Color32, vector01, vector02, vector3, "", modeDraw);
        }

        public static void DrawRect(Rectangle rect, Color32 Color32, string name, DrawMode modeDraw)
        {
            //ClearPixels(name);
            for(int x = rect.posx; x < rect.posx + rect.sizex; x++)
            {
                for(int y = rect.posy; y < rect.posy + rect.sizey; y++)
                {
                    DrawPixel(x, y, Color32, name, modeDraw);
                }
            }
        }

        // To rotate an object given as order set of points in a[] 
        // (x_pivot, y_pivot) 
        void rotate(float[][] a, int n, int x_pivot, int y_pivot, int angle){
            int i = 0;
            while (i < n)
            {
                // Shifting the pivot point to the origin 
                // and the given points accordingly 
                int x_shifted = (int)a[i][0] - x_pivot;
                int y_shifted = (int)a[i][1] - y_pivot;

                // Calculating the rotated point co-ordinates 
                // and shifting it back 
                a[i][0] = (x_pivot + (int)(x_shifted * Math.Cos(angle) - y_shifted * Math.Sin(angle)));
                a[i][1] = (y_pivot + (int)(x_shifted * Math.Sin(angle) + y_shifted * Math.Cos(angle)));
                Console.WriteLine("(" + a[i][0] + ", " + a[i][1] + ") ");
                i++;
            }
        }

        /*public static void DrawRect(Rectangle rect, Color32 Color32, string name, DrawMode modeDraw, int angle)
        {
            //ClearPixels(name);
            for (int x = rect.posx; x < rect.posx + rect.sizex; x++)
            {
                for (int y = rect.posy; y < rect.posy + rect.sizey; y++)
                {
                    int size1 = 4; 
                    float[][] points_list1 = {{100, 100}, {150, 200}, {200, 200}, {200, 150}};  
                    int y_ = y * (int)Math.Sin(angle * x) * (int)Math.Cos(angle);
                    DrawPixel(x_, y_, Color32, name, modeDraw);
                }
            }
        }*/

        public static void DrawRect(Rectangle rect, Color32 Color32, DrawMode modeDraw)
        {
            DrawRect(rect, Color32, "", modeDraw);
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
                    System.Drawing.Color32 imaColor32 = ima.GetPixel(x, y);
                    Color32 Color32 = new Color32(imaColor32.R, imaColor32.G, imaColor32.B);
                    DrawPixel(x, y, Color32);
                }
            }
            ima.Dispose();
        }*/

        public static void Clear(Color32 Color32, DrawMode modeDraw)
        {
            if (modeRender == RenderMode.DirectX)
            {
                if (modeDraw == DrawMode.DIRECT)
                {
                    SharpDX.Mathematics.Interop.RawColor4 clearColor32 = new SharpDX.Mathematics.Interop.RawColor4(Color32.R, Color32.G, Color32.B, 1);
                    libraryGet("SharpDX").getRenderTarget().Clear(clearColor32);
                }
                else
                {
                    ScreenRender.Clear();
                    Clear(Color32, DrawMode.DIRECT);
                }
            }
            else if(modeRender == RenderMode.Software)
            {
                //renderSoftware.g.Clear(System.Drawing.Color32.Black);
                Console.WriteLine("Software mode is deprecated.");
            }else if(modeRender == RenderMode.Vulkan)
            {

            }
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, Color32 Color32, string name, DrawMode modeDraw)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), Color32, name, modeDraw);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, int r, int g, int b, string name, DrawMode modeDraw)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), new Color32(r, g, b, 1), name, modeDraw);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, int r, int g, int b, DrawMode modeDraw)
        {
            DrawRect(x, y, sizex, sizey, r, g, b, "", modeDraw);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, Color32 Color32, DrawMode modeDraw)
        {
            DrawRect(x, y, sizex, sizey, Color32, modeDraw);
        }
    }
}

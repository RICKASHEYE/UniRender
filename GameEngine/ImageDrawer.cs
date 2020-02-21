using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GameEngine
{
    public class ParEngineImage
    {
        public Bitmap map_;
        public string name;

        public ParEngineImage(Bitmap map, string name_)
        {
            map_ = map;
            name = name_;
        }
    }

    public static class ImageDrawer
    {
        public static List<ParEngineImage> CachedImages = new List<ParEngineImage>();

        public static void DrawImage(string path, System.Drawing.Point point, System.Drawing.Point scale, Graphics g)
        {
            try
            {
                string filename = Path.GetFileName(path);
                if (!CachedImageExists(filename))
                {
                    if (File.Exists(path))
                    {
                        Image image = Image.FromFile(path);
                        if (image != null)
                        {
                            //Scan the image for white points and if white points we want to remove them
                            Bitmap map = ResizeImage(image, scale.X, scale.Y);
                            CachedImages.Add(new ParEngineImage(map, filename));
                            DrawImage(path, point, scale, g);
                        }
                        else
                        {
                            Console.WriteLine("Image is null drawing something else");
                            DrawReplacement(System.Drawing.Color.Black, new System.Drawing.Rectangle(point.X, point.Y, scale.X, scale.Y), g);
                        }
                    }
                    else
                    {
                        Console.WriteLine("File doesnt exist! drawing rectangle...");
                        DrawReplacement(System.Drawing.Color.Black, new System.Drawing.Rectangle(point.X, point.Y, scale.X, scale.Y), g);
                    }
                }
                else
                {
                    Console.WriteLine("Found cached image... drawing...");
                    g.DrawImage(getImage(filename).map_, point);
                }
            }
            catch (Exception)
            {
                //Draw a box instead
                DrawReplacement(System.Drawing.Color.Black, new System.Drawing.Rectangle(point.X, point.Y, scale.X, scale.Y), g);
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static ParEngineImage getImage(string name)
        {
            ParEngineImage imageGet = null;
            foreach(ParEngineImage m in CachedImages)
            {
                if(m.name == name)
                {
                    imageGet = m;
                }
            }
            return imageGet;
        }

        public static bool CachedImageExists(string name)
        {
            bool exists = false;
            foreach(ParEngineImage image in CachedImages)
            {
                if(image.name == name)
                {
                    exists = true;
                }
            }
            return exists;
        }

        public static void DrawReplacement(System.Drawing.Color color, System.Drawing.Rectangle rect, Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), rect);
        }
    }
}

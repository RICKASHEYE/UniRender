using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class Pixel
    {
        public int X;
        public int Y;
        public Color32 color;
        public Vector2 previousDest;
        public string name_;
        public bool isCollidable;

        public Pixel(int x_, int y_, Color32 color_, string name)
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

        public void DrawPixel()
        {
            if (isCollidable)
            {
                bool overlay = false;
                foreach (Pixel p in Canvas.ScreenRender)
                {
                    if (p.name_ != name_)
                    {
                        Vector2 pixelPosition = new Vector2(p.X, p.Y);
                        Vector2 currentPixelPosition = new Vector2(X, Y);
                        if (pixelPosition == currentPixelPosition)
                        {
                            overlay = true;
                        }
                    }
                }
                if (overlay == true)
                {
                    MovePixel((int)previousDest.x, (int)previousDest.y);
                }
            }
            Canvas.DirectDrawPixel(X, Y, color);
        }
    }
}

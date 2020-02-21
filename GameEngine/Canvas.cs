using System.Collections.Generic;

namespace GameEngine
{
    public class Pixel
    {
        public int X;
        public int Y;
        public Color color;
        public string namePixel;

        public Pixel(int x_, int y_, Color color_, string name_)
        {
            X = x_;
            Y = y_;
            color = color_;
            namePixel = name_;
        }
    }

    public class Canvas
    {
        //Draw a rectangle or a screen
        public static List<Pixel> ScreenRender = new List<Pixel>();
        
        public static void DrawPixel(int x, int y, Color color, string name)
        {
            ScreenRender.Add(new Pixel(x, y, color, name));
        }

        public static void DrawRect(Rectangle rect, Color color, string name)
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

        public static void MoveAllPixels(Vector2 direction, string blackList)
        {
            foreach(Pixel m in ScreenRender)
            {
                if (m.namePixel != blackList)
                {
                    m.X = m.X + (int)direction.x;
                    m.Y = m.Y + (int)direction.y; 
                }
            }
        }

        public static void ClearPixels(string name)
        {
            foreach(Pixel m in ScreenRender.ToArray())
            {
                if(m.namePixel == name)
                {
                    ScreenRender.Remove(m);
                }
            }
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, Color color, string name)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), color, name);
        }

        public static void DrawRect(int x, int y, int sizex, int sizey, int r, int g, int b, string name)
        {
            DrawRect(new Rectangle(sizex, sizey, x, y), new Color(r, g, b), name);
        }
    }
}

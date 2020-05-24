namespace SubrightEngine.Types
{
    public class Rectangle
    {
        public int posx;
        public int posy;
        public int sizex, sizey;

        public Rectangle(int _x, int _y, int _xPos, int _yPos)
        {
            posx = (int)_x;
            posy = (int)_y;
            sizex = (int)_xPos;
            sizey = (int)_yPos;
        }

        public Rectangle(Vector2 size_, Vector2 pos_)
        {
            posx = (int)pos_.x;
            posy = (int)pos_.y;
            sizex = (int)size_.x;
            sizey = (int)size_.y;
        }

        public Rectangle(int _x, int _y, Vector2 pos)
        {
            posx = (int)pos.x;
            posy = (int)pos.y;
            sizex = (int)_x;
            sizey = (int)_y;
        }

        public Rectangle(Vector2 size_, int _xPos, int _yPos)
        {
            posx = _xPos;
            posy = _yPos;
            sizex = (int)size_.x;
            sizey = (int)size_.y;
        }

        public static Rectangle Zero { get; set; } = new Rectangle(0, 0, 0, 0);
    }
}

namespace GameEngine
{
    public class Rectangle
    {
        public int posx;
        public int posy;
        public int sizex, sizey;

        public Rectangle(float _x, float _y, float _xPos, float _yPos)
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

        public static Rectangle Zero { get; set; } = new Rectangle(0, 0, 0, 0);
    }
}

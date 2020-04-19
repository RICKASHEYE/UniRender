using SubrightEngine;

namespace SubrightWindow
{
    public class Apple
    {
        public Vector2 sPosition;

        public Apple(Vector2 position)
        {
            sPosition = position;
        }

        public void RenderApple()
        {
            Canvas.DrawCircle(Color.Ruby, sPosition, 4);
        }
    }
}

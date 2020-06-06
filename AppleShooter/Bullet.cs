using SubrightEngine;
using SubrightEngine.Types;

namespace SubrightWindow
{
    public class Bullet
    {
        public Vector2 positionStart;
        public Vector2 brotation;

        public Bullet(Vector2 startingPosition, Vector2 rotation)
        {
            //Use the starting position to operate the bullet across the screen.
            positionStart = startingPosition;
            brotation = rotation;
        }

        public void RenderBullet()
        {
            positionStart = new Vector2(positionStart.x + (brotation.x * 8), positionStart.y + (brotation.y * 8));
            Canvas.DrawCircle(Color32.Black, positionStart, 4, DrawMode.DIRECT);
        }
    }
}

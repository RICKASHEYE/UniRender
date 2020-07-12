using SubrightEngine;
using SubrightEngine.Types;

namespace SubrightWindow
{
    public class Bullet : Object
    {
        public Bullet(Vector2 position, Vector2 rotation) : base(position, rotation) { }
        public override void Render()
        {
            positionStart = new Vector2(positionStart.x + (brotation.x * 8), positionStart.y + (brotation.y * 8));
            Canvas.DrawCircle(Color32.Black, positionStart, 4, DrawMode.DIRECT);
        }
    }
}

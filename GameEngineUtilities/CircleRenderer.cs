using SubrightEngine;

namespace SubrightEngineUtil
{
    public class CircleRenderer : Renderer
    {
        public Color color;
        public Vector2 position;
        public int radius;

        public CircleRenderer(Vector2 p, Color c, int r) : base("Circle Renderer")
        {
            position = p;
            color = c;
            radius = r;
        }

        public override void Update()
        {
            //Canvas.DrawCircle(color, (int)position.x, (int)position.y, radius, name);
            base.Update();
        }
    }
}

using GameEngine;

namespace GameEngineUtil
{
    public class RectangleRenderer : Renderer
    {
        public Color color;

        public RectangleRenderer(Rectangle rect_, Color color_) : base("Rectangle Renderer")
        {
            rect = rect_;
            color = color_;
        }

        public override void Update()
        {
            Canvas.DrawRect(rect, color, name);
            base.Update();
        }
    }
}

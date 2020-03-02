using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Organisation
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

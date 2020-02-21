using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Organisation
{
    public class RectangleRenderer : Component
    {
        public Rectangle rect;
        public Color color;

        public RectangleRenderer(Rectangle rect_, Color color_)
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

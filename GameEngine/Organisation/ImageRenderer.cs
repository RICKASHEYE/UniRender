using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Organisation
{
    public class ImageRenderer : Component
    {
        public Rectangle rect;
        public ParEngineImage image;

        public ImageRenderer(Rectangle rect_, ParEngineImage image_)
        {
            rect = rect_;
            image = image_;
        }

        public override void Update()
        {
            Canvas.DrawImage(rect, image, name);
            base.Update();
        }
    }
}

using GameEngine;

namespace GameEngineUtil
{
    public class ImageRenderer : Renderer
    {
        public ParEngineImage image;

        public ImageRenderer(Rectangle rect_, ParEngineImage image_) : base("Image Renderer")
        {
            rect = rect_;
            image = image_;
        }

        public override void Update()
        {
            //Canvas.DrawImage(rect, image, name);
            base.Update();
        }
    }
}

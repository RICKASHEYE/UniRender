using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Organisation
{
    public class BoxCollider : Component
    {
        public Renderer render;

        public BoxCollider ():base("Box Collider")
        {

        }

        public override void Run()
        {
            base.Run();
        }

        //One day.. an editor!!!!
        public virtual void OnColliderEnter()
        {
            //Check if another group of pixels is overlapping this one!
            List<PixelObjects> pixelGroup = Canvas.pixelObjects;
            PixelObjects selectedObject = null;
            foreach(PixelObjects objectsPixel in pixelGroup)
            {
                if(objectsPixel.name != render.parent.name_)
                {
                    //Then avoid this one
                    if (parent.X <= objectsPixel.pixels[objectsPixel.pixels.Count].X && parent.X >= objectsPixel.pixels[0].X && parent.Y <= objectsPixel.pixels[objectsPixel.pixels.Count].Y && parent.Y >= objectsPixel.pixels[0].Y)
                    {
                        Canvas.MovePixelGroup(new Vector2(parent.X - 1, parent.Y - 1), render.parent.name_); 
                    }
                }
            }
        }
    }
}

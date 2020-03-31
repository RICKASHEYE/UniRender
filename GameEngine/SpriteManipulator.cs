using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class SpriteManipulator
    {
        public static ParEngineImage cutImage(int posX, int posY, int sizeX, int sizeY, ParEngineImage image)
        {
            //We want to cut the image and return a value
            ParEngineImage currentImage = image;
            currentImage.map_ = ImageDrawer.ResizeImage(image.map_, posX, posY, sizeX, sizeY);
            return currentImage;
        }
    }
}

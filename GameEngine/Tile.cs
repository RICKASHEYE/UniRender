using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Tile
    {
        //Usually used when the user does not want certain objects to pass through or have a tile based game instead.
        public string name = "unnamed tile";
        public ParEngineImage tileImage;
        public Vector2 tilePos;

        public Tile(string n, ParEngineImage imageAttached, Vector2 storedTilePos)
        {
            name = n;
            tileImage = imageAttached;
            tilePos = storedTilePos;
        }
    }
}

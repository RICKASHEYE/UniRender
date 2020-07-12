using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightWindow
{
    public class Object
    {
        public Vector2 positionStart;
        public Vector2 brotation;

        public Object(Vector2 startingPosition, Vector2 rotation)
        {
            positionStart = startingPosition;
            brotation = rotation;
        }

        public virtual void Render()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SubrightEngine.Types
{
    public class Vector
    {
        public int x;
        public int y;
        public int z;

        /// <summary>
        /// The General Vector class to be a modular object for Vector 2, Vector 3 and Vector4
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// The General Vector class to be a modular object for Vector 2, Vector 3 and Vector4
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

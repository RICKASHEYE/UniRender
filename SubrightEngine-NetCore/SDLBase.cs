using System;
using System.IO;
using SubrightEngine.Types;
using System.Drawing;

namespace SubrightEngine.DirectX
{
    public class SDLBase : IRenderingLibrary
    {
        private const int wwidth = 1024;
        private const int wheight = 768;
        private static Surface Screen;

        protected void Draw(AppTime time)
        {
            DrawEvent?.Invoke(time);
        }

        public delegate void DrawEventHandler(AppTime time);
        public static event DrawEventHandler DrawEvent;

        public string renderName = "SharpDX";
        public string getName()
        {
            return renderName;
        }

        public SDLBase(string name)
        {
            renderName = name;
        }

        public void Draw()
        {
            //throw new NotImplementedException();
        }

        public void Initialise(AppConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}

using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vulkan;

namespace SubrightEngine.DirectX
{
    public class SharpDXBase : Direct2D1Handler, IRenderingLibrary
    {
        protected override void Draw(AppTime time)
        {
            base.Draw(time);
            DrawEvent?.Invoke(time);
        }

        public delegate void DrawEventHandler(AppTime time);
        public static event DrawEventHandler DrawEvent;

        public string renderName = "SharpDX";
        public string getName()
        {
            return renderName;
        }

        public SharpDXBase (string name)
        {
            renderName = name;
        }

        public void Initialise(AppConfiguration configuration)
        {
            SharpDXBase baseScript = new SharpDXBase(renderName);
            baseScript.Run(configuration);
        }

        public void Intialise(AppConfiguration config)
        {
            Initialize(config);
        }

        protected override void KeyDown(KeyEventArgs e)
        {
            base.KeyDown(e);
            foreach (KeyCode coedes in RenderingLibraryManager.codes)
            {
                if (coedes.KeyUsePositive != null)
                {
                    if (e.KeyCode == coedes.KeyUsePositive)
                    {
                        coedes.keyAxis = 1;
                        // Debug.Log("Positive");
                    }
                }

                if (coedes.KeyUseNegative != null)
                {
                    if (e.KeyCode == coedes.KeyUseNegative)
                    {
                        coedes.keyAxis = -1;
                        //Debug.Log("Negative");
                    }
                }
            }
        }

        protected override void KeyUp(KeyEventArgs e)
        {
            base.KeyUp(e);
            foreach (KeyCode coedes in RenderingLibraryManager.codes)
            {
                if (coedes.keyAxis != 0)
                {
                    coedes.keyAxis = 0;
                    //Debug.Log("Key Up");
                }
            }
        }

        protected override void Initialize(AppConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
        }

        public RenderTarget getRenderTarget()
        {
            return RenderTarget2D;
        }

        public void Draw()
        {
            //throw new NotImplementedException();
        }
    }
}

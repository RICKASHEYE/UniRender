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
        public void Draw()
        {
            Draw();
        }

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

        public void OnKeyDown(KeyEventArgs e)
        {
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

        public void OnKeyUp(KeyEventArgs e)
        {
            foreach (KeyCode coedes in RenderingLibraryManager.codes)
            {
                if (coedes.keyAxis != 0)
                {
                    coedes.keyAxis = 0;
                    //Debug.Log("Key Up");
                }
            }
        }

        protected override void Draw(AppTime time)
        {
            base.Draw(time);
        }

        protected override void Initialize(AppConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
        }

        public RenderTarget getRenderTarget()
        {
            return RenderTarget2D;
        }
    }
}

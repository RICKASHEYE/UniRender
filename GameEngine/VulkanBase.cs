using SharpDX.Direct2D1;
using System.Windows.Forms;
using Vulkan;

namespace SubrightEngine.VulkanBranch
{
    public class VulkanBase : VulkanRenderP, IRenderingLibrary
    {
        public void Draw()
        {
            DrawFrame();
        }

        public override void DrawFrame()
        {
            //throw new System.NotImplementedException();
            DrawEvent?.Invoke();
        }

        public delegate void DrawEventHandler();
        public static event DrawEventHandler DrawEvent;

        public string renderName = "Vulkan";
        public VulkanBase(string name)
        {
            renderName = name;
        }

        public string getName()
        {
            return renderName;
        }

        public void Initialise(AppConfiguration configuration)
        {
            var form = new Form();
            form.Controls.Add(new VulkanC(this) { Dock = DockStyle.Fill });
            form.Text = configuration.Title;
            form.Size = new System.Drawing.Size(configuration.Width, configuration.Height);
            Application.Run(form);
            //throw new System.NotImplementedException();
            //Debug.Log("You're initialising the app through DirectX mode. but this is vulkan so converting i guess");
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

        public RenderTarget getRenderTarget()
        {
            throw new System.NotImplementedException();
        }
    }
}

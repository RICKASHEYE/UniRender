using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vulkan;

namespace SubrightEngine
{
    public interface IRenderingLibrary
    {
        void Initialise(AppConfiguration configuration);
        RenderTarget getRenderTarget();
        void Draw();
        string getName();
    }
}

using System.Windows.Forms;
using Vulkan;

namespace SubrightEngine.VulkanBranch
{
    public interface IVulkanRender
    {
        void DrawFrame(PaintEventArgs e);
        void Initialize(PhysicalDevice physicalDevice, SurfaceKhr surface);
    }
}

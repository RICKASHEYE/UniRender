using Vulkan;

namespace SubrightEngine.VulkanBranch
{
    public interface IVulkanRender
    {
        void DrawFrame();
        void Initialize(PhysicalDevice physicalDevice, SurfaceKhr surface);
    }
}

using System;
using System.Windows.Forms;
using Vulkan;
using Vulkan.Windows;

namespace SubrightEngine.VulkanBranch
{
	public class VulkanC : VulkanControl
	{
		private IVulkanRender _vulkanSample;
		private PhysicalDevice _physicalDevice;

		public VulkanC(IVulkanRender vulkanSample)
		{
			_vulkanSample = vulkanSample;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			_physicalDevice = Instance.EnumeratePhysicalDevices()[0];
			_vulkanSample.Initialize(_physicalDevice, Surface);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			_vulkanSample.DrawFrame();
		}
	}
}

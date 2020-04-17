using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;

namespace GameEngine
{
    /// <summary>
    /// Root class for Direct2D and DirectWrite Demo App.
    /// </summary>
    public class Direct2D1Handler : Direct3D11Handler
    {
        public Factory Factory2D { get; private set; }
        public static SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public static RenderTarget RenderTarget2D;
        public SolidColorBrush SceneColorBrush { get; private set; }

        protected override void Initialize(AppConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
            Factory2D = new SharpDX.Direct2D1.Factory();
            using (var surface = BackBuffer.QueryInterface<Surface>())
            {
                RenderTarget2D = new RenderTarget(Factory2D, surface,
                                                  new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            }
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, new SharpDX.Mathematics.Interop.RawColor4(1, 1, 1, 100));
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();
            RenderTarget2D.BeginDraw();
        }

        protected override void EndDraw()
        {
            RenderTarget2D.EndDraw();
            base.EndDraw();
        }
    }
}
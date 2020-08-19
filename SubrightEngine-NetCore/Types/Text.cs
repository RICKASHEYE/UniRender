using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace SubrightEngine.Types
{
    public class Text
    {
        public string text;
        public Vector2 position;
        public TextFormat TextFormat { get; private set; }
        public TextLayout TextLayout { get; private set; }
        public string font_stored;
        public int size_stored;
        public Color color;

        public Text(string textTitle, Vector2 positionPos, RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite, string font, int size, AppConfiguration Config, Color colorP)
        {
            Initialise(textTitle, positionPos, RenderTarget2D, FactoryDWrite, font, size, Config, colorP);
        }

        public void Initialise(string textTitle, Vector2 positionPos, RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite, string font, int size, AppConfiguration Config, Color colorP)
        {
            text = textTitle;
            position = positionPos;

            font_stored = font;
            size_stored = size;

            color = colorP;

            TextFormat = new TextFormat(FactoryDWrite, font, size) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };
            RenderTarget2D.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;
            TextLayout = new TextLayout(FactoryDWrite, textTitle, TextFormat, Config.Width, Config.Height);
        }

        public void Render(RenderTarget RenderTarget2D, SolidColorBrush SceneBrush)
        {
            if (TextFormat == null) { Debug.Error("Text Format has not been initialised as it is null!!!"); }
            if (TextLayout == null) { Debug.Error("Text Layout has not been initialised as it is null!!!"); }
            SharpDX.Mathematics.Interop.RawVector2 vector2 = new SharpDX.Mathematics.Interop.RawVector2(position.x, position.y);
            RenderTarget2D.DrawTextLayout(vector2, TextLayout, SceneBrush, DrawTextOptions.None);
        }
    }
}

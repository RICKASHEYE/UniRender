using SubrightEngine;
using SubrightEngine.Types;

namespace SubrightWindow
{
    public class Apple
    {
        public Vector2 sPosition;

        public Apple(Vector2 position)
        {
            sPosition = position;
        }

        public void RenderApple()
        {
            Canvas.DrawCircle(Color32.Black, sPosition, 4, DrawMode.DIRECT);
            //Canvas.DrawImage((int)sPosition.x *4, (int)sPosition.y * 4, "C:/Users/lethen/Source/Repos/UniRender/GameEngineWindow/bin/Debug/Assets/apple.png", DrawMode.DIRECT);
        }
    }
}

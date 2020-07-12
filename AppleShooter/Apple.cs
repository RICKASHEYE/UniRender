using SubrightEngine;
using SubrightEngine.Types;

namespace SubrightWindow
{
    public class Apple : Object
    {
        public Apple(Vector2 position):base(position, Vector2.zero){}

        public override void Render()
        {
            Canvas.DrawCircle(Color32.Black, positionStart, 4, DrawMode.DIRECT);
            //Canvas.DrawImage((int)sPosition.x *4, (int)sPosition.y * 4, "C:/Users/lethen/Source/Repos/UniRender/GameEngineWindow/bin/Debug/Assets/apple.png", DrawMode.DIRECT);
        }
    }
}

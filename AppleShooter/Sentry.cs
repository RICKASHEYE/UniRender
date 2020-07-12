using SubrightEngine;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightWindow
{
    public class Sentry : Object
    {
        public Sentry(Vector2 position) : base(position, Vector2.zero) { }

        public override void Render()
        {
            Canvas.DrawCircle(Color32.Purple, positionStart, 10, DrawMode.DIRECT);
        }

        Vector2 secondHandBulletStart = Vector2.zero;
        public void Shoot()
        {
            float distance = Vector2.Distance(positionStart, RenderWindow.playerPosition);
            if(distance <= 20)
            {
                //Shoot at the player
                Canvas.DrawCircle(Color32.Red, positionStart, 5, DrawMode.DIRECT);
                if(secondHandBulletStart == Vector2.zero)
                {secondHandBulletStart = positionStart;}
                //brotation = Vector2.Rotate()
                secondHandBulletStart = new Vector2(secondHandBulletStart.x + (brotation.x * 8), secondHandBulletStart.y + (brotation.y * 8));
                Canvas.DrawCircle(Color32.Pink, positionStart, 2, DrawMode.DIRECT);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace DungeonCrawlerDemo
{
    public class Apple : Prop
    {
        public Vector2 position;
        public int randString;

        public Apple(Vector2 position_) : base("Apple")
        {
            position = position_;
            randString = GameEngine.Random.Range(100, 1000);
        }

        public override void Draw()
        {
            //GameEngine.Canvas.DrawCircle(Color.Ruby, position, 10, "Apple " + randString);
        }
    }
}

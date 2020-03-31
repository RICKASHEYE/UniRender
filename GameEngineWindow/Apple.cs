using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace GameEngineWindow
{
    public class Apple
    {
        public Vector2 position;
        public bool deleted;
        public int randString;

        public Apple(Vector2 position_)
        {
            position = position_;
            Random rand = new Random();
            randString = rand.Next(100, 1000);
        }

        public void render()
        {
            if (deleted == false)
            {
                if (Vector2.Distance(Canvas.cameraOffset + position, Form1.playerPosition) <= 10)
                {
                    //delete
                    deleted = true;
                    GameEngine.PlayerValues.SetIntValue("Apples", (int)GameEngine.PlayerValues.GetInteger("Apples") + 1);
                }
                GameEngine.Canvas.DrawCircle(Color.Ruby, position + Canvas.cameraOffset, 10, "Apple " + randString); 
            }else if(deleted == true)
            {
                GameEngine.Canvas.ClearPixels("Apple " + randString);
            }
        }
    }
}

using GameEngine;
using System;

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
            randString = GameEngine.Random.Range(100, 1000);
        }

        public void render(Canvas canvas)
        {
            if (deleted == false)
            {
                if (Vector2.Distance(position, Form1.playerPosition) <= 10)
                {
                    //delete
                    deleted = true;
                    GameEngine.PlayerValues.SetIntValue("Apples", (int)GameEngine.PlayerValues.GetInteger("Apples") + 1);
                }
                canvas.DrawCircle(Color.Ruby, position, 10, "Apple " + randString);
            }
            else if (deleted == true)
            {
                canvas.ClearPixels("Apple " + randString);
            }
        }
    }
}
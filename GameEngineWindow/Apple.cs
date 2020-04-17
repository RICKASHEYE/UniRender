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
    }
}
using System;

namespace GameEngine
{
    public static class Random
    {
        public static int Range(int start, int end)
        {
            int finalInteger = 0;
            System.Random rnd = new System.Random();
            finalInteger = rnd.Next(start, end);
            return finalInteger;
        }

        public static int Range(int end)
        {
            return Range(0, end);
        }
    }
}

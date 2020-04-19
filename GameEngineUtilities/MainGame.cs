using SubrightEngine;

namespace SubrightEngineUtil
{
    public static class MainGame
    {
        public static void Update()
        {
            //Update the game
            if (GameObjectHandler.gameObjects != null)
            {
                foreach (GameObject gameObjects in GameObjectHandler.gameObjects)
                {
                    gameObjects.Update();
                } 
            }
        }

        public static void Run()
        {
            //Run the game
            //Run the necessary components too
            if (GameObjectHandler.gameObjects != null)
            {
                foreach (GameObject gameobjects in GameObjectHandler.gameObjects)
                {
                    gameobjects.Run();
                }
            }
            else
            {
                Debug.Error("No GameObjects to run yet!");
            }
        }

        public static void Unload()
        {
        }
    }
}

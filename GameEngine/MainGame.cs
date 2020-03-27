using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class MainGame
    {
        public static void Update()
        {
            //Update the game
            if (GameObjectHandler.gameObjects != null)
            {
                foreach (Organisation.GameObject gameObjects in GameObjectHandler.gameObjects)
                {
                    gameObjects.Update();
                } 
            }
        }

        public static void Run()
        {
            //Run the game
            //Run the necessary components too
            PluginLoader.LoadPlugins();
            if (GameObjectHandler.gameObjects != null)
            {
                foreach (Organisation.GameObject gameobjects in GameObjectHandler.gameObjects)
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
            PluginLoader.UnloadPlugins();
        }
    }
}

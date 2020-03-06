using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class GameObjectHandler
    {
        public static List<GameEngine.Organisation.GameObject> gameObjects;

        public static void CreateGameObject(string name)
        {
            if (!gameObjectExists(name))
            {
                if (gameObjects == null)
                {
                    gameObjects = new List<GameEngine.Organisation.GameObject>();
                    GameEngine.Debug.Log("Creating a new list of gameObjects");
                }
                GameEngine.Organisation.GameObject newObject = new GameEngine.Organisation.GameObject(name);
                gameObjects.Add(newObject);
                //GameEngine.Debug.Log("Added new Game Object");
            }
            else
            {
                int countOfObjects = countObjectSame(name);
                string finalName = name + "(COPY)";
                GameEngine.Debug.Error("GameObject of the same name exists! but then creating a new object with the name of: " + finalName);
                CreateGameObject(finalName);
            }

            if (gameObjects != null)
            {
                GameEngine.Debug.Log("Total list of " + gameObjects.Count + " gameObjects!");
            }
        }

        public static int countObjectSame(string name)
        {
            int finalCount = 0;
            foreach(GameEngine.Organisation.GameObject gameObject in gameObjects)
            {
                if(gameObject.name_ == name)
                {
                    finalCount++;
                }
            }
            return finalCount;
        }

        public static void RemoveGameObject(string name)
        {
            if (gameObjects != null)
            {
                if (gameObjectExists(name))
                {
                    //Remove the gameobject
                    GameEngine.Organisation.GameObject objectReference = getGameObject(name);
                    gameObjects.Remove(objectReference);
                }
                else
                {
                    GameEngine.Debug.Error("Game Object does not exist! unable to remove.");
                }
            }
            else
            {
                GameEngine.Debug.Error("No GameObjects exist so how is one supposed to be removed!");
            }
        }

        public static GameEngine.Organisation.GameObject returnObjectFromComponent(Organisation.Component com)
        {
            return com.parent;
        }

        public static bool gameObjectExists(string name)
        {
            //The gameobject exists or not?
            bool exists = false;
            if (gameObjects != null)
            {
                foreach (GameEngine.Organisation.GameObject ObjectGame in gameObjects)
                {
                    if (ObjectGame.name_ == name)
                    {
                        exists = true;
                    }
                }
            }
            else
            {
                GameEngine.Debug.Log("GameObjects are non existant at this moment");
            }
            return exists;
        }

        public static GameEngine.Organisation.GameObject getGameObject(string name)
        {
            GameEngine.Organisation.GameObject gameObject = null;
            if (gameObjects != null)
            {
                foreach (GameEngine.Organisation.GameObject ObjectGame in gameObjects)
                {
                    if (ObjectGame.name_ == name)
                    {
                        gameObject = ObjectGame;
                    }
                }
            }
            else
            {
                GameEngine.Debug.Log("GameObjects are non existant at this moment");
            }
            return gameObject;
        }
    }
}

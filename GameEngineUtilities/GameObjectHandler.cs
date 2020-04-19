using System.Collections.Generic;
using SubrightEngine;

namespace SubrightEngineUtil
{
    public static class GameObjectHandler
    {
        public static List<GameObject> gameObjects;

        public static void CreateGameObject(string name)
        {
            if (!gameObjectExists(name))
            {
                if (gameObjects == null)
                {
                    gameObjects = new List<GameObject>();
                    Debug.Log("Creating a new list of gameObjects");
                }
                GameObject newObject = new GameObject(name);
                gameObjects.Add(newObject);
                //GameEngine.Debug.Log("Added new Game Object");
            }
            else
            {
                int countOfObjects = countObjectSame(name);
                string finalName = name + "(COPY)";
                Debug.Error("GameObject of the same name exists! but then creating a new object with the name of: " + finalName);
                CreateGameObject(finalName);
            }

            if (gameObjects != null)
            {
                Debug.Log("Total list of " + gameObjects.Count + " gameObjects!");
            }
        }

        public static int countObjectSame(string name)
        {
            int finalCount = 0;
            foreach(GameObject gameObject in gameObjects)
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
                    GameObject objectReference = getGameObject(name);
                    gameObjects.Remove(objectReference);
                }
                else
                {
                    Debug.Error("Game Object does not exist! unable to remove.");
                }
            }
            else
            {
                Debug.Error("No GameObjects exist so how is one supposed to be removed!");
            }
        }

        public static GameObject returnObjectFromComponent(Component com)
        {
            return com.parent;
        }

        public static bool gameObjectExists(string name)
        {
            //The gameobject exists or not?
            bool exists = false;
            if (gameObjects != null)
            {
                foreach (GameObject ObjectGame in gameObjects)
                {
                    if (ObjectGame.name_ == name)
                    {
                        exists = true;
                    }
                }
            }
            else
            {
                Debug.Log("GameObjects are non existant at this moment");
            }
            return exists;
        }

        public static GameObject getGameObject(string name)
        {
            GameObject gameObject = null;
            if (gameObjects != null)
            {
                foreach (GameObject ObjectGame in gameObjects)
                {
                    if (ObjectGame.name_ == name)
                    {
                        gameObject = ObjectGame;
                    }
                }
            }
            else
            {
                Debug.Log("GameObjects are non existant at this moment");
            }
            return gameObject;
        }
    }
}

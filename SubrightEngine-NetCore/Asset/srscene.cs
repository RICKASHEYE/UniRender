using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;

namespace SubrightEngine.Asset
{
    public class srscene
    {
        //Subright Engine Scene File
        public void LoadScene(string path)
        {
            if (File.Exists(path))
            {
                string fileLines = File.ReadAllText(path);
                //Load the gameObjects
                srscene scene = JsonConvert.DeserializeObject<srscene>(fileLines);
                gameObjects.AddRange(scene.gameObjects);
            }
            else
            {
                Debug.Error("File does not exist!");
            }
        }

        public void SaveScene(string path)
        {
            if (File.Exists(path))
            {
                string file = JsonConvert.SerializeObject(this);
                File.WriteAllText(path, file);
            }
            else
            {
                Debug.Error("File does not exist... creating...");
                File.Create(path);
                //Reload
                SaveScene(path);
            }
        }

        //Store the gameObjects into the scene
        public List<GameObject> gameObjects = new List<GameObject>();

        public void AddGameObject(GameObject m)
        {
            if (!checkExists(m))
            {
                gameObjects.Add(m);
            }
            else
            {
                Debug.Error("Game object already exists!");
            }
        }

        public void RemoveGameObject(GameObject n)
        {
            if (checkExists(n))
            {
                gameObjects.Remove(n);
            }
            else
            {
                Debug.Error("Game object does not exist!");
            }
        }

        public bool checkExists(GameObject n)
        {
            bool exists = false;
            foreach(GameObject m in gameObjects)
            {
                if(m == n)
                {
                    exists = true;
                }
            }
            return exists;
        }
    }
}

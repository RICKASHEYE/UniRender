using SubrightEngine.Asset;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class ProjectSettings
    {
        //Mostly what controls the game to a certain point

        public string gameName = "Default Name";
        public int buildNo = 1;

        public List<srscene> scenes = new List<srscene>();
        public srscene sceneLoaded;

        public void LoadScene(srscene scene)
        {
            //Loads the scene into the game container!
            if(sceneLoaded != null)
            {
                //Unload that current scene
                foreach(GameObject gObject in sceneLoaded.gameObjects)
                {
                    gObject.UnloadGameObject();
                }
            }

            sceneLoaded = null;

            sceneLoaded = scene;
        }

        public void LoadScene(int index)
        {
            //Loads a scene by a number
            if (scenes.Count > index)
            {
                srscene scenetoload = null;
                for (int i = 0; i < scenes.Count; i++)
                {
                    if (i == index)
                    {
                        scenetoload = scenes[i];
                    }
                }
                LoadScene(scenetoload);
            }
            else
            {
                Debug.Error("Out of index for scene choice as there is " + scenes.Count + " and you picked " + index);
            }
        }

        public void AddScene(srscene scene)
        {
            //Add the scene
            if (!checkExists(scene))
            {
                scenes.Add(scene);
            }
            else
            {
                Debug.Error("Scene already exists!");
            }
        }

        public void RemoveScene(srscene scene)
        {
            //remove the scene
            if (checkExists(scene))
            {
                scenes.Remove(scene);
            }
            else
            {
                Debug.Error("Scene doesnt exist!");
            }
        }

        public bool checkExists(srscene scene)
        {
            bool exists = false;
            foreach(srscene sc in scenes)
            {
                if(sc == scene)
                {
                    exists = true;
                }
            }
            return exists;
        }
    }
}

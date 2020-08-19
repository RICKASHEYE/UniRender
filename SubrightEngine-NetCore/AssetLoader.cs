using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class AssetLoader : Canvas
    {
        //This is the asset loader for the engine it will replace the main class!
        public static List<Texture2D> textures = new List<Texture2D>();

        public override void MiddleInit()
        {
            base.MiddleInit();
            //This will run the intiialisation method!
            //We want to load all of the assets here!
            Debug.Log("Checking if assets folder exists!");
            string assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            Debug.Log(assetsDir);
            if (!Directory.Exists(assetsDir))
            {
                Directory.CreateDirectory(assetsDir);
            }


            //Checking if there is content inside assets!
            Debug.Log("Checking for content inside assets!");
            foreach(string file in Directory.GetFiles(assetsDir))
            {
                Debug.Log("Asset Load: " + file);
                Texture2D text = new Texture2D(file);
                textures.Add(text);
            }
        }

        public static Texture2D getTexture(string name)
        {
            Texture2D usedTexture = null;
            foreach(Texture2D texture in textures)
            {
                string findablePath = Path.GetFileNameWithoutExtension(texture.name);
                Debug.Log("finding: " + findablePath);
                if(Path.GetFileNameWithoutExtension(texture.name) == name)
                {
                    usedTexture = texture;
                }
            }

            if(usedTexture == null)
            {
                Debug.Error("Texture does not exist!");
            }
            return usedTexture;
        }
    }
}

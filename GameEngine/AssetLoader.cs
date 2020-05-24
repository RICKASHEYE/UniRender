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
        public List<Texture> textures = new List<Texture>();

        public override void Initialize(AppConfiguration config)
        {
            base.Initialize(config);
            //This will run the intiialisation method!
            //We want to load all of the assets here!
            string assetsDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            if (!Directory.Exists(assetsDir))
            {
                Directory.CreateDirectory(assetsDir);
            }

            foreach(string file in Directory.GetFiles(assetsDir))
            {
                //Load all of the assets here!
                Console.WriteLine("Asset Load: " + file);
                Texture text = new Texture(file);
                textures.Add(text);
            }
        }
    }
}

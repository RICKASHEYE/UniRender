using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    //Subrights own image class!
    public class Texture
    {
        public System.Drawing.Bitmap map;
        public string name;

        public Texture(string path)
        {
            LoadTexture(path);
            name = Path.GetFileNameWithoutExtension(path);
        }

        public void LoadTexture(string filepath)
        {
            if (File.Exists(filepath))
            {
                map = new System.Drawing.Bitmap(filepath);
            }
            else
            {
                Debug.Log("File does not exist");
                //map = new System.Drawing.Bitmap(null);
            }
        }
    }
}

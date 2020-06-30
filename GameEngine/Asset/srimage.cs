using SharpDX.Direct2D1;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Asset
{
    public class srimage
    {
        //Subright Image Reader & Compiler
        public Dictionary<int, Color> colours = new Dictionary<int, Color>();
        public int width;
        public int height;

        //Read a uncompiled image file!
        public void ReadFile(string file)
        {
            if (File.Exists(file))
            {
                string text = File.ReadAllText(file);
                string[] array = text.Split('|');

                //Calculate the width and the height of the image!
                height = array.Length;
                int previousMeasurement = 0;
                foreach(string m in array)
                {
                    string[] split = m.Split('/');
                    if(split.Length > previousMeasurement)
                    {
                        previousMeasurement = split.Length;
                    }
                    else
                    {
                        Debug.Error("Inconsistant Width Detected!");
                    }
                }
                width = previousMeasurement;

                //Plot down the pixels of the image!
                for(int y = 0; y < height; y++)
                {
                    string[] split = array[y].Split('/');
                    if (split.Length > width)
                    {
                        Debug.Error("Inconsistant Width Detected!");
                        return;
                    }

                    for(int x = 0; x < width; x++)
                    {
                        string[] values = split[x].Split(',');
                        //split is the colour array to assort from.
                        colours.Add(y, new Color(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), 255));
                    }
                }
            }
            else
            {
                Debug.Error("File is not avaliable!");
            }
        }

        //Create an image file
        public void CreateImageFile(string file)
        {
        }
    }
}

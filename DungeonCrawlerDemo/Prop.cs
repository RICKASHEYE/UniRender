using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawlerDemo
{
    public class Prop
    {
        public string name = "Unnamed Prop";

        public Prop(string n)
        {
            name = n;
        }

        public virtual void Draw()
        {
            //This method is called everytime the game draws
        }

        public virtual void Start()
        {
            //This method is called everytime the game starts, or well when the item loads in.
        }
    }
}

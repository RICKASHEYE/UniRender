using SubrightEngine;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightWindow
{
    public class Inventory
    {
        //Store all of the inventory items in this!
        List<Item> items = new List<Item>();

        public void AddItem(Item itemAdd)
        {
            //Adds an item into the inventory
            items.Add(itemAdd);
        }

        public void RemoveItem(string nameItem)
        {
            for(int i = 0; i < items.Count; i++)
            {
                //Go through all of the items and find a match to remove
                if(items[i].name == nameItem)
                {
                    items.RemoveAt(i);
                }
            }
        }

        public void RemoveItem(Item item)
        {
            RemoveItem(item.name);
        }

        //Check if the item exists in the inventory!
        public bool itemExists(string nameItem)
        {
            bool result = false;
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].name == nameItem)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool itemExists(Item item)
        {
            return itemExists(item.name);
        }

        public void RenderInventory()
        {
            //Draw an initial rectangle to store the main item in!
            Canvas.DrawRect(new SubrightEngine.Types.Rectangle(new Vector2(30, 30), new Vector2(10, 10)), Color32.Black, DrawMode.DIRECT);
            for(int i = 1; i < items.Count; i++)
            {
                //Draw the remaining inventory icons!
                Canvas.DrawRect(new SubrightEngine.Types.Rectangle(new Vector2(20, 20), new Vector2(10 * i + 3, 10)), Color32.Black, DrawMode.DIRECT);
            }
        }
    }
}

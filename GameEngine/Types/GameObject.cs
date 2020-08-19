using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class GameObject
    {
        //A class that can store information like types and rendering stuff.
        public List<Type> types = new List<Type>();
        public string name;

        public void AddType(Type type)
        {
            //Add the type
            if (!checkTypeExists(type))
            {
                types.Add(type);
            }
            else
            {
                Debug.Error("Type already exists!");
            }
        }

        public void RemoveType(Type type)
        {
            //Remove the type
            if (checkTypeExists(type))
            {
                types.Remove(type);
            }
            else
            {
                Debug.Error("Type does not exist!");
            }
        }

        public void UnloadGameObject()
        {

        }

        public bool checkTypeExists(Type type)
        {
            bool exists = false;
            foreach(Type m in types)
            {
                if(m.Name == type.Name)
                {
                    //it exists!
                    exists = true;
                }
            }
            return exists;
        }
    }
}

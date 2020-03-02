using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class ComponentHandler
    {
        public static void AddComponent(Organisation.Component com, Organisation.GameObject addableGameObject)
        {
            //Add the component
            if (!ComponentExists(com.name, addableGameObject))
            {
                addableGameObject.RegisterComponent(com);
            }
            else
            {
                Debug.Error("Component already exists on this object...");
            }
        }

        public static void RemoveComponent(Organisation.Component com, Organisation.GameObject removableGameObject)
        {
            //Remove the component on the object
            if (ComponentExists(com.name, removableGameObject))
            {
                removableGameObject.DeRegisterComponent(com);
            }
            else
            {
                Debug.Error("Component doesnt exist!");
            }
        }

        public static bool ComponentExists(string name, Organisation.GameObject parent)
        {
            bool exists = false;
            List<Organisation.Component> coms = parent.components;
            foreach(Organisation.Component com in coms)
            {
                if(com.name == name)
                {
                    exists = true;
                }
            }
            return exists;
        }

        public static bool comHasGameObject(Organisation.Component com)
        {
            bool exists = false;
            if(com.parent != null) { exists = true; }
            return exists;
        }

        public static Organisation.Component FindCom(string name, Organisation.GameObject parent)
        {
            Organisation.Component com = null;
            foreach(Organisation.Component coms in parent.components)
            {
                if(com.name == name)
                {
                    com = coms;
                }
            }
            return com;
        }
    }
}

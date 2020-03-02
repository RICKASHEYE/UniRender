using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Organisation
{
    public class Component
    {
        public string name;
        public GameObject parent;

        public Component(string name_)
        {
            name = name_;
        }

        public virtual void Run()
        {

        }

        public virtual void Update()
        {

        }

        public Component GetComponent<T>(string name)
        {
            Component comFound = null;
            foreach(Component com in parent.components)
            {
                if(com.name == name)
                {
                    comFound = com;
                }
            }
            return comFound;
        }

        public Component AddComponent<T>(Component component)
        {
            parent.RegisterComponent(component);
            return GetComponent<T>(component.name);
        }
    }
}

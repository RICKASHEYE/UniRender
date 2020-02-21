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

        public Component()
        {
            name = nameof(parent);
        }

        public virtual void Run()
        {

        }

        public virtual void Update()
        {

        }

        public Component GetComponent(Component component)
        {
            Component comFound = null;
            foreach(Component com in parent.components)
            {
                if(com.name == component.name)
                {
                    comFound = com;
                }
            }
            return comFound;
        }

        public Component AddComponent(Component component)
        {
            parent.RegisterComponent(component);
            return GetComponent(component);
        }
    }
}

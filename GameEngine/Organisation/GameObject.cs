using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Organisation
{
    public class GameObject
    {
        public int X;
        public int Y;
        public List<Component> components = new List<Component>();
        public string name_;

        public GameObject (string name)
        {
            name_ = name;
        }

        public void Update()
        {
            if (components != null)
            {
                foreach (Component com in components)
                {
                    com.Update();
                } 
            }
        }

        public void Run()
        {
            if (components != null)
            {
                foreach (Component com in components)
                {
                    com.Run();
                }
            }
            else
            {
                Debug.Error("No Components to run yet...");
            }
        }

        public void RegisterComponent(Component com)
        {
            if (comExists(com))
            {
                Console.WriteLine("Component already exists!!!");
            }
            else
            {
                components.Add(com);
            }
        }

        public void DeRegisterComponent(Component com)
        {
            if (!comExists(com))
            {
                Console.WriteLine("Component doesnt exist!!!");
            }
            else
            {
                //Much easier than just removing from list (less prone to errors)
                components.Remove(GetComponent(com.name));
            }
        }

        public Component GetComponent(string name)
        {
            Component foundComponent = null;
            foreach(Component com in components)
            {
                if(name == com.name)
                {
                    foundComponent = com;
                }
            }
            return foundComponent;
        }

        public bool comExists(Component com)
        {
            bool compExists = false;
            foreach(Component com_ in components)
            {
                if(com_.name == com.name)
                {
                    compExists = true;
                }
            }
            return compExists;
        }
    }
}

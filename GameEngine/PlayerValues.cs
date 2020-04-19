using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public static class PlayerValues
    {
        public static Dictionary<string, int> playerValuesInteger = new Dictionary<string, int>();
        public static Dictionary<string, string> playerValuesString = new Dictionary<string, string>();

        public static void SetStringValue(string name, string ChangeValue)
        {
            if (EntryExists(name))
            {
                bool setValue = false;
                foreach (string m in playerValuesString.Keys)
                {
                    if (m == name)
                    {
                        //this is the value
                        playerValuesString[m] = ChangeValue;
                        Console.WriteLine("Found value: " + name + " and replaced the value with " + ChangeValue);
                        setValue = true;
                    }
                }

                if (setValue == false)
                {
                    Console.WriteLine("Unable to find value!");
                }
            }
            else
            {
                playerValuesString.Add(name, ChangeValue);
            }
        }

        public static void SetIntValue(string name, int ChangeValue)
        {
            //Make a clone of the array.
            Dictionary<string, int> playerValuesInteger_ = new Dictionary<string, int>();
            playerValuesInteger_ = playerValuesInteger;
            foreach (string p in playerValuesInteger.Keys)
            {
                playerValuesInteger_.Add(p, playerValuesInteger[p]);
            }

            if (EntryExists(name))
            {
                bool setValue = false;

                //Use that cloned array to be modified....
                foreach (string m in playerValuesInteger_.Keys)
                {
                    if (m == name)
                    {
                        //this is the value
                        playerValuesInteger_[m] = ChangeValue;
                        Console.WriteLine("Found value: " + name + " and replaced the value with " + ChangeValue);
                        setValue = true;
                    }
                }

                if (setValue == false)
                {
                    Console.WriteLine("Unable to find value!");
                }
            }
            else
            {
                playerValuesInteger_.Add(name, ChangeValue);
            }
            playerValuesInteger = playerValuesInteger_;
        }

        public static void RemoveStringValue(string name)
        {
            if (EntryExists(name))
            {
                playerValuesString.Remove(name);
            }
            else
            {
                Console.WriteLine("Entry doesnt exists!");
            }
        }

        public static void RemoveIntValue(string name)
        {
            if (EntryExists(name))
            {
                playerValuesInteger.Remove(name);
            }
            else
            {
                Console.WriteLine("Entry doesnt exists!");
            }
        }

        public static int GetInteger(string name)
        {
            int value = 0;
            if (EntryExists(name))
            {
                //Find the value
                //and parse it then return it
                value = playerValuesInteger[name];
            }
            return value;
        }

        public static string GetString(string name)
        {
            string value = "";
            if (EntryExists(name))
            {
                //Find the value
                //and parse it then return it
                value = playerValuesString[name];
            }
            return value;
        }

        public static bool EntryExists(string name)
        {
            bool exists = false;
            foreach(string m in playerValuesString.Keys)
            {
                if(m == name)
                {
                    exists = true;
                }
            }

            foreach(string n in playerValuesInteger.Keys)
            {
                if(n == name)
                {
                    exists = true;
                }
            }
            return exists;
        }

        public static void Save()
        {
            Console.WriteLine("TODO: Save function");
        }
    }
}

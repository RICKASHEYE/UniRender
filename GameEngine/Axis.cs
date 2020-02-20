using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public static class Axis
    {

        //Axis class to interpret input and keys to work with SamEngine!!!
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);
        public static List<KeyCode> keyCodes = new List<KeyCode>();

        //Get all of the keys and interpret them
        public static int GetKeyAxis(KeyCode code)
        {
            int state = 0;
            if(GetAsyncKeyState(code.KeyUsePositive) != 0 && GetAsyncKeyState(code.KeyUseNegative) == 0)
            {
                state = 1;
            }
            
            if(GetAsyncKeyState(code.KeyUseNegative) != 0 && GetAsyncKeyState(code.KeyUsePositive) == 0){
                state = -1;
            }

            if(GetAsyncKeyState(code.KeyUsePositive) == 0 && GetAsyncKeyState(code.KeyUseNegative) == 0)
            {
                state = 0;
            }
            return state;
        }

        public static void AssignNewKey(KeyCode code)
        {
            //Assign a new key and check if the existing exists!
            if (checkKeyExists(code))
            {
                //Key exists break
                Console.WriteLine("Key already exists!");
                return;
            }
            else
            {
                //Key doesnt exist so create one
                Console.WriteLine("Created key or registered key!");
                //Register key here
                keyCodes.Add(code);
            }
        }

        public static void AssignNewKey(string codeName, Keys keyPos, Keys keyNeg)
        {
            KeyCode code = new KeyCode(codeName, keyPos, keyNeg);
            AssignNewKey(code);
        }

        public static void RemoveOldKey(string name)
        {
            if (checkKeyExists(name))
            {
                Console.WriteLine("Key exists removing...");
                foreach(KeyCode code_ in keyCodes)
                {
                    if(code_.name == name)
                    {
                        keyCodes.Remove(code_);
                        Console.WriteLine("Deregistering, " + code_.name + " or well deleting...");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nothing to remove so not doing anything!!!");
                return;
            }
        }

        public static bool checkKeyExists(string name)
        {
            bool keyExists = false;
            if(getCodeFromName(name) == null)
            {
                keyExists = false;
            }
            else
            {
                keyExists = true;
            }
            return keyExists;
        }

        public static bool checkKeyExists(KeyCode code)
        {
            return checkKeyExists(code.name);
        }

        public static KeyCode getCodeFromName(string name)
        {
            KeyCode returnKey = null;
            foreach(KeyCode code in keyCodes)
            {
                if(code.name == name)
                {
                    returnKey = code;
                }
            }

            if(returnKey == null)
            {
                //Returned key but theres no key here
                Console.WriteLine("No key here!");
            }

            return returnKey;
        }

        public static KeyCode getCodeFromName(KeyCode code_)
        {
            KeyCode returnKey = null;
            foreach (KeyCode code in keyCodes)
            {
                if (code.name == code_.name)
                {
                    returnKey = code;
                }
            }

            if (returnKey == null)
            {
                //Returned key but theres no key here
                Console.WriteLine("No key here!");
            }

            return returnKey;
        }
    }
}

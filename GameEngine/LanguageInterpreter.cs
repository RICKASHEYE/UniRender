using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class LanguageInterpreter
    {
        public void LoadLanguage(string[] code)
        {
            foreach(string m in code)
            {
                ExecuteCommand(m);
            }
        }

        public void ExecuteCommand(string command)
        {
            string[] args = command.Split(' ');
            switch (args[0])
            {
                case "place":
                    //Place a texture
                    break;
                case "break":
                    //Remove a texture
                    break;
                case "repeat":
                    //Repeat the command
                    ExecuteCommand(args[1]);
                    break;
                case "log":
                    //Log the command
                    Debug.Log(args[1]);
                    break;
            }
        }
    }
}

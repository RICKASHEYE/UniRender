using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class Debug
    {
        public static void Log(string log)
        {
            Console.WriteLine(DateTime.Now + " {DEBUG} " + log);
        }

        public static void Error(string log)
        {
            Console.WriteLine(DateTime.Now + " {ERROR} " + log);
        }

        public static void Warning(string log)
        {
            Console.WriteLine(DateTime.Now + " {WARNING} " + log);
        }

        public static void Render(int WindowWidth, int WindowHeight)
        {
            //Render or have the ability to render the console.
            if (!Axis.checkKeyExists("DebugOpen"))
            {
                GameEngine.Axis.AssignNewKey(new KeyCode("DebugOpen", System.Windows.Forms.Keys.Tab));
            }

            if(Axis.GetKeyAxis(Axis.getCodeFromName("DebugOpen")) >= 1)
            {
                //Open the debug menu
                //Canvas.DrawRect(new Rectangle(new Vector2(WindowHeight / 3, WindowWidth), new Vector2(0, 0)), Color.Black, "DebugMenu");
            }
            else
            {

            }
        }
    }
}

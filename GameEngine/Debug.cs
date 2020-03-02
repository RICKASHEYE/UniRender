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
    }
}

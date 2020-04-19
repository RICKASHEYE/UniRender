using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
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
    }
}

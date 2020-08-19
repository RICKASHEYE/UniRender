using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public static class Debug
    {
        public static void Log(string log)
        {
            System.Console.WriteLine(DateTime.Now + " {DEBUG} " + log);
        }

        public static void Log(string log, string[] replaceables)
        {
            for(int i = 0; i < replaceables.Length; i++)
            {
                log = log.Replace('{' + i.ToString() + '}', replaceables[i]);
            }
            Debug.Log(log);
        }

        public static void Error(string log)
        {
            System.Console.WriteLine(DateTime.Now + " {ERROR} " + log);
        }

        public static void Warning(string log)
        {
            System.Console.WriteLine(DateTime.Now + " {WARNING} " + log);
        }
    }
}

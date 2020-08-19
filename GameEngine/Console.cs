using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class Console
    {
        public static void WriteLine(string line)
        {
            //the write line
            Debug.Log(line);
        }

        public static void WriteLine(string line, string[] replaceables)
        {
            //the write line to write with an array object.
            Debug.Log(line, replaceables);
        }
    }
}

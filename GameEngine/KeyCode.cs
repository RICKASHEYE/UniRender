using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubrightEngine
{
    public class KeyCode
    {
        public string name = "default key";
        public Keys KeyUsePositive;
        public Keys KeyUseNegative;
        public int keyAxis = 0;

        public KeyCode(string name, Keys keyUsePos, Keys keyUseNeg)
        {
            KeyUsePositive = keyUsePos;
            KeyUseNegative = keyUseNeg;
            this.name = name;
        }

        public KeyCode(string name, Keys keyUsePos)
        {
            KeyUsePositive = keyUsePos;
            this.name = name;
        }
    }
}

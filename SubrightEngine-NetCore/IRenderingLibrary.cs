using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public interface IRenderingLibrary
    {
        void Initialise(AppConfiguration configuration);
        void Draw();
        string getName();
    }
}

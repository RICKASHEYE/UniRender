using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineEditor
{
    public class ComponentNode
    {
        public string name;
        public GameEngine.Organisation.Component com;

        public ComponentNode(string name_)
        {
            name = name_;
        }
    }
}

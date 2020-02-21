using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineEditor
{
    public class GameObjectNode
    {
        public string name;
        public GameEngine.Organisation.GameObject objectParent;
        public List<ComponentNode> ComponentNodes = new List<ComponentNode>();

        public GameObjectNode(string name_)
        {
            name = name_;
        }
    }
}

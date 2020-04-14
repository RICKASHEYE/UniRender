using GameEngine;

namespace DungeonCrawlerDemo
{
    public class Node
    {
        //Import the node

        public Node(Node lasNode, Vector2 cposition)
        {
            lasNode = lastNode;
            position = cposition;
        }

        public Node lastNode;
        public Vector2 position;
    }
}

using GameEngine;

namespace GameEngineUtil
{
    public class LineRenderer : Renderer
    {
        public Vector2 startPosition;
        public Vector2 endPosition;
        public Color color;

        public LineRenderer(Vector2 sPosition, Vector2 ePosition, Color c):base("Line Renderer")
        {
            //Initialization variable.
            startPosition = sPosition;
            endPosition = ePosition;
            color = c;
        }

        public override void Update()
        {
            //Run an update to draw this
            //Canvas.DrawLine(color, startPosition, endPosition, name);
            base.Update();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class Particle
    {
        //an object to store a particle.
        public int size;
        public Color32 color;
        public Vector2 position;
        public ParticleShape shape;

        public Vector2 rectSize;

        public Particle(int size, Color32 color, Vector2 pos, ParticleShape shape)
        {
            this.size = size;
            this.color = color;
            this.position = pos;
            this.shape = shape;
        }

        public Particle(Vector2 rectSize, Color32 color, Vector2 pos, ParticleShape shape)
        {
            this.rectSize = rectSize;
            this.color = color;
            this.position = pos;
            this.shape = shape;
        }

        public void RenderParticle()
        {
            switch (shape)
            {
                case ParticleShape.BOX:
                    //Render a box.
                    Canvas.DrawRect(new Rectangle((int)position.x, (int)position.y, size, size), color, DrawMode.DIRECT);
                    break;
                case ParticleShape.CIRCLE:
                    //Render a circle.
                    Canvas.DrawFilledCircle(color, position, size, DrawMode.DIRECT);
                    break;
                case ParticleShape.TRIANGLE:
                    //Render a rectangle.
                    Canvas.DrawTriangle(color, position, new Vector2(position.x + 1, position.y + 1), new Vector2(position.x + 2, position.y), DrawMode.DIRECT);
                    break;
            }
        }
    }
}

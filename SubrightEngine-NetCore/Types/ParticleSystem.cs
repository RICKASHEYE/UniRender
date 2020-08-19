using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine.Types
{
    public class ParticleSystem
    {
        public int radius;
        public int scaleParticle;
        public int speed;
        public Color32 color;

        public Vector2 position;

        public ParticleSystem(int radius, int scaleParticle, int speed, Color32 color)
        {
            //Setup particles 01
            this.radius = radius;
            this.scaleParticle = scaleParticle;
            this.speed = speed;
            this.color = color;
        }

        public ParticleSystem(int scaleParticle, int speed, Color32 color)
        {
            //Setup particles 02
            this.radius = 10;
            this.scaleParticle = scaleParticle;
            this.speed = speed;
            this.color = color;
        }

        public ParticleSystem(int speed, Color32 color)
        {
            //Setup particles 03
            this.radius = 10;
            this.scaleParticle = 2;
            this.speed = speed;
            this.color = color;
        }

        public ParticleSystem(Color32 color)
        {
            //Setup particles 04
            this.radius = 10;
            this.scaleParticle = 2;
            this.speed = 3;
            this.color = color;
        }

        public ParticleSystem()
        {
            //Setup Particles 05
            this.radius = 10;
            this.scaleParticle = 2;
            this.speed = 3;
            this.color = Color32.Black;
        }

        public List<Particle> particles = new List<Particle>();

        public void DrawParticles()
        {
            //Usually pointed to a update system.
            foreach(Particle particle in particles)
            {
                float distance = Vector2.Distance(position, particle.position);
                if(distance <= radius)
                {

                }
            }
        }
    }
}

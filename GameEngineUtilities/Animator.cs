using GameEngine;
using System.Collections.Generic;
using System.Threading;

namespace GameEngineUtil
{
    public struct Frame
    {
        public ParEngineImage image;//??????
        public int timeBefore;
    }

    public class Animator : Component
    {
        //Creating a animator might be easy but not easy enough
        //One day a 3d animator will sit here!!!
        public List<Frame> frames = new List<Frame>();
        public ImageRenderer render;

        public Animator() : base("Animator")
        {

        }

        public override void Run()
        {
            //In the future get the Image Renderer here!!!
            base.Run();
        }

        public bool running = false;

        public void PlayAnimation()
        {
            running = true;
            for(int i = 0; i < frames.Count; i++)
            {
                if(running == false)
                {
                    break;
                }
                render.image = frames[i].image;
                Thread.Sleep(frames[i].timeBefore);
            }
        }

        public void StopAnimation()
        {
            running = false;
        }


    }
}

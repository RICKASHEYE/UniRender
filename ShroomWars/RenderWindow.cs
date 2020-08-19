using SubrightEngine;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShroomWars
{
    public class RenderWindow : AssetLoader
    {
        //ahh yes this is the rip off of a game i made in c

        static RenderWindow canvas;
        const int screenWidth = 800;
        const int screenHeight = 500;
        [STAThread]
        static void Main(string[] args)
        {
            canvas = new RenderWindow();
            modeRender = RenderMode.DirectX;
            currentDimension = Dimension.TWOD;
            canvas.Run(new AppConfiguration("Shroom Wars", screenWidth, screenHeight));
        }

        public override void Draw()
        {
            base.Draw();
            //running a draw method
            Clear(SubrightEngine.Types.Color32.White, SubrightEngine.DrawMode.DIRECT);
            CreateHouses();
        }

        public override void Initialize(AppConfiguration config)
        {
            base.Initialize(config);
            //add those 8 houses!
            for(int i = 0; i < SubrightEngine.Types.Random.Range(4, 8); i++)
            {
                houses.Add(new House());
            }
        }

        public List<House> houses = new List<House>();
        public void CreateHouses()
        {
            for(int i = 0; i < houses.Count; i++)
            {
                if(houses[i].position == Vector2.zero)
                {
                    System.Random rnage = new System.Random();
                    int randomX = rnage.Next(0, screenWidth);
                    int randomY = rnage.Next(0, screenHeight);
                    houses[i].position = new Vector2(randomX, randomY);
                }
                DrawRect(new Rectangle(30, 30, houses[i].position), Color32.Blue, SubrightEngine.DrawMode.DIRECT);
            }
        }
    }
}

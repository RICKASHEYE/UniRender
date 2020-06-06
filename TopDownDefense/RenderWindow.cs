using SubrightEngine;
using SubrightEngine.DirectX;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SubrightWindow
{
    public class RenderWindow : AssetLoader
    {
        public static Inventory inventory;
        public static RenderWindow canvas;

        [STAThread]
        static void Main(string[] args)
        {
            canvas = new RenderWindow();
            modeRender = RenderMode.DirectX;
            currentDimension = Dimension.TWOD;
            AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            playerPosition = new Vector2(Config.Width / 2, Config.Height / 2);
            inventory = new Inventory();
            
            canvas.Run(new AppConfiguration("Perlin Noise Demo!"));
        }

        public static Vector2 playerPosition = Vector2.zero;

        public override void Initialize(AppConfiguration config)
        {
            base.Initialize(config);
        }

        public override void Run()
        {
            base.Run();
            Debug.Log("Ran");
        }

        public override void Draw()
        {
            base.Draw();
            DrawCanvas();
            //RenderNoise();
        }

        public static void DrawCanvas()
        {
            Clear(SubrightEngine.Types.Color32.White, SubrightEngine.DrawMode.DIRECT);
            inventory.RenderInventory();
            Texture2D chosenTexture = getTexture("perlin");
            DrawImage(30, 30, chosenTexture.name, SubrightEngine.DrawMode.DIRECT);
            DrawPlayer();
        }

        public static void DrawPlayer()
        {
            int Horizontal = getCodeFromName("Horizontal").keyAxis;
            int Vertical = getCodeFromName("Vertical").keyAxis;
            playerPosition += new Vector2(Horizontal, Vertical) * 1;
            SubrightEngine.Types.Rectangle rect = new SubrightEngine.Types.Rectangle((int)playerPosition.x, (int)playerPosition.y, 10, 10);
            DrawRect(rect, SubrightEngine.Types.Color32.Black, null, SubrightEngine.DrawMode.DIRECT);
        }
    }
}
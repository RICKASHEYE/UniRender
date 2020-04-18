using GameEngine;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameEngineWindow
{
    public class RenderWindow : DumpedCanvas
    {
        static RenderWindow window;
        public RenderWindow()
        {
            //Run(new AppConfiguration(name));
        }

        [STAThread]
        static void Main(string[] args)
        {
            window = new RenderWindow();
            window.Run(new AppConfiguration("Control Demo"));
        }

        public static Vector2 playerPosition = Vector2.zero;

        protected override void Draw(AppTime time)
        {
            base.Draw(time);
            DrawCanvas();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            playerPosition = new Vector2(Config.Width / 2, Config.Height / 2);
        }

        public static void DrawCanvas()
        {
            Clear(Color.White);
            //Debug.Log("Drawing");
            playerPosition += new Vector2(getCodeFromName("Horizontal").keyAxis, getCodeFromName("Vertical").keyAxis);
            //Debug.Log("Horizontal: " + getCodeFromName("Horizontal").keyAxis + " Vertical: " + getCodeFromName("Vertical").keyAxis);
            GameEngine.Rectangle rect = new GameEngine.Rectangle((int)playerPosition.x, (int)playerPosition.y, 10, 10);
            DrawRect(rect, Color.Black);
            playerPosition = Vector2.zero;
        }
    }
}
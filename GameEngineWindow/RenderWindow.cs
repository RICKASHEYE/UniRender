using GameEngine;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameEngineWindow
{
    public class RenderWindow : Canvas
    {

        public RenderWindow()
        {
            //Run(new AppConfiguration(name));
        }

        [STAThread]
        static void Main(string[] args)
        {
            GameEngine.Axis.AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            GameEngine.Axis.AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            RenderWindow window = new RenderWindow();
            window.Run(new AppConfiguration("SubrightEngine"));
        }

        public static Vector2 playerPosition = Vector2.zero;

        public static List<Apple> apples = new List<Apple>();
        public static int clock;

        protected override void Draw(AppTime time)
        {
            base.Draw(time);
            DrawCanvas();
        }

        public static void DrawCanvas()
        {
            //Debug.Log("Drawing");
            playerPosition += new Vector2(GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Horizontal")), GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Vertical")));
            GameEngine.Rectangle rect = new GameEngine.Rectangle(Config.Width / 2, Config.Height / 2, 10, 10);
            DrawRect(rect, Color.Black, "Player");
            playerPosition = Vector2.zero;

            if (clock >= 200)
            {
                GameEngine.Debug.Log("Clock is 200");
                Vector2 pos = new Vector2(GameEngine.Random.Range(Config.Width), GameEngine.Random.Range(Config.Height));
                apples.Add(new Apple(pos));
                GameEngine.Debug.Log("Drawn Apple");
                clock = 0;
            }
            else
            {
                clock++;
            }

            //Render the apples
            foreach (Apple m in apples)
            {
                if (m.deleted == false)
                {
                    if (Vector2.Distance(m.position, playerPosition) <= 10)
                    {
                        //delete
                        m.deleted = true;
                        GameEngine.PlayerValues.SetIntValue("Apples", (int)GameEngine.PlayerValues.GetInteger("Apples") + 1);
                    }
                    DrawCircle(Color.Black, m.position, 10, "Apple " + m.randString);
                }
                else if (m.deleted == true)
                {
                    ClearPixels("Apple " + m.randString);
                }
            }
            Clear(Color.White);
        }
    }
}
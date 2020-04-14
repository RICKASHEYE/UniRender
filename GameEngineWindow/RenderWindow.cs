using GameEngine;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameEngineWindow
{
    public class RenderWindow
    {
        static Canvas canvas;

        static void Main(string[] args)
        {
            GameEngine.Axis.AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            GameEngine.Axis.AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            canvas = new Canvas();
            canvas.DrawText("Apples: " + GameEngine.PlayerValues.GetInteger("Apples"), "Arial", 16, new Vector2(10, 10));
            canvas.Run(new AppConfiguration("Apple Picker Window"));
        }

        public static Vector2 playerPosition = Vector2.zero;

        public List<Apple> apples = new List<Apple>();
        public int clock;

        public void DrawCanvas()
        {
            playerPosition += new Vector2(GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Horizontal")), GameEngine.Axis.GetKeyAxis(GameEngine.Axis.getCodeFromName("Vertical")));
            GameEngine.Rectangle rect = new GameEngine.Rectangle(canvas.Config.Width / 2, canvas.Config.Height / 2, 10, 10);
            canvas.DrawRect(rect, new GameEngine.Color(0, 0, 0), "Player");
            playerPosition = Vector2.zero;

            if (clock >= 200)
            {
                //GameEngine.Debug.Log("Clock is 200");
                Vector2 pos = new Vector2(GameEngine.Random.Range(canvas.Config.Width), GameEngine.Random.Range(canvas.Config.Height));
                apples.Add(new Apple(pos));
                //GameEngine.Debug.Log("Drawn Apple");
                clock = 0;
            }
            else
            {
                clock++;
            }

            //Render the apples
            foreach (Apple m in apples)
            {
                m.render(canvas);
            }
        }
    }
}
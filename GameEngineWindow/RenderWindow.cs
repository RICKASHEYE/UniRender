using SubrightEngine;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SubrightWindow
{
    public class RenderWindow : Canvas
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
            window.Run(new AppConfiguration("Apple Shooter Demo"));
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
            AssignNewKey(new KeyCode("Shoot", Keys.Space));
            playerPosition = new Vector2(Config.Width / 2, Config.Height / 2);
            position = playerPosition;
        }

        public static Vector2 position;
        public static Vector2 size = new Vector2(50, 15);

        public static void DrawCanvas()
        {
            Clear(Color.White, SubrightEngine.DrawMode.DIRECT);
            //Debug.Log("Drawing");
            int Horizontal = getCodeFromName("Horizontal").keyAxis;
            int Vertical = getCodeFromName("Vertical").keyAxis;
            playerPosition += new Vector2(Horizontal, Vertical) * 0.15f;
            //Debug.Log("Horizontal: " + playerPosition.x + " Vertical: " + playerPosition.y);
            Rectangle rect = new Rectangle((int)playerPosition.x, (int)playerPosition.y, 10, 10);
            DrawRect(rect, Color.Black, SubrightEngine.DrawMode.DIRECT);
            DrawPortalGun(Horizontal, Vertical);
            ShootPortal();
            Apples();
        }

        public static List<Bullet> bullets = new List<Bullet>();
        public static List<Apple> apples = new List<Apple>();
        static int appletimer = 0;
        static int applescount = 0;
        static int timeout;

        public static void Apples()
        {
            //DrawText("Apples: " + applescount, "Arial", 16, new Vector2(10, 10), Color.Black);
            if (apples.Count < 10)
            {
                if (appletimer > 250)
                {
                    System.Random rangeX = new System.Random();
                    int randomX = rangeX.Next(Config.Width);
                    System.Random rangeY = new System.Random();
                    int randomY = rangeY.Next(Config.Height);
                    apples.Add(new Apple(new Vector2(randomX, randomY)));
                    appletimer = 0;
                }
                else
                {
                    appletimer++;
                } 
            }

            List<Apple> moddedApples = new List<Apple>();
            foreach(Apple m in apples)
            {
                m.RenderApple();
                float closestBullet = 4;
                Bullet chosenBullet = null;
                foreach(Bullet b in bullets)
                {
                    float distance = Vector2.Distance(m.sPosition, b.positionStart);
                    if(distance < closestBullet)
                    {
                        chosenBullet = b;
                        closestBullet = distance;
                    }
                }

                if(chosenBullet != null)
                {
                    moddedApples.AddRange(apples);
                    moddedApples.Remove(m);
                    applescount++;
                    apples = moddedApples;
                }
            }
        }

        public static void ShootPortal()
        {
            if (timeout == 0)
            {
                if (getCodeFromName("Shoot").keyAxis > 0)
                {
                    //Positive key which means that the player should shoot the bullet.
                    bullets.Add(new Bullet(playerPosition, axis));
                    timeout = 1;
                } 
            }else if(timeout > 0)
            {
                timeout++;
                if(timeout > 100)
                {
                    timeout = 0;
                }
            }

            foreach(Bullet bullet in bullets)
            {
                bullet.RenderBullet();
            }
        }

        public static Vector2 axis = Vector2.zero;

        public static void DrawPortalGun(int horizontal, int vertical)
        {
            if (horizontal > 0)
            {
                axis = new Vector2(1, 0);
                //DrawRect(new Rectangle(50, 15, (int)playerPosition.x + 5, (int)playerPosition.y + 5), Color.DarkGray);
            }
            
            if (horizontal < 0)
            {
                axis = new Vector2(-1, 0);
            }

            if(vertical > 0)
            {
                axis = new Vector2(0, 1);
            }
            if(vertical < 0)
            {
                axis = new Vector2(0, -1);
            }
            //DrawRect(new Rectangle(size, position), Color.DarkGray);
        }
    }
}
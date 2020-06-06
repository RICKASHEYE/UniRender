using SubrightEngine;
using SubrightEngine.DirectX;
using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SubrightWindow
{
    public class RenderWindow : AssetLoader
    {
        static RenderWindow canvas;

        [STAThread]
        static void Main(string[] args)
        {
            canvas = new RenderWindow();
            modeRender = RenderMode.DirectX;
            currentDimension = Dimension.TWOD;
            AssignNewKey(new KeyCode("Horizontal", Keys.D, Keys.A));
            AssignNewKey(new KeyCode("Vertical", Keys.S, Keys.W));
            AssignNewKey(new KeyCode("Shoot", Keys.Space));
            playerPosition = new Vector2(Config.Width / 2, Config.Height / 2);
            position = playerPosition;
            canvas.Run(new AppConfiguration("Apple Shooter"));
        }

        public static Vector2 playerPosition = Vector2.zero;

        public override void Initialize(AppConfiguration config)
        {
            base.Initialize(config);
        }

        public override void Draw()
        {
            base.Draw();
            DrawCanvas();
        }

        public static Vector2 position;
        public static Vector2 size = new Vector2(50, 15);

        public static void DrawCanvas()
        {
            Clear(SubrightEngine.Types.Color32.White, SubrightEngine.DrawMode.DIRECT);
            //Debug.Log("Drawing");
            int Horizontal = getCodeFromName("Horizontal").keyAxis;
            int Vertical = getCodeFromName("Vertical").keyAxis;
            playerPosition += new Vector2(Horizontal, Vertical);
            //Debug.Log("Horizontal: " + playerPosition.x + " Vertical: " + playerPosition.y);
            SubrightEngine.Types.Rectangle rect = new SubrightEngine.Types.Rectangle((int)playerPosition.x, (int)playerPosition.y, 10, 10);
            DrawRect(rect, SubrightEngine.Types.Color32.Black, SubrightEngine.DrawMode.DIRECT);
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
            if (apples.Count < 1200)
            {
                if (appletimer > 50)
                {
                    System.Random rnage = new System.Random();
                    int randomX = rnage.Next(0, Config.Width);
                    int randomY = rnage.Next(0, Config.Height);
                    //Console.WriteLine("Added apple!");
                    apples.Add(new Apple(new SubrightEngine.Types.Vector2(randomX, randomY)));
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
                if(timeout > 20)
                {
                    timeout = 0;
                }
            }

            foreach(Bullet bullet in bullets)
            {
                if (bullet.positionStart.x < Config.Width && bullet.positionStart.x > 0 && bullet.positionStart.y > 0 && bullet.positionStart.y < Config.Height)
                {
                    bullet.RenderBullet();
                }
                else
                {
                    //duplicateBulletArray.Add(bullet);
                }
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
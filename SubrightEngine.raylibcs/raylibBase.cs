using System;
using Raylib_cs;

namespace SubrightEngine.raylibcs
{
    public class raylibBase 
    {
        public void Draw()
        {
            DrawEvent?.Invoke();
        }

        public delegate void DrawEventHandler();
        public static event DrawEventHandler DrawEvent;

        public string getName()
        {
            throw new NotImplementedException();
        }

        public void Initialise(AppConfiguration configuration)
        {
            //Initialise the raylib libraries!
            Raylib.InitWindow(configuration.Width, configuration.Height, configuration.Title);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                //Call the draw method from here!
                Draw();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}

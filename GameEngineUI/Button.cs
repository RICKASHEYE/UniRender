using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineUI
{
    public class Button : UIElement
    {
        public Button() : base("Button")
        {
            //Initialise the button
        }

        public delegate void ClickEventHandler(Button button);
        public static event ClickEventHandler ClickEvent;
        public static void RaiseClickEvent(Button button) { ClickEvent.Invoke(button); }
    }
}

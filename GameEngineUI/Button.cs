using SubrightEngine;

namespace SubrightEngineUI
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

using SubrightEngineUtil;
using System;
using System.Windows.Forms;
using SubrightEngine;

namespace SubrightEngineEditor
{
    public partial class EditProperty : Form
    {
        GameObject editableObjectGame;
        Component editableObjectComponent;

        public EditProperty(GameObject gameObject)
        {
            InitializeComponent();
            editableObjectGame = gameObject;
            Debug.Log("Editing a gameObject");
        }

        public EditProperty(Component component)
        {
            InitializeComponent();
            editableObjectComponent = component;
            Debug.Log("Editing a component Object");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Complete the issue
            if(editableObjectComponent != null && editableObjectGame == null)
            {
                //Edit the component
                editableObjectComponent.name = textBox1.Text;
            }
            else if(editableObjectGame != null && editableObjectComponent == null)
            {
                //Edit the GameObject
                editableObjectGame.name_ = textBox1.Text;
            }
        }
    }
}

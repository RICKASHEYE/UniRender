using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngineEditor
{
    public partial class EditProperty : Form
    {
        GameEngine.Organisation.GameObject editableObjectGame;
        GameEngine.Organisation.Component editableObjectComponent;

        public EditProperty(GameEngine.Organisation.GameObject gameObject)
        {
            InitializeComponent();
            editableObjectGame = gameObject;
            GameEngine.Debug.Log("Editing a gameObject");
        }

        public EditProperty(GameEngine.Organisation.Component component)
        {
            InitializeComponent();
            editableObjectComponent = component;
            GameEngine.Debug.Log("Editing a component Object");
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AE2Tightening.Frame
{
    public class ToggleButton : Button
    {
        private bool toggle = false;
        [Browsable(true)]
        public bool Value
        {
            get => toggle;
            set
            {
                toggle = value;
                DisplayToggle();
            }
        }

        public ToggleButton()
        {
            Toggle();
        }

        public void Toggle()
        {
            Value = !toggle;
            
        }
        private void DisplayToggle()
        {
            if (Value)
            {
                BackColor = Color.Red;
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.Green;
                ForeColor = Color.White;
            }
        }
        protected override void OnClick(EventArgs e)
        {
            Toggle();
            base.OnClick(e);
        }
    }
}

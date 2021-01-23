using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AE2Devices;

namespace AE2Tightening.Frame
{
    public partial class CardControl : UserControl
    {
        private UserInfo theUser;
        public CardControl()
        {
            InitializeComponent();
            SetCarInfo(null);
        }

        public void SetCarInfo(UserInfo user)
        {
            theUser = user;
            if (user == null)
            {
                lblId.Text = "";
                lblName.Text = "";
                lblRunLine.Text = "";
                lblShield.Text = "";
            }
            else
            {
                lblId.Text = user.CardId;
                lblName.Text = user.Name;
                lblRunLine.Text = user.CanRunLine ? "有" : "无";
                lblShield.Text = user.CanSystemShield? "有" : "无";
            }
        }

        public UserInfo GetCard()
        {
            return theUser;
        }
    }
}

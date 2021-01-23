using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AE2Tightening.Frame
{
    public partial class FormAuthor : Form
    {
        private AuthorType authorType = AuthorType.放行;
        private UserInfo theCard = null;
        private Timer tm = new Timer();
        private int i = 10;
        private Action<UserInfo> callbackAction;
        public FormAuthor(AuthorType type,Action<UserInfo> CallBack)
        {
            InitializeComponent();
            authorType = type;
            tm.Interval = 1000;
            tm.Tick += Tm_Tick;
            callbackAction = CallBack;
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            i--;
            lblTime.Text = i.ToString();
            if(i==0)
            {
                tm.Stop();
                Close();
            }
        }

        public void SetCardInfo(UserInfo user)
        {
            if (user == null)
                return;
            authorControl1.SetCarInfo(user);
            if (authorType == AuthorType.放行)
            {
                authorControl1.BackColor = user.CanRunLine ? Color.Green : Color.Red;
                if (user.CanRunLine)
                {
                    theCard = user;
                    callbackAction?.Invoke(user);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else if(authorType == AuthorType.屏蔽)
            {
                authorControl1.BackColor = user.CanSystemShield ? Color.Green : Color.Red;
                if (user.CanSystemShield)
                {
                    theCard = user;
                    callbackAction?.Invoke(user);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        public UserInfo GetCard()
        {
            return theCard;
        }

        private void FormAuthor_Load(object sender, EventArgs e)
        {
            i = 10;
            lblTime.Text = i.ToString();
            tm.Start();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

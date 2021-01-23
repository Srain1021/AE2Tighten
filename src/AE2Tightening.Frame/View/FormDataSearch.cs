using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AE2Tightening.Frame.Data;

namespace AE2Tightening.Frame.View
{
    public partial class FormDataSearch : Form
    {
        private string code;
        private DateTime sTime;
        private DateTime endTime;
        
        public FormDataSearch()
        {
            InitializeComponent();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            code = textBoxCode.Text.Trim();
            sTime = dateTimeStart.Value;
            endTime = dateTimeEnd.Value;
            Search();
        }

        private async void Search()
        {
            long count = await RFIDDBHelper.LocalSQLHandler.TighteningService.GetCount(code, sTime, endTime);
            if(count > 0)
            {
                await RFIDDBHelper.LocalSQLHandler.TighteningService.GetTightensAsync(code, sTime, endTime, 1, 50);
            }
            else
            {
                
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {

        }

        private void btUp_Click(object sender, EventArgs e)
        {

        }

        private void btDown_Click(object sender, EventArgs e)
        {

        }
    }
}

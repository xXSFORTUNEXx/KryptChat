using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Activate : Form
    {
        public Activate()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Program.netClient.Shutdown("shutdown");
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text;

            if (code != "" && code.Length == 25)
            {
                OutgoingData.SendCode(code);
                Close();
            }
        }
    }
}

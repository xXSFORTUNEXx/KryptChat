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
    public partial class Login : Form
    {
        static int recheck;
        public Login()
        {
            InitializeComponent();
            tmrConnect.Enabled = true;
        }

        private void tmrConnect_Tick(object sender, EventArgs e)
        {
            if (Program.netClient.ServerConnection == null)
            {
                Program.Connect();
                recheck = 1;
            }
        }

        private void lblNewUser_MouseHover(object sender, EventArgs e)
        {
            lblNewUser.ForeColor = Color.Red;
        }

        private void lblNewUser_MouseLeave(object sender, EventArgs e)
        {
            lblNewUser.ForeColor = Color.Blue;
        }

        private void lblNewUser_Click(object sender, EventArgs e)
        {
            Program.Disconnect();
        }
    }
}

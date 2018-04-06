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
    public partial class Krypt : Form
    {
        public Krypt()
        {
            InitializeComponent();
            txtGlobalChat.Text = "[" + DateTime.Now.ToString() + "] Welcome to Krypt Chat!";
        }

        private void Krypt_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.login.Show();
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtMessage.Text == "") { return; }

            if (e.KeyChar == (char)Keys.Enter)
            {
                OutgoingData.SendMessage(txtMessage.Text);
                txtMessage.Text = "";
            }
        }
    }
}

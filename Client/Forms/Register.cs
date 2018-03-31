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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtRUser.Text;
            string pass = txtRPass.Text;
            string rpass = txtRRPass.Text;
            string email = txtREmail.Text;

            if (email == "" || email.Length <= 8) { return; }

            if (name != "" && name.Length > 3)
            {
                if (pass != "" && rpass != "" && pass.Length >= 8 && rpass.Length >= 8)
                {
                    if (pass == rpass)
                    {
                        OutgoingData.SendRegistration(name, pass, email);                        
                    }
                }
            }
            Close();
        }
    }
}

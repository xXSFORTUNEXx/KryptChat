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
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (btnStatus.Text == "Connect")
            {
                Program.Connect();
                btnStatus.Text = "Disconnect";
            }
            else
            {
                Program.Shutdown();
                btnStatus.Text = "Connect";
            }
        }
    }
}

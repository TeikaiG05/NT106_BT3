using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.AnimatorNS;

namespace NT106_BT2
{
    public partial class Login_Signup : Form
    {
        private Guna2Transition Guna2Transistion1;
        public Login_Signup()
        {
            InitializeComponent();
            Guna2Transistion1 = new Guna2Transition();
            pn_login.Visible = true;
            pn_regis.Visible = false;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cToLogin_Click(object sender, EventArgs e)
        {
            pn_regis.Visible = false;
            Guna2Transistion1.ShowSync(pn_login);
        }

        private void cSignup_Click(object sender, EventArgs e)
        {
            pn_regis.Visible = true;
            Guna2Transistion1.ShowSync(pn_regis);
        }

        private void pn_regis_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

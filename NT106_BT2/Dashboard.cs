using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace NT106_BT2
{
    public partial class Dashboard : Form
    {
        public Dashboard(string firstname, string surname, string birthday, string gender, string email)
        {
            InitializeComponent();
            cName.Text= firstname + " " + surname;
            cBirthday.Text= birthday;
            cEmail.Text= email;

        }

        private void lb_profile_Click(object sender, EventArgs e)
        {
            pn_profile.Visible = true;
        }

        private void pn_profile_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseProfile_Click(object sender, EventArgs e)
        {
            pn_profile.Visible = false;
        }

        private void Close_profile_Click(object sender, EventArgs e)
        {
            pn_profile.Visible = false;
        }

        private void cEmail_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cName_Click(object sender, EventArgs e)
        {
            
        }
    }
}

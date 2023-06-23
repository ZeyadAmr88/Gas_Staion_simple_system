using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gas_station
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // function to login
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string user = txtUsername.Text;
            string pass = txtPass.Text;
            if (user == "" || pass == "")
            {
                MessageBox.Show("Please enter a username and password.");
                return;
            }

            if (user == "admin" && pass == "admin")
            {
                this.Hide();
                systemData form2 = new systemData();
                form2.ShowDialog();
                form2 = null;
                this.Show();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.");
                txtPass.Clear();
                txtPass.Focus();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}

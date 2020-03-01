using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UKE_sample02
{
    public partial class Login : Form
    {
        private static DataConnection con = new DataConnection();
        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string uname = userName.Text;
            string pwd = password.Text;

            //bool ret = con.AddUser(uname, pwd);
            bool ret = con.ValidateUser(uname, pwd);

            if (ret == true)
            {
                Label1.Text = "Success";
            }
            else {
                Label1.Text = "Failed";
            }
        }
    }
}

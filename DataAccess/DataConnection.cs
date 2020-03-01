using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKE_sample02.Helpers;

namespace UKE_sample02
{
    public class DataConnection
    {
        private static SqlConnection cnn;
        public static bool EstablishConnection() {

            string path = System.IO.Directory.GetCurrentDirectory();

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\Databases\UKEDatabase.mdf;Integrated Security=True";

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            return true;
        }

        public static bool CloseConnection() {
            cnn.Close();
            return true;
        }

        //Adding a login user method
        public bool AddUser(string userName, string password) {
            //string path = System.IO.Directory.GetCurrentDirectory();
            //string hashedPwd = PasswordHasher.Hash(password);
            //string path = System.IO.Directory.GetCurrentDirectory();
            string hashedPassword = PasswordHasher.Hash(password);
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = D:\VS projects\UKE\DataAccess\Databases\UKEDatabase.mdf; Integrated Security = True");
            string query = string.Format("INSERT INTO Login(userName, password) VALUES('{0}', '{1}')", userName, hashedPassword);


            con.Open();
            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@password", hashedPassword);
            //bool ret = con.AddUser(uname, hashedPwd);
            int ret = command.ExecuteNonQuery();

            con.Close();
            if (ret > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        //Validating a login user
        public bool ValidateUser(string userName, string password) {

            //string hashedPassword = PasswordHasher.Hash(password);
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename = D:\VS projects\UKE\DataAccess\Databases\UKEDatabase.mdf; Integrated Security = True");
            string query = string.Format("SELECT * FROM Login WHERE userName = '{0}'", userName);
            con.Open();
            SqlCommand command = new SqlCommand(query, con);
            SqlDataAdapter adapt = new SqlDataAdapter(command);

            DataTable dt = new DataTable("AllLoginUsers");
            adapt.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows) {
                string uname = dr["userName"].ToString();
                string pawd = dr["password"].ToString();
                bool dehashed = PasswordHasher.Verify(password, pawd);

                if (uname.Equals(userName) && dehashed == true)
                {
                    return true;
                }
                else {
                    return false;
                }
                
            }
            return false;
        }

    }
}

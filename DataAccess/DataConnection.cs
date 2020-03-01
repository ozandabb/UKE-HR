using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

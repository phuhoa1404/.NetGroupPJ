using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GroupPJ
{
    class ClsDatabase
    {
        public static SqlConnection con;

        public static bool OpenConnection()
        {
            try
            {
                con = new SqlConnection(@"Server=DESKTOP-8830UQI;Database=coffeshop;uid=mylogin;pwd=mylogin");
                con.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool CloseConnection()
        {
            try
            {
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

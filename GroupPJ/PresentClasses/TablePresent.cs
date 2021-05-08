using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupPJ.Classes;
using System.Data.SqlClient;

namespace GroupPJ.PresentClasses
{
    class TablePresent
    {
        private static TablePresent instance;
        public static TablePresent Instance
        {
            get { if (instance == null) instance = new TablePresent(); return TablePresent.instance; }
            set { TablePresent.instance = value; }
        }
        public static int TableWidth = 80;
        public static int TableHeight = 80;
        private TablePresent()
        {

        }
        //chuyển bàn ???
        public List<Table> LoadTablesList()
        {
            List<Table> tableList = new List<Table>();
            ClsDatabase.OpenConnection();

            SqlCommand command = new SqlCommand("SELECT * FROM TableDrinks", ClsDatabase.con);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                Table table = new Table();
                table.ID = rd.GetInt32(0);
                table.Name = rd.GetString(1);
                table.Status = rd.GetString(2);
                tableList.Add(table);
            }
            ClsDatabase.CloseConnection();
            return tableList;
        }
    }
}

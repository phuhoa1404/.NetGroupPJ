using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupPJ.Classes;
using System.Data.SqlClient;

namespace GroupPJ.PresentClasses
{
    class MenuPresent
    {
        private static MenuPresent instance;

        public static MenuPresent Instance
        {
            get { if (instance == null) instance = new MenuPresent(); return MenuPresent.instance; }
            private set { MenuPresent.instance = value; }
        }

        private MenuPresent() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            ClsDatabase.OpenConnection();
            string query = "SELECT dr.dr_name, bi.count, dr.dr_price, dr.dr_price*bi.count AS totalPrice FROM Bill_Infor AS bi, Bill AS b, Drinks AS dr WHERE bi.id_bill = b.id AND bi.id_drinks = dr.id AND b.bill_status = 0 AND b.id_table = " + id;
            SqlCommand command = new SqlCommand(query, ClsDatabase.con);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                Menu menu = new Menu(rd.GetString(0), rd.GetInt32(1), (float)rd.GetDouble(2),(float) rd.GetDouble(3));
                listMenu.Add(menu);
            }
            ClsDatabase.CloseConnection();
            return listMenu;
        }
    }
}

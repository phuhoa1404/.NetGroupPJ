using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupPJ.Classes;
using System.Data.SqlClient;

namespace GroupPJ.PresentClasses
{
    class BillPresent
    {
        private static BillPresent instance;
        public static BillPresent Instance
        {
            get { if (instance == null) instance = new BillPresent(); return BillPresent.instance; }
            private set { BillPresent.instance = value; }
        }
        public int GetUncheckBillIDByTableID(int idTable)
        {
            List<Bill> bills = new List<Bill>();
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Bill WHERE ID_TABLE = " + idTable + " AND BILL_STATUS = 0", ClsDatabase.con);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                Bill bill = new Bill(rd.GetInt32(0),rd.GetDateTime(1), rd.GetDateTime(2), rd.GetInt32(3), rd.GetInt32(4) );
                return bill.Id;
            }
            return -1;
        }
        public void InsertBill(int id)
        {
            try
            {
                ClsDatabase.OpenConnection();
                SqlCommand command = new SqlCommand("INSERT INTO Bill (DATE_CHECKIN, DATE CHECKOUT, ID_TABLE, BILL_STATUS) VALUES (GETDATE(), NULL, '" + id + "', 0)", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
            }
            catch (Exception)
            {
            }
        }
        public int GetMaxIDBill()
        {
            try
            {
                int id;
                ClsDatabase.OpenConnection();
                SqlCommand command = new SqlCommand("SELECT MAX(id) FROM dbo.Bill");
                SqlDataReader rd = command.ExecuteReader();
                id =(int) rd.GetInt32(0);
                return id;
            }
            catch
            {
                return 1;
            }
        }
    }
}

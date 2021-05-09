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
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Bill WHERE ID_TABLE = " + idTable + " AND BILL_STATUS = 0", ClsDatabase.con);
            SqlDataReader rd = command.ExecuteReader();
            if (rd.Read())
            {
                Bill bill = new Bill(rd.GetInt32(0), rd.GetDateTime(1), rd.GetDateTime(2), rd.GetInt32(3), rd.GetInt32(4));
                return bill.Id;
            }
            return -1;
        }
        public void InsertBill(int id)
        {
            try
            {
                ClsDatabase.OpenConnection();
                SqlCommand command = new SqlCommand("INSERT INTO Bill (DATE_CHECKIN, DATE_CHECKOUT, ID_TABLE, BILL_STATUS) VALUES (GETDATE(), '', '" + id + "', 0)", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
            }
            catch (Exception)
            {
            }
        }
        public void CheckOut(int idBill, float totalPrice)
        {
            ClsDatabase.OpenConnection();
            string update = "UPDATE Bill SET BILL_STATUS = 1, DATE_CHECKOUT = GETDATE(), TotalPrice = "+totalPrice+" WHERE ID = " + idBill;
            SqlCommand command = new SqlCommand(update, ClsDatabase.con);
            command.ExecuteNonQuery();
            ClsDatabase.CloseConnection();
        }
        public int GetMaxIDBill()
        {
            try
            {
                ClsDatabase.OpenConnection();
                SqlCommand command = new SqlCommand("SELECT MAX(id) FROM Bill", ClsDatabase.con);
                int id = Convert.ToInt32(command.ExecuteScalar());
                ClsDatabase.CloseConnection();
                return id;
            }
            catch
            {
                return 1;
            } 
        }
    }
}

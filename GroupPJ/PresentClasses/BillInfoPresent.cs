using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupPJ.Classes;
using System.Data.SqlClient;
using System.Data;

namespace GroupPJ.PresentClasses
{
    class BillInfoPresent
    {
        private static BillInfoPresent instance;

        public static BillInfoPresent Instance
        {
            get { if (instance == null) instance = new BillInfoPresent(); return BillInfoPresent.instance; }
            private set { BillInfoPresent.instance = value; }
        }

        private BillInfoPresent() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            ClsDatabase.OpenConnection();

            SqlCommand command = new SqlCommand("SELECT * FROM Bill_Infor WHERE ID_BILL = " + id, ClsDatabase.con);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                BillInfo billinfo = new BillInfo(rd.GetInt32(0), rd.GetInt32(1), rd.GetInt32(2), rd.GetInt32(3));
                listBillInfo.Add(billinfo);
            }
            ClsDatabase.CloseConnection();
            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idDrink, int count)
        {
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand("InsertBillInfor", ClsDatabase.con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(
                new SqlParameter("@idBill", idBill));
            command.Parameters.Add(
                new SqlParameter("@idDrink", idDrink));
            command.Parameters.Add(
                new SqlParameter("@count", count));
            command.ExecuteNonQuery();
            ClsDatabase.CloseConnection();
        }
    }
}

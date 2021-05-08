using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace GroupPJ
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            LoadAccountList();
            LoadThucUongList();
            LoadBanList();
        }
        void LoadAccountList()
        {
            string query = "SELECT * FROM Accounts";
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand(query, ClsDatabase.con);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            //SqlDataReader rd = command.ExecuteReader();
            ClsDatabase.CloseConnection();
            dtgvAccounts.DataSource = data;
        }
        void LoadThucUongList()
        {
            string query = "SELECT DR_NAME, idCategory, DR_PRICE FROM Drinks";
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand(query, ClsDatabase.con);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            //SqlDataReader rd = command.ExecuteReader();
            ClsDatabase.CloseConnection();
            dtgvThucUong.DataSource = data;
        }
        void LoadBanList()
        {
            string query = "SELECT TD_NAME, TD_STATUS FROM TableDrinks";
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand(query, ClsDatabase.con);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            //SqlDataReader rd = command.ExecuteReader();
            ClsDatabase.CloseConnection();
            dtgvBan.DataSource = data;
        }
    }
}

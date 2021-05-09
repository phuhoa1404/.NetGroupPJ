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
using GroupPJ.Classes;

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
            GetBillByDate(datePicker1.Value, datePicker2.Value);
            DrinkBinding();
            AccountBinding();
            TableBinding();
        }
        void LoadAccountList()
        {
            string query = "SELECT ACC_USERNAME AS [Tài khoản], ACC_NAME AS [Tên hiển thị], ACC_TYPE AS [Loại tài khoản] FROM Accounts";
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
            string query = "SELECT ID, DR_NAME AS [Tên đồ uống], DR_PRICE AS [Giá] FROM Drinks";
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
            string query = "SELECT TD_ID AS [ID], TD_NAME AS [Tên bàn], TD_STATUS AS [Trạng thái] FROM TableDrinks";
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand(query, ClsDatabase.con);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            //SqlDataReader rd = command.ExecuteReader();
            ClsDatabase.CloseConnection();
            dtgvBan.DataSource = data;
        }

        public void GetBillByDate(DateTime checkin, DateTime checkout)
        {
            ClsDatabase.OpenConnection();
            SqlCommand command = new SqlCommand("GetListBillByDate", ClsDatabase.con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(
                new SqlParameter("@checkIn", checkin));
            command.Parameters.Add(
                new SqlParameter("@checkOut", checkout));
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            ClsDatabase.CloseConnection();
            dvBillByDate.DataSource = data;
        }

        void TableBinding()
        {
            txtIdBan.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenBan.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never));
            cbbStatus.DataBindings.Add(new Binding("Text", dtgvBan.DataSource, "Trạng thái", true, DataSourceUpdateMode.Never));
        }
        private void btnThemBan_Click(object sender, EventArgs e)
        {
            try
            {
                string tenban = txtTenBan.Text;
                string status = cbbStatus.SelectedItem.ToString();
                ClsDatabase.OpenConnection();
                SqlCommand command = new SqlCommand("INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES ('" + tenban + "', N'" + status + "')", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBanList();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnThemDoUong_Click(object sender, EventArgs e)
        {
            ClsDatabase.OpenConnection();
            string ten = txtTenDoUong.Text;
            float gia = (float)numGia.Value;
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'" + ten + "', " + gia + ")", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadThucUongList();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void DrinkBinding()
        {
            txtID.DataBindings.Add(new Binding("Text", dtgvThucUong.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenDoUong.DataBindings.Add(new Binding("Text", dtgvThucUong.DataSource, "Tên đồ uống", true, DataSourceUpdateMode.Never));
            numGia.DataBindings.Add(new Binding("Value", dtgvThucUong.DataSource, "Giá", true, DataSourceUpdateMode.Never));
        }
        void AccountBinding()
        {
            txtTenTK.DataBindings.Add(new Binding("Text", dtgvAccounts.DataSource, "Tài khoản", true, DataSourceUpdateMode.Never));
            txtTenHT.DataBindings.Add(new Binding("Text", dtgvAccounts.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            numLoaiTK.DataBindings.Add(new Binding("Value", dtgvAccounts.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }

        private void btnAddAcount_Click(object sender, EventArgs e)
        {
            ClsDatabase.OpenConnection();
            string tentk = txtTenTK.Text;
            string tenhienthi = txtTenHT.Text;
            int type = (int)numLoaiTK.Value;
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO Accounts (ACC_USERNAME, ACC_TYPE, ACC_NAME) VALUES ('" + tentk + "', " + type + ", N'" + tenhienthi + "')", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccountList();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelAccount_Click(object sender, EventArgs e)
        {
            ClsDatabase.OpenConnection();
            string tentk = txtTenTK.Text;
            try
            {
                SqlCommand command = new SqlCommand("DELETE Accounts WHERE ACC_USERNAME = '" + tentk + "'", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccountList();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPwd_Click(object sender, EventArgs e)
        {
            ClsDatabase.OpenConnection();
            string tentk = txtTenTK.Text;
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Accounts SET ACC_PWD = 0 WHERE ACC_USERNAME = '" + tentk + "'", ClsDatabase.con);
                command.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Đặt lại mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccountList();
            }
            catch (Exception)
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaDoUong_Click(object sender, EventArgs e)
        {
            try
            {
                ClsDatabase.OpenConnection();
                int id = Convert.ToInt32(txtID.Text);
                SqlCommand cmd = new SqlCommand("DELETE FROM Drinks WHERE ID = " + id, ClsDatabase.con);
                cmd.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadThucUongList();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa Thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaDoUong_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text);
                string tendouong = txtTenDoUong.Text;
                float gia = (float)numGia.Value;
                ClsDatabase.OpenConnection();
                SqlCommand cmd = new SqlCommand("UPDATE Drinks SET DR_NAME = N'" + tendouong + "', DR_PRICE = " + gia + "WHERE ID = " + id, ClsDatabase.con);
                cmd.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadThucUongList();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            try
            {
                ClsDatabase.OpenConnection();
                int id = Convert.ToInt32(txtIdBan.Text);
                SqlCommand cmd = new SqlCommand("DELETE FROM TableDrinks WHERE TD_ID = " + id, ClsDatabase.con);
                cmd.ExecuteNonQuery();
                ClsDatabase.CloseConnection();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBanList();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa Thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

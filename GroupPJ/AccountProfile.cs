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

namespace GroupPJ
{
    public partial class AccountProfile : Form
    {
        string tenDangNhap = "";
        public AccountProfile(string tendangnhap, string tenhienthi)
        {
            InitializeComponent();
            this.tenDangNhap = tendangnhap;
            txtTenDangNhap.Text = tendangnhap;
            txtTenHienThi.Text = tenhienthi;
            txtPwd.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            ClsDatabase.OpenConnection();
            string pwd = txtPwd.Text;
            string newpwd = txtNewPwd.Text;
            string renewpwd = txtReNewPwd.Text;
            string username = tenDangNhap;
            string name = txtTenHienThi.Text;
            if (newpwd == renewpwd)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Accounts WHERE ACC_USERNAME = '" + username + "' and ACC_PWD = '" + pwd + "'", ClsDatabase.con);
                    SqlDataReader rd = command.ExecuteReader();
                    if (rd.Read())
                    {
                        ClsDatabase.OpenConnection();
                        SqlCommand command1 = new SqlCommand("UPDATE Accounts SET ACC_NAME = '" + name + "', ACC_PWD = '" + newpwd + "' WHERE ACC_USERNAME = '" + username + "'", ClsDatabase.con);
                        command1.ExecuteNonQuery();
                        MessageBox.Show("Thay đổi mật khẩu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClsDatabase.CloseConnection();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cũ không chính xác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    txtPwd.Clear();
                    txtNewPwd.Clear();
                    txtReNewPwd.Clear();
                    ClsDatabase.CloseConnection();
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

    }
    
}

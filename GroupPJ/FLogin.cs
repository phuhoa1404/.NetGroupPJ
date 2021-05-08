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
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
            txtUsername.Focus();
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thự sụ muốn thoát ?","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK){
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ClsDatabase.OpenConnection();
            try
            {
                string tk = txtUsername.Text;
                string ten = "";
                String mk = txtPwd.Text;
                String sql = "SELECT * FROM Accounts WHERE ACC_USERNAME = '" + tk + "' and ACC_PWD = '" + mk + "'";
                SqlCommand com = new SqlCommand(sql, ClsDatabase.con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read() == true)
                {
                    ten = reader.GetString(0);
                    int type = reader.GetInt32(2);
                    MessageBox.Show("Đăng nhập thành công !!", "Đăng nhập", MessageBoxButtons.OK);
                    FManager fManager = new FManager(ten, type);
                    fManager.Visible = true;
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Text = "";
                    txtPwd.Text = "";
                    txtUsername.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối", "Lỗi", MessageBoxButtons.OK);
            }
            
            
        }
    }
}

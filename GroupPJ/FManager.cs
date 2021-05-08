using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GroupPJ.Classes;
using GroupPJ.PresentClasses;
using System.Data.SqlClient;


namespace GroupPJ
{
    public partial class FManager : Form
    {
        string tendangnhap = "";
        int type = 0;
        public FManager(string tenDangNhap, int type)
        {
            InitializeComponent();
            this.type = type;
            this.tendangnhap = tenDangNhap;
            lbName.Text = tenDangNhap;
            LoadTable();
            LoadComboboxTable(cbbChuyenBan);
            LoadDrinks(cbbDoUong);
        }
        void LoadTable()
        {
            flTable.Controls.Clear();

            List<Table> tableList = TablePresent.Instance.LoadTablesList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TablePresent.TableWidth, Height = TablePresent.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flTable.Controls.Add(btn);
            }
        }
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        void ShowBill(int id)
        {
            lvBill.Items.Clear();
            List<GroupPJ.Classes.Menu> listMenuInfo = MenuPresent.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (GroupPJ.Classes.Menu item in listMenuInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.DrinkName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvBill.Items.Add(lsvItem);
            }
            lbTotal.Text = totalPrice.ToString();

        }

        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TablePresent.Instance.LoadTablesList();
            cb.DisplayMember = "Name";
        }
        void LoadDrinks(ComboBox cb)
        {
            
            cb.DataSource = DrinksPresent.Instance.LoadDrinksList();
            cb.DisplayMember = "Name";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfile acc = new AccountProfile(tendangnhap);
            acc.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin adm = new Admin();
            adm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FLogin fLogin = new FLogin();
            this.Close();
            fLogin.Visible = true;
        }

        private void FManager_Load(object sender, EventArgs e)
        {
            if (type == 0) { adminMenu.Enabled = false; }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }

            int idBill = BillPresent.Instance.GetUncheckBillIDByTableID(table.ID);
            int drinkID = (cbbDoUong.SelectedItem as Drinks).Id;
            int count = (int)numThem.Value;

            if (idBill == -1)
            {
                BillPresent.Instance.InsertBill(table.ID);
                BillInfoPresent.Instance.InsertBillInfo(BillPresent.Instance.GetMaxIDBill(), drinkID, count);
            }
            else
            {
                BillInfoPresent.Instance.InsertBillInfo(idBill, drinkID, count);
            }

            ShowBill(table.ID);

            LoadTable();
        }
    }
}

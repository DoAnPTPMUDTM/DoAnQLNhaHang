using QLNhaHang.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaHang
{
    public partial class frmCauHinh : Form
    {
        QL_NguoiDung cauHinh = new QL_NguoiDung();
        public frmCauHinh()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == string.Empty || txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập TênDN, Mật khẩu!");
                return;
            }
            MessageBox.Show(cbbServerName.Text + cbbDatabase.Text + txtUserName.Text + txtPassword.Text);
            cauHinh.saveConfig(cbbServerName.Text, cbbDatabase.Text, txtUserName.Text, txtPassword.Text);
            this.Close();
        }

        private void cbbServerName_DropDown(object sender, EventArgs e)
        {
            cbbServerName.DataSource = cauHinh.getServerName();
            cbbServerName.DisplayMember = "ServerName";
        }

        private void cbbDatabase_DropDown(object sender, EventArgs e)
        {
            if (txtPassword.Text == string.Empty || txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập TênDN, Mật khẩu!");
                return;
            }
            cbbDatabase.DataSource = cauHinh.getDbName(cbbServerName.Text, txtUserName.Text, txtPassword.Text);
            cbbDatabase.DisplayMember = "name";
        }
    }
}

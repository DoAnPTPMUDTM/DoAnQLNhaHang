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
    public partial class frmDangNhap : Form
    {
        QL_NguoiDung cauHinh = new QL_NguoiDung();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDN.Text.Trim()))
            {
                MessageBox.Show("Không được bỏ trống " + label1.Text.ToLower());
                txtTenDN.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
            {
                MessageBox.Show("Không được bỏ trống " + label2.Text.ToLower());
                txtMatKhau.Focus();
                return;
            }
            int kq = cauHinh.checkConfig();
            if (kq == 0)
            {
                processLogin();//Chuỗi cấu hình hợp lệ, tiến hành đăng nhập
            }
            if (kq == 1)
            {
                MessageBox.Show("Chuỗi cấu hình không tồn tại!");
                processConfig();//Chuỗi cấu hình không tồn tại, tiến hành tạo chuỗi cấu hình
            }
            if (kq == 2)
            {
                MessageBox.Show("Chuỗi cấu hình không hợp lệ!");
                processConfig();//Chuỗi cấu hình không hợp lệ, tiến hành tạo chuỗi cấu hình
            }
        }
        public void processLogin()
        {
            int kq = cauHinh.checkUser(txtTenDN.Text, txtMatKhau.Text);
            if (kq == 0)
            {
                MessageBox.Show("Tên DN hoặc mật khẩu không chính xác!");
                return;
            }
            if (kq == 1)
            {
                MessageBox.Show("Tài khoản đã bị khóa!");
                return;
            }
            if (kq == 2)
            {
                frmMain mainForm = new frmMain();
                mainForm.Show();
                this.Hide();
            }

        }
        public void processConfig()
        {
            frmCauHinh cauHinhForm = new frmCauHinh();
            cauHinhForm.Show();
        }
    }
}

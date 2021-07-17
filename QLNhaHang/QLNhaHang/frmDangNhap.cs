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
using BLLDAL;

namespace QLNhaHang
{
    public partial class frmDangNhap : Form
    {
        QL_NguoiDung cauHinh = new QL_NguoiDung();
        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        Mail mail = new Mail(); 
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

        private void btnQuenMK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuenMK.Text))
            {
                MessageBox.Show("Tên đăng nhập không được bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!nguoiDungBLLDAL.ktraTrungTenDN(txtQuenMK.Text))
            {
                MessageBox.Show("Tên đăng nhập không tồn tại trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            NguoiDung nd = nguoiDungBLLDAL.isExistTenDN(txtQuenMK.Text);
            if (nd != null)
            {
                mail.sendMail(nd.Email, nd.MatKhau, nd.HoTen + " !");
                MessageBox.Show("Hãy kiểm tra hộp thư Email. Mật khẩu đã được gửi đến địa chỉ Email của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkHienThiMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienThiMK.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }    
        }
    }
}

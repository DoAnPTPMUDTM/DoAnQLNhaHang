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
    public partial class frmDoiMatKhau : Form
    {
        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtMKCu.Text))
            {
                MessageBox.Show("Mật khẩu cũ không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
            if(string.IsNullOrEmpty(txtMKMoi.Text))
            {
                MessageBox.Show("Mật khẩu mới không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(string.IsNullOrEmpty(txtNhapLaiMK.Text))
            {
                MessageBox.Show("Nhập lại mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Kiểm tra mk có trùng vs mk trong hệ thống k?
            if (!nguoiDungBLLDAL.kTraTrungMK(txtMKCu.Text))
            {
                MessageBox.Show("Mật khẩu không khớp với thông tin lưu trong hệ thống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Kiểm tra mk mới và nhập lại có khớp k?
            if (!txtMKMoi.Text.Equals(txtNhapLaiMK.Text))
            {
                MessageBox.Show("Nhập lại mật khẩu phải trùng khớp với mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                nguoiDungBLLDAL.updateMatKhau(2, txtMKMoi.Text);
                MessageBox.Show("Cập nhật mật khẩu thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void chkHienThiMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienThiMK.Checked)
            {
                txtMKCu.UseSystemPasswordChar = false;
                txtMKMoi.UseSystemPasswordChar = false;
                txtNhapLaiMK.UseSystemPasswordChar = false;
            }
            else
            {
                txtMKCu.UseSystemPasswordChar = true;
                txtMKMoi.UseSystemPasswordChar = true;
                txtNhapLaiMK.UseSystemPasswordChar = true;
            }
        }
    }
}

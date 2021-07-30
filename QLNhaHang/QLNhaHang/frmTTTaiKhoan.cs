using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNhaHang.Classes;
using BLLDAL;

namespace QLNhaHang
{
    public partial class frmTTTaiKhoan : Form
    {

        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        NguoiDung nd;
        public frmTTTaiKhoan()
        {
            InitializeComponent();
        }
        public frmTTTaiKhoan(NguoiDung nd)
        {
            InitializeComponent();
            this.nd = nd;
        }

        private void frmTTTaiKhoan_Load(object sender, EventArgs e)
        {
            //disable
            txtHoTen.Enabled = false;
            rdBtnNam.Enabled = false;
            rdBtnNu.Enabled = false;
            mmEDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            chkHoatDong.Enabled = false;
            //
            NguoiDung nd = nguoiDungBLLDAL.getNguoiDungByMaND(this.nd.MaND);
            txtHoTen.Text = nd.HoTen;
            txtSDT.Text = nd.SDT;
            mmEDiaChi.Text = nd.DiaChi;
            txtEmail.Text = nd.Email;
            txtTenDangNhap.Text = nd.TenDN;
            if (nd.GioiTinh.Equals("Nam"))
            {
                rdBtnNam.Checked = true;
                rdBtnNu.Checked = false;
            }
            else
            {
                rdBtnNam.Checked = false;
                rdBtnNu.Checked = true;
            }
            //
            if (nd.HoatDong)
            {
                chkHoatDong.Checked = true;
            }
            else
            {
                chkHoatDong.Checked = false;
            }    
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Họ và tên không được để trống","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(mmEDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (btnSua.Tag.Equals("1"))
            {
                btnSua.Text = "Cập nhật";
                btnSua.Tag = "2";
                //enable textbox
                txtHoTen.Enabled = true;
                rdBtnNam.Enabled = true;
                rdBtnNu.Enabled = true;
                mmEDiaChi.Enabled = true;
                txtSDT.Enabled = true;
                chkHoatDong.Enabled = true;
                //MessageBox.Show("" + btnSua.Tag + "Name"+ btnSua.Text);
            }
            else
            {
                string gioiTinh = "";
                if (rdBtnNam.Checked)
                {
                    gioiTinh = "Nam";
                }
                else if (rdBtnNu.Checked)
                {
                    gioiTinh = "Nữ";
                }
                bool hoatDong = false;
                if (chkHoatDong.Checked)
                {
                    hoatDong = true;
                }
                else
                {
                    hoatDong = false;
                }
                nguoiDungBLLDAL.updateThongTinTK(nd.MaND, txtHoTen.Text, gioiTinh, txtSDT.Text, mmEDiaChi.Text, hoatDong);
                MessageBox.Show("Sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                btnSua.Tag = "1";
                btnSua.Text = "Chỉnh sửa";
                //disable textbox
                txtHoTen.Enabled = false;
                rdBtnNam.Enabled = false;
                rdBtnNu.Enabled = false;
                mmEDiaChi.Enabled = false;
                txtSDT.Enabled = false;
                chkHoatDong.Enabled = false;
                //MessageBox.Show("" + btnSua.Tag + "Name" + btnSua.Text);
                //
            }
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frmDoiMatKhau = new frmDoiMatKhau(nd);
            frmDoiMatKhau.Name = "frmDoiMatKhau";
            frmDoiMatKhau.ShowDialog(this);
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

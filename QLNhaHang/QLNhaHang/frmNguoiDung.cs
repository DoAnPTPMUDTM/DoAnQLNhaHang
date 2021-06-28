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
    public partial class frmNguoiDung : Form
    {
        NguoiDungBLLDAL NguoiDungBLLDAL = new NguoiDungBLLDAL();
        public frmNguoiDung()
        {
            InitializeComponent();
            this.dtgvNguoiDung.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvNguoiDung.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));   
        }
        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            loadDataNguoiDung();
        }
        private void loadDataNguoiDung()
        {
            txtMaND.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            var NguoiDungs = from nd in NguoiDungBLLDAL.getDataNguoiDung()
                             select new
                             {
                                 MaND = nd.MaND,
                                 HoTen = nd.HoTen,
                                 TenDN = nd.TenDN,
                                 MatKhau = nd.MatKhau,
                                 GioiTinh = nd.GioiTinh,
                                 Email = nd.Email,
                                 SDT = nd.SDT,
                                 DiaChi = nd.DiaChi,
                                 HoatDong = nd.HoatDong
                             };
            dtgvNguoiDung.DataSource = NguoiDungs.ToList();
            
        }

        private void clearControls()
        {
            txtMaND.Clear();
            txtHoTen.Clear();
            rdbNu.Checked = false;
            rdbNam.Checked = false;
            txtSDT.Clear();
            memoEditDiaChi.Text = "";
            txtEmail.Clear();
            txtTenDN.Clear();
            txtMatKhau.Clear();
            chkHoatDong.Checked = false;
            txtHoTen.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //mã nd, họ tên, giới tính, sdt, địa chỉ, email, tên đn, mk, hoạt động
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(string.IsNullOrEmpty(memoEditDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(string.IsNullOrEmpty(txtTenDN.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(rdbNu.Checked == false && rdbNam.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Kiểm tra trùng email, tên đn
            if (NguoiDungBLLDAL.ktraTrungTenDN(txtTenDN.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (NguoiDungBLLDAL.ktraTrungEmail(txtEmail.Text))
            {
                MessageBox.Show("Email đã tồn tại. Vui lòng chọn Email khác!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                //họ tên, giới tính, sdt, địa chỉ, email, tên đn, mk, hoạt động
                NguoiDung nd = new NguoiDung();
                nd.HoTen = txtHoTen.Text;
                if (rdbNam.Checked)
                {
                    nd.GioiTinh = "Nam";
                }
                else
                {
                    nd.GioiTinh = "Nữ";
                }
                nd.SDT = txtSDT.Text;
                nd.DiaChi = memoEditDiaChi.Text;
                nd.Email = txtEmail.Text;
                nd.TenDN = txtTenDN.Text;
                nd.MatKhau = txtMatKhau.Text;
                if (chkHoatDong.Checked)
                {
                    nd.HoatDong = true;
                }
                else
                {
                    nd.HoatDong = false;
                }
                NguoiDungBLLDAL.insertNguoiDung(nd);
                MessageBox.Show("Thêm người dùng thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNguoiDung();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Thêm người dùng thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dtgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //mã nd, họ tên, giới tính, sdt, địa chỉ, email, tên đn, mk, hoạt động
            dynamic row = dtgvNguoiDung.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            try
            {
                txtMaND.Text = row.MaND.ToString();
                txtHoTen.Text = row.HoTen;
                if(row.GioiTinh == "Nam")
                {
                    rdbNam.Checked = true;
                }
                if(row.GioiTinh == "Nữ")
                {
                    rdbNu.Checked = true;
                }
                txtSDT.Text = row.SDT;
                memoEditDiaChi.Text = row.DiaChi;
                txtEmail.Text = row.Email;
                txtTenDN.Text = row.TenDN;
                txtMatKhau.Text = row.MatKhau;
                if((bool)row.HoatDong == true)
                {
                    chkHoatDong.Checked = true;
                }  
                else if((bool)row.HoatDong == false)
                {
                    chkHoatDong.Checked = false;
                }    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvNguoiDung.CurrentRow.DataBoundItem;
            if (row == null)
            {
                return;
            }
            try
            {
                string gioiTinh = "";
                if (rdbNam.Checked)
                {
                    gioiTinh = "Nam";
                }
                else if(rdbNu.Checked)
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
                txtMaND.Text = row.MaND.ToString();
                NguoiDungBLLDAL.updateNguoiDung(int.Parse(txtMaND.Text), txtHoTen.Text, gioiTinh, txtSDT.Text, memoEditDiaChi.Text, txtEmail.Text, txtTenDN.Text, txtMatKhau.Text, hoatDong);
                MessageBox.Show("Sửa thông tin người dùng thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNguoiDung();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Sửa thông tin người dùng thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvNguoiDung.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            try
            {
                txtMaND.Text = row.MaND.ToString();
                NguoiDungBLLDAL.deleteNguoiDung(int.Parse(txtMaND.Text));
                MessageBox.Show("Xóa thông tin người dùng thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNguoiDung();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Xóa thông tin người dùng thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}

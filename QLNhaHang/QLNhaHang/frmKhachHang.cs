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
    public partial class frmKhachHang : Form
    {
        KhachHangBLLDAL khachHangBLLDAL = new KhachHangBLLDAL();
        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            
            loadDataKhachHang();
        }
        private void loadDataKhachHang()
        {
            disableControls();
            var khachHangs = from kh in khachHangBLLDAL.getDataKhachHang()
                             select new
                             {
                                 MaKH = kh.MaKH,
                                 TenKH = kh.TenKH == null ? "" : kh.TenKH,
                                 DiaChi = kh.DiaChi == null ? "" : kh.DiaChi,
                                 SDT = kh.SDT == null ? "" :  kh.SDT,
                                 TongThanhTien = 0,
                                 MaHD = khachHangBLLDAL.soHoaDon(kh.MaKH),
                                 DiemTichLuy = kh.DiemTichLuy == null ? 0 : kh.DiemTichLuy.Value
                             };
            gridViewKhachHang.DataSource = khachHangs.ToList();

        }
        private void disableControls()
        {
            txtMaKH.Enabled = false;
            txtDiemTL.Text = "0";
            txtDiemTL.Enabled = false;
            txtTongTien.Enabled = false;
            txtTongTien.Text = "0";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(150, 0,192,192);
                    e.Appearance.BackColor2 = Color.White;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }

        private void clearControls()
        {
            //mã kh, tên kh, địa chỉ, sdt, diem tl.
            txtMaKH.Clear();
            txtTenKH.Clear();
            mmeDiaChi.Text = "";
            txtSDT.Clear();
            txtTenKH.Focus();
            txtDiemTL.Text = "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //mã kh, tên kh, địa chỉ, sdt, diem tl.
            if (string.IsNullOrEmpty(txtTenKH.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(mmeDiaChi.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                return;
            }

            //try
            //{
            KhachHang kh = new KhachHang();
            kh.TenKH = txtTenKH.Text;
            kh.DiaChi = mmeDiaChi.Text;
            kh.SDT = txtSDT.Text;
            kh.DiemTichLuy = 0;
            khachHangBLLDAL.insertKhachHang(kh);
            MessageBox.Show("Thêm khách hàng thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadDataKhachHang();
            clearControls();
            //}
            //catch
            //{
            //MessageBox.Show("Thêm khách hàng thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
          

            if(gridView1.FocusedRowHandle >= 0)
            {
                if (gridView1.GetFocusedRowCellValue("MaKH").ToString().Equals("0"))
                {

                    clearControls();
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    txtDiemTL.Enabled = false;
                    return;
                }
                //enable txtDiemTL, button Sửa, btn Xóa.
                txtDiemTL.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMaKH.Text = gridView1.GetFocusedRowCellValue("MaKH").ToString();
                txtTenKH.Text = gridView1.GetFocusedRowCellValue("TenKH").ToString();
                mmeDiaChi.Text = gridView1.GetFocusedRowCellValue("DiaChi").ToString();
                txtSDT.Text = gridView1.GetFocusedRowCellValue("SDT").ToString();
                txtDiemTL.Text = gridView1.GetFocusedRowCellValue("DiemTichLuy").ToString();
            }   
            //try
            //{


            //}
            //catch(Exception ex)
            //{
            //MessageBox.Show(ex.Message);
            //}

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = gridView1.GetFocusedRowCellValue("MaKH").ToString();
            if(maKH == null)
            {
                return;
            }
            try
            {
                khachHangBLLDAL.deleteKhachHang(int.Parse(maKH));
                MessageBox.Show("Xóa khách hàng thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataKhachHang();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Xóa khách hàng thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
            
            string maKH = gridView1.GetFocusedRowCellValue("MaKH").ToString();
            if(maKH.Equals("0"))
            {
                return;
            }    
            if(maKH == null)
            {
                return;
            }
            khachHangBLLDAL.updateKhachHang(int.Parse(maKH), txtTenKH.Text, mmeDiaChi.Text, txtSDT.Text, int.Parse(txtDiemTL.Text));
            MessageBox.Show("Sửa khách hàng thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadDataKhachHang();
            clearControls();
        }

        private void txtDiemTL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            loadDataKhachHang();
            clearControls();
        }
    }
}

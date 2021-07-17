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
    public partial class frmThemPhieuNhap : Form
    {
        LoaiMatHangBLLDAL loaiMatHangBLLDAL = new LoaiMatHangBLLDAL();
        MatHangBLLDAL matHangBLLDAL = new MatHangBLLDAL();
        NhaCungCapBLLDAL nhaCungCapBLLDAL = new NhaCungCapBLLDAL();
        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        PhieuNhapBLLDAL phieuNhapBLLDAL = new PhieuNhapBLLDAL();
        CTPNBLLDAL cTPNBLLDAL = new CTPNBLLDAL();
        List<CTPNs> lstCTPN;
        public frmThemPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmThemPhieuNhap_Load(object sender, EventArgs e)
        {
            lstCTPN = new List<CTPNs>();
            loadDataLoaiMH();
            loadDataNhaCC();
            loadDataMatHang();
            loadDataNhanVien();
        }

        private void loadDataMatHang()
        {
            var matHangs = from mh in matHangBLLDAL.getAllMatHang()
                           select new
                           {
                               MaMH = mh.MaMH,
                               MaDVT = mh.DonViTinh.TenDVT,
                               TenMH = mh.TenMH,
                               MaLoaiMH = mh.LoaiMatHang.TenLoaiMH
                           };
            gridControlMH.DataSource = matHangs.ToList();
        }

        private void loadDataNhaCC()
        {
            cbbNhaCC.Properties.DataSource = nhaCungCapBLLDAL.getAllNhaCungCap();
            cbbNhaCC.Properties.DisplayMember = "TenNCC";
            cbbNhaCC.Properties.ValueMember = "MaNCC";
        }

        private void loadDataNhanVien()
        {
            cbbNhanVien.DataSource = nguoiDungBLLDAL.getDataNguoiDung();
            cbbNhanVien.DisplayMember = "HoTen";
            cbbNhanVien.ValueMember = "MaND";
        }

        private void loadDataLoaiMH()
        {

            LoaiMatHang loaiMatHang = new LoaiMatHang();
            loaiMatHang.MaLoaiMH = 0;
            loaiMatHang.TenLoaiMH = "Tất cả";
            List<LoaiMatHang> lstLoaiMH = loaiMatHangBLLDAL.getAllLoaiMatHang();
            lstLoaiMH.Insert(0, loaiMatHang);
            cbbLoaiMH.Properties.DataSource = lstLoaiMH;
            cbbLoaiMH.Properties.DisplayMember = "TenLoaiMH";
            cbbLoaiMH.Properties.ValueMember = "MaLoaiMH";

        }

        private void loadDataMatHangByMaLoai(int maLoaiMH)
        {
            var matHangs = from mh in matHangBLLDAL.getMatHangByMaMaLoaiMH(maLoaiMH)
                           select new
                           {
                               MaMH = mh.MaMH,
                               MaDVT = mh.DonViTinh.TenDVT,
                               TenMH = mh.TenMH,
                               MaLoaiMH = mh.LoaiMatHang.TenLoaiMH
                           };
            gridControlMH.DataSource = matHangs.ToList();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGiaNhap.Text))
            {
                MessageBox.Show("Giá nhập không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //check row
            if (gridViewMatHang.GetFocusedRowCellValue("MaMH") == null || gridViewMatHang.FocusedRowHandle < 0)
            {
                return;
            }

            int soLuong = Convert.ToInt32(numericUpDownSL.Value);
            string maMH = gridViewMatHang.GetFocusedRowCellValue("MaMH").ToString();
            string tenMH = gridViewMatHang.GetFocusedRowCellValue("TenMH").ToString();
            string donGia = txtGiaNhap.Text;
            if (isExitedCTPN(int.Parse(maMH)))
            {
                MessageBox.Show("Đã tồn tại mặt hàng này trong phiếu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                CTPNs cTPNs = new CTPNs();
                cTPNs.maMH = int.Parse(maMH);
                cTPNs.tenMH = tenMH;
                cTPNs.soLuong = soLuong;
                cTPNs.donGia = double.Parse(donGia);
                lstCTPN.Add(cTPNs);
                txtGiaNhap.Clear();
                numericUpDownSL.Value = 1;
                gridControlCTPN.DataSource = lstCTPN.ToList();
            }    
        }

        private void cbbLoaiMH_EditValueChanged(object sender, EventArgs e)
        {
            if (cbbLoaiMH.EditValue == null)
            {
                loadDataMatHang();
            }
            else if (cbbLoaiMH.EditValue.ToString().Equals("0"))
            {
                loadDataMatHang();
            }
            else
            {
                loadDataMatHangByMaLoai(int.Parse(cbbLoaiMH.EditValue.ToString()));
            }
        }
        
        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnXoaMH_Click(object sender, EventArgs e)
        {
            if (gridViewCTPN.GetFocusedRowCellValue("maMH") == null || gridViewCTPN.FocusedRowHandle < 0)
            {
                return;
            }
            else
            {
                string maMH = gridViewCTPN.GetFocusedRowCellValue("maMH").ToString();
                CTPNs ctpns = lstCTPN.Where(t => t.maMH == int.Parse(maMH)).FirstOrDefault();
                if (ctpns != null)
                {
                    lstCTPN.Remove(ctpns);
                }
                gridControlCTPN.DataSource = lstCTPN.ToList();
            }
        }

        private void btnGiamSL_Click(object sender, EventArgs e)
        {
            int rowFocus = gridViewCTPN.FocusedRowHandle;
            if (rowFocus >= 0)
            {
                if (gridViewCTPN.GetFocusedRowCellValue("maMH") == null || gridViewCTPN.FocusedRowHandle < 0)
                {
                    return;
                }
                string maMH = gridViewCTPN.GetFocusedRowCellValue("maMH").ToString();
                if (getCurrentNumber(int.Parse(maMH)) == 1)
                {
                    MessageBox.Show("Không thể giảm vì số lượng hiện tại là 1!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (getCurrentNumber(int.Parse(maMH)) == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin về CTPN này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CTPNs cTPNs = lstCTPN.Where(t => t.maMH == int.Parse(maMH)).FirstOrDefault();
                if (cTPNs != null)
                {
                    if (cTPNs.soLuong > 1)
                    {
                        int soLuong = cTPNs.soLuong - 1;
                        cTPNs.soLuong = soLuong;
                    }
                }
                gridControlCTPN.DataSource = lstCTPN.ToList();
            }
        }
        public int getCurrentNumber(int maMH)
        {
            CTPNs cTPNs = lstCTPN.Where(t => t.maMH == maMH).FirstOrDefault();
            if (cTPNs != null)
            {
                return cTPNs.soLuong;
            }
            return 0;
        }

        private void btnTangSL_Click(object sender, EventArgs e)
        {
            int rowFocus = gridViewCTPN.FocusedRowHandle;
            if (rowFocus >= 0)
            {
                if (gridViewCTPN.GetFocusedRowCellValue("maMH") == null || gridViewCTPN.FocusedRowHandle < 0)
                {
                    return;
                }
                string maMH = gridViewCTPN.GetFocusedRowCellValue("maMH").ToString();
                if (getCurrentNumber(int.Parse(maMH)) == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin về CTPN này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CTPNs cTPNs = lstCTPN.Where(t => t.maMH == int.Parse(maMH)).FirstOrDefault();
                if (cTPNs != null)
                {

                    int soLuong = cTPNs.soLuong + 1;
                    cTPNs.soLuong = soLuong;
                }
                gridControlCTPN.DataSource = lstCTPN.ToList();
            }
        }

        private void btnLapPhieuNhap_Click(object sender, EventArgs e)
        {
            if (lstCTPN.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin mặt hàng trong phiếu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //check cbb: nhân viên, nhà cc
            if (cbbNhaCC.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Cái này cho chọn nhân viên nào mình gắn module đăng nhập zo thì fix cứng nhân viên ấy luôn.
            if (cbbNhanVien.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                
                PhieuNhap pn = new PhieuNhap();
                pn.MaNV = int.Parse(cbbNhanVien.SelectedValue.ToString());
                pn.MaNCC = int.Parse(cbbNhaCC.EditValue.ToString());
                pn.Ngay = DateTime.Now;
                pn.TongTien = (decimal?)tinhTongTien();
                phieuNhapBLLDAL.insertPhieuNhapHang(pn);
                foreach (var item in lstCTPN)
                {
                    CTPN cTPN = new CTPN();
                    cTPN.MaPN = pn.MaPN;
                    cTPN.MaMH = item.maMH;
                    cTPN.SoLuong = item.soLuong;
                    cTPN.DonGia = (decimal?)item.donGia;
                    cTPN.ThanhTien = (decimal?)item.thanhTien;
                    cTPNBLLDAL.insertCTPN(cTPN);
                }
                MessageBox.Show("Thêm phiếu nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridControlCTPN.DataSource = null;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private double tinhTongTien()
        {
            double tongTien = 0;
            if (lstCTPN.Count > 0)
            {
                tongTien = lstCTPN.Sum(t => t.thanhTien);
            }
            return tongTien;
        }
        public bool isExitedCTPN(int maMH)
        {
            CTPNs cTPNs = lstCTPN.Where(t => t.maMH == maMH).FirstOrDefault();
            if (cTPNs == null)
            {
                return false;
            }
            return true;
        }
    }
}

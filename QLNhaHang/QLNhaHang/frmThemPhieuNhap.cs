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
        NguoiDung nd;
        public frmThemPhieuNhap()
        {
            InitializeComponent();
        }
        public frmThemPhieuNhap(NguoiDung nd)
        {
            InitializeComponent();
            this.nd = nd;
        }

        private void frmThemPhieuNhap_Load(object sender, EventArgs e)
        {
            lstCTPN = new List<CTPNs>();
            loadDataLoaiMH();
            loadDataNhaCC();
            loadDataMatHang();
            loadDataNhanVien();
            txtNhanVien.Text = nd.HoTen;
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
            //cbbNhanVien.DataSource = nguoiDungBLLDAL.getDataNguoiDung();
            //cbbNhanVien.DisplayMember = "HoTen";
            //cbbNhanVien.ValueMember = "MaND";
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
            string dvt = gridViewMatHang.GetFocusedRowCellValue("MaDVT").ToString();
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
                cTPNs.dvt = dvt;
                lstCTPN.Add(cTPNs);
                txtGiaNhap.Clear();
                numericUpDownSL.Value = 1;
                gridControlCTPN.DataSource = lstCTPN.ToList();
                gridViewCTPN.FocusedRowHandle = gridViewCTPN.RowCount - 1;
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
                gridViewCTPN.FocusedRowHandle = rowFocus;
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
                gridViewCTPN.FocusedRowHandle = rowFocus;
            }
        }
        public delegate void StatusUpdateHandler(object sender, EventArgs e, PhieuNhap pn, List<CTPNs> lstCTPN);
        public event StatusUpdateHandler OnInsertNhapHang;

        private void InsertNhapHang(PhieuNhap pn, List<CTPNs> lstCTPN)
        {
            EventArgs args = new EventArgs();
            OnInsertNhapHang?.Invoke(this, args, pn, lstCTPN);
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
            

            try
            {

                PhieuNhap pn = new PhieuNhap();
                pn.MaNV = nd.MaND;
                pn.MaNCC = int.Parse(cbbNhaCC.EditValue.ToString());
                pn.Ngay = DateTime.Now;
                pn.TongTien = (decimal)tinhTongTien();              
                InsertNhapHang(pn, lstCTPN);
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

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            Microsoft.Office.Interop.Excel.Range xlRange;
            string strFileName;
            openFileDialog1.Filter = "Execel file | *.xls; *.xlsx";         
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFileName = openFileDialog1.FileName;
                if (strFileName != string.Empty)
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(strFileName);
                    xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                    xlRange = xlWorkSheet.UsedRange;
                    int xlRow;
                    List<CTPNs> ctpnExcel = new List<CTPNs>();
                    for (xlRow = 2; xlRow <= xlRange.Rows.Count; xlRow++)
                    {
                        if (xlRange.Cells[xlRow, 1].Text != "")
                        {
                            try
                            {
                                int maMH = int.Parse(xlRange.Cells[xlRow, 1].Text.ToString().Trim());
                                MatHang mh = matHangBLLDAL.getMatHangByMaMH(maMH);
                                if (mh == null)
                                {
                                    continue;
                                }
                                if (isExitedCTPN(mh.MaMH))
                                {
                                    continue;
                                }
                                int soLuong = int.Parse(xlRange.Cells[xlRow, 2].Text.ToString().Trim());
                                double giaNhap = double.Parse(xlRange.Cells[xlRow, 3].Text.ToString().Trim());
                                CTPNs ctpn = new CTPNs();
                                ctpn.maMH = mh.MaMH;
                                ctpn.soLuong = soLuong;
                                ctpn.donGia = giaNhap;
                                ctpn.tenMH = mh.TenMH;
                                ctpnExcel.Add(ctpn);
                            }
                            catch
                            {
                                MessageBox.Show("Bỏ qua");
                                continue;
                            }
                        }
                    }
                    xlWorkBook.Close();
                    xlApp.Quit();
                    if (ctpnExcel.Count > 0)
                    {
                        lstCTPN.AddRange(ctpnExcel);
                        gridControlCTPN.DataSource = lstCTPN;
                    }

                }
            }

        }
    }
}

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
using QLNhaHang.Classes;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;

namespace QLNhaHang
{
    public partial class frmQLNhapHang : Form
    {
        PhieuNhapBLLDAL phieuNhapBLLDAL = new PhieuNhapBLLDAL();
        CTPNBLLDAL cTPNBLLDAL = new CTPNBLLDAL();
        NguoiDung nd;
        public frmQLNhapHang()
        {
            InitializeComponent();
        }
        public frmQLNhapHang(NguoiDung nd)
        {
            InitializeComponent();
            this.nd = nd;
        }


        private void frmQLNhapHang_Load(object sender, EventArgs e)
        {
            loadDataPhieuNhap();
            startListenner();
        }
        private void loadDataPhieuNhap()
        {
            var phieuNhaps = from pn in phieuNhapBLLDAL.getAllPhieuNhap()
                             select new
                             {
                                 MaPN = pn.MaPN,
                                 MaNV = pn.NguoiDung.HoTen,
                                 MaNCC = pn.NhaCungCap.TenNCC,
                                 Ngay = pn.Ngay,
                                 TongTien = pn.TongTien
                             };
            gridControlPN.DataSource = phieuNhaps.ToList();
        }
        private void loadCTPNByMaPN(int maPN)
        {
            var CTPNs = from ctpn in cTPNBLLDAL.getCTPNByMaPN(maPN)
                        select new
                        {
                            MaPN = ctpn.MaPN,
                            MaMH = ctpn.MatHang.MaMH,
                            TenMH = ctpn.MatHang.TenMH,
                            SoLuong = ctpn.SoLuong,
                            DonGia = ctpn.DonGia,
                            ThanhTien = ctpn.ThanhTien
                        };
            gridControlCTPN.DataSource = CTPNs.ToList();
        }

        private void gridViewPN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string maPN = gridViewPN.GetFocusedRowCellValue("MaPN").ToString();
            if (maPN == null)
            {
                return;
            }
            loadCTPNByMaPN(int.Parse(maPN));
        }

        private void barBtnThemPN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemPhieuNhap frmThemPhieuNhap = new frmThemPhieuNhap(nd);
            frmThemPhieuNhap.Name = "frmThemPhieuNhap";
            frmThemPhieuNhap.ShowDialog(this);
        }
        SqlTableDependency<PhieuNhap> dep;
        public void startListenner()
        {
            string stringConnection = Properties.Settings.Default.ChuoiKetNoi;
            var mapper = new ModelToTableMapper<PhieuNhap>();
            mapper.AddMapping(t => t.MaPN, "MaPN");
            dep = new SqlTableDependency<PhieuNhap>(stringConnection, "PhieuNhap", mapper: mapper);
            dep.OnChanged += Dep_OnChanged;
            dep.Start();
        }

        private void Dep_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<PhieuNhap> e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                loadDataPhieuNhap();
            }));
        }

        private void frmQLNhapHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            dep.Stop();
        }

        private void barBtnXuatExcelPN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelExport excel = new ExcelExport();
            SaveFileDialog saveFile = new SaveFileDialog();
            if (gridViewPN.RowCount == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất");
                return;
            }
            string maPN = gridViewPN.GetFocusedRowCellValue("MaPN").ToString();
            if (maPN == null)
            {
                return;
            }
            PhieuNhap pn = phieuNhapBLLDAL.getPhieuNhapByMaPN(int.Parse(maPN));
            pn.NhaCungCap.TenNCC = gridViewPN.GetFocusedRowCellValue("MaNCC").ToString();
            List<CTPNExport> lstCTPN = new List<CTPNExport>();
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                CTPNExport cTPNs = new CTPNExport();
                cTPNs.MaPhieu = maPN;
                cTPNs.MaMH = gridView2.GetRowCellValue(i, "MaMH").ToString();
                cTPNs.TenMH = gridView2.GetRowCellValue(i, "TenMH").ToString();
                cTPNs.SoLuong = gridView2.GetRowCellValue(i, "SoLuong").ToString();
                cTPNs.DonGia = Convert.ToDouble(gridView2.GetRowCellValue(i, "DonGia").ToString());
                cTPNs.ThanhTien = Convert.ToDouble(gridView2.GetRowCellValue(i, "ThanhTien").ToString());
                lstCTPN.Add(cTPNs);
            }
            string path = string.Empty;
            excel.ExportPhieuNhap(lstCTPN, pn, ref path, false);
            if (!string.IsNullOrEmpty(path) && MessageBox.Show("Ban co muon mo file khong?", "Thong tin", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(path);
            }

        }
    }
}

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
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;

namespace QLNhaHang
{
    public partial class frmQLNhapHang : Form
    {
        PhieuNhapBLLDAL phieuNhapBLLDAL = new PhieuNhapBLLDAL();
        CTPNBLLDAL cTPNBLLDAL = new CTPNBLLDAL();
        public frmQLNhapHang()
        {
            InitializeComponent();
        }

        private void frmQLNhapHang_Load(object sender, EventArgs e)
        {
            loadDataPhieuNhap();
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
                            MaMH = ctpn.MatHang.TenMH,
                            SoLuong = ctpn.SoLuong,
                            DonGia = ctpn.DonGia,
                            ThanhTien = ctpn.ThanhTien
                        };
            gridControlCTPN.DataSource = CTPNs.ToList();
        }

        private void gridViewPN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string maPN = gridViewPN.GetFocusedRowCellValue("MaPN").ToString();
            if(maPN == null)
            {
                return;
            }
            loadCTPNByMaPN(int.Parse(maPN));
        }

        private void barBtnThemPN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemPhieuNhap frmThemPhieuNhap = new frmThemPhieuNhap();
            frmThemPhieuNhap.Name = "frmThemPhieuNhap";
            frmThemPhieuNhap.ShowDialog(this);
        }
        SqlTableDependency<PhieuNhap> dep;
        public void startListenner()
        {
            string stringConnection = Properties.Settings.Default.ChuoiKetNoi;
            var mapper = new ModelToTableMapper<PhieuNhap>();
            mapper.AddMapping(t => t.MaPN, "MaPN");
            dep = new SqlTableDependency<PhieuNhap>(stringConnection,"PhieuNhap", mapper: mapper );
            dep.OnChanged += Dep_OnChanged;
            dep.Start();
        }

        private void Dep_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<PhieuNhap> e)
        {
            this.Invoke(new MethodInvoker(delegate ()
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
            }));
        }
    }
}

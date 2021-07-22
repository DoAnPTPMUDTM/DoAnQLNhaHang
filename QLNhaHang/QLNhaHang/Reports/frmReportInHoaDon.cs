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

namespace QLNhaHang.Reports
{
    public partial class frmReportInHoaDon : Form
    {
        int maHD;
        private double tongTien, tienGiam, thanhTien;

        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        KhachHangBLLDAL khachHangBLLDAL = new KhachHangBLLDAL();
        public frmReportInHoaDon()
        {
            InitializeComponent();
        }
        public frmReportInHoaDon(int maHD, double tienGiam, double thanhTien, double tongTien)
        {
            InitializeComponent();
            this.maHD = maHD;
            this.tienGiam = tienGiam;
            this.thanhTien = thanhTien;
            this.tongTien = tongTien;
        }
        public void loadIn()
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaHD(maHD);
            if(hd == null)
            {
                return;
            }
            List<CTHD> lstCTHD = cTHDBLLDAL.getCTDHByMaHD(maHD);
            if(lstCTHD == null)
            {
                return;
            }
            string tienChu = Utils.NumberToText(thanhTien);
            var data = (from c in lstCTHD
                        select new
                        {
                            KhachHang = khachHangBLLDAL.getTenKHByMaKH(hd.MaKH.Value),
                            TenBan = hd.Ban.TenBan,
                            Ngay = hd.Ngay.Value.ToString("dd/MM/yyyy hh:mm tt"),
                            MaHD = hd.MaHD,
                            TenMon = monBLLDAL.getTenByMa(c.MaMon),
                            DVT = c.Mon.DonViTinh.TenDVT,
                            SL = c.SoLuong.Value,
                            DonGia = c.DonGia == null ? 0 : c.DonGia.Value,
                            ThanhTien = c.ThanhTien == null ? 0 : c.ThanhTien.Value,
                            TongTien = tongTien,    
                            TongCong = thanhTien,
                            TienGiam = tienGiam,
                            TienChu = tienChu

                        }).ToList();
            CrystalReportInHoaDon CrystalReportInHoaDon = new CrystalReportInHoaDon();
            CrystalReportInHoaDon.SetDataSource(data);
            crystalReportViewer1.ReportSource = CrystalReportInHoaDon;
        }

        private void frmReportInHoaDon_Load(object sender, EventArgs e)
        {
            loadIn();
        }
    }
}

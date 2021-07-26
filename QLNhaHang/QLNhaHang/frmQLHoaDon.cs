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
    public partial class frmQLHoaDon : Form
    {
        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
        public frmQLHoaDon()
        {
            InitializeComponent();
        }

        private void frmQLHoaDon_Load(object sender, EventArgs e)
        {
            loadDataHoaDon();
        }
        public void loadDataHoaDon()
        {
            var hoaDons = from hd in hoaDonBLLDAL.getDataHoaDonTT()
                          select new
                          {
                              MaHD = hd.MaHD,
                              MaBan = hd.MaBan,
                              HoTen = hd.NguoiDung.HoTen,
                              TenKH = hd.KhachHang.TenKH,
                              Ngay = hd.Ngay.Value,
                              TongTien = hd.TongTien,
                              TienGiam = hd.TienGiam,
                              ThanhTien = hd.ThanhTien
                          };
            gridControlHoaDon.DataSource = hoaDons.ToList();

        }
        public void loadDataCTHDByMaHD(int maHD)
        {
            var cthds = from cthd in cTHDBLLDAL.getCTHDByMaHD(maHD)
                        select new
                        {
                            MaHD = cthd.MaHD,
                            TenMon = cthd.Mon.TenMon,
                            SoLuong = cthd.SoLuong,
                            DonGia = cthd.DonGia,
                            ThanhTien = cthd.ThanhTien
                        };
            gridControlCTHD.DataSource = cthds.ToList();
        }

        private void gridViewHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string maHD = gridViewHoaDon.GetFocusedRowCellValue("MaHD").ToString();
            if (maHD == null)
            {
                return;
            }
            loadDataCTHDByMaHD(int.Parse(maHD));
        }
    }
}

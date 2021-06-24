using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
   public class HoaDonBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public HoaDonBLLDAL()
        {

        }
        public List<HoaDon> getDataHoaDon()
        {
            return db.HoaDons.ToList<HoaDon>();
        }
        public void themHoaDon(HoaDon hoaDon)
        {
            db.HoaDons.InsertOnSubmit(hoaDon);
            db.SubmitChanges();
        }

        public HoaDon getHoaDonByMaBanCoKhach(int maBan)
        {
            return db.HoaDons.Where(h => h.MaBan == maBan && h.TinhTrang == 0).FirstOrDefault();
        }
        public void updateTTKH(int maHD, int? maKH)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if(hd != null)
            {
                hd.MaKH = maKH;
                db.SubmitChanges();
            }
        }
        public void clearTTKH(int maHD)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd != null)
            {
                if (hd.MaKH != null)
                {
                    hd.MaKH = null;
                    db.SubmitChanges();
                }
            }
        }
        public void thanhToan(int maHD, double tongTien, double tienGiam, double thanhTien, double tienNhan, double tienThua)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if(hd != null && hd.TinhTrang == 0)
            {
                hd.TongTien = (decimal?)tongTien;
                hd.TienGiam = (decimal?)tienGiam;
                hd.ThanhTien = (decimal?)thanhTien;
                hd.TienNhan = (decimal?)tienNhan;
                hd.TienThua = (decimal?)tienThua;
                hd.TinhTrang = 1;
                db.SubmitChanges();
            }
        }
        public HoaDon getHoaDonByMaHD(int maHD)
        {
            return db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
        }

        public void updateKHVL(int maHD)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd != null)
            {
                if(hd.MaKH != 0)
                {
                    hd.MaKH = 0;
                    db.SubmitChanges();
                }    
            }
        }

        public void deleteHDByMaHD(int maHD)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if(hd != null)
            {
                db.HoaDons.DeleteOnSubmit(hd);
                db.SubmitChanges();
            }
        }
        
        public void updateMaBanDoiBan(int maHD,int maBan)
        {
            HoaDon hoaDon = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if(hoaDon != null)
            {
                hoaDon.MaBan = maBan;
                db.SubmitChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class MatHangBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public MatHangBLLDAL()
        {

        }
        public List<MatHang> getAllMatHang()
        {
            return db.MatHangs.OrderByDescending(m => m.MaMH).ToList();
        }
        public List<MatHang> getMatHangByMaMaLoaiMH(int maLoaiMH)
        {
            return db.MatHangs.Where(t => t.MaLoaiMH == maLoaiMH).ToList();
        }
        public MatHang getMatHangByMaMH(int maMH)
        {
            return db.MatHangs.Where(t => t.MaMH == maMH).FirstOrDefault();
        }
        public void insertMatHang(MatHang matHang)
        {
            if (matHang != null)
            {
                db.MatHangs.InsertOnSubmit(matHang);
                db.SubmitChanges();
            }
        }
        public bool ktKhoaNgoai(int maMatHang)
        {
            bool checkCTPN = db.CTPNs.Where(c => c.MaMH == maMatHang).Count() > 0;
            bool checkDinhLuong = db.DinhLuongs.Where(d => d.MaMH == maMatHang).Count() > 0;
            if (checkCTPN || checkDinhLuong)
            {
                return true;
            }
            return false;
        }
        public void deleteMatHang(int maMatHang)
        {
            MatHang matHang = db.MatHangs.Where(m => m.MaMH == maMatHang).FirstOrDefault();
            if (matHang != null)
            {
                db.MatHangs.DeleteOnSubmit(matHang);
                db.SubmitChanges();
            }
        }
        public void updateMatHang(int maMatHang, MatHang matHang)
        {
            MatHang matHangHT = db.MatHangs.Where(m => m.MaMH == maMatHang).FirstOrDefault();
            if (matHang != null)
            {
                matHangHT.TenMH = matHang.TenMH;
                matHangHT.MaDVT = matHang.MaDVT;
                matHangHT.MaLoaiMH = matHang.MaLoaiMH;
                db.SubmitChanges();
            }
        }
        public void updateSLNhap(List<CTPNs> lstCTPN)
        {
            foreach (CTPNs c in lstCTPN)
            {
                if (c != null)
                {
                    MatHang mh = db.MatHangs.Where(m => m.MaMH == c.maMH).FirstOrDefault();
                    if (mh != null)
                    {
                        decimal sl = c.soLuong + mh.SoLuongTon;
                        mh.SoLuongTon = sl;
                    }
                }
            }
            db.SubmitChanges();
        }
        public bool ktGoiMon(int maMon, int sl)
        {
            List<DinhLuong> lstDinhLuong = db.DinhLuongs.Where(d => d.MaMon == maMon).ToList();
            if (lstDinhLuong == null || (lstDinhLuong != null && lstDinhLuong.Count == 0))
            {
                return true;
            }
            foreach (DinhLuong dl in lstDinhLuong)
            {
                MatHang mh = db.MatHangs.Where(m => m.MaMH == dl.MaMH).FirstOrDefault();
                if (mh.SoLuongTon == 0 || mh.SoLuongTon < (dl.QuyDoi * sl))
                {
                    return false;
                }
            }
            return true;
        }
        public void truSLNguyenLieu(int maMon, int sl)
        {
            List<DinhLuong> lstDinhLuong = db.DinhLuongs.Where(d => d.MaMon == maMon).ToList();
            if (lstDinhLuong == null || (lstDinhLuong != null && lstDinhLuong.Count == 0))
            {
                return;
            }
            foreach (DinhLuong dl in lstDinhLuong)
            {
                MatHang mh = db.MatHangs.Where(m => m.MaMH == dl.MaMH).FirstOrDefault();
                if (mh != null)
                {
                    decimal slTru = mh.SoLuongTon - (dl.QuyDoi * sl);
                    mh.SoLuongTon = slTru;
                }
            }
            db.SubmitChanges();
        }

        public void congSLNguyenLieu(int maMon, int sl)
        {
            List<DinhLuong> lstDinhLuong = db.DinhLuongs.Where(d => d.MaMon == maMon).ToList();
            if (lstDinhLuong == null || (lstDinhLuong != null && lstDinhLuong.Count == 0))
            {
                return;
            }
            foreach (DinhLuong dl in lstDinhLuong)
            {
                MatHang mh = db.MatHangs.Where(m => m.MaMH == dl.MaMH).FirstOrDefault();
                if (mh != null)
                {
                    decimal slCong = mh.SoLuongTon + (dl.QuyDoi * sl);
                    mh.SoLuongTon = slCong;
                }
            }
            db.SubmitChanges();
        }
        public string getTenMHByMa(int maMH)
        {
            MatHang mh = db.MatHangs.Where(m => m.MaMH == maMH).FirstOrDefault();
            if(mh == null)
            {
                return "";
            }
            return mh.TenMH;
        }
        public string getTenDVTByMaMH(int maMH)
        {
            MatHang mh = db.MatHangs.Where(m => m.MaMH == maMH).FirstOrDefault();
            if (mh == null)
            {
                return "";
            }
            return mh.DonViTinh.TenDVT;
        }
    }
}

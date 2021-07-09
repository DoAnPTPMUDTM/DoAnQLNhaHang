using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class MonBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public MonBLLDAL()
        {

        }
        public List<Mon> getDataMon()
        {
            return db.Mons.ToList();
        }
        public List<Mon> getDataMonByNhomMon(int maNhom)
        {
            return db.Mons.Where(m => m.MaNhom == maNhom).ToList();
        }
        public Mon getMonByMaMon(int maMon)
        {
            return db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
        }
        //insert
        public void insertMonAn(Mon mon)
        {
            if(mon != null)
            {
                db.Mons.InsertOnSubmit(mon);
                db.SubmitChanges();
            }
        }
        //update
        //mã món, tên món,nhóm món, đvt, anh mon, giá gốc,giá km,maKm
        public void updateMonAn(int maMon, int maNhom, string tenMon, int donViTinh,string anh, double giaGoc, double giaKM, int maKM)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            //KhuyenMai km = db.KhuyenMais.Where(t => t.MaKM == maKM).FirstOrDefault();
            if (mon != null)
            {
                mon.TenMon = tenMon;
                mon.MaNhom = maNhom;
                mon.DonViTinh = db.DonViTinhs.Where(t => t.MaDVT == donViTinh).FirstOrDefault();
                mon.Anh = anh;
                mon.GiaGoc = (decimal?)giaGoc;
                mon.GiaKM = (decimal?)giaKM;
                mon.KhuyenMai = db.KhuyenMais.Where(k => k.MaKM == maKM).FirstOrDefault(); 
                db.SubmitChanges();
            }
        }
        public void updateMonAnNoImg(int maMon, int maNhom, string tenMon, int donViTinh, double giaGoc, double giaKM, int maKM)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            if (mon != null)
            {
                mon.TenMon = tenMon;
                mon.MaNhom = maNhom;
                mon.DonViTinh = db.DonViTinhs.Where(t => t.MaDVT == donViTinh).FirstOrDefault(); ;
                mon.GiaGoc = (decimal?)giaGoc;
                mon.GiaKM = (decimal?)giaKM;
                mon.KhuyenMai = db.KhuyenMais.Where(k => k.MaKM == maKM).FirstOrDefault();
                db.SubmitChanges();
            }
        }
        //delete
        public void deleteMonAn(int maMon)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            if(mon != null)
            {
                db.Mons.DeleteOnSubmit(mon);
                db.SubmitChanges();
            }
        }
        public int getMaMonContinue()
        {
            int maMon = 1;
            if (db.Mons.Count() > 0)
            {
                maMon = (db.Mons.OrderByDescending(m => m.MaMon).FirstOrDefault()).MaMon + 1;
            }
            return maMon;
        }
        public string getTenMonByMaMon(int maMon)
        {
            Mon mon = db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
            if(mon != null)
            {
                return mon.TenMon;
            }
            return "Trống";
        }

    }
}

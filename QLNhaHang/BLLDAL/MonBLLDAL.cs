using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class MonBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
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
            if (mon != null)
            {
                db.Mons.InsertOnSubmit(mon);
                db.SubmitChanges();
            }
        }
        //update
        //mã món, tên món,nhóm món, đvt, anh mon, giá gốc,giá km,maKm
        public void updateMonAn(int maMon, int maNhom, string tenMon, int donViTinh, string anh, double giaGoc, double giaKM, int maKM)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            //KhuyenMai km = db.KhuyenMais.Where(t => t.MaKM == maKM).FirstOrDefault();
            if (mon != null)
            {
                mon.TenMon = tenMon;
                mon.MaNhom = maNhom;
                mon.DonViTinh = db.DonViTinhs.Where(t => t.MaDVT == donViTinh).FirstOrDefault();
                mon.Anh = anh;
                mon.GiaGoc = (decimal)giaGoc;
                mon.GiaKM = (decimal)giaKM;
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
                mon.GiaGoc = (decimal)giaGoc;
                mon.GiaKM = (decimal)giaKM;
                mon.KhuyenMai = db.KhuyenMais.Where(k => k.MaKM == maKM).FirstOrDefault();
                db.SubmitChanges();
            }

        }
        //check befor delete
        public bool ktKhoaNgoaiMon(int maMon)
        {
            bool checkCTHD = db.CTHDs.Where(c => c.MaMon == maMon).Count() > 0;
            bool checkGMTB = db.GoiMonTaiBans.Where(g => g.MaMon == maMon).Count() > 0;
            if(checkCTHD || checkGMTB)
            {
                return true;
            }
            return false;
        }

        //delete
        public void deleteMonAn(int maMon)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            if (mon != null)
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
            if (mon != null)
            {
                return mon.TenMon;
            }
            return "Trống";
        }
        public List<Chart> getTKMonTheo(DateTime ngayBD, DateTime ngayKT)
        {
            List<Chart> lstChart = new List<Chart>();
            var data = from h in db.HoaDons
                       from c in db.CTHDs
                       where c.MaHD == h.MaHD && h.Ngay.Value.Date >= ngayBD.Date && h.Ngay.Value.Date <= ngayKT.Date && h.TinhTrang == 1
                       group c by c.MaMon into g
                       select new
                       {
                           Ma = g.Key,
                           X = getTenByMa(g.Key),
                           Y = g.Sum(s => s.DonGia * s.SoLuong),
                           SL = g.Sum(s => s.SoLuong)
                       };
            foreach (var item in data)
            {
                Chart chart = new Chart((double)item.Y, item.X.ToString(), (int)item.SL, item.Ma);
                lstChart.Add(chart);
            }
            return lstChart;
        }
        public string getTenByMa(int maMon)
        {
            Mon mon = db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
            if (mon == null)
            {
                return "";
            }
            return mon.TenMon;
        }
        public decimal getDonGiaByMaMon(int maMon)
        {
            Mon mon = db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
            if (mon == null)
                return 0;
            return mon.GiaKM;
        }
        public List<Mon> getMonByResult(int[][] matches)
        {
            List<Mon> lstMon = new List<Mon>();
            for (int i = 0; i < matches.Length; i++)
            {

                for (int j = 0; j < matches[i].Length; j++)
                {
                    Mon mon = db.Mons.Where(m => m.MaMon == matches[i][j]).FirstOrDefault();
                    if (mon != null)
                    {
                        bool check = lstMon.Where(a => a.MaMon == mon.MaMon).Count() > 0;
                        if(check == false)
                        {
                            lstMon.Add(mon);
                        }
                    }
                }
            }
            return lstMon;
        }

        public string getTenDVTByMaMon(int maMon)
        {
            Mon mon = db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
            if(mon == null)
            {
                return "";
            }
            return mon.DonViTinh.TenDVT;
        }
        public void updateMaKMChange(int maKM)
        {
            List<Mon> lst = db.Mons.Where(m => m.MaKM == maKM).ToList();
            if(lst == null || (lst != null && lst.Count == 0))
            {
                return;
            }
            KhuyenMai km = db.KhuyenMais.Where(k => k.MaKM == maKM).FirstOrDefault();
            if(km != null)
            {
                foreach(Mon m in lst)
                {
                    Mon monUpdate = db.Mons.Where(ma => ma.MaMon == m.MaMon).FirstOrDefault();
                    if(monUpdate != null)
                    {
                        decimal giaKM = monUpdate.GiaGoc - (monUpdate.GiaGoc * (decimal)km.TyLe.Value);
                        monUpdate.GiaKM = giaKM;
                    }
                }
                db.SubmitChanges();
            }           
        }
    }
}

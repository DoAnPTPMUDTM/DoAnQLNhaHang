using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class PhanQuyenBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public PhanQuyenBLLDAL()
        {

        }
        public List<PhanQuyen> getDataPhanQuyen()
        {
            return db.PhanQuyens.ToList<PhanQuyen>();
        }
        public void insertPhanQuyen(PhanQuyen pq)
        {
            if(pq != null)
            {
                db.PhanQuyens.InsertOnSubmit(pq);
                db.SubmitChanges();
            }
        }
        //public void updatePhanQuyen(int maNhom, int maMH,int coQuyen)
        //{
        //    PhanQuyen pq = db.PhanQuyens.Where(t => t.MaNhom == maNhom && t.MaMH == maMH).FirstOrDefault();
        //    if(pq != null)
        //    {
        //        pq.CoQuyen = coQuyen;
        //        db.SubmitChanges();
        //    }

        //}
        public bool kiemTraKhoaChinhPhanQuyen(int maNhom, int maMH)
        {
            bool countMaNhom = db.NhomNguoiDungs.Where(t => t.MaNhom == maNhom).Count() > 0;
            bool countMaMH = db.ManHinhs.Where(t => t.MaMH == maMH).Count() > 0;
            if(countMaNhom || countMaMH)
            {
                return true;
            }
            return false;
        }
        //
        public List<PhanQuyen> getPhanQuyenByMaNhom(int maNhom)
        {
            return db.PhanQuyens.Where(t => t.MaNhom == maNhom).ToList<PhanQuyen>();
        }
        public List<PhanQuyens> getPhanQuyensByMaNhom(int maNhom)
        {
            var phanQuyens = from mh in db.ManHinhs
                             join pq in db.PhanQuyens.Where(p => p.MaNhom == maNhom)
                             on mh.MaMH equals pq.MaMH into t
                             from nt in t.DefaultIfEmpty()
                             select new
                             {
                                 MaMH = mh.MaMH,
                                 TenMH = mh.TenMH,
                                 CoQuyen = convertIntToBool(nt.CoQuyen)
                             };
            List<PhanQuyens> lstPhanQuyen = new List<PhanQuyens>();
            foreach(var p in phanQuyens)
            {
                PhanQuyens phanQuyen = new PhanQuyens(p.MaMH, p.TenMH, p.CoQuyen);
                lstPhanQuyen.Add(phanQuyen);
            }
            return lstPhanQuyen;
        }
        public bool convertIntToBool(int? coQuyen)
        {
            if (coQuyen == null)
                return false;
            if (coQuyen == 0)
                return false;
            return true;
        }
        public void updatePhanQuyen(int maNhom, int maMH, int quyen)
        {
            PhanQuyen phanQuyen = db.PhanQuyens.Where(p => p.MaMH == maMH && p.MaNhom == maNhom).FirstOrDefault();
            if(phanQuyen != null)
            {
                if(phanQuyen.CoQuyen != quyen)
                {
                    phanQuyen.CoQuyen = quyen;
                    db.SubmitChanges();
                }
            }
            else
            {
                PhanQuyen pq = new PhanQuyen();
                pq.MaNhom = maNhom;
                pq.MaMH = maMH;
                pq.CoQuyen = quyen;
                db.PhanQuyens.InsertOnSubmit(pq);
                db.SubmitChanges();
            }
        }
        public List<PhanQuyen> getQuyenByMaNhom(int maNhom)
        {
            return db.PhanQuyens.Where(p => p.MaNhom == maNhom).ToList();
        }
    }
}

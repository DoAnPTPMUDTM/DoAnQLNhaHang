using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class PhanQuyenBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
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
        public void updatePhanQuyen(int maNhom, int maMH,int coQuyen)
        {
            PhanQuyen pq = db.PhanQuyens.Where(t => t.MaNhom == maNhom && t.MaMH == maMH).FirstOrDefault();
            if(pq != null)
            {
                pq.CoQuyen = coQuyen;
                db.SubmitChanges();
            }

        }
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
    }
}

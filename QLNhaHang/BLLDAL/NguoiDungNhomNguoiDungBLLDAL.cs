using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class NguoiDungNhomNguoiDungBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public NguoiDungNhomNguoiDungBLLDAL()
        {

        }
        public List<NguoiDungNhomNguoiDung> getNhomNguoiDungByMaNhom(int maNhom)
        {
            return db.NguoiDungNhomNguoiDungs.Where(t => t.MaNhom == maNhom).ToList<NguoiDungNhomNguoiDung>();
        }
        //
        
        //
        public void insertnguoiDungNhomNguoiDung(NguoiDungNhomNguoiDung ndnnd)
        {
            if(ndnnd != null)
            {
                db.NguoiDungNhomNguoiDungs.InsertOnSubmit(ndnnd);
                db.SubmitChanges();
            }
        }
        public bool kTraTrungMaNhom(int maNhom, int maND)
        {
            bool countMaNhom = db.NguoiDungNhomNguoiDungs.Where(t => t.MaNhom == maNhom && t.MaND == maND).Count() > 0;
            if (countMaNhom)
            {
                return true;
            }
            return false;
        }
        public void deleteNguoiDungNhomNguoiDung(int maND)
        {
            NguoiDungNhomNguoiDung nguoiDungNhomNguoiDung = db.NguoiDungNhomNguoiDungs.Where(t => t.MaND == maND).FirstOrDefault();
            if (nguoiDungNhomNguoiDung != null)
            {
                db.NguoiDungNhomNguoiDungs.DeleteOnSubmit(nguoiDungNhomNguoiDung);
                db.SubmitChanges();
            }
        }

    }
}

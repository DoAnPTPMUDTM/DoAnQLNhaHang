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
        public void insertnguoiDungNhomNguoiDung(NguoiDungNhomNguoiDung ndnnd)
        {
            if(ndnnd != null)
            {
                db.NguoiDungNhomNguoiDungs.InsertOnSubmit(ndnnd);
                db.SubmitChanges();
            }
        }
        public bool kTraTrungMaNhom(int maNhom, string tenDN)
        {
            bool countMaNhom = db.NguoiDungNhomNguoiDungs.Where(t => t.MaNhom == maNhom && t.TenDN == tenDN).Count() > 0;
            if (countMaNhom)
            {
                return true;
            }
            return false;
        }
        public void deleteNguoiDungNhomNguoiDung(string tenDN)
        {
            NguoiDungNhomNguoiDung nguoiDungNhomNguoiDung = db.NguoiDungNhomNguoiDungs.Where(t => t.TenDN == tenDN).FirstOrDefault();
            if(nguoiDungNhomNguoiDung != null)
            {
                db.NguoiDungNhomNguoiDungs.DeleteOnSubmit(nguoiDungNhomNguoiDung);
                db.SubmitChanges();
            }
        }
    }
}

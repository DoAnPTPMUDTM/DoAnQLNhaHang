using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class NhomNguoiDungBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public NhomNguoiDungBLLDAL()
        {

        }
        public List<NhomNguoiDung> getDataNhomNguoiDung()
        {
            //Cách 1:
            //var nhomNguoiDungs = from nnd in db.NhomNguoiDungs
            //                     select new
            //                     {
            //                         nnd.MaNhom,
            //                         nnd.TenNhom,
            //                         nnd.GhiChu
            //                     };
            //var convertNhomNguoiDung = nhomNguoiDungs.ToList().ConvertAll(t => new NhomNguoiDung()
            //{
            //    MaNhom = t.MaNhom,
            //    TenNhom = t.TenNhom,
            //    GhiChu = t.GhiChu
            //});
            //return convertNhomNguoiDung.ToList<NhomNguoiDung>();
            return db.NhomNguoiDungs.ToList<NhomNguoiDung>();
        }
        public void insertNhomNguoiDung(NhomNguoiDung nhomNguoiDung)
        {
            db.NhomNguoiDungs.InsertOnSubmit(nhomNguoiDung);
            db.SubmitChanges();
        }
        public void updateNhomNguoiDung(int maNhom, string tenNhom, string ghiChu)
        {
            NhomNguoiDung nhomNguoiDung = db.NhomNguoiDungs.Where(t => t.MaNhom == maNhom).FirstOrDefault();
            if(nhomNguoiDung != null)
            {
                nhomNguoiDung.TenNhom = tenNhom;
                nhomNguoiDung.GhiChu = ghiChu;
                db.SubmitChanges();
            }

        }
        public void deleteNhomNguoiDung(int maNhom)
        {
            NhomNguoiDung nhomNguoiDung = db.NhomNguoiDungs.Where(t => t.MaNhom == maNhom).FirstOrDefault();
            if(nhomNguoiDung != null)
            {
                db.NhomNguoiDungs.DeleteOnSubmit(nhomNguoiDung);
                db.SubmitChanges();
            }
        }
        //
        
       
    }
}

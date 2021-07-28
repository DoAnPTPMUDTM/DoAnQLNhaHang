using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class DinhLuongBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public DinhLuongBLLDAL()
        {

        }
        public List<DinhLuong> getDinhLuongByMaMon(int maMon)
        {
            return db.DinhLuongs.Where(d => d.MaMon == maMon).ToList();
        }
        public bool ktKhoaChinh(int maMon, int maMH)
        {
            DinhLuong dl = db.DinhLuongs.Where(d => d.MaMon == maMon && d.MaMH == maMH).FirstOrDefault();
            if (dl == null)
                return false;
            return true;
        }
        public void insertDinhLuong(DinhLuong dl)
        {
            if(dl != null)
            {
                db.DinhLuongs.InsertOnSubmit(dl);
                db.SubmitChanges();
            }
        }
        public void updateDinhLuong(int maMon, int maMH, decimal quyDoi)
        {
            DinhLuong dl = db.DinhLuongs.Where(d => d.MaMon == maMon && d.MaMH == maMH).FirstOrDefault();
            if(dl != null)
            {
                dl.QuyDoi = quyDoi;
                db.SubmitChanges();
            }
        }

        public void deleteDinhLuong(int maMon, int maMH)
        {
            DinhLuong dl = db.DinhLuongs.Where(d => d.MaMon == maMon && d.MaMH == maMH).FirstOrDefault();
            if(dl != null)
            {
                db.DinhLuongs.DeleteOnSubmit(dl);
                db.SubmitChanges();
            }
        }
    }
}

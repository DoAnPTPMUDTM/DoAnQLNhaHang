using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class PhieuNhapBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public PhieuNhapBLLDAL()
        {

        }
        public List<PhieuNhap> getAllPhieuNhap()
        {
            return db.PhieuNhaps.ToList();
        }
        //insert PhieuNhap
        public void insertPhieuNhapHang(PhieuNhap pn)
        {
            if(pn!= null)
            {
                db.PhieuNhaps.InsertOnSubmit(pn);
                db.SubmitChanges();
            }    
        }
    }
}


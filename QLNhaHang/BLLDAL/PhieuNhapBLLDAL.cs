using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class PhieuNhapBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public PhieuNhapBLLDAL()
        {

        }
        public List<PhieuNhap> getAllPhieuNhap()
        {
            return db.PhieuNhaps.OrderBy(p => p.MaPN).ToList();
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
        public PhieuNhap getPhieuNhapByMaPN(int maPN)
        {
            return db.PhieuNhaps.Where(p => p.MaPN == maPN).FirstOrDefault();
        }
    }
}


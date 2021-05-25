using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
   public class KhachHangBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public KhachHangBLLDAL()
        {

        }
        public List<KhachHang> getDataKhachHang()
        {
            return db.KhachHangs.ToList();
        }
        public KhachHang getDataKhachHangByMaKH(int maKH)
        {
            return db.KhachHangs.Where(k => k.MaKH == maKH).FirstOrDefault();
        }
        public void addDiemTL(int maKH, int diemCong)
        {
            KhachHang kh = db.KhachHangs.Where(k => k.MaKH == maKH).FirstOrDefault();
            if(kh != null)
            {
                kh.DiemTichLuy += diemCong;
                db.SubmitChanges();
            }
        }
        public void subDiemTL(int maKH, int diemTru)
        {
            KhachHang kh = db.KhachHangs.Where(k => k.MaKH == maKH).FirstOrDefault();
            if (kh != null)
            {
                kh.DiemTichLuy -= diemTru;
                db.SubmitChanges();
            }
        }
    }
}

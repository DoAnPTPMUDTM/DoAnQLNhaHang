using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class KhuyenMaiBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public KhuyenMaiBLLDAL()
        {

        }
        public List<KhuyenMai> getDataKhuyenMai()
        {
            return db.KhuyenMais.Where(k => k.NgayKT.Value.Date >= DateTime.Now.Date).ToList();
        }
        public double getGiaKhuyenMaiByMaKM(int maKM)
        {
            KhuyenMai km = db.KhuyenMais.Where(t => t.MaKM == maKM).FirstOrDefault();
            if (km != null)
            {
                return (double)km.TyLe;
            }
            return 1;
        }
        // ktra ngày khuyến mãi hợp lệ hay k
        public bool kTraKhuyenMai(int maKM)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Where(t => t.MaKM == maKM).FirstOrDefault();
            if (khuyenMai.NgayKT < DateTime.Now)
            {
                return true;
            }
            return false;
        }
        public KhuyenMai getKhuyeMaiByMaKM(int maKM)
        {
            return db.KhuyenMais.Where(t => t.MaKM == maKM).FirstOrDefault();
        }
    }
}

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
            return db.KhuyenMais.ToList();
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
       
        public KhuyenMai getKhuyeMaiByMaKM(int maKM)
        {
            return db.KhuyenMais.Where(t => t.MaKM == maKM).FirstOrDefault();
        }
    }
}

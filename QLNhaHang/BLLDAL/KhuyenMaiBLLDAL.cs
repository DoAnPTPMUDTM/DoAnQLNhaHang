using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class KhuyenMaiBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
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
        public void insertKhuyenMai(KhuyenMai khuyenMai)
        {
            if(khuyenMai != null)
            {
                db.KhuyenMais.InsertOnSubmit(khuyenMai);
                db.SubmitChanges();
            }
        }
        public void updateKhuyenMai(int maKM, KhuyenMai khuyenMai)
        {
            KhuyenMai km = db.KhuyenMais.Where(k => k.MaKM == maKM).FirstOrDefault();
            if(km != null)
            {
                km.TenKM = khuyenMai.TenKM;
                km.TyLe = khuyenMai.TyLe;
                db.SubmitChanges();
            }
        }
        public bool ktKhoaNgoai(int maKM)
        {
            return db.Mons.Where(m => m.MaKM == maKM).Count() > 0;
        }
        public void deleteKhuyenMai(int maKM)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Where(k => k.MaKM == maKM).FirstOrDefault();
            if(khuyenMai != null)
            {
                db.KhuyenMais.DeleteOnSubmit(khuyenMai);
                db.SubmitChanges();
            }
        }
    }
}

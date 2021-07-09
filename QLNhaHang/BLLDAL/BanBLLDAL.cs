using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class BanBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public BanBLLDAL()
        {
        }
        public List<Ban> getDataBan()
        {
            return db.Bans.ToList();
        }
        public void capNhatTTMoBan(int maBan)
        {
            Ban ban = db.Bans.Where(b => b.MaBan == maBan).FirstOrDefault();
            if(ban.TrangThai == 0)
            {
                ban.TrangThai = 1;
                db.SubmitChanges();
            }
        }
        public void capNhatTTDongBan(int maBan)
        {
            Ban ban = db.Bans.Where(b => b.MaBan == maBan).FirstOrDefault();
            if (ban.TrangThai == 1)
            {
                ban.TrangThai = 0;
                db.SubmitChanges();
            }
        }
        public int ktTinhTrangBan(int maBan)
        {
            Ban ban = db.Bans.Where(b => b.MaBan == maBan).FirstOrDefault();
            return (int)ban.TrangThai;
        }
        public Ban getBanByMaBan(int maBan)
        {
            return db.Bans.Where(b => b.MaBan == maBan).FirstOrDefault();
        }
        public void insertBan(Ban ban)
        {
            if(ban != null)
            {
                db.Bans.InsertOnSubmit(ban);
                db.SubmitChanges();
            }
        }
        public bool ktKhoaNgoai(int maBan)
        {
            return db.HoaDons.Where(h => h.MaBan == maBan).Count() > 0;
        }
        public void updateBan(int maBan, Ban ban)
        {
            Ban banUpdate = db.Bans.Where(b => b.MaBan == maBan).FirstOrDefault();
            if (banUpdate != null)
            {
                banUpdate.TenBan = ban.TenBan;
                banUpdate.ViTri = ban.ViTri;
                db.SubmitChanges();
            }
        }
        public void deleteBan(int maBan)
        {
            Ban banDelete = db.Bans.Where(b => b.MaBan == maBan).FirstOrDefault();
            if (banDelete != null)
            {
                db.Bans.DeleteOnSubmit(banDelete);
                db.SubmitChanges();
            }
        }
    }
}

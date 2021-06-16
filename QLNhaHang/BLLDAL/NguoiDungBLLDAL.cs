using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class NguoiDungBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public NguoiDungBLLDAL()
        {

        }
        public List<NguoiDung> getDataNguoiDung()
        {
            return db.NguoiDungs.ToList();
        }
        public void insertNguoiDung(NguoiDung nd)
        {
            if(nd != null)
            {
                db.NguoiDungs.InsertOnSubmit(nd);
                db.SubmitChanges();
            }
        }
        public void updateNguoiDung(int maND, string hoTen, string gioiTinh, string SDT, string diaChi, string Email,string tenDN, string matKhau,bool hoatDong)
        {
            //mã nd, họ tên, giới tính, sdt, địa chỉ, email, tên đn, mk, hoạt động
            NguoiDung nd = db.NguoiDungs.Where(t => t.MaND == maND).FirstOrDefault();
            if(nd != null)
            {
                nd.HoTen = hoTen;
                nd.GioiTinh = gioiTinh;
                nd.SDT = SDT;
                nd.DiaChi = diaChi;
                nd.Email = Email;
                nd.TenDN = tenDN;
                nd.MatKhau = matKhau;
                nd.HoatDong = hoatDong;
                db.SubmitChanges();
            }
        }
        public void deleteNguoiDung(int maND)
        {
            NguoiDung nd = db.NguoiDungs.Where(t => t.MaND == maND).FirstOrDefault();
            if(nd != null)
            {
                db.NguoiDungs.DeleteOnSubmit(nd);
                db.SubmitChanges();
            }
        }
        public bool ktraTrungEmail(string email)
        {
            bool countEmail = db.NguoiDungs.Where(t => t.Email == email).Count() > 0;
            if (countEmail)
            {
                return true;
            }
            return false;
        }
        public bool ktraTrungTenDN(string tenDN)
        {
            bool countTenDN = db.NguoiDungs.Where(t => t.TenDN == tenDN).Count() > 0;
            if (countTenDN)
            {
                return true;
            }
            return false;
        }
    }
}

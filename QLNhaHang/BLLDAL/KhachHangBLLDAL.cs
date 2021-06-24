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
        //get số hoá đơn theo mã khách hàng
        public int soHoaDon(int maKH)
        {
            return db.HoaDons.Where(k => k.MaKH == maKH).Count();
        }
        //insert
        public void insertKhachHang(KhachHang khachHang)
        {
            if(khachHang != null)
            {
                db.KhachHangs.InsertOnSubmit(khachHang);
                db.SubmitChanges();
            }
        }
        //update/
        //mã kh, tên kh, địa chỉ, sdt, diem tl.
        public void updateKhachHang(int maKH, string tenKH, string diaChi, string SDT, int diemTL)
        {
            KhachHang kh = db.KhachHangs.Where(t => t.MaKH == maKH).FirstOrDefault();
            if(kh != null)
            {
                kh.TenKH = tenKH;
                kh.DiaChi = diaChi;
                kh.SDT = SDT;
                kh.DiemTichLuy = diemTL;
                db.SubmitChanges();
            }
        }
        //delete
        public void deleteKhachHang(int maKH)
        {
            KhachHang kh = db.KhachHangs.Where(t => t.MaKH == maKH).FirstOrDefault();
            if(kh!= null)
            {
                db.KhachHangs.DeleteOnSubmit(kh);
                db.SubmitChanges();
            }
        }
    }
}

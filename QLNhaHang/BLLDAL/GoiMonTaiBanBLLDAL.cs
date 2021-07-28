using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class GoiMonTaiBanBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public GoiMonTaiBanBLLDAL()
        {

        }
        public List<GoiMonTaiBan> getGMTBByMaBan(int maBan)
        {
            List<GoiMonTaiBan> lst = (from h in db.HoaDons
                                      from g in db.GoiMonTaiBans
                                      where g.MaHD == h.MaHD && h.MaBan == maBan && h.TinhTrang == 0 && g.TinhTrang == 0
                                      select g).ToList();
            return lst;
        }
        public void ghiNhanGMTB(int maBan)
        {
            List<GoiMonTaiBan> lstGMTB = getGMTBByMaBan(maBan);
            if (lstGMTB == null || (lstGMTB != null && lstGMTB.Count == 0))
            {
                return;
            }
            foreach (GoiMonTaiBan g in lstGMTB)
            {
                CTHD cthd = new CTHD();
                cthd.MaHD = g.MaHD.Value;
                cthd.MaMon = g.MaMon.Value;
                cthd.SoLuong = g.SoLuong.Value;
                cthd.DonGia = getDonGiaByMaMon(g.MaMon.Value);
                cthd.ThanhTien = g.SoLuong.Value * getDonGiaByMaMon(g.MaMon.Value);
                insertCTHD(cthd);
                capNhatTT(g.MaGoiMon);
                truSLNguyenLieu(g.MaMon.Value, g.SoLuong.Value);
            }
        }
        public void truSLNguyenLieu(int maMon, int sl)
        {
            List<DinhLuong> lstDinhLuong = db.DinhLuongs.Where(d => d.MaMon == maMon).ToList();
            if (lstDinhLuong == null || (lstDinhLuong != null && lstDinhLuong.Count == 0))
            {
                return;
            }
            foreach (DinhLuong dl in lstDinhLuong)
            {
                MatHang mh = db.MatHangs.Where(m => m.MaMH == dl.MaMH).FirstOrDefault();
                if (mh != null)
                {
                    decimal slTru = mh.SoLuongTon - (dl.QuyDoi * sl);
                    mh.SoLuongTon = slTru;
                }
            }
            db.SubmitChanges();
        }
        public bool ktGhiNhanGMTB(int maBan,ref string messeage)
        {
            bool check = false;
            List<GoiMonTaiBan> lstGMTB = getGMTBByMaBan(maBan);
            if (lstGMTB == null || (lstGMTB != null && lstGMTB.Count == 0))
            {
                return false;
            }
            foreach (GoiMonTaiBan g in lstGMTB)
            {
                List<DinhLuong> lstDL = db.DinhLuongs.Where(d => d.MaMon == g.MaMon).ToList();
                if(lstDL == null || (lstDL != null && lstDL.Count == 0))
                {
                    continue;
                }
                foreach(DinhLuong dl in lstDL)
                {
                    MatHang mh = db.MatHangs.Where(m => m.MaMH == dl.MaMH).FirstOrDefault();
                    if(mh == null)
                    {
                        continue;
                    }

                    if(mh.SoLuongTon == 0 || mh.SoLuongTon < (dl.QuyDoi*g.SoLuong))
                    {
                        messeage += dl.Mon.TenMon + " ,";
                        check = true;
                    }
                }
            }
            if (check)
            {
                messeage += "không đủ số lượng bán";
            }
            return check;
        }
        public void capNhatTT(int maGoiMon)
        {
            GoiMonTaiBan goiMonTaiBan = db.GoiMonTaiBans.Where(g => g.MaGoiMon == maGoiMon).FirstOrDefault();
            if (goiMonTaiBan != null)
            {
                goiMonTaiBan.TinhTrang = 1;
                db.SubmitChanges();
            }
        }
        public decimal getDonGiaByMaMon(int maMon)
        {
            Mon mon = db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
            if (mon == null)
                return 0;
            return mon.GiaKM;
        }
        public void insertCTHD(CTHD cthd)
        {
            CTHD ct = db.CTHDs.Where(c => c.MaHD == cthd.MaHD && c.MaMon == cthd.MaMon).FirstOrDefault();
            if (ct == null)
            {
                db.CTHDs.InsertOnSubmit(cthd);
                db.SubmitChanges();
            }
            else
            {
                ct.SoLuong += cthd.SoLuong;
                ct.ThanhTien += cthd.ThanhTien;
                db.SubmitChanges();
            }
        }
        public void delete(int maGoiMon)
        {
            GoiMonTaiBan goiMonTaiBan = db.GoiMonTaiBans.Where(g => g.MaGoiMon == maGoiMon).FirstOrDefault();
            if (goiMonTaiBan != null)
            {
                db.GoiMonTaiBans.DeleteOnSubmit(goiMonTaiBan);
                db.SubmitChanges();
            }
        }
    }
}

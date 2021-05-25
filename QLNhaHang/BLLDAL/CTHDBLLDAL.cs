using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class CTHDBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public CTHDBLLDAL()
        {

        }
        public List<CTHD> getCTDHByMaHD(int maHD)
        {
            return db.CTHDs.Where(c => c.MaHD == maHD).ToList();
        }
        public int getNumberCTHDByMaHD(int maHD)
        {
            return db.CTHDs.Where(c => c.MaHD == maHD).Count();
        }
        public void deleteCTHDByMaHD(int maHD)
        {
            List<CTHD> lstCTDH = db.CTHDs.Where(c => c.MaHD == maHD).ToList();
            if(lstCTDH.Count > 0)
            {
                db.CTHDs.DeleteAllOnSubmit(lstCTDH);
                db.SubmitChanges();
            }
        }
        public void inCreNumberMon(int maHD,int maMon)
        {
            CTHD cTHD = db.CTHDs.Where(c => c.MaHD == maHD && c.MaMon == maMon).FirstOrDefault();
            if(cTHD != null)
            {
                cTHD.SoLuong += 1;
                cTHD.ThanhTien = cTHD.SoLuong * cTHD.DonGia;
                db.SubmitChanges();
            }
        }
        public void deCreNumberMon(int maHD, int maMon)
        {
            CTHD cTHD = db.CTHDs.Where(c => c.MaHD == maHD && c.MaMon == maMon).FirstOrDefault();
            if (cTHD != null)
            {
                if (cTHD.SoLuong > 1)
                {
                    cTHD.SoLuong -= 1;
                    cTHD.ThanhTien = cTHD.SoLuong * cTHD.DonGia;
                    db.SubmitChanges();
                }
            }
        }
        
        public int getNumberMonCurrent(int maHD, int maMon)
        {
            CTHD cTHD = db.CTHDs.Where(c => c.MaHD == maHD && c.MaMon == maMon).FirstOrDefault();
            if (cTHD != null)
            {
                return cTHD.SoLuong.Value;
            }
            return 0;
        }
        public void deleteCTHD(int maHD, int maMon)
        {
            CTHD cTHD = db.CTHDs.Where(c => c.MaHD == maHD && c.MaMon == maMon).FirstOrDefault();
            if (cTHD != null)
            {
                db.CTHDs.DeleteOnSubmit(cTHD);
                db.SubmitChanges();
            }
        }
        public void insertCTHD(CTHD cthd)
        {
            CTHD ct = db.CTHDs.Where(c => c.MaHD == cthd.MaHD && c.MaMon == cthd.MaMon).FirstOrDefault();
            if(ct == null)
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

        public double totalMoney(int maHD)
        {
            return (double)db.CTHDs.Where(c => c.MaHD == maHD).ToList().Sum(s => s.ThanhTien);
        }


        public bool isExitedCTHD(int maHD, int maMon)
        {
            CTHD cTHD = db.CTHDs.Where(c => c.MaHD == maHD && c.MaMon == maMon).FirstOrDefault();
            if (cTHD == null)
            {
                return false;
            }
            return true;
        }

        public void updateMaHDDoiBan(int maHDCu, int maHDMoi)
        {
            List<CTHD> lstCTHD = db.CTHDs.Where(c => c.MaHD == maHDCu).ToList();
            if(lstCTHD.Count > 0)
            {
                foreach(CTHD cthdCu in lstCTHD)
                {
                    CTHD ktCTHDMoi = db.CTHDs.Where(c => c.MaHD == maHDMoi && c.MaMon == cthdCu.MaMon).FirstOrDefault();
                    if(ktCTHDMoi == null)
                    {
                        CTHD cthdMoi = new CTHD();
                        cthdMoi.MaHD = maHDMoi;
                        cthdMoi.MaMon = cthdCu.MaMon;
                        cthdMoi.SoLuong = cthdCu.SoLuong;
                        cthdMoi.DonGia = cthdCu.DonGia;
                        cthdMoi.ThanhTien = cthdCu.ThanhTien;
                        cthdMoi.GhiChu = cthdCu.GhiChu;
                        db.CTHDs.InsertOnSubmit(cthdMoi);
                    }
                    else
                    {
                        ktCTHDMoi.SoLuong += cthdCu.SoLuong;
                        ktCTHDMoi.ThanhTien += cthdCu.ThanhTien;
                    }
                    db.CTHDs.DeleteOnSubmit(cthdCu);
                    db.SubmitChanges();
                }    
            }
        }
    }
}

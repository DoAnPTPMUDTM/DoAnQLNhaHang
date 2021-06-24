﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class MonBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public MonBLLDAL()
        {

        }
        public List<Mon> getDataMon()
        {
            return db.Mons.ToList();
        }
        public List<Mon> getDataMonByNhomMon(int maNhom)
        {
            return db.Mons.Where(m => m.MaNhom == maNhom).ToList();
        }
        public Mon getMonByMaMon(int maMon)
        {
            return db.Mons.Where(m => m.MaMon == maMon).FirstOrDefault();
        }
        //insert
        public void insertMonAn(Mon mon)
        {
            if(mon != null)
            {
                db.Mons.InsertOnSubmit(mon);
                db.SubmitChanges();
            }
        }
        //update
        //mã món, tên món,nhóm món, đvt, anh mon, giá gốc,giá km,maKm
        public void updateMonAn(int maMon, int maNhom, string tenMon, string donViTinh,string anh, double giaGoc, double giaKM, int maKM)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            if(mon != null)
            {
                mon.MaNhom = maNhom;
                mon.TenMon = tenMon;
                mon.DVT = donViTinh;
                mon.Anh = anh;
                mon.GiaGoc = (decimal?)giaGoc;
                mon.GiaKM = (decimal?)giaKM;
                mon.MaKM = maKM;
                db.SubmitChanges();
            }
        }
        //delete
        public void deleteMonAn(int maMon)
        {
            Mon mon = db.Mons.Where(t => t.MaMon == maMon).FirstOrDefault();
            if(mon != null)
            {
                db.Mons.DeleteOnSubmit(mon);
                db.SubmitChanges();
            }
        }

    }
}

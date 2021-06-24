using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class NhomMonBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public NhomMonBLLDAL()
        {

        }
        public List<NhomMon> getDataNhomMon()
        {
            return db.NhomMons.ToList();
        }
        //get 1 NhomMon by MaNhom
        public NhomMon getNhomMonByMaMon(int maNhom)
        {
            return db.NhomMons.Where(t => t.MaNhom == maNhom).FirstOrDefault();
        }
        //insert
        public void insertNhomMon(NhomMon nhomMon)
        {
            if(nhomMon != null)
            {
                db.NhomMons.InsertOnSubmit(nhomMon);
                db.SubmitChanges();
            }
        }
        //update
        public void updateNhomMon(int maNhom, string tenNhom)
        {
            NhomMon nhomMon = db.NhomMons.Where(t => t.MaNhom == maNhom).FirstOrDefault();
            if(nhomMon != null)
            {
                nhomMon.TenNhom = tenNhom;
                db.SubmitChanges();
            }
        }
        //delete
        public void deleteNhomMon(int maNhom)
        {
            //Kiểm tra nếu có món ăn thuộc nhóm món ăn này thì không xoá được
            NhomMon nhomMon = db.NhomMons.Where(t => t.MaNhom == maNhom).FirstOrDefault();
            if(nhomMon != null)
            {
                db.NhomMons.DeleteOnSubmit(nhomMon);
                db.SubmitChanges();
            }
        }
        //
    }
}

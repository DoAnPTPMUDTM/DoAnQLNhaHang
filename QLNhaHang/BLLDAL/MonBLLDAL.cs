using System;
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
    }
}

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
    }
}

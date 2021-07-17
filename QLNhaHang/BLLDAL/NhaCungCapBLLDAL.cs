using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class NhaCungCapBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public NhaCungCapBLLDAL()
        {

        }
        public List<NhaCungCap> getAllNhaCungCap()
        {
            return db.NhaCungCaps.ToList();
        }
    }
}

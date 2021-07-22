using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class LoaiMatHangBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public LoaiMatHangBLLDAL()
        {

        }
        public List<LoaiMatHang> getAllLoaiMatHang()
        {
            return db.LoaiMatHangs.ToList();
        }
    }
}

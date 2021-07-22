using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class MatHangBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public MatHangBLLDAL()
        {

        }
        public List<MatHang> getAllMatHang()
        {
            return db.MatHangs.ToList();
        }
        public List<MatHang> getMatHangByMaMaLoaiMH(int maLoaiMH)
        {
            return db.MatHangs.Where(t => t.MaLoaiMH == maLoaiMH).ToList();
        }
        public MatHang getMatHangByMaMH(int maMH)
        {
            return db.MatHangs.Where(t => t.MaMH == maMH).FirstOrDefault();
        }
        
    }
}

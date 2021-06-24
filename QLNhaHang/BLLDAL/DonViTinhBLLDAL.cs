using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class DonViTinhBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public DonViTinhBLLDAL()
        {

        }
        public List<DonViTinh> getDataDVT()
        {
            return db.DonViTinhs.ToList();
        }
    }
}

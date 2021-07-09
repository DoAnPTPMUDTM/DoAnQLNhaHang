using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class PhanQuyens
    {
        public PhanQuyens()
        {

        }
        public PhanQuyens(int maMH, string tenMH, bool coQuyen)
        {
            this.MaMH = maMH;
            this.TenMH = tenMH;
            this.CoQuyen = coQuyen;
        }

        public int MaMH { get; set; }
        public string TenMH { get; set; }
        public bool CoQuyen { get; set; }
    }
}

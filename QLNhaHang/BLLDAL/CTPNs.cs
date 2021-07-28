using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class CTPNs
    {
        public CTPNs()
        {

        }
        public int maMH { get; set; }
        public string tenMH { get; set; }
        public int soLuong { get; set; }
        public double donGia { get; set; }
        public string dvt { get; set; }
        public double thanhTien
        {
            get { return soLuong * donGia; }
        } 
        
    }
}

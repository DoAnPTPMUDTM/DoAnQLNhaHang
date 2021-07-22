using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class InCheBien
    {
        public InCheBien()
        {

        }

        public InCheBien(int maMon, string tenMon, int soLuong, string ghiChu)
        {
            MaMon = maMon;
            TenMon = tenMon;
            SoLuong = soLuong;
            GhiChu = ghiChu;
        }

        public InCheBien(int maBan, int maMon, string tenMon, int soLuong, string ghiChu)
        {
            this.MaBan = maBan;
            this.MaMon = maMon;
            this.TenMon = tenMon;
            this.SoLuong = soLuong;
            this.GhiChu = ghiChu;
        }
        public int MaBan { get; set; }
        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}

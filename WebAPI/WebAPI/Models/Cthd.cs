using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Cthd
    {
        public int MaHd { get; set; }
        public int MaMon { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual Mon MaMonNavigation { get; set; }
    }
}

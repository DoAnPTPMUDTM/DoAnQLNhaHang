using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Ctpn
    {
        public int MaPn { get; set; }
        public int MaMh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal? ThanhTien { get; set; }

        public virtual MatHang MaMhNavigation { get; set; }
        public virtual PhieuNhap MaPnNavigation { get; set; }
    }
}

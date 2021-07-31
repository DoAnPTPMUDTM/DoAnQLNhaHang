using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public int? DiemTichLuy { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}

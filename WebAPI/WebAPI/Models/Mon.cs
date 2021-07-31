using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Mon
    {
        public Mon()
        {
            Cthds = new HashSet<Cthd>();
            DinhLuongs = new HashSet<DinhLuong>();
            GoiMonTaiBans = new HashSet<GoiMonTaiBan>();
        }

        public int MaMon { get; set; }
        public int MaNhom { get; set; }
        public int MaDvt { get; set; }
        public string TenMon { get; set; }
        public string Anh { get; set; }
        public decimal GiaGoc { get; set; }
        public decimal GiaKm { get; set; }
        public int? MaKm { get; set; }

        public virtual DonViTinh MaDvtNavigation { get; set; }
        public virtual KhuyenMai MaKmNavigation { get; set; }
        public virtual NhomMon MaNhomNavigation { get; set; }
        public virtual ICollection<Cthd> Cthds { get; set; }
        public virtual ICollection<DinhLuong> DinhLuongs { get; set; }
        public virtual ICollection<GoiMonTaiBan> GoiMonTaiBans { get; set; }
    }
}

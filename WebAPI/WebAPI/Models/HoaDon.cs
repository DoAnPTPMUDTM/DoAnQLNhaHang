using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            Cthds = new HashSet<Cthd>();
            GoiMonTaiBans = new HashSet<GoiMonTaiBan>();
        }

        public int MaHd { get; set; }
        public int? MaBan { get; set; }
        public int? MaNv { get; set; }
        public int? MaKh { get; set; }
        public DateTime? Ngay { get; set; }
        public decimal? TongTien { get; set; }
        public decimal? TienGiam { get; set; }
        public decimal? ThanhTien { get; set; }
        public int? TinhTrang { get; set; }

        public virtual Ban MaBanNavigation { get; set; }
        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual NguoiDung MaNvNavigation { get; set; }
        public virtual ICollection<Cthd> Cthds { get; set; }
        public virtual ICollection<GoiMonTaiBan> GoiMonTaiBans { get; set; }
    }
}

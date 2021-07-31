using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class MatHang
    {
        public MatHang()
        {
            Ctpns = new HashSet<Ctpn>();
            DinhLuongs = new HashSet<DinhLuong>();
        }

        public int MaMh { get; set; }
        public int MaDvt { get; set; }
        public string TenMh { get; set; }
        public int? MaLoaiMh { get; set; }
        public decimal SoLuongTon { get; set; }

        public virtual DonViTinh MaDvtNavigation { get; set; }
        public virtual LoaiMatHang MaLoaiMhNavigation { get; set; }
        public virtual ICollection<Ctpn> Ctpns { get; set; }
        public virtual ICollection<DinhLuong> DinhLuongs { get; set; }
    }
}

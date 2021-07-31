using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            Ctpns = new HashSet<Ctpn>();
        }

        public int MaPn { get; set; }
        public int? MaNv { get; set; }
        public int? MaNcc { get; set; }
        public DateTime? Ngay { get; set; }
        public decimal TongTien { get; set; }

        public virtual NhaCungCap MaNccNavigation { get; set; }
        public virtual NguoiDung MaNvNavigation { get; set; }
        public virtual ICollection<Ctpn> Ctpns { get; set; }
    }
}

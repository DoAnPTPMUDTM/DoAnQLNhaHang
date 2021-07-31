using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class Ban
    {
        public Ban()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaBan { get; set; }
        public string TenBan { get; set; }
        public string ViTri { get; set; }
        public int? TrangThai { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}

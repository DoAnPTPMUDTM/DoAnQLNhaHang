using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            Mons = new HashSet<Mon>();
        }

        public int MaKm { get; set; }
        public string TenKm { get; set; }
        public double? TyLe { get; set; }

        public virtual ICollection<Mon> Mons { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class DonViTinh
    {
        public DonViTinh()
        {
            MatHangs = new HashSet<MatHang>();
            Mons = new HashSet<Mon>();
        }

        public int MaDvt { get; set; }
        public string TenDvt { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<MatHang> MatHangs { get; set; }
        public virtual ICollection<Mon> Mons { get; set; }
    }
}

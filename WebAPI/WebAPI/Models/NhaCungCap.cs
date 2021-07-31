using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            PhieuNhaps = new HashSet<PhieuNhap>();
        }

        public int MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }

        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}

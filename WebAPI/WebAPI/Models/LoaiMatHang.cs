using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class LoaiMatHang
    {
        public LoaiMatHang()
        {
            MatHangs = new HashSet<MatHang>();
        }

        public int MaLoaiMh { get; set; }
        public string TenLoaiMh { get; set; }

        public virtual ICollection<MatHang> MatHangs { get; set; }
    }
}

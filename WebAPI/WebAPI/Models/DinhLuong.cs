using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class DinhLuong
    {
        public int MaMon { get; set; }
        public int MaMh { get; set; }
        public decimal QuyDoi { get; set; }

        public virtual MatHang MaMhNavigation { get; set; }
        public virtual Mon MaMonNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class GoiMonTaiBan
    {
        public int MaGoiMon { get; set; }
        public int? MaHd { get; set; }
        public int? MaMon { get; set; }
        public int? SoLuong { get; set; }
        public int? TinhTrang { get; set; }
        public string GhiChu { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual Mon MaMonNavigation { get; set; }
    }
}

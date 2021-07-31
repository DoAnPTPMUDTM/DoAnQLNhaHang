using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class NguoiDungNhomNguoiDung
    {
        public int MaNd { get; set; }
        public int MaNhom { get; set; }
        public string GhiChu { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; }
        public virtual NhomNguoiDung MaNhomNavigation { get; set; }
    }
}

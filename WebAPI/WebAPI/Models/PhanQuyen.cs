using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class PhanQuyen
    {
        public int MaNhom { get; set; }
        public int MaMh { get; set; }
        public int? CoQuyen { get; set; }

        public virtual ManHinh MaMhNavigation { get; set; }
        public virtual NhomNguoiDung MaNhomNavigation { get; set; }
    }
}

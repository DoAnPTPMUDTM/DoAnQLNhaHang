using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class NhomNguoiDung
    {
        public NhomNguoiDung()
        {
            NguoiDungNhomNguoiDungs = new HashSet<NguoiDungNhomNguoiDung>();
            PhanQuyens = new HashSet<PhanQuyen>();
        }

        public int MaNhom { get; set; }
        public string TenNhom { get; set; }
        public string GhiChu { get; set; }

        public virtual ICollection<NguoiDungNhomNguoiDung> NguoiDungNhomNguoiDungs { get; set; }
        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}

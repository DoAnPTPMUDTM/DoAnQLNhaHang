using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            HoaDons = new HashSet<HoaDon>();
            NguoiDungNhomNguoiDungs = new HashSet<NguoiDungNhomNguoiDung>();
            PhieuNhaps = new HashSet<PhieuNhap>();
        }

        public int MaNd { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string Sdt { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string TenDn { get; set; }
        public string MatKhau { get; set; }
        public bool HoatDong { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<NguoiDungNhomNguoiDung> NguoiDungNhomNguoiDungs { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}

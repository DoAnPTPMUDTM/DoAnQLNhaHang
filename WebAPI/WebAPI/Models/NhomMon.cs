using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class NhomMon
    {
        public NhomMon()
        {
            Mons = new HashSet<Mon>();
        }

        public int MaNhom { get; set; }
        public string TenNhom { get; set; }
        public string Anh { get; set; }

        public virtual ICollection<Mon> Mons { get; set; }
    }
}

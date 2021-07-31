using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models
{
    public partial class ManHinh
    {
        public ManHinh()
        {
            PhanQuyens = new HashSet<PhanQuyen>();
        }

        public int MaMh { get; set; }
        public string TenMh { get; set; }

        public virtual ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}

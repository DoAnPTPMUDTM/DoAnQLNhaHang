using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class BaoCao
    {
        public int MaBaoCao { get; set; }
        public string TenBaoCao { get; set; }
        public BaoCao()
        {

        }
        public BaoCao(int maBaoCao, string tenBaoCao)
        {
            this.MaBaoCao = maBaoCao;
            this.TenBaoCao = tenBaoCao;
        }
        public static List<BaoCao> getLoaiBaoCao()
        {
            List<BaoCao> lstBaoCao = new List<BaoCao>();
            lstBaoCao.Add(new BaoCao(1, "Thống kê theo ngày"));
            lstBaoCao.Add(new BaoCao(2, "Thống kê theo tháng"));
            lstBaoCao.Add(new BaoCao(3, "Thống kê theo món"));
            return lstBaoCao;
        }
    }
}

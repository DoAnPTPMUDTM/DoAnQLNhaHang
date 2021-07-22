using BLLDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaHang.Reports
{
    public partial class frmReportInCheBien : Form
    {
        private List<InCheBien> lstInCheBien;
        string tenBan;
        public frmReportInCheBien()
        {
            InitializeComponent();
        }

        public frmReportInCheBien(List<InCheBien> lstInCheBien, string tenBan)
        {
            InitializeComponent();
            this.lstInCheBien = lstInCheBien;
            this.tenBan = tenBan;
        }
        public void loadRP()
        {
            if (lstInCheBien == null)
            {
                return;
            }
            var data = from p in lstInCheBien
                       select new
                       {
                           TenBan = tenBan,
                           ThoiGian = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                           MaMon = p.MaMon,
                           TenMon = p.TenMon,
                           SoLuong = p.SoLuong,
                           GhiChu = p.GhiChu
                       };
            if (data == null)
            {
                return;
            }
            CrystalReportInCheBien crystalReportInCheBien = new CrystalReportInCheBien();
            crystalReportInCheBien.SetDataSource(data);
            crystalReportViewer1.ReportSource = crystalReportInCheBien;
        }

        private void frmReportInCheBien_Load(object sender, EventArgs e)
        {
            loadRP();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

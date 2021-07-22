using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLLDAL;
using QLNhaHang.Reports;

namespace QLNhaHang
{
    public partial class frmInCheBien : Form
    {
        private List<InCheBien> lstInCheBien;
        string tenBan;
        public frmInCheBien()
        {
            InitializeComponent();
        }
        public frmInCheBien(List<InCheBien> lst, string tenBan)
        {
            InitializeComponent();
            this.lstInCheBien = lst;
            this.tenBan = tenBan;
        }

        private void frmInCheBien_Load(object sender, EventArgs e)
        {
            loadDataInCheBien();
        }
        public void loadDataInCheBien()
        {
            dtgvCheBien.DataSource = lstInCheBien;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (lstInCheBien == null)
            {
                return;
            }
            frmReportInCheBien frm = new frmReportInCheBien(lstInCheBien, tenBan);
            frm.ShowDialog(this);
            this.Hide();
        }
    }
}

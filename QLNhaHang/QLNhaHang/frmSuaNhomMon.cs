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

namespace QLNhaHang
{
    public partial class frmSuaNhomMon : Form
    {
        private string tenNhom;
        private int maNhom;
        public frmSuaNhomMon()
        {
            InitializeComponent();
        }

        public frmSuaNhomMon(string tenNhom, int maNhom)
        {
            InitializeComponent();
            this.maNhom = maNhom;
            this.tenNhom = tenNhom;
        }
        //
        public delegate void StatusUpdateHandler(object sender, EventArgs e, string tenNhom, int maNhom);
        public event StatusUpdateHandler OnUpdate;
        private void UpdateStatus(int maNhom,string tenNhom)
        {
            EventArgs eventArgs = new EventArgs();
            OnUpdate?.Invoke(this, eventArgs, tenNhom, maNhom);
        }


        private void frmSuaNhomMon_Load(object sender, EventArgs e)
        {
            txtMaNhom.Text = maNhom.ToString();
            txtTenNhom.Text = tenNhom;
            txtTenNhom.Focus();
        }

        private void btnSuaNhom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNhom.Text))
            {
                MessageBox.Show("Tên nhóm món không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UpdateStatus(int.Parse(txtMaNhom.Text), txtTenNhom.Text);
            this.Close();
        }
    }
}

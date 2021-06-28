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
    public partial class frmThemNhomMon : Form
    {
        NhomMonBLLDAL nhomMonBLLDAL = new NhomMonBLLDAL();
        public frmThemNhomMon()
        {
            InitializeComponent();
        }
        //
        public delegate void StatusUpdateHandler(object sender, EventArgs e, string tenNhomMon);
        public event StatusUpdateHandler OnUpdate;
        private void UpdateStatus(string tenNhomMon)
        {
            EventArgs args = new EventArgs();
            OnUpdate?.Invoke(this, args, tenNhomMon);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNhom.Text))
            {
                MessageBox.Show("Tên nhóm món không được bỏ trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UpdateStatus(txtTenNhom.Text);

            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

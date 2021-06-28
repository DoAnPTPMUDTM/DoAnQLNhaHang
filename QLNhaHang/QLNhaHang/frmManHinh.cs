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
    public partial class frmManHinh : Form
    {
        ManHinhBLLDAL manHinhBLLDAL = new ManHinhBLLDAL();
        public frmManHinh()
        {
            InitializeComponent();
            this.dtgvManHinh.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvManHinh.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));

        }

        private void frmManHinh_Load(object sender, EventArgs e)
        {
            loadDataManHinh();
        }

        private void loadDataManHinh()
        {
            disableControls();
            var manHinhs = from mh in manHinhBLLDAL.getDataManHinh()
                           select new
                           {
                               MaMH = mh.MaMH,
                               TenMH = mh.TenMH
                           };
            dtgvManHinh.DataSource = manHinhs.ToList();
        }
        private void clearControls()
        {
            txtMaMH.Clear();
            txtTenMH.Clear();
            txtTenMH.Focus();
        }
        private void disableControls()
        {
            txtMaMH.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenMH.Text))
            {
                MessageBox.Show("Tên màn hình không được để trống", "Thông báo!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            try
            {
                ManHinh mh = new ManHinh();
                mh.TenMH = txtTenMH.Text;
                manHinhBLLDAL.insertManHinh(mh);
                MessageBox.Show("Thêm thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataManHinh();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Thêm thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvManHinh.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            try
            {
                txtMaMH.Text = row.MaMH.ToString();
                if (string.IsNullOrEmpty(txtMaMH.Text))
                {
                    MessageBox.Show("Không tồn tại mã màn hình này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                manHinhBLLDAL.updateManHinh(int.Parse(txtMaMH.Text), txtTenMH.Text);
                MessageBox.Show("Sửa thông tin màn hình thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataManHinh();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Sửa thông tin màn hình thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtgvManHinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            dynamic row = dtgvManHinh.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            txtMaMH.Text = row.MaMH.ToString();
            txtTenMH.Text = row.TenMH;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvManHinh.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            try
            {
                txtMaMH.Text = row.MaMH.ToString();
                if (string.IsNullOrEmpty(txtMaMH.Text))
                {
                    MessageBox.Show("Không tồn tại mã màn hình này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                manHinhBLLDAL.deleteManHinh(int.Parse(txtMaMH.Text));
                MessageBox.Show("Xóa thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataManHinh();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Xóa thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadDataManHinh();
            clearControls();
        }
    }
}

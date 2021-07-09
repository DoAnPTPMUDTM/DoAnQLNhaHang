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
    public partial class frmBanAn : Form
    {
        BanBLLDAL banBLLDAL = new BanBLLDAL();
        List<Ban> lstBan;
        public frmBanAn()
        {
            InitializeComponent();
            lstBan = new List<Ban>();
        }

        private void frmBanAn_Load(object sender, EventArgs e)
        {
            loadBan();
        }
        public void loadBan()
        {
            lstBan = banBLLDAL.getDataBan();
            imgLstBoxBan.Items.Clear();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            this.imgLstBoxBan.ImageList = imageList;
            int i = 0;
            foreach (Ban ban in lstBan)
            {
                imageList.Images.Add(Properties.Resources.bancokhach);
                this.imgLstBoxBan.Items.Add(ban.TenBan, i);
                i++;
            }
            this.imgLstBoxBan.ColumnWidth = 130;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenBan = txtTenBan.Text;
            string viTri = txtViTri.Text;
            if (string.IsNullOrEmpty(tenBan))
            {
                MessageBox.Show("Vui lòng nhập tên bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(viTri))
            {
                MessageBox.Show("Vui lòng nhập vị trí", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Ban ban = new Ban();
            ban.TenBan = tenBan;
            ban.ViTri = viTri;
            ban.TrangThai = 0;
            try
            {
                banBLLDAL.insertBan(ban);
                loadBan();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imgLstBoxBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = imgLstBoxBan.SelectedIndex;
            if(index < 0)
            {
                return;
            }
            txtMaBan.Text = lstBan[index].MaBan.ToString();
            txtTenBan.Text = lstBan[index].TenBan;
            txtViTri.Text = lstBan[index].ViTri;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int index = imgLstBoxBan.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            string tenBan = txtTenBan.Text;
            string viTri = txtViTri.Text;
            if (string.IsNullOrEmpty(tenBan))
            {
                MessageBox.Show("Vui lòng nhập tên bàn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(viTri))
            {
                MessageBox.Show("Vui lòng nhập vị trí", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Ban ban = new Ban();
            ban.TenBan = txtTenBan.Text;
            ban.ViTri = txtViTri.Text;
            try
            {
                banBLLDAL.updateBan(lstBan[index].MaBan, ban);
                loadBan();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int index = imgLstBoxBan.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            try
            {
                banBLLDAL.deleteBan(lstBan[index].MaBan);
                loadBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

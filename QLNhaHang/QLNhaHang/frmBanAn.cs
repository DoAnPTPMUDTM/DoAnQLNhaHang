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
using TableDependency.SqlClient.Base.Enums;

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
            banBLLDAL = new BanBLLDAL();
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
            clearText();
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
                
                UpdateBan(ChangeType.Insert);
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
                UpdateBan(ChangeType.Update);
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
                if (banBLLDAL.ktKhoaNgoai(lstBan[index].MaBan))
                {
                    MessageBox.Show("Hiện tại bạn không thể xoá bàn này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                banBLLDAL.deleteBan(lstBan[index].MaBan);
                loadBan();
                UpdateBan(ChangeType.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public delegate void StatusUpdateHandler(object sender, EventArgs e, ChangeType c);
        public event StatusUpdateHandler OnUpdateBan;

        private void UpdateBan(ChangeType c)
        {
            EventArgs args = new EventArgs();
            OnUpdateBan?.Invoke(this, args, c);
        }
        public void clearText()
        {
            txtMaBan.Clear();
            txtTenBan.Clear();
            txtViTri.Clear();
        }

        private void imgLstBoxBan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
        }
    }
}

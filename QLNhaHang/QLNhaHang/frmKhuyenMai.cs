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
    public partial class frmKhuyenMai : Form
    {
        KhuyenMaiBLLDAL khuyenMaiBLLDAL = new KhuyenMaiBLLDAL();
        MonBLLDAL mon = new MonBLLDAL();
        public frmKhuyenMai()
        {
            InitializeComponent();
        }

        private void frmKhuyenMai_Load(object sender, EventArgs e)
        {
            loadKhuyenMai();
        }
        public void loadKhuyenMai()
        {
            khuyenMaiBLLDAL = new KhuyenMaiBLLDAL();
            gridControlKM.DataSource = (from k in khuyenMaiBLLDAL.getDataKhuyenMai()
                                        select new
                                        {
                                            MaKM = k.MaKM,
                                            TenKM = k.TenKM,
                                            TyLe = (k.TyLe * 100).ToString()
                                        });
            gridView1.ClearSelection();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenKM = txtTenKM.Text;
            string tyLe = txtTyLe.Text;
            if (string.IsNullOrEmpty(tenKM))
            {
                MessageBox.Show("Vui lòng nhập tên khuyến mãi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(tyLe))
            {
                MessageBox.Show("Vui lòng nhập tỷ lệ khuyến mãi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                double tyLeKM = double.Parse(txtTyLe.Text) / 100;
                KhuyenMai km = new KhuyenMai();
                km.TenKM = tenKM;
                km.TyLe = tyLeKM;
                khuyenMaiBLLDAL.insertKhuyenMai(km);
                loadKhuyenMai();
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                clearText();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            khuyenMaiBLLDAL = new KhuyenMaiBLLDAL();
            string maKM = gridView1.GetFocusedRowCellValue("MaKM").ToString();
            if (string.IsNullOrEmpty(maKM))
            {
                return;
            }
            string tenKM = txtTenKM.Text;
            string tyLe = txtTyLe.Text;
            if (string.IsNullOrEmpty(tenKM))
            {
                MessageBox.Show("Vui lòng nhập tên khuyến mãi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(tyLe))
            {
                MessageBox.Show("Vui lòng nhập tỷ lệ khuyến mãi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                int index = gridView1.FocusedRowHandle;
                double tyLeKM = double.Parse(txtTyLe.Text) / 100;
                KhuyenMai km = new KhuyenMai();
                km.TenKM = tenKM;
                km.TyLe = tyLeKM;
                khuyenMaiBLLDAL.updateKhuyenMai(int.Parse(maKM),km);
                loadKhuyenMai();
                mon = new MonBLLDAL();
                mon.updateMaKMChange(int.Parse(maKM));
                gridView1.FocusedRowHandle = index;
                UpdateKhuyenMai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKM = gridView1.GetFocusedRowCellValue("MaKM").ToString();
            if (string.IsNullOrEmpty(maKM))
            {
                return;
            }
            try
            {
                if (khuyenMaiBLLDAL.ktKhoaNgoai(int.Parse(maKM)))
                {
                    MessageBox.Show("Hiện tại không thể xoá khuyến mãi này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                khuyenMaiBLLDAL.deleteKhuyenMai(int.Parse(maKM));
                loadKhuyenMai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearText()
        {
            txtMaKM.Clear();
            txtTenKM.Clear();
            txtTyLe.Clear();
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                txtMaKM.Text = gridView1.GetFocusedRowCellValue("MaKM").ToString();
                txtTenKM.Text = gridView1.GetFocusedRowCellValue("TenKM").ToString();
                txtTyLe.Text = gridView1.GetFocusedRowCellValue("TyLe").ToString();
            }
        }
        public delegate void StatusUpdateHandler(object sender, EventArgs e);
        public event StatusUpdateHandler OnUpdateKM;

        private void UpdateKhuyenMai()
        {
            EventArgs args = new EventArgs();
            OnUpdateKM?.Invoke(this, args);
        }


    }
}

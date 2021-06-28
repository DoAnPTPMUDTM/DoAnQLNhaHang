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
using QLNhaHang.Classes;
using System.IO;
using DevExpress.XtraEditors;

namespace QLNhaHang
{
    public partial class frmThemMonAn : Form
    {
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        NhomMonBLLDAL nhomMonBLLDAL = new NhomMonBLLDAL();
        KhuyenMaiBLLDAL khuyenMaiBLLDAL = new KhuyenMaiBLLDAL();
        DonViTinhBLLDAL donViTinhBLLDAL = new DonViTinhBLLDAL();
        ConfigImage configImage = new ConfigImage();
        string imgNameMonAn = "";
        string filePathLocalMonAn = "";
        string appPathLocalMonAn = "";
        //tên món ăn, nhóm món, khuyến mãi, giá gốc, giá km, đvt, ảnh món.
        //private string tenMon, nhomMon, khuyenMai, donViTinh;
        //private double giaGoc, giaKM;
        public frmThemMonAn()
        {
            InitializeComponent();
        }
        //

        public delegate void StatusUpdateHandler(object sender, EventArgs eventArgs, Mon mon, string fileLocal, string fileApp);
        public event StatusUpdateHandler OnUpdateMonAn;
        private void UpdateStatus(Mon mon, string fileLocal, string fileApp)
        {
            EventArgs eventArgs = new EventArgs();
            OnUpdateMonAn?.Invoke(this, eventArgs, mon, fileLocal, fileApp);
        }
        private void frmThemMonAn_Load(object sender, EventArgs e)
        {
            //txtGiaKM.Text = "0";
            //txtGiaGoc.Enabled = false;
            loadDataNhomMon();
            loadDataKhuyenMai();
            loadDataDVT();
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {

            //tên món ăn, nhóm món, khuyến mãi, giá gốc, giá km, đvt, ảnh món.
            if (string.IsNullOrEmpty(txtTenMon.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbNhomMon.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm món!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            //check Khuyến mãi
            if (cbbKhuyenMai.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                MessageBox.Show("Giá món ăn không được để trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbDVT.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (picImgAnhMon.Image == null)
            {
                MessageBox.Show("Bạn chưa chọn hình ảnh cho món ăn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(filePathLocalMonAn) || string.IsNullOrEmpty(imgNameMonAn))
            {
                MessageBox.Show("Hệ thống chưa ghi nhận được ảnh món ăn. Vui lòng chọn lại!");
                picImgAnhMon.Image = null;
                return;
            }
            //? Coi check tất cả dữ liệu đã hợp lệ chưa
            //
            string appPath = configImage.GetProjectLinkDirectory() + configImage.imgAnhMon;
            string imgRename = monBLLDAL.getMaMonContinue().ToString() + "_" + DateTime.Now.ToString("ddMMyyhhmmsstt") + Path.GetExtension(imgNameMonAn);
            appPathLocalMonAn = appPath + imgRename;
            //MessageBox.Show("FileLocal: " + filePathLocalMonAn + " - " + "FileApp: " + appPathLocalMonAn);
            //
            int maNhom = int.Parse(cbbNhomMon.EditValue.ToString());
            int maDVT = int.Parse(cbbDVT.EditValue.ToString());
            string tenMon = txtTenMon.Text;
            decimal giaGoc = decimal.Parse(txtGiaGoc.Text);
            decimal giaKM = decimal.Parse(txtGiaKM.Text);
            int maKM = int.Parse(cbbKhuyenMai.EditValue.ToString());
            Mon mon = new Mon();
            mon.TenMon = tenMon;
            mon.MaDVT = maDVT;
            mon.MaNhom = maNhom;
            mon.Anh = imgRename;
            mon.GiaGoc = giaGoc;
            mon.GiaKM = giaKM;
            mon.MaKM = maKM;

            UpdateStatus(mon, filePathLocalMonAn, appPathLocalMonAn);
            this.Close();

        }

        private void txtGiaGoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiaGoc_TextChanged(object sender, EventArgs e)
        {
            //double giaKM = double.Parse(txtGiaKM.Text);
            //string maKM = ;
            if (string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                txtGiaKM.Clear();
                return;
            }
            if (cbbKhuyenMai.EditValue == null)
            {
                txtGiaKM.Text = txtGiaGoc.Text;
            }
            else
            {
                txtGiaKM.Text = (double.Parse(txtGiaGoc.Text) - khuyenMaiBLLDAL.getGiaKhuyenMaiByMaKM(int.Parse(cbbKhuyenMai.EditValue.ToString())) * double.Parse(txtGiaGoc.Text)).ToString();
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void loadDataNhomMon()
        {
            cbbNhomMon.Properties.DataSource = nhomMonBLLDAL.getDataNhomMon();
            cbbNhomMon.Properties.DisplayMember = "TenNhom";
            cbbNhomMon.Properties.ValueMember = "MaNhom";
            cbbNhomMon.Properties.NullText = "Chọn nhóm món";
        }

        private void cbbKhuyenMai_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGiaGoc.Text))
                {
                    txtGiaGoc.Clear();
                    return;
                }
                if (!string.IsNullOrEmpty(txtGiaGoc.Text))
                {
                    txtGiaKM.Text = (double.Parse(txtGiaGoc.Text) - khuyenMaiBLLDAL.getGiaKhuyenMaiByMaKM(int.Parse(cbbKhuyenMai.EditValue.ToString())) * double.Parse(txtGiaGoc.Text)).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void picImgAnhMon_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a image";
            openFileDialog.Filter = "jpg files (*.jpg)|*.jpg|All files(*.*)|*.*";
            string appPath = configImage.GetProjectLinkDirectory() + configImage.imgAnhMon;
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string iName = openFileDialog.SafeFileName;
                    filePathLocalMonAn = openFileDialog.FileName;
                    imgNameMonAn = iName;
                    picImgAnhMon.Image = new Bitmap(openFileDialog.OpenFile());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to open file" + ex.Message);
                }
            }
            else
            {
                openFileDialog.Dispose();
            }
        }

        private void loadDataKhuyenMai()
        {
            //KhuyenMai km = new KhuyenMai();
            //km.MaKM = 0;
            //km.TenKM = "Không khuyến mãi";
            ////km.TyLe = 0;
            //List<KhuyenMai> lstKhuyenMai = khuyenMaiBLLDAL.getDataKhuyenMai();
            //lstKhuyenMai.Insert(0, km);
            //cbbKhuyenMai.Properties.DataSource = lstKhuyenMai;
            //cbbKhuyenMai.Properties.DisplayMember = "TenKM";
            //cbbKhuyenMai.Properties.ValueMember = "MaKM";

            cbbKhuyenMai.Properties.DataSource = khuyenMaiBLLDAL.getDataKhuyenMai();
            cbbKhuyenMai.Properties.DisplayMember = "TenKM";
            cbbKhuyenMai.Properties.ValueMember = "MaKM";
            cbbKhuyenMai.Properties.NullText = "Chọn khuyến mãi";
        }
        private void loadDataDVT()
        {
            cbbDVT.Properties.DataSource = donViTinhBLLDAL.getDataDVT();
            cbbDVT.Properties.DisplayMember = "TenDVT";
            cbbDVT.Properties.ValueMember = "MaDVT";
            cbbDVT.Properties.NullText = "Chọn đơn vị tính";
        }


        private void picImgAnhMon_Click(object sender, EventArgs e)
        {
            //int stt = 0;
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = "Select a image";
            //openFileDialog.Filter = "jpg files (*.jpg)|*.jpg|All files(*.*)|*.*";
            //string appPath = configImage.GetProjectLinkDirectory() + configImage.imgAnhMon;
            //if (Directory.Exists(appPath) == false)
            //{
            //    Directory.CreateDirectory(appPath);
            //}
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        string iName = openFileDialog.SafeFileName;
            //        filePathLocalMonAn = openFileDialog.FileName;
            //        appPathLocalMonAn = appPath + iName;
            //        imgMonAn = iName;
            //        picImgAnhMon.Image = new Bitmap(openFileDialog.OpenFile());
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Unable to open file" + ex.Message);
            //    }
            //}
            //else
            //{
            //    openFileDialog.Dispose();
            //}
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

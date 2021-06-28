using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLLDAL;
using QLNhaHang.Classes;

namespace QLNhaHang
{
    public partial class frmSuaMonAn : Form
    {
        NhomMonBLLDAL nhomMonBLLDAL = new NhomMonBLLDAL();
        KhuyenMaiBLLDAL khuyenMaiBLLDAL = new KhuyenMaiBLLDAL();
        DonViTinhBLLDAL donViTinhBLLDAL = new DonViTinhBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        ConfigImage configImage = new ConfigImage();
        
        private string imgMonAn = "";
        private string filePathLocalMonAn = "";
        private string appPathLocalMonAn = "";
        //private string  tenMon;
        private string tenMon, nhomMon, khuyenMai, donViTinh;
        private double giaGoc, giaKM;
        private int maMon;
        public frmSuaMonAn()
        {
            InitializeComponent();
        }

        public frmSuaMonAn(int maMon, string tenMon, string nhomMon, string khuyenMai, string donViTinh, double giaGoc, double giaKM, string imgMonAn)
        {
            InitializeComponent();
            this.maMon = maMon;
            this.tenMon = tenMon;
            this.nhomMon = nhomMon;
            this.khuyenMai = khuyenMai;
            this.giaGoc = giaGoc;
            this.giaKM = giaKM;
            this.donViTinh = donViTinh;
            this.imgMonAn = imgMonAn;
        }
        //public frmSuaMonAn(int maMon, string tenMon, double giaGoc, double giaKM, string imgMonAn)
        //{
        //    InitializeComponent();
        //    this.maMon = maMon;
        //    this.tenMon = tenMon;
        //    this.giaGoc = giaGoc;
        //    this.giaKM = giaKM;
        //    this.imgMonAn = imgMonAn;
        //}

        public delegate void StatusUpdateHandler(object sender, EventArgs eventArgs, int maMon, string tenMon, string nhomMon, string khuyenMai, string donViTinh, double giaGoc, double giaKM, string imgMonAn, string fileLocal, string fileApp, bool check);
        public event StatusUpdateHandler OnUpdateMonAn;
        private void UpdateStatus(int maMon, string tenMon, string nhomMon, string khuyenMai, string donViTinh, double giaGoc, double giaKM, string imgMonAn, string fileLocal, string fileApp, bool check)
        {
            EventArgs eventArgs = new EventArgs();
            OnUpdateMonAn?.Invoke(this, eventArgs, maMon, tenMon, nhomMon, khuyenMai, donViTinh, giaGoc, giaKM, imgMonAn, fileLocal,fileApp,check);
        }
        private void frmSuaMonAn_Load(object sender, EventArgs e)
        {
            loadDataNhomMon();
            loadDataKhuyenMai();
            loadDataDVT();
            //
            txtMaMon.Text = maMon.ToString();
            txtTenMon.Text = tenMon;
            txtGiaGoc.Text = giaGoc.ToString();
            txtGiaKM.Text = giaKM.ToString();
            //
            cbbNhomMon.SelectedValue = int.Parse(nhomMon);
            cbbKhuyenMai.SelectedValue = int.Parse(khuyenMai);
            cbbDVT.SelectedValue = int.Parse(donViTinh);

            picImgAnhMon.Image = Image.FromFile(configImage.GetProjectLinkDirectory() + configImage.imgAnhMon + imgMonAn);
            txtTenMon.Focus();

        }

        private void txtGiaGoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiaGoc_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                txtGiaKM.Clear();
                return;
            }
            if (cbbKhuyenMai.SelectedIndex < 0)
            {
                txtGiaKM.Text = txtGiaGoc.Text;
            }
            txtGiaKM.Text = (double.Parse(txtGiaGoc.Text) - khuyenMaiBLLDAL.getGiaKhuyenMaiByMaKM(int.Parse(cbbKhuyenMai.SelectedValue.ToString())) * double.Parse(txtGiaGoc.Text)).ToString();

            
        }
        bool check = false;
        private void picImgAnhMon_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a Image";
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
                    imgMonAn = iName;
                    picImgAnhMon.Image = new Bitmap(openFileDialog.OpenFile());
                    check = true;
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cbbKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                txtGiaGoc.Clear();
                txtGiaKM.Clear();
                return;
            }
            if (!string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                txtGiaKM.Text = (double.Parse(txtGiaGoc.Text) - khuyenMaiBLLDAL.getGiaKhuyenMaiByMaKM(int.Parse(cbbKhuyenMai.SelectedValue.ToString())) * double.Parse(txtGiaGoc.Text)).ToString();
            }
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtTenMon.Text))
            {
                MessageBox.Show("Tên món không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenMon.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                MessageBox.Show("Giá món ăn không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiaGoc.Focus();
                txtGiaKM.Clear();
                return;
            }

            //kiểm tra combobox
            if (cbbNhomMon.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(cbbKhuyenMai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbDVT.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            if (picImgAnhMon.Image == null)
            {
                MessageBox.Show("Bạn chưa chọn hình ảnh cho món ăn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (check)
            {
                if (string.IsNullOrEmpty(filePathLocalMonAn) || string.IsNullOrEmpty(imgMonAn))
                {
                    MessageBox.Show("Hệ thống chưa ghi nhận được ảnh món ăn. Vui lòng chọn lại!");
                    picImgAnhMon.Image = null;
                    return;
                }
            }

            //MessageBox.Show(nhomMon + " - " + khuyenMai + " - " + donViTinh);

            //?Check tất cả dữ liệu
            string tenMonEdit = txtTenMon.Text;
            string nhomMonEdit = cbbNhomMon.SelectedValue.ToString();
            string khuyenMaiEdit = cbbKhuyenMai.SelectedValue.ToString();
            string donViTinhEdit = cbbDVT.SelectedValue.ToString();
            Double giaGocEdit = Double.Parse(txtGiaGoc.Text);
            Double giaKMEdit = Double.Parse(txtGiaKM.Text);
            string appPath = configImage.GetProjectLinkDirectory() + configImage.imgAnhMon;
            string imgRename = maMon.ToString() + "_" + DateTime.Now.ToString("ddMMyyhhmmsstt") + Path.GetExtension(imgMonAn);
            appPathLocalMonAn = appPath + imgRename;
            MessageBox.Show("FileLocal: " + filePathLocalMonAn + " - " + "FileApp: " + appPathLocalMonAn);
            UpdateStatus(this.maMon, tenMonEdit, nhomMonEdit, khuyenMaiEdit, donViTinhEdit, giaGocEdit,giaKMEdit, imgRename, filePathLocalMonAn, appPathLocalMonAn,check);
            this.Close();
        }

        private void loadDataNhomMon()
        {
            cbbNhomMon.DataSource = nhomMonBLLDAL.getDataNhomMon();
            cbbNhomMon.DisplayMember = "TenNhom";
            cbbNhomMon.ValueMember = "MaNhom";
        }

        private void loadDataKhuyenMai()
        {
            cbbKhuyenMai.DataSource = khuyenMaiBLLDAL.getDataKhuyenMai();
            cbbKhuyenMai.DisplayMember = "TenKM";
            cbbKhuyenMai.ValueMember = "MaKM";
        }
        private void loadDataDVT()
        {
            cbbDVT.DataSource = donViTinhBLLDAL.getDataDVT();
            cbbDVT.DisplayMember = "TenDVT";
            cbbDVT.ValueMember = "MaDVT";
        }
    }
}

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
        ConfigImage configImage = new ConfigImage();
        
        private string imgMonAn = "";
        private string filePathLocalMonAn = "";
        private string appPathLocalMonAn = "";
        private string  tenMon, nhomMon, khuyenMai, donViTinh;
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
        public delegate void StatusUpdateHandler(object sender, EventArgs eventArgs, int maMon, string tenMon, string nhomMon, string khuyenMai, string donViTinh, double giaGoc, double giaKM, string imgMonAn);
        public event StatusUpdateHandler OnUpdateMonAn;
        private void UpdateStatus(int maMon, string tenMon, string nhomMon, string khuyenMai, string donViTinh, double giaGoc, double giaKM, string imgMonAn)
        {
            EventArgs eventArgs = new EventArgs();
            OnUpdateMonAn?.Invoke(this, eventArgs, maMon, tenMon, nhomMon, khuyenMai, donViTinh, giaGoc, giaKM, imgMonAn);
        }
        private void frmSuaMonAn_Load(object sender, EventArgs e)
        {
            loadDataNhomMon();
            loadDataKhuyenMai();
            loadDataDVT();
            //
            //cbbDVT.SelectedItem = donViTinh;
            //
            txtMaMon.Text = maMon.ToString();
            txtTenMon.Text = tenMon;
            txtGiaGoc.Text = giaGoc.ToString();
            txtGiaKM.Text = giaKM.ToString();

            //anh dang bi loi
            //Note Sơn check lại
            picImgAnhMon.Image = Image.FromFile(configImage.GetProjectLinkDirectory() + configImage.imgAnhMon + imgMonAn);
            txtTenMon.Focus();

        }

        private void picImgAnhMon_Click(object sender, EventArgs e)
        {
            

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
            //double giaKMMoi = double.Parse(txtGiaKM.Text);
            //if (!string.IsNullOrEmpty(txtGiaGoc.Text))
            //{
            //    giaKMMoi = (double.Parse(txtGiaGoc.Text) - khuyenMaiBLLDAL.getGiaKhuyenMaiByMaKM(int.Parse(cbbKhuyenMai.SelectedValue.ToString())) * double.Parse(txtGiaGoc.Text)).ToString();
            //}
            //else
            //{
            //    giaKMMoi = 0;
            //}
        }

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
                    appPathLocalMonAn = appPath + iName;
                    imgMonAn = iName;
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

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            string maNhom = cbbNhomMon.SelectedValue.ToString();
            string maKM = cbbKhuyenMai.SelectedValue.ToString();
            string DVT = cbbDVT.SelectedItem.ToString();
            MessageBox.Show(maNhom + " - " + maKM + " - " + DVT);
            //đang lỗi chưa sửa đc.

            //if (khuyenMaiBLLDAL.kTraKhuyenMai(int.Parse(maKM)))
            //{
            //    MessageBox.Show("Khuyến mãi này đã hết hạn", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtTenMon.Text))
            {
                MessageBox.Show("Tên món không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //File.Copy(filePathLocalMonAn, appPathLocalMonAn);
            //Kiem tat ca du lieu hop le chua.
            //UpdateStatus(this.maMon, this.tenMon, maNhom, maKM, DVT, this.giaGoc, this.giaKM, this.imgMonAn);
           // this.Close();
        }

        private void loadDataNhomMon()
        {
            //code here.
            cbbNhomMon.DataSource = nhomMonBLLDAL.getDataNhomMon();
            cbbNhomMon.DisplayMember = "TenNhom";
            cbbNhomMon.ValueMember = "MaNhom";
        }

        private void loadDataKhuyenMai()
        {
            cbbKhuyenMai.DataSource = khuyenMaiBLLDAL.getDataKhuyenMai();
            cbbKhuyenMai.DisplayMember = "TenKM";
            cbbKhuyenMai.ValueMember = "MaKM";
            //cbbKhuyenMai.NullText = "Không khuyến mãi";
        }
        private void loadDataDVT()
        {
            cbbDVT.Items.Add("Bát");
            cbbDVT.Items.Add("Đĩa");
            cbbDVT.Items.Add("Nồi");
            cbbDVT.Items.Add("Lon");
            cbbDVT.Items.Add("Hủ");
        }
    }
}

﻿using System;
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
        ConfigImage configImage = new ConfigImage();
        string imgMonAn = "";
        string filePathLocalMonAn = "";
        string appPathLocalMonAn = "";
        //tên món ăn, nhóm món, khuyến mãi, giá gốc, giá km, đvt, ảnh món.
        private string tenMon, nhomMon, khuyenMai, donViTinh;
        private double giaGoc, giaKM;
        public frmThemMonAn()
        {
            InitializeComponent();
        }
        //
        public frmThemMonAn(string tenMon, string nhomMon, string khuyenMai, double giaGoc, double giaKM, string donViTinh, string imgMonAn)
        {
            InitializeComponent();
            this.tenMon = tenMon;
            this.nhomMon = nhomMon;
            this.khuyenMai = khuyenMai;
            this.giaGoc = giaGoc;
            this.giaKM = giaKM;
            this.donViTinh = donViTinh;
            this.imgMonAn = imgMonAn;
        }
        public delegate void StatusUpdateHandler(object sender, EventArgs eventArgs, string tenMon, string nhomMon, string khuyenMai, double giaGoc, double giaKM, string donViTinh, string imgMonAn);
        public event StatusUpdateHandler OnUpdateMonAn;
        private void UpdateStatus(string tenMon, string nhomMon, string khuyenMai, double giaGoc, double giaKM, string donViTinh, string imgMonAn)
        {
            EventArgs eventArgs = new EventArgs();
            OnUpdateMonAn?.Invoke(this, eventArgs, tenMon, nhomMon, khuyenMai, giaGoc, giaKM, donViTinh, imgMonAn);
        }
        private void frmThemMonAn_Load(object sender, EventArgs e)
        {
            txtGiaKM.Text = "0";
            //txtGiaGoc.Enabled = false;
            loadDataNhomMon();
            //Ko khuyến mãi Vy chưa viết đc.
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
            khuyenMai = cbbKhuyenMai.EditValue.ToString();
            MessageBox.Show(khuyenMai);
            if (khuyenMai == null)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (int.Parse(khuyenMai) != 0)
            {
                if (khuyenMaiBLLDAL.kTraKhuyenMai(int.Parse(khuyenMai)))
                {
                    MessageBox.Show("Khuyến mãi này đã hết hạn", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtGiaGoc.Text))
            {
                MessageBox.Show("Giá món ăn không được để trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbDVT.SelectedIndex < 0)
            {
                MessageBox.Show("Đơn vị tính không được để trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (picImgAnhMon.Image == null)
            {
                MessageBox.Show("Bạn chưa chọn hình ảnh cho món ăn!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            File.Copy(filePathLocalMonAn, appPathLocalMonAn);
            UpdateStatus(txtTenMon.Text, cbbNhomMon.EditValue.ToString(), cbbKhuyenMai.EditValue.ToString(), double.Parse(txtGiaGoc.Text), double.Parse(txtGiaKM.Text), cbbDVT.SelectedItem.ToString(), imgMonAn);
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
            khuyenMai = cbbKhuyenMai.EditValue.ToString();
            //try
            //{
            if (khuyenMai != null)
            {

                if (!string.IsNullOrEmpty(txtGiaGoc.Text))
                {
                    txtGiaKM.Text = (double.Parse(txtGiaGoc.Text) - khuyenMaiBLLDAL.getGiaKhuyenMaiByMaKM(int.Parse(cbbKhuyenMai.EditValue.ToString())) * double.Parse(txtGiaGoc.Text)).ToString();
                }
                else
                {
                    txtGiaKM.Text = "0";
                }
            }
            else
            {
                txtGiaKM.Text = "0";
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
            //txtGiaGoc.Enabled = true;
            txtGiaGoc.Clear();
            txtGiaKM.Text = "0";
            //double giaGoc = double.Parse(txtGiaGoc.Text);
            //double giaKM = double.Parse(txtGiaKM.Text);
            //khuyenMai = cbbKhuyenMai.EditValue.ToString();
            //if (khuyenMai.Equals("0"))
            //{
            //    giaGoc = giaKM;
            //}
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

            KhuyenMai km = new KhuyenMai();
            km.MaKM = 0;
            km.TenKM = "Không khuyến mãi";
            km.TyLe = 0;
            List<KhuyenMai> lstKhuyenMai = khuyenMaiBLLDAL.getDataKhuyenMai();
            lstKhuyenMai.Insert(0, km);
            cbbKhuyenMai.Properties.DataSource = lstKhuyenMai;
            // cbbKhuyenMai.Properties.DataSource = khuyenMaiBLLDAL.getDataKhuyenMai();

            cbbKhuyenMai.Properties.DisplayMember = "TenKM";
            cbbKhuyenMai.Properties.ValueMember = "MaKM";
            cbbKhuyenMai.Properties.NullText = "Chọn khuyến mãi";
        }
        private void loadDataDVT()
        {
            cbbDVT.Properties.Items.Add("Bát");
            cbbDVT.Properties.Items.Add("Đĩa");
            cbbDVT.Properties.Items.Add("Nồi");
            cbbDVT.Properties.Items.Add("Lon");
            cbbDVT.Properties.Items.Add("Hủ");
        }

        private void cbbDVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDVT.SelectedIndex < 0)
            {
                return;
            }
            //MessageBox.Show(cbbDVT.SelectedItem.ToString());

        }

        private void picImgAnhMon_Click(object sender, EventArgs e)
        {
            //int stt = 0;
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
    }
}
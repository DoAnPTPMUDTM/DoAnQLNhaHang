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

namespace QLNhaHang
{
    public partial class frmNhomNguoiDung : Form
    {
        NhomNguoiDungBLLDAL nhomNguoiDungBLLDAL = new NhomNguoiDungBLLDAL();
        public frmNhomNguoiDung()
        {
            InitializeComponent();
            this.dtgvNhomNguoiDung.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvNhomNguoiDung.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));
        }

        private void frmNhomNguoiDung_Load(object sender, EventArgs e)
        {
            loadDataNhomNguoiDung();
        }
        private void loadDataNhomNguoiDung()
        {
            //Disable
            txtMaNhom.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //
            var nguoiDungs = from nd in nhomNguoiDungBLLDAL.getDataNhomNguoiDung()
                             select new
                             {
                                 MaNhom = nd.MaNhom,
                                 TenNhom = nd.TenNhom,
                                 GhiChu = nd.GhiChu
                             };
            dtgvNhomNguoiDung.DataSource = nguoiDungs.ToList();
        }
        private void clearControls()
        {
            txtMaNhom.Clear();
            txtTenNhom.Clear();
            memoEditGhiChu.Text = "";
            txtTenNhom.Focus();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNhom.Text))
            {
                MessageBox.Show("Tên nhóm không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(memoEditGhiChu.Text))
            {
                MessageBox.Show("Ghi chú không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Thêm vào nhóm nd
            try
            {
                NhomNguoiDung nhomNguoiDung = new NhomNguoiDung();
                nhomNguoiDung.TenNhom = txtTenNhom.Text;
                nhomNguoiDung.GhiChu = memoEditGhiChu.Text;
                nhomNguoiDungBLLDAL.insertNhomNguoiDung(nhomNguoiDung);
                loadDataNhomNguoiDung();
                clearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtgvNhomNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            dynamic row = dtgvNhomNguoiDung.CurrentRow.DataBoundItem;
            if (row == null)
            {
                return;
            }
            try
            {
                txtMaNhom.Text = row.MaNhom.ToString();
                txtTenNhom.Text = row.TenNhom;
                memoEditGhiChu.Text = row.GhiChu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvNhomNguoiDung.CurrentRow.DataBoundItem;
            if (row == null)
            {
                return;
            }
            try
            {
                txtMaNhom.Text = row.MaNhom.ToString();
                if (string.IsNullOrEmpty(txtMaNhom.Text))
                {
                    MessageBox.Show("Không tồn tại mã nhóm này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                nhomNguoiDungBLLDAL.updateNhomNguoiDung(int.Parse(txtMaNhom.Text), txtTenNhom.Text, memoEditGhiChu.Text);
                MessageBox.Show("Sửa thông tin nhóm người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNhomNguoiDung();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Sửa thông tin nhóm người dùng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvNhomNguoiDung.CurrentRow.DataBoundItem;
            if (row == null)
            {
                return;
            }
            try
            {
                txtMaNhom.Text = row.MaNhom.ToString();
                if (string.IsNullOrEmpty(txtMaNhom.Text))
                {
                    MessageBox.Show("Không tồn tại mã nhóm này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                nhomNguoiDungBLLDAL.deleteNhomNguoiDung(int.Parse(txtMaNhom.Text));
                MessageBox.Show("Xóa thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNhomNguoiDung();
                clearControls();
            }
            catch
            {
                MessageBox.Show("Xóa không thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

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
    public partial class frmQLNguyenLieu : Form
    {
        LoaiMatHangBLLDAL loaiMatHangBLLDAL = new LoaiMatHangBLLDAL();
        MatHangBLLDAL matHangBLLDAL = new MatHangBLLDAL();
        DonViTinhBLLDAL donViTinhBLLDAL = new DonViTinhBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        DinhLuongBLLDAL dinhLuongBLLDAL = new DinhLuongBLLDAL();
        public frmQLNguyenLieu()
        {
            InitializeComponent();
        }
        public void loadLoaiMH()
        {
            loaiMatHangBLLDAL = new LoaiMatHangBLLDAL();
            cbbLoaiMHNL.Properties.DataSource = loaiMatHangBLLDAL.getAllLoaiMatHang();
            cbbLoaiMHNL.Properties.DisplayMember = "TenLoaiMH";
            cbbLoaiMHNL.Properties.ValueMember = "MaLoaiMH";
            //
            cbbLoaiMH.Properties.DataSource = loaiMatHangBLLDAL.getAllLoaiMatHang();
            cbbLoaiMH.Properties.DisplayMember = "TenLoaiMH";
            cbbLoaiMH.Properties.ValueMember = "MaLoaiMH";
        }
        public void loadGridViewMHNL()
        {
            matHangBLLDAL = new MatHangBLLDAL();
            var data = (from m in matHangBLLDAL.getAllMatHang()
                        select new
                        {
                            MaMH = m.MaMH,
                            TenMH = m.TenMH,
                            TenDVT = m.DonViTinh.TenDVT,
                            SoLuongTon = m.SoLuongTon,
                            MaDVT = m.MaDVT,
                            MaLoaiMH = m.MaLoaiMH
                        }).ToList();
            gridControlMHNL.DataSource = data;
            gridControlMHDL.DataSource = data;
        }
        public void loadGridViewMHDL()
        {
            matHangBLLDAL = new MatHangBLLDAL();
            gridControlMHDL.DataSource = (from m in matHangBLLDAL.getAllMatHang()
                                          select new
                                          {
                                              MaMH = m.MaMH,
                                              TenMH = m.TenMH,
                                              TenDVT = m.DonViTinh.TenDVT,
                                              SoLuongTon = m.SoLuongTon,
                                              MaDVT = m.MaDVT,
                                              MaLoaiMH = m.MaLoaiMH
                                          }).ToList();
        }
        public void loadDVT()
        {
            cbbDonViTinh.Properties.DataSource = donViTinhBLLDAL.getDataDVT();
            cbbDonViTinh.Properties.DisplayMember = "TenDVT";
            cbbDonViTinh.Properties.ValueMember = "MaDVT";
        }
        private void frmQLNguyenLieu_Load(object sender, EventArgs e)
        {
            loadLoaiMH();
            loadGridViewMHNL();
            loadDVT();
            loadCbbMon();
        }

        private void barBtnThemMH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            matHangBLLDAL = new MatHangBLLDAL();
            if (cbbDonViTinh.EditValue == null)
            {
                cbbDonViTinh.Focus();
                MessageBox.Show("Vui lòng chọn đơn vị tính", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbLoaiMHNL.EditValue == null)
            {
                cbbLoaiMHNL.Focus();
                MessageBox.Show("Vui lòng chọn loại mặt hàng", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string tenMH = txtTenMH.Text;
            if (string.IsNullOrEmpty(tenMH))
            {
                txtTenMH.Focus();
                MessageBox.Show("Vui lòng nhập tên mặt hàng", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                MatHang mh = new MatHang();
                mh.MaDVT = int.Parse(cbbDonViTinh.EditValue.ToString());
                mh.MaLoaiMH = int.Parse(cbbLoaiMHNL.EditValue.ToString());
                mh.TenMH = tenMH;
                mh.SoLuongTon = 0;
                matHangBLLDAL.insertMatHang(mh);
                loadGridViewMHNL();
                //gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                clearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm mặt hàng không thành công " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barBtnXoaMH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                matHangBLLDAL = new MatHangBLLDAL();
                if (gridView1.FocusedRowHandle < 0 || gridView1.GetFocusedRowCellValue("MaMH") == null)
                {
                    return;
                }
                int maMH = int.Parse(gridView1.GetFocusedRowCellValue("MaMH").ToString());
                if (matHangBLLDAL.ktKhoaNgoai(maMH))
                {
                    MessageBox.Show("Hiện tại không thể xoá mặt hàng này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                matHangBLLDAL.deleteMatHang(maMH);
                loadGridViewMHNL();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xoá mặt hàng không thành công " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //txtMaMH.Text = gridView1.GetFocusedRowCellValue("MaMH").ToString();
            //txtSLCon.Text = gridView1.GetFocusedRowCellValue("SoLuongTon").ToString();
            //if(gridView1.GetFocusedRowCellValue("MaDVT") != null)
            //{
            //    cbbDonViTinh.EditValue = int.Parse(gridView1.GetFocusedRowCellValue("MaDVT").ToString());
            //}    
            //if(gridView1.GetFocusedRowCellValue("MaLoaiMH") != null)
            //{
            //    cbbLoaiMHNL.EditValue = int.Parse(gridView1.GetFocusedRowCellValue("MaLoaiMH").ToString());
            //}

            //txtTenMH.Text = gridView1.GetFocusedRowCellValue("TenMH").ToString();
        }

        private void barBtbSuaMH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            matHangBLLDAL = new MatHangBLLDAL();
            if (cbbDonViTinh.EditValue == null)
            {
                cbbDonViTinh.Focus();
                MessageBox.Show("Vui lòng chọn đơn vị tính", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbLoaiMHNL.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại mặt hàng", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbLoaiMHNL.Focus();
                return;
            }
            string tenMH = txtTenMH.Text;
            if (string.IsNullOrEmpty(tenMH))
            {
                MessageBox.Show("Vui lòng nhập tên mặt hàng", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenMH.Focus();
                return;
            }
            if (gridView1.FocusedRowHandle < 0 || gridView1.GetFocusedRowCellValue("MaMH") == null)
            {
                return;
            }
            int maMH = int.Parse(gridView1.GetFocusedRowCellValue("MaMH").ToString());
            try
            {
                MatHang mh = new MatHang();
                mh.MaDVT = int.Parse(cbbDonViTinh.EditValue.ToString());
                mh.MaLoaiMH = int.Parse(cbbLoaiMHNL.EditValue.ToString());
                mh.TenMH = tenMH;
                matHangBLLDAL.updateMatHang(maMH, mh);
                int index = gridView1.FocusedRowHandle;
                loadGridViewMHNL();
                gridView1.FocusedRowHandle = index;
                clearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa mặt hàng không thành công " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearControl()
        {
            txtSLCon.Text = "0";
            txtTenMH.Clear();
            txtMaMH.Clear();
            cbbLoaiMHNL.EditValue = null;
            cbbDonViTinh.EditValue = null;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            txtMaMH.Text = gridView1.GetFocusedRowCellValue("MaMH").ToString();
            txtSLCon.Text = gridView1.GetFocusedRowCellValue("SoLuongTon").ToString();
            if (gridView1.GetFocusedRowCellValue("MaDVT") != null)
            {
                cbbDonViTinh.EditValue = int.Parse(gridView1.GetFocusedRowCellValue("MaDVT").ToString());
            }
            if (gridView1.GetFocusedRowCellValue("MaLoaiMH") != null)
            {
                cbbLoaiMHNL.EditValue = int.Parse(gridView1.GetFocusedRowCellValue("MaLoaiMH").ToString());
            }

            txtTenMH.Text = gridView1.GetFocusedRowCellValue("TenMH").ToString();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }

        private void cbbLoaiMH_EditValueChanged(object sender, EventArgs e)
        {
            matHangBLLDAL = new MatHangBLLDAL();
            if (cbbLoaiMH.EditValue == null)
            {
                return;
            }
            int maMH = int.Parse(cbbLoaiMH.EditValue.ToString());
            gridControlMHDL.DataSource = (from m in matHangBLLDAL.getMatHangByMaMaLoaiMH(maMH)
                                          select new
                                          {
                                              MaMH = m.MaMH,
                                              TenMH = m.TenMH,
                                              TenDVT = m.DonViTinh.TenDVT,
                                              SoLuongTon = m.SoLuongTon,
                                              MaDVT = m.MaDVT,
                                              MaLoaiMH = m.MaLoaiMH
                                          }).ToList();
        }
        public void loadCbbMon()
        {
            cbbMon.Properties.DataSource = (from m in monBLLDAL.getDataMon()
                                            select new
                                            {
                                                m.MaMon,
                                                m.TenMon
                                            }).ToList();
            cbbMon.Properties.DisplayMember = "TenMon";
            cbbMon.Properties.ValueMember = "MaMon";
        }

        private void btnThemNLVaoDL_Click(object sender, EventArgs e)
        {

        }
        public void loadGridControlDL(int maMon)
        {
            dinhLuongBLLDAL = new DinhLuongBLLDAL();
            monBLLDAL = new MonBLLDAL();
            gridControlDL.DataSource = (from d in dinhLuongBLLDAL.getDinhLuongByMaMon(maMon)
                                        select new
                                        {
                                            MaMon = d.MaMon,
                                            MaMH = d.MaMH,
                                            TenMH = matHangBLLDAL.getTenMHByMa(d.MaMH),
                                            QuyDoi = d.QuyDoi,
                                            DVT = matHangBLLDAL.getTenDVTByMaMH(d.MaMH)
                                        }).ToList();
        }

        private void cbbMon_EditValueChanged(object sender, EventArgs e)
        {
            monBLLDAL = new MonBLLDAL();
            if (cbbMon.EditValue == null)
            {
                gridControlDL.DataSource = null;
                txtDVT.Clear();
                return;
            }
            int maMon = int.Parse(cbbMon.EditValue.ToString());
            loadGridControlDL(maMon);
            txtDVT.Text = monBLLDAL.getTenDVTByMaMon(maMon);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0 || gridView2.GetFocusedRowCellValue("MaMH") == null)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbMon.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn món", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMon.Focus();
                return;
            }
            string quyDoi = txtQuyDoi.Text;
            if (string.IsNullOrEmpty(quyDoi))
            {
                MessageBox.Show("Vui lòng nhập quy đổi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuyDoi.Focus();
                return;
            }
            decimal qd = 0;
            try
            {
                dinhLuongBLLDAL = new DinhLuongBLLDAL();
                int maMH = int.Parse(gridView2.GetFocusedRowCellValue("MaMH").ToString());
                int maMon = int.Parse(cbbMon.EditValue.ToString());
                if (dinhLuongBLLDAL.ktKhoaChinh(maMon, maMH))
                {
                    MessageBox.Show("Đã tồn tại mặt hàng này trong bảng định lượng", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                qd = decimal.Parse(quyDoi);
                DinhLuong dl = new DinhLuong();
                dl.MaMon = maMon;
                dl.MaMH = maMH;
                dl.QuyDoi = qd;
                dinhLuongBLLDAL.insertDinhLuong(dl);
                loadGridControlDL(maMon);
                txtQuyDoi.Clear();
            }
            catch
            {
                txtQuyDoi.Focus();
                MessageBox.Show("Sai định dạng quy đổi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.FocusedRowHandle < 0 || gridView3.GetFocusedRowCellValue("MaMH") == null || gridView3.GetFocusedRowCellValue("MaMon") == null)
            {
                return;
            }
            try
            {
                int maMon = int.Parse(gridView3.GetFocusedRowCellValue("MaMon").ToString());
                int maMH = int.Parse(gridView3.GetFocusedRowCellValue("MaMH").ToString());
                dinhLuongBLLDAL = new DinhLuongBLLDAL();
                dinhLuongBLLDAL.deleteDinhLuong(maMon, maMH);
                loadGridControlDL(maMon);
                txtQuyDoi.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xoá không thành công " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView3.FocusedRowHandle < 0 || gridView3.GetFocusedRowCellValue("MaMH") == null || gridView3.GetFocusedRowCellValue("MaMon") == null)
            {
                return;
            }
            string quyDoi = txtQuyDoi.Text;
            if (string.IsNullOrEmpty(quyDoi))
            {
                txtQuyDoi.Focus();
                MessageBox.Show("Vui lòng nhập quy đổi", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            decimal qd = 0;
            try
            {
                int maMon = int.Parse(gridView3.GetFocusedRowCellValue("MaMon").ToString());
                int maMH = int.Parse(gridView3.GetFocusedRowCellValue("MaMH").ToString());
                qd = decimal.Parse(quyDoi);
                dinhLuongBLLDAL = new DinhLuongBLLDAL();
                dinhLuongBLLDAL.updateDinhLuong(maMon, maMH, qd);
                loadGridControlDL(maMon);
                txtQuyDoi.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa không thành công " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView3_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridView3.FocusedRowHandle < 0)
            {
                txtQuyDoi.Clear();
                return;
            }
            txtQuyDoi.Text = gridView3.GetFocusedRowCellValue("QuyDoi").ToString();
        }

        private void gridControlMHDL_Click(object sender, EventArgs e)
        {

        }
    }
}

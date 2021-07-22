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
using DevExpress.XtraGrid.Views.Grid;
using QLNhaHang.Reports;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Enums;

namespace QLNhaHang
{
    public partial class frmGoiMonTaiQuay : Form
    {
        BanBLLDAL banBLLDAL = new BanBLLDAL();
        KhachHangBLLDAL khachHangBLLDAL = new KhachHangBLLDAL();
        NhomMonBLLDAL nhomMonBLLDAL = new NhomMonBLLDAL();
        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        int soBan = -1;
        List<Ban> lstBan;
        List<InCheBien> lstInCheBien;
        NguoiDung nd;
        public frmGoiMonTaiQuay()
        {
            InitializeComponent();
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));
            //dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            lstBan = new List<Ban>();
            lstInCheBien = new List<InCheBien>();
            lstBan = banBLLDAL.getDataBan();
            loadNhomMon();
            //
            btnGiam1SL.Enabled = false;
            btnThem1SL.Enabled = false;
            loadKhachHang();
        }

        public frmGoiMonTaiQuay(NguoiDung nd)
        {
            InitializeComponent();
            //
            this.nd = nd;
            lstBan = new List<Ban>();
            lstInCheBien = new List<InCheBien>();
            lstBan = banBLLDAL.getDataBan();
            loadNhomMon();
            //
            btnGiam1SL.Enabled = false;
            btnThem1SL.Enabled = false;
            loadKhachHang();
        }


        private void frmGoiMonTaiQuay_Load(object sender, EventArgs e)
        {
            loadBan();
            firstLoadDataGridDSMon();
            //
            lbQuyDinhTichDiem.Text = String.Format("{0:0,00}", Properties.Settings.Default.DiemTich) + "đ";
            lbQuyDinhDoiDiem.Text = Properties.Settings.Default.DiemDoi.ToString() + "%";
            //
            //Control.CheckForIllegalCrossThreadCalls = false;
            //
            startListenner();
            //
            lbNhanVien.Text = nd.HoTen;
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
                if (ban.TrangThai == 0)
                {
                    imageList.Images.Add(Properties.Resources.bantrong);
                }
                else
                {
                    imageList.Images.Add(Properties.Resources.bancokhach);
                }
                this.imgLstBoxBan.Items.Add(ban.TenBan, i);
                i++;
            }
            this.imgLstBoxBan.ColumnWidth = 130;
        }

        private void btnMoBan_Click(object sender, EventArgs e)
        {
            if (btnMoBan.Tag.Equals("1"))//Tag = 1 Ban dang dong, Tag = 0 Ban dang mo
            {
                if (soBan >= 0 && banBLLDAL.ktTinhTrangBan(lstBan[soBan].MaBan) == 0)//Mo ban
                {
                    //MessageBox.Show("Bàn được chọn: " + lstBan[soBan].MaBan + lstBan[soBan].TenBan);
                    banBLLDAL.capNhatTTMoBan(lstBan[soBan].MaBan);
                    //int soBamTemp = soBan;
                    //loadBan();//???? ListboxSelectedChange
                    //MessageBox.Show("Ban temp: " + lstBan[soBamTemp].MaBan + lstBan[soBamTemp].TenBan);
                    //imgLstBoxBan.SelectedIndex = soBamTemp;
                    //soBan = soBamTemp;
                    //MessageBox.Show("Sau khi loadBan: " + lstBan[imgLstBoxBan.SelectedIndex].MaBan + lstBan[imgLstBoxBan.SelectedIndex].TenBan);
                    btnMoBan.Tag = "0";
                    btnMoBan.Text = "Đóng bàn";
                    groupDSMonAn.Enabled = true;
                    //Tao hoa don cho ban dc mo
                    //try
                    //{
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.MaBan = lstBan[soBan].MaBan;
                    hoaDon.Ngay = DateTime.Now;
                    hoaDon.TinhTrang = 0;
                    hoaDon.MaNV = nd.MaND;
                    hoaDonBLLDAL.themHoaDon(hoaDon);
                    lbHoaDon.Text = hoaDon.MaHD.ToString();
                    lbGioVao.Text = hoaDon.Ngay.Value.ToString("hh: mm tt");
                    //Enabled control CT goi mon = true
                    enabledControlCTGoiMonTrue();
                    cbbKhachHang.Enabled = true;
                    //
                    int soBamTemp = soBan;
                    loadBan();
                    imgLstBoxBan.SelectedIndex = soBamTemp;
                    soBan = soBamTemp;
                    //}
                    //catch (Exception ex)
                    //{
                    // MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
            else
            {
                if (soBan >= 0 && banBLLDAL.ktTinhTrangBan(lstBan[soBan].MaBan) == 1)//Dong ban
                {
                    //Xac nhan dong ban
                    HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
                    if (hd == null)
                    {
                        return;
                    }
                    CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                    if (cTHDBLLDAL.getNumberCTHDByMaHD(hd.MaHD) == 0)//Hoa don chua co bat ki CTHD nao
                    {
                        DialogResult res = MessageBox.Show(lstBan[soBan].TenBan + " chưa gọi bất kỳ món nào!" + Environment.NewLine + "Bạn có muốn huỷ hoá đơn và đóng bàn này?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            //Huy hoa don
                            hoaDonBLLDAL.deleteHDByMaHD(hd.MaHD);
                            //Cap nhat đóng bàn
                            banBLLDAL.capNhatTTDongBan(lstBan[soBan].MaBan);
                            int soBamTemp = soBan;
                            loadBan();
                            imgLstBoxBan.SelectedIndex = soBamTemp;
                            soBan = soBamTemp;
                            btnMoBan.Tag = "1";
                            btnMoBan.Text = "Mở bàn";
                            groupDSMonAn.Enabled = false;
                            //Enabled control CT goi mon = false
                            enabledControlCTGoiMonFalse();
                            //Clear Label
                            clearLabel();
                        }
                        if (res == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else //Hoá đơn có CTHD
                    {
                        DialogResult res = MessageBox.Show(lstBan[soBan].TenBan + " chưa thanh toán!" + Environment.NewLine + "Bạn có muốn thanh toán và đóng bàn này?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            //Thanh toan
                            btnThanhToan.PerformClick();
                            //Cap nhat dong ban
                            //banBLLDAL.capNhatTTDongBan(lstBan[soBan].MaBan);
                            //int soBamTemp = soBan;
                            //loadBan();
                            //imgLstBoxBan.SelectedIndex = soBamTemp;
                            //soBan = soBamTemp;
                            //btnMoBan.Tag = "1";
                            //btnMoBan.Text = "Mở bàn";
                            //groupDSMonAn.Enabled = false;
                            ////Enabled control CT goi mon = false
                            //enabledControlCTGoiMonFalse();
                            ////Clear Label
                            //clearLabel();
                        }
                        if (res == DialogResult.No)
                        {
                            return;
                        }
                    }

                }
            }
        }

        private void imgLstBoxBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            soBan = imgLstBoxBan.SelectedIndex;
            if (soBan >= 0)
            {
                lbTenBan.Text = lstBan[soBan].TenBan;
                int trangThai = banBLLDAL.ktTinhTrangBan(lstBan[soBan].MaBan);
                if (trangThai == 0)
                {
                    btnMoBan.Tag = "1";
                    btnMoBan.Text = "Mở bàn";
                    groupDSMonAn.Enabled = false;
                    //Enabled control CT goi mon = false
                    enabledControlCTGoiMonFalse();
                    //Clear Label
                    clearLabel();
                    //
                    cbbKhachHang.Enabled = false;
                    cbbKhachHang.EditValue = null;
                }
                else if (trangThai == 1)
                {
                    btnMoBan.Tag = "0";
                    btnMoBan.Text = "Đóng bàn";
                    groupDSMonAn.Enabled = true;
                    //Load hoa don, CTHD bàn đang có khách
                    HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
                    if (hd == null)
                    {
                        return;
                    }
                    //Load MaHD
                    lbHoaDon.Text = hd.MaHD.ToString();
                    //Load CTHD
                    loadCTHD(hd.MaHD);
                    //Enabled control CT goi mon = true
                    enabledControlCTGoiMonTrue();
                    //
                    if (hd.Ngay != null)
                    {

                        lbGioVao.Text = hd.Ngay.Value.ToString("hh:mm tt");
                    }
                    else
                    {
                        lbGioVao.Text = "";
                    }
                    cbbKhachHang.Enabled = true;
                    cbbKhachHang.EditValue = hd.MaKH;
                    if (hd.MaKH == null || hd.MaKH == 0)
                    {
                        groupControlTichDiemGG.Enabled = false;
                        cbSDTichDiemGG.Enabled = false;
                    }
                    else
                    {
                        groupControlTichDiemGG.Enabled = true;
                        cbSDTichDiemGG.Enabled = true;
                    }
                    if (cbSDTichDiemGG.Checked)
                    {
                        cbSDTichDiemGG.Checked = false;
                    }
                    else
                    {
                        loadUnCheckSDGG();
                    }
                    //if (hd.MaKH != null)
                    //{
                    //    cbbKhachHang.Enabled = true;
                    //    cbbKhachHang.EditValue = hd.MaKH.Value;
                    //    if (hd.MaKH.Value != 0)//KH thanh vien
                    //    {
                    //        //Load thanh tien
                    //        double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
                    //        lbTienGiam.Text = "0";
                    //        lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                    //        int diemCong = (int)(tongTien / Properties.Settings.Default.DiemTich);
                    //        //Load TT KH 

                    //        //cbbKhachHang.Text = hd.KhachHang.TenKH;

                    //        //Load diem cong
                    //        if (diemCong > 0)
                    //        {
                    //            lbDiemCong.Text = diemCong.ToString();
                    //        }
                    //    }
                    //    else//Kh vãng lai
                    //    {
                    //        double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
                    //        lbTienGiam.Text = "0";
                    //        lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                    //    }
                    //}
                    //else
                    //{
                    //    cbbKhachHang.Enabled = true;
                    //    cbbKhachHang.EditValue = null;
                    //    double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
                    //    lbTienGiam.Text = "0";
                    //    lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                    //}

                }
            }
        }
        public void loadCTHD(int maHD)
        {
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            gridControlCTHD.DataSource = (from cthd in cTHDBLLDAL.getCTDHByMaHD(maHD)
                                          select new
                                          {
                                              MaHD = cthd.MaHD,
                                              MaMon = cthd.MaMon,
                                              TenMon = cthd.Mon.TenMon,
                                              DVT = cthd.Mon.DonViTinh.TenDVT,
                                              SoLuong = cthd.SoLuong,
                                              DonGia = cthd.DonGia,
                                              ThanhTien = cthd.ThanhTien
                                          }).ToList();
            gridControlCTHD.Update();
            gridControlCTHD.Refresh();
            gridView1.ClearSelection();
        }
        public void loadKhachHang()
        {
            List<KhachHang> lstKhachHang = khachHangBLLDAL.getDataKhachHang();
            cbbKhachHang.Properties.DataSource = lstKhachHang;
            cbbKhachHang.Properties.DisplayMember = "TenKH";
            cbbKhachHang.Properties.ValueMember = "MaKH";
        }

        //cbSDTichDiemGG.Enabled = false;
        //        groupControlTichDiemGG.Enabled = false;
        //        cbSDTichDiemGG.Checked = false;
        //        cbbKhachHang.Enabled = false;
        //        cbbKhachHang.EditValue = null;

        private void cbbKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            //if (cbbKhachHang.EditValue != null)
            //{
            //    string maKH = cbbKhachHang.EditValue.ToString();
            //    //Update MaKH
            //    if (hd != null)
            //    {
            //        hoaDonBLLDAL.updateTTKH(hd.MaHD, int.Parse(maKH));
            //    }
            //    if (!maKH.Equals("0"))
            //    {
            //        if (cbSDTichDiemGG.Checked)
            //        {
            //            loadThanhToanKHTV();
            //        }
            //        else
            //        {
            //            KhachHang kh = khachHangBLLDAL.getDataKhachHangByMaKH(int.Parse(maKH));
            //            if (kh == null || kh.MaKH == 0)
            //            {
            //                return;
            //            }
            //            lbDiemTichLuy.Text = kh.DiemTichLuy.ToString();
            //            lbGiamGia.Text = (kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi).ToString() + "%";

            //        }
            //        cbSDTichDiemGG.Checked = false;
            //        cbSDTichDiemGG.Enabled = true;
            //        groupControlTichDiemGG.Enabled = true;
            //    }
            //    else
            //    {
            //        loadThanhToanKVL();
            //        lbDiemCong.Text = "0";
            //        cbSDTichDiemGG.Enabled = false;
            //        groupControlTichDiemGG.Enabled = false;
            //        clearLabelDiemTLGiamGia();
            //    }
            //}
            //else
            //{
            //    cbSDTichDiemGG.Enabled = false;
            //    groupControlTichDiemGG.Enabled = false;
            //    clearLabelDiemTLGiamGia();
            //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            if (cbSDTichDiemGG.Checked)
            {
                cbSDTichDiemGG.Checked = false;
            }
            else
            {
                loadUnCheckSDGG();
            }
            if (cbbKhachHang.EditValue == null)
            {
                groupControlTichDiemGG.Enabled = false;
                cbSDTichDiemGG.Enabled = false;
                hoaDonBLLDAL.updateTTKH(hd.MaHD, null);
            }
            else
            {
                string maKH = cbbKhachHang.EditValue.ToString();
                hoaDonBLLDAL.updateTTKH(hd.MaHD, int.Parse(maKH));
                if (maKH.Equals("0"))
                {
                    groupControlTichDiemGG.Enabled = false;
                    cbSDTichDiemGG.Enabled = false;
                }
                else
                {
                    groupControlTichDiemGG.Enabled = true;
                    cbSDTichDiemGG.Enabled = true;
                }
            }
        }
        public void loadNhomMon()
        {
            NhomMon nhomTatCa = new NhomMon();
            nhomTatCa.MaNhom = 0;
            nhomTatCa.TenNhom = "Tất cả món";
            List<NhomMon> lstNhomMon = nhomMonBLLDAL.getDataNhomMon();
            lstNhomMon.Insert(0, nhomTatCa);
            var lstNM = from nhomMon in lstNhomMon
                        select new
                        {
                            MaNhom = nhomMon.MaNhom,
                            TenNhom = nhomMon.TenNhom
                        };
            cbbNhomMon.Properties.DataSource = lstNM;
            cbbNhomMon.Properties.DisplayMember = "TenNhom";
            cbbNhomMon.Properties.ValueMember = "MaNhom";
        }
        public void firstLoadDataGridDSMon()
        {
            gridControlDSMon.DataSource = (from mon in monBLLDAL.getDataMon()
                                           select new
                                           {
                                               MaMon = mon.MaMon,
                                               TenMon = mon.TenMon,
                                               DVT = mon.DonViTinh.TenDVT,
                                               GiaGoc = mon.GiaGoc,
                                               GiaKM = mon.GiaKM
                                           }).ToList();
            gridView2.ClearSelection();
        }
        private void cbbNhomMon_EditValueChanged(object sender, EventArgs e)
        {

            if (cbbNhomMon.EditValue == null)
            {
                gridControlDSMon.DataSource = (from mon in monBLLDAL.getDataMon()
                                               select new
                                               {
                                                   MaMon = mon.MaMon,
                                                   TenMon = mon.TenMon,
                                                   DVT = mon.DonViTinh.TenDVT,
                                                   GiaGoc = mon.GiaGoc,
                                                   GiaKM = mon.GiaKM
                                               }).ToList();
                gridView2.ClearSelection();
            }
            else
            {
                string maNhomMon = cbbNhomMon.EditValue.ToString();
                if (maNhomMon.Equals("0"))
                {
                    gridControlDSMon.DataSource = (from mon in monBLLDAL.getDataMon()
                                                   select new
                                                   {
                                                       MaMon = mon.MaMon,
                                                       TenMon = mon.TenMon,
                                                       DVT = mon.DonViTinh.TenDVT,
                                                       GiaGoc = mon.GiaGoc,
                                                       GiaKM = mon.GiaKM
                                                   }).ToList();
                    gridView2.ClearSelection();
                }
                else
                {
                    //try
                    //{
                    gridControlDSMon.DataSource = (from mon in monBLLDAL.getDataMonByNhomMon(int.Parse(maNhomMon))
                                                   select new
                                                   {
                                                       MaMon = mon.MaMon,
                                                       TenMon = mon.TenMon,
                                                       DVT = mon.DonViTinh.TenDVT,
                                                       GiaGoc = mon.GiaGoc,
                                                       GiaKM = mon.GiaKM
                                                   }).ToList();
                    gridView2.ClearSelection();
                    //}
                    // catch (Exception ex)
                    //{
                    // MessageBox.Show(ex.Message);
                    //}
                }
            }
            gridView2.ClearSelection();
        }

        private void gridControlMonAn_Click(object sender, EventArgs e)
        {

        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            if (gridView2.GetFocusedRowCellValue("MaMon") == null || gridView2.FocusedRowHandle < 0)
            {
                return;
            }
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                MessageBox.Show("Không tìm thấy thông tin hoá đơn!", "Thông báo - Checked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //try
            //{
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            int soLuong = Convert.ToInt32(numericUpDownSL.Value);
            string ghiChu = txtGhiChu.Text;
            int maHD = hd.MaHD;
            int maMon = int.Parse(gridView2.GetFocusedRowCellValue("MaMon").ToString());
            Mon mon = monBLLDAL.getMonByMaMon(maMon);
            if (mon == null)
            {
                MessageBox.Show("Không tìm thấy thông tin món!", "Thông báo - Checked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CTHD cthd = new CTHD();
            cthd.MaHD = maHD;
            cthd.MaMon = maMon;
            cthd.SoLuong = soLuong;
            cthd.DonGia = mon.GiaKM;
            cthd.ThanhTien = soLuong * mon.GiaKM;
            cTHDBLLDAL.insertCTHD(cthd);
            loadCTHD(maHD);
            txtGhiChu.Clear();
            numericUpDownSL.Value = 1;

            //if (cTHDBLLDAL.isExitedCTHD(maHD, maMon))
            //{
            //    gridView1.FocusedRowHandle = getIndexRowUpdate(maMon.ToString());
            //}
            //else
            //{
            //    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            //}
            //Tinh lai tien
            tinhLaiTien();
            //Them in che bien
            InCheBien inCheBien = new InCheBien(lstBan[soBan].MaBan, maMon, monBLLDAL.getTenMonByMaMon(maMon), soLuong, ghiChu);
            lstInCheBien.Add(inCheBien);
            //}
            //catch (Exception ex)
            //{
            //MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("MaMon") == null || gridView1.GetFocusedRowCellValue("MaHD") == null || gridView1.FocusedRowHandle < 0)
            {
                btnGiam1SL.Enabled = false;
                btnThem1SL.Enabled = false;
                btnXoaCTHD.Enabled = false;
                return;
            }
            btnGiam1SL.Enabled = true;
            btnThem1SL.Enabled = true;
            btnXoaCTHD.Enabled = true;
            //MessageBox.Show(gridView1.GetFocusedRowCellValue("MaMon").ToString() + " Ma HD " + gridView1.GetFocusedRowCellValue("MaHD").ToString());
            //btnThem.Enabled = true;
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView2.GetFocusedRowCellValue("MaMon") == null || gridView2.FocusedRowHandle < 0)
            {
                MessageBox.Show("Null");
                return;
            }
            else
            {
                //MessageBox.Show(gridView2.GetFocusedRowCellValue("MaMon").ToString());
                btnThem.Enabled = true;
            }

        }

        private void btnGiam1SL_Click(object sender, EventArgs e)
        {
            int rowFocus = gridView1.FocusedRowHandle;
            if (rowFocus >= 0)
            {
                if (gridView1.GetFocusedRowCellValue("MaMon") == null || gridView1.GetFocusedRowCellValue("MaHD") == null)
                {
                    return;
                }
                //try
                //{
                CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                int maHd = int.Parse(gridView1.GetFocusedRowCellValue("MaHD").ToString());
                int maMon = int.Parse(gridView1.GetFocusedRowCellValue("MaMon").ToString());
                if (cTHDBLLDAL.getNumberMonCurrent(maHd, maMon) == 1)
                {
                    MessageBox.Show("Không thể giảm vì số lượng hiện tại là 1!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cTHDBLLDAL.getNumberMonCurrent(maHd, maMon) == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin về CTHD này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadCTHD(maHd);
                    return;
                }
                HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
                if (hd == null)
                {
                    return;
                }
                if (hd.MaHD == maHd)
                {
                    cTHDBLLDAL.deCreNumberMon(maHd, maMon);
                    //loadCTHD(maHd);
                    //gridView1.FocusedRowHandle = rowFocus;
                }
                //Tinh lai tien
                tinhLaiTien();
                //Cap nhat so luong in che bien
                InCheBien inCheBien = lstInCheBien.Where(i => i.MaBan == lstBan[soBan].MaBan && i.MaMon == maMon).FirstOrDefault();
                if (inCheBien != null)
                {
                    if (inCheBien.SoLuong > 1)
                    {
                        int soLuong = inCheBien.SoLuong - 1;
                        inCheBien.SoLuong = soLuong;
                    }
                }
                //}
                //catch (Exception ex)
                //{
                // MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }

        private void btnThem1SL_Click(object sender, EventArgs e)
        {
            int rowFocus = gridView1.FocusedRowHandle;
            if (rowFocus >= 0)
            {
                if (gridView1.GetFocusedRowCellValue("MaMon") == null || gridView1.GetFocusedRowCellValue("MaHD") == null)
                {
                    return;
                }
                //try
                //{
                CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                int maHd = int.Parse(gridView1.GetFocusedRowCellValue("MaHD").ToString());
                int maMon = int.Parse(gridView1.GetFocusedRowCellValue("MaMon").ToString());
                HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
                if (hd == null)
                {
                    return;
                }
                if (hd.MaHD == maHd)
                {
                    cTHDBLLDAL.inCreNumberMon(maHd, maMon);
                    //loadCTHD(maHd);
                    //gridView1.FocusedRowHandle = rowFocus;
                }
                //Tinh lai tien
                tinhLaiTien();
                //Cap nhat so luong in che bien
                InCheBien inCheBien = lstInCheBien.Where(i => i.MaBan == lstBan[soBan].MaBan && i.MaMon == maMon).FirstOrDefault();
                if (inCheBien != null)
                {
                    int soLuong = inCheBien.SoLuong + 1;
                    inCheBien.SoLuong = soLuong;
                }

                //}
                //catch (Exception ex)
                //{
                // MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }

        private void gridView1_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            e.Cache.DrawString("Tổng Tiền:", e.Appearance.GetFont(), e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        }

        private void btnXoaCTHD_Click(object sender, EventArgs e)
        {
            int rowFocus = gridView1.FocusedRowHandle;
            if (rowFocus >= 0)
            {
                if (gridView1.GetFocusedRowCellValue("MaMon") == null || gridView1.GetFocusedRowCellValue("MaHD") == null)
                {
                    return;
                }
                //try
                //{
                CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                int maHd = int.Parse(gridView1.GetFocusedRowCellValue("MaHD").ToString());
                int maMon = int.Parse(gridView1.GetFocusedRowCellValue("MaMon").ToString());
                HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
                if (hd == null)
                {
                    return;
                }
                if (hd.MaHD == maHd)
                {
                    cTHDBLLDAL.deleteCTHD(maHd, maMon);
                    loadCTHD(maHd);
                }
                //Tinh lai tien
                tinhLaiTien();
                //Xoa in che bien
                InCheBien inCheBien = lstInCheBien.Where(i => i.MaBan == lstBan[soBan].MaBan && i.MaMon == maMon).FirstOrDefault();
                if (inCheBien != null)
                {
                    lstInCheBien.Remove(inCheBien);
                }
                //}
                //catch (Exception ex)
                //{
                // MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }
        public void enabledControlCTGoiMonFalse()
        {
            btnDoiBan.Enabled = false;
            btnGopBan.Enabled = false;
            dtNgayHienTai.Enabled = false;
            btnGiam1SL.Enabled = false;
            btnThem1SL.Enabled = false;
            btnInCheBien.Enabled = false;
            btnThanhToan.Enabled = false;
            btnXoaCTHD.Enabled = false;
            groupControlTichDiemGG.Enabled = false;
            cbSDTichDiemGG.Enabled = false;
            cbSDTichDiemGG.Checked = false;
            gridControlCTHD.DataSource = null;
        }
        public void enabledControlCTGoiMonTrue()
        {
            btnDoiBan.Enabled = true;
            btnGopBan.Enabled = true;
            btnGiam1SL.Enabled = true;
            btnThem1SL.Enabled = true;
            btnInCheBien.Enabled = true;
            btnThanhToan.Enabled = true;
            btnXoaCTHD.Enabled = true;
        }

        private void cbSDTichDiemGG_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSDTichDiemGG.Checked)
            {
                loadCheckesSDGG();
            }
            else
            {
                loadUnCheckSDGG();
            }
        }

        public void loadThanhToanKHTV()
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            if (cbbKhachHang.EditValue != null)
            {
                //try
                //{
                string maKH = cbbKhachHang.EditValue.ToString();
                if (!maKH.Equals("0"))
                {
                    CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                    double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
                    KhachHang kh = khachHangBLLDAL.getDataKhachHangByMaKH(int.Parse(maKH));
                    lbDiemTichLuy.Text = kh.DiemTichLuy.ToString();
                    double ptGiamGia = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi;
                    double tienGiam = 0;
                    double thanhTien = 0;
                    if (ptGiamGia <= 100)
                    {
                        lbGiamGia.Text = ptGiamGia.ToString() + "%";
                        if (cbSDTichDiemGG.Checked)
                        {
                            tienGiam = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi * 0.01 * tongTien;
                            thanhTien = tongTien - tienGiam;
                        }
                        else
                        {
                            tienGiam = 0;
                            thanhTien = tongTien;
                        }
                    }
                    else // > 100%
                    {
                        lbGiamGia.Text = "100%";
                        //
                        if (cbSDTichDiemGG.Checked)
                        {
                            tienGiam = tongTien;
                            thanhTien = 0;
                        }
                        else
                        {
                            tienGiam = 0;
                            thanhTien = tongTien;
                        }
                    }
                    int diemCong = (int)(thanhTien / Properties.Settings.Default.DiemTich);
                    lbTienGiam.Text = String.Format("{0:0,00}", tienGiam);
                    lbThanhTien.Text = String.Format("{0:0,00}", thanhTien);
                    lbDiemCong.Text = diemCong.ToString();
                }
                //}
                //catch (Exception ex)
                //{
                // MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            //Load MaHD
            lbHoaDon.Text = hd.MaHD.ToString();
            //Load CTHD
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            CTHDBLLDAL c = new CTHDBLLDAL();
            MessageBox.Show(cTHDBLLDAL.getCTDHByMaHD(hd.MaHD).Count.ToString() + " - " + c.getCTDHByMaHD(hd.MaHD)[0].SoLuong);
            gridControlCTHD.DataSource = (from cthd in c.getCTDHByMaHD(hd.MaHD)
                                          select new
                                          {
                                              MaHD = cthd.MaHD,
                                              MaMon = cthd.MaMon,
                                              TenMon = cthd.Mon.TenMon,
                                              DVT = cthd.Mon.DonViTinh.TenDVT,
                                              SoLuong = cthd.SoLuong,
                                              DonGia = cthd.DonGia,
                                              ThanhTien = cthd.ThanhTien
                                          }).ToList();
            gridControlCTHD.Update();
            gridControlCTHD.Refresh();
            gridView1.ClearSelection();





            //txtGhiChu.Clear();
            //numericUpDownSL.Value = 1;
            //firstLoadDataGridDSMon();
        }
        public int getIndexRowUpdate(string maMon)
        {
            try
            {
                for (int i = 1; i < gridView1.RowCount; i++)
                {
                    string mm = gridView1.GetRowCellValue(i, "MaMon").ToString();
                    if (mm.Equals(maMon))
                    {
                        return i;
                    }
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
            if (tongTien == 0)
            {
                MessageBox.Show(lstBan[soBan].TenBan + " chưa gọi bất kỳ món nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            double tienGiam = 0;
            double thanhTien = 0;
            int diemCong = 0;
            int diemTru = 0;
            if (hd.MaKH == 0)//Khach vang lai
            {
                tienGiam = 0;
                thanhTien = tongTien;
                diemCong = 0;
                diemTru = 0;
            }
            else if (hd.MaKH == null)//Chua chon khach hang
            {
                DialogResult res = MessageBox.Show("Bạn chưa chọn thông tin khách hàng" + Environment.NewLine + "Bạn có muốn thanh toán hoá đơn này dưới dạng Khách Vãng Lai?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    //Cap nhat KH = 0
                    hoaDonBLLDAL.updateKHVL(hd.MaHD);
                    tienGiam = 0;
                    thanhTien = tongTien;
                    diemCong = 0;
                    diemTru = 0;
                }
                if (res == DialogResult.No)
                {
                    return;
                }
            }
            else//Da chon khach hang
            {
                if (cbSDTichDiemGG.Checked)
                {
                    KhachHang kh = khachHangBLLDAL.getDataKhachHangByMaKH(hd.MaKH.Value);
                    //tienGiam = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi * 0.01 * tongTien;
                    //tienGiam = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi * 0.01 * tongTien;
                    double ptGiamGia = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi;
                    if (ptGiamGia <= 100)
                    {
                        diemTru = kh.DiemTichLuy.Value;
                        tienGiam = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi * 0.01 * tongTien;
                        thanhTien = tongTien - tienGiam;
                    }
                    else
                    {
                        diemTru = (int)(100 / Properties.Settings.Default.DiemDoi);
                        tienGiam = tongTien;
                        thanhTien = 0;
                    }
                }
                else
                {
                    tienGiam = 0;
                    thanhTien = tongTien;
                    diemTru = 0;
                }
                diemCong = (int)(thanhTien / Properties.Settings.Default.DiemTich);
            }
            MessageBox.Show("Tong tien : " + tongTien + " - Giam " + tienGiam + " - Thanh tien " + thanhTien + " - Diem cong " + diemCong + " - Diem tru " + diemTru);
            frmThanhToan frm = new frmThanhToan(hd.MaHD, tongTien, tienGiam, thanhTien, diemCong, diemTru);
            frm.OnUpdateStatus += Frm_OnUpdateStatus;
            frm.ShowDialog(this);
        }

        private void Frm_OnUpdateStatus(object sender, EventArgs e, int maHD, double tongTien, double tienGiam, double thanhTien, int diemCong, int diemTru)
        {
            try
            {
                HoaDon hd = hoaDonBLLDAL.getHoaDonByMaHD(maHD);
                if (hd == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin hoá đơn có mã " + maHD, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                hoaDonBLLDAL.thanhToan(maHD, tongTien, tienGiam, thanhTien);
                if (hd.MaKH != null && hd.MaKH.Value != 0)
                {
                    if (diemCong > 0)
                    {
                        khachHangBLLDAL.addDiemTL(hd.MaKH.Value, diemCong);
                    }
                    if (diemTru > 0)
                    {
                        khachHangBLLDAL.subDiemTL(hd.MaKH.Value, diemTru);
                    }
                }
                //
                banBLLDAL.capNhatTTDongBan(lstBan[soBan].MaBan);
                int soBamTemp = soBan;
                loadBan();
                loadKhachHang();
                imgLstBoxBan.SelectedIndex = soBamTemp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void tinhLaiTien()
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            if (cbSDTichDiemGG.Checked)
            {
                loadCheckesSDGG();
            }
            else
            {
                loadUnCheckSDGG();
            }
        }
        //private void Frm_OnUpdateStatus(object sender, EventArgs e)
        //{
        //    banBLLDAL.capNhatTTDongBan(lstBan[soBan].MaBan);
        //    int soBamTemp = soBan;           
        //    loadBan();
        //    cbbKhachHang.Refresh();
        //    loadKhachHang();
        //    cbbKhachHang.Refresh();
        //    imgLstBoxBan.SelectedIndex = soBamTemp;
        //}
        public void loadAfterThanhToan()
        {
            banBLLDAL.capNhatTTDongBan(lstBan[soBan].MaBan);
            int soBamTemp = soBan;
            loadBan();
            loadKhachHang();
            imgLstBoxBan.SelectedIndex = soBamTemp;
        }
        public void clearLabel()
        {
            lbHoaDon.Text = "";
            lbDiemTichLuy.Text = "0";
            lbGiamGia.Text = "0%";
            lbDiemCong.Text = "0";
            lbTienGiam.Text = "0";
            lbThanhTien.Text = "0";
            lbGioVao.Text = "";
        }
        public void clearLabelDiemTLGiamGia()
        {
            lbDiemTichLuy.Text = "0";
            lbGiamGia.Text = "0%";
            lbDiemCong.Text = "0";
        }

        private void btnDoiBan_Click(object sender, EventArgs e)
        {
            frmDoiBan frmDoiBan = new frmDoiBan(lstBan[soBan].MaBan);
            frmDoiBan.OnUpdateStatusDoiBan += FrmDoiBan_OnUpdateStatusDoiBan;
            frmDoiBan.ShowDialog(this);
        }

        private void FrmDoiBan_OnUpdateStatusDoiBan(object sender, EventArgs e, int maBanCu, int maBanMoi, int soBan)
        {
            try
            {
                HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(maBanCu);
                if (hd == null)
                {
                    return;
                }
                hoaDonBLLDAL.updateMaBanDoiBan(hd.MaHD, maBanMoi);
                banBLLDAL.capNhatTTDongBan(maBanCu);
                banBLLDAL.capNhatTTMoBan(maBanMoi);
                loadBan();
                imgLstBoxBan.SelectedIndex = soBan;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGopBan_Click(object sender, EventArgs e)
        {
            frmGopBan frmGopBan = new frmGopBan(lstBan[soBan].MaBan);
            frmGopBan.OnUpdateStatusGopBan += FrmGopBan_OnUpdateStatusGopBan;
            frmGopBan.ShowDialog(this);
        }

        private void FrmGopBan_OnUpdateStatusGopBan(object sender, EventArgs e, int maBanCu, int maBanMoi, int soBan)
        {
            try
            {
                HoaDon hdBanCu = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(maBanCu);
                HoaDon hdBanMoi = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(maBanMoi);
                if (hdBanCu == null || hdBanMoi == null)
                {
                    return;
                }
                CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                cTHDBLLDAL.updateMaHDDoiBan(hdBanCu.MaHD, hdBanMoi.MaHD);//Cap nhat CTHD ban cu sang  ban moi
                hoaDonBLLDAL.deleteHDByMaHD(hdBanCu.MaHD);//Xoa hoa don ban cu
                banBLLDAL.capNhatTTDongBan(maBanCu);
                loadBan();
                imgLstBoxBan.SelectedIndex = soBan;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void loadThanhToanKVL()
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
            lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
            lbTienGiam.Text = "0";
            lbDiemTichLuy.Text = "0";
            lbDiemCong.Text = "0";

        }
        public void loadUnCheckSDGG()
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
            if (cbbKhachHang.EditValue == null)
            {
                lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                lbTienGiam.Text = "0";
                lbDiemTichLuy.Text = "0";
                lbGiamGia.Text = "0%";
                lbDiemCong.Text = "0";
            }
            else
            {
                string maKH = cbbKhachHang.EditValue.ToString();
                if (maKH.Equals("0"))
                {
                    lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                    lbTienGiam.Text = "0";
                    lbGiamGia.Text = "0%";
                    lbDiemTichLuy.Text = "0";
                    lbDiemCong.Text = "0";
                }
                else
                {
                    KhachHang kh = khachHangBLLDAL.getDataKhachHangByMaKH(int.Parse(maKH));
                    double ptGiamGia = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi;
                    if (ptGiamGia <= 100)
                    {
                        lbGiamGia.Text = ptGiamGia + "%";
                    }
                    else
                    {
                        lbGiamGia.Text = "100%";
                    }
                    double thanhTien = tongTien;
                    lbTienGiam.Text = "0";
                    lbThanhTien.Text = String.Format("{0:0,00}", thanhTien);
                    int diemCong = (int)(thanhTien / Properties.Settings.Default.DiemTich);
                    lbDiemCong.Text = diemCong.ToString();
                    lbDiemTichLuy.Text = kh.DiemTichLuy.ToString();
                }
            }
        }

        public void loadCheckesSDGG()
        {
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaBanCoKhach(lstBan[soBan].MaBan);
            if (hd == null)
            {
                return;
            }
            CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
            double tongTien = cTHDBLLDAL.totalMoney(hd.MaHD);
            if (cbbKhachHang.EditValue == null)
            {
                lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                lbTienGiam.Text = "0";
                lbDiemTichLuy.Text = "0";
                lbGiamGia.Text = "0%";
                lbDiemCong.Text = "0";
            }
            else
            {
                string maKH = cbbKhachHang.EditValue.ToString();
                if (maKH.Equals("0"))
                {
                    lbThanhTien.Text = String.Format("{0:0,00}", tongTien);
                    lbTienGiam.Text = "0";
                    lbDiemTichLuy.Text = "0";
                    lbDiemCong.Text = "0";
                    lbGiamGia.Text = "0%";
                }
                else
                {
                    KhachHang kh = khachHangBLLDAL.getDataKhachHangByMaKH(int.Parse(maKH));
                    double ptGiamGia = kh.DiemTichLuy.Value * Properties.Settings.Default.DiemDoi;
                    if (ptGiamGia <= 100)
                    {
                        lbGiamGia.Text = ptGiamGia + "%";
                        double tienGiam = ptGiamGia * 0.01 * tongTien;
                        double tt = tongTien - tienGiam;
                        lbTienGiam.Text = String.Format("{0:0,00}", tienGiam);
                        lbThanhTien.Text = String.Format("{0:0,00}", tt);
                        int diemCong = (int)(tt / Properties.Settings.Default.DiemTich);
                        lbDiemCong.Text = diemCong.ToString();
                    }
                    else
                    {
                        lbGiamGia.Text = "100%";
                        double tienGiam = tongTien;
                        lbTienGiam.Text = String.Format("{0:0,00}", tienGiam);
                        lbThanhTien.Text = "0";
                        lbDiemCong.Text = "0";
                    }
                    lbDiemTichLuy.Text = kh.DiemTichLuy.ToString();
                }
            }

        }

        private void btnInCheBien_Click(object sender, EventArgs e)
        {
            List<InCheBien> lst = lstInCheBien.Where(l => l.MaBan == lstBan[soBan].MaBan).ToList();
            if (lst.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin gọi món", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmReportInCheBien frm = new frmReportInCheBien(lst, lstBan[soBan].TenBan);
            frm.ShowDialog(this);
            lstInCheBien.RemoveAll(r => r.MaBan == lstBan[soBan].MaBan);
        }
        SqlTableDependency<Ban> dep;
        SqlTableDependency<CTHD> depCTHD;
        public void startListenner()
        {
            string stringConnection = Properties.Settings.Default.ChuoiKetNoi;
            var mapper = new ModelToTableMapper<Ban>();
            mapper.AddMapping(c => c.MaBan, "MaBan");

            dep = new SqlTableDependency<Ban>(stringConnection, "Ban", mapper: mapper);
            dep.OnChanged += Dep_OnChanged;
            //
            var mapperCTHD = new ModelToTableMapper<CTHD>();
            mapperCTHD.AddMapping(c => c.MaHD, "MaHD");
            depCTHD = new SqlTableDependency<CTHD>(stringConnection, "CTHD", mapper: mapperCTHD);
            depCTHD.OnChanged += DepCTHD_OnChanged;

            depCTHD.Start();
            dep.Start();
        }

        private void DepCTHD_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<CTHD> e)
        {

            if (e.ChangeType == ChangeType.Insert || e.ChangeType == ChangeType.Update)
            {
                CTHD cthd = e.Entity;
                int maBanHT = hoaDonBLLDAL.getMaBanByMaHD(cthd.MaHD);
                if (maBanHT == lstBan[soBan].MaBan)
                {
                    reloadCTHD(cthd.MaHD, cthd.MaMon, e.ChangeType);
                }
            }
        }

        private void Dep_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Ban> e)
        {
            var changedEntity = e.Entity;
            //var str = new StringBuilder();
            //str.Append("====================================================" + Environment.NewLine);
            //str.Append("DML operation: " + e.ChangeType + Environment.NewLine);
            //str.Append("MaBan: " + changedEntity.MaBan + Environment.NewLine);
            //str.Append("====================================================");
            //MessageBox.Show(str.ToString());
            someMethod();
            //reloadBan();
        }
        private void reloadCTHD(int maHD, int maMon, ChangeType t)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                loadCTHD(maHD);
                if (t == ChangeType.Insert)
                {

                    gridView1.FocusedRowHandle = gridView1.RowCount - 1;
                }
                else if (t == ChangeType.Update)
                {
                    CTHDBLLDAL cTHDBLLDAL = new CTHDBLLDAL();
                    if (cTHDBLLDAL.isExitedCTHD(maHD, maMon))
                    {
                        gridView1.FocusedRowHandle = getIndexRowUpdate(maMon.ToString());
                    }
                }
            }));
        }
        private void someMethod()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                int banHienTai = imgLstBoxBan.SelectedIndex;
                lstBan = banBLLDAL.getDataBan();
                imgLstBoxBan.Items.Clear();
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(64, 64);
                this.imgLstBoxBan.ImageList = imageList;
                int i = 0;
                foreach (Ban ban in lstBan)
                {
                    if (ban.TrangThai == 0)
                    {
                        imageList.Images.Add(Properties.Resources.bantrong);
                    }
                    else
                    {
                        imageList.Images.Add(Properties.Resources.bancokhach);
                    }
                    this.imgLstBoxBan.Items.Add(ban.TenBan, i);
                    i++;
                }
                this.imgLstBoxBan.ColumnWidth = 130;
                if (banHienTai >= 0)
                {
                    imgLstBoxBan.SelectedIndex = banHienTai;
                }
            }));
        }
        public void reloadBan()
        {
            int banHienTai = imgLstBoxBan.SelectedIndex;
            lstBan = banBLLDAL.getDataBan();
            imgLstBoxBan.Items.Clear();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            this.imgLstBoxBan.ImageList = imageList;
            int i = 0;
            foreach (Ban ban in lstBan)
            {
                if (ban.TrangThai == 0)
                {
                    imageList.Images.Add(Properties.Resources.bantrong);
                }
                else
                {
                    imageList.Images.Add(Properties.Resources.bancokhach);
                }
                this.imgLstBoxBan.Items.Add(ban.TenBan, i);
                i++;
            }
            this.imgLstBoxBan.ColumnWidth = 130;
            if (banHienTai >= 0)
            {
                imgLstBoxBan.SelectedIndex = banHienTai;
            }
        }

        private void frmGoiMonTaiQuay_FormClosing(object sender, FormClosingEventArgs e)
        {
            depCTHD.Stop();
            dep.Stop();
        }
    }
}

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
using DevExpress.XtraBars;
using TableDependency.SqlClient.Base.Enums;

namespace QLNhaHang
{
    public partial class frmMain : Form
    {
        NguoiDungNhomNguoiDungBLLDAL nguoiDungNhomNguoiDungBLLDAL = new NguoiDungNhomNguoiDungBLLDAL();
        PhanQuyenBLLDAL phanQuyenBLLDAL = new PhanQuyenBLLDAL();
        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        NguoiDung nd = new NguoiDung();
        string tenDN;
        public frmMain()
        {
            InitializeComponent();
        }
        
        public frmMain(string tenDN)
        {
            InitializeComponent();
            this.tenDN = tenDN;
        }
        public void showForm(Form form)
        {
            //Check before open
            if (!IsFormActived(form))
            {
                form.MdiParent = this;
                form.Dock = DockStyle.Fill;
                form.Show();
            }
        }
        private bool IsFormActived(Form form)
        {
            bool IsOpenend = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        IsOpenend = true;
                    }

                }
            }
            return IsOpenend;
        }
        private bool checkExitForm(string name)
        {
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (name == item.Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void barBtnGoiMonTaiQuay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGoiMonTaiQuay frm = new frmGoiMonTaiQuay(nd);
            frm.Name = "frmGoiMonTaiQuay";
            showForm(frm);
        }

        private void barBtnGoiMonTaiBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGoiMonTaiBan frmGoiMonTaiBan = new frmGoiMonTaiBan();
            frmGoiMonTaiBan.Name = "frmGoiMonTaiBan";
            showForm(frmGoiMonTaiBan);
        }

        private void barBtnNguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNguoiDung frmNguoiDung = new frmNguoiDung();
            frmNguoiDung.Name = "frmNguoiDung";
            showForm(frmNguoiDung);
        }

        private void barBtnNhomNguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhomNguoiDung frmNhomNguoiDung = new frmNhomNguoiDung();
            frmNhomNguoiDung.Name = "frmNhomNguoiDung";
            showForm(frmNhomNguoiDung);
        }

        private void barBtnManHinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmManHinh frmManHinh = new frmManHinh();
            frmManHinh.Name = "frmManHinh";
            showForm(frmManHinh);
        }

        private void barBtnThemNDNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemNguoiDungVaoNhom frmThemNguoiDungVaoNhom = new frmThemNguoiDungVaoNhom();
            frmThemNguoiDungVaoNhom.Name = "frmThemNguoiDungVaoNhom";
            showForm(frmThemNguoiDungVaoNhom);
        }

        private void barBtnPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhanQuyen frmPhanQuyen = new frmPhanQuyen();
            frmPhanQuyen.Name = "frmPhanQuyen";
            showForm(frmPhanQuyen);
        }

        private void barButtonQLMonAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNhomMonMonAn frmNhomMonMonAn = new frmNhomMonMonAn();
            frmNhomMonMonAn.Name = "frmNhomMonMonAn";
            frmNhomMonMonAn.OnUpdateMon += FrmNhomMonMonAn_OnUpdateMon;
            frmNhomMonMonAn.OnUpdateNhomMon += FrmNhomMonMonAn_OnUpdateNhomMon;
            showForm(frmNhomMonMonAn);
        }
        bool checkUpdateNhomMon = false;
        private void FrmNhomMonMonAn_OnUpdateNhomMon(object sender, EventArgs e)
        {
            if (checkExitForm("frmGoiMonTaiQuay"))
            {
                checkUpdateNhomMon = true;
            }
        }
        bool checkUpdateMon = false;
        private void FrmNhomMonMonAn_OnUpdateMon(object sender, EventArgs e)
        {
            if (checkExitForm("frmGoiMonTaiQuay"))
            {
                checkUpdateMon = true;
            }
        }

        private void barBtnQLKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhachHang frmKhachHang = new frmKhachHang();
            frmKhachHang.Name = "frmKhachHang";
            frmKhachHang.OnUpdateStatus += FrmKhachHang_OnUpdateStatus;
            showForm(frmKhachHang);
        }
        bool checkInsertUpdateKH = false;
        bool checkDeleteKH = false;
        private void FrmKhachHang_OnUpdateStatus(object sender, EventArgs e, TableDependency.SqlClient.Base.Enums.ChangeType c)
        {
            if (checkExitForm("frmGoiMonTaiQuay") && (c == ChangeType.Insert || c == ChangeType.Update))
            {
                checkInsertUpdateKH = true;
            }
            if (checkExitForm("frmGoiMonTaiQuay") && c == ChangeType.Delete)
            {
                checkDeleteKH = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            nd = nguoiDungBLLDAL.getNDByTenDN(tenDN);
            phanQuyenMenu(nd.MaND);
        }

        private void barBtnQLBanAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBanAn frmBanAn = new frmBanAn();
            frmBanAn.Name = "frmBanAn";
            frmBanAn.OnUpdateBan += FrmBanAn_OnUpdateBan;
            frmBanAn.ShowDialog(this);
        }
        bool checkInsertUpdateBan = false;
        bool checkDeleteBan = false;
        private void FrmBanAn_OnUpdateBan(object sender, EventArgs e, ChangeType c)
        {
            if (checkExitForm("frmGoiMonTaiQuay") && (c == ChangeType.Insert || c == ChangeType.Update))
            {
                checkInsertUpdateBan = true;
            }
            if (checkExitForm("frmGoiMonTaiQuay") && c == ChangeType.Delete)
            {
                checkDeleteBan = true;
            }
            try
            {
                DevExpress.XtraTabbedMdi.XtraMdiTabPage a = xtraTabbedMdiManager1.SelectedPage;
                if (a != null)
                {
                    Form form = a.MdiChild.FindForm();
                    if (form.Name.Equals("frmGoiMonTaiQuay"))
                    {
                        frmGoiMonTaiQuay frmGoiMonTaiQuay = (frmGoiMonTaiQuay)form;
                        if (checkInsertUpdateBan == true)
                        {
                            frmGoiMonTaiQuay.reloadBan();
                            MessageBox.Show("In - Up ban");
                            checkInsertUpdateBan = false;
                        }

                        if (checkDeleteBan == true)
                        {
                            frmGoiMonTaiQuay.loadBan();
                            checkDeleteBan = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barBtnQLGiamGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmKhuyenMai frmKhuyenMai = new frmKhuyenMai();
            frmKhuyenMai.Name = "frmKhuyenMai";
            showForm(frmKhuyenMai);
        }
        public void timKiemMenu(PhanQuyen phanQuyen)
        {
            foreach (var item in ribbonControl1.Items)
            {
                if (item.GetType() == typeof(BarButtonItem))
                {
                    if (((BarButtonItem)item).Tag != null)
                    {
                        if (((BarButtonItem)item).Tag.ToString().Equals(phanQuyen.MaMH.ToString()))
                        {
                            if(phanQuyen.CoQuyen == 0)
                            {
                                ((BarButtonItem)item).Enabled = false;
                                ((BarButtonItem)item).Visibility = BarItemVisibility.Never;
                                break;
                            }
                            else
                            {
                                ((BarButtonItem)item).Enabled = true;
                                ((BarButtonItem)item).Visibility = BarItemVisibility.Always;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void phanQuyenMenu(int maND)
        {
            List<int> lstMaNhomND = nguoiDungNhomNguoiDungBLLDAL.getMaNhomByMaND(maND);//Thay sau khi login
            foreach(int maNhom in lstMaNhomND)
            {
                List<PhanQuyen> lstQuyen = phanQuyenBLLDAL.getQuyenByMaNhom(maNhom);
                foreach(PhanQuyen phanQuyen in lstQuyen)
                {
                    timKiemMenu(phanQuyen);
                }
            }
        }

        private void barBtnSaoLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSaoLuu frmSaoLuu = new frmSaoLuu();
            frmSaoLuu.Name = "frmSaoLuu";
            frmSaoLuu.ShowDialog(this);
        }

        private void barBtnTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmTTTaiKhoan frmTTTaiKhoan = new frmTTTaiKhoan();
            frmTTTaiKhoan.Name = "frmTTTaiKhoan";
            showForm(frmTTTaiKhoan);
        }

        private void barBtnQLNhapKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmQLNhapHang frmQLNhapHang = new frmQLNhapHang(nd);
            frmQLNhapHang.Name = "frmQLNhapHang";
            showForm(frmQLNhapHang);
        }

        private void barBtnTKDoanhThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDoanhThu frmDoanhThu = new frmDoanhThu();
            frmDoanhThu.Name = "frmDoanhThu";
            showForm(frmDoanhThu);
        }

        private void xtraTabbedMdiManager1_SelectedPageChanged(object sender, EventArgs e)
        {
            DevExpress.XtraTabbedMdi.XtraMdiTabPage tabPage = xtraTabbedMdiManager1.SelectedPage;
            if (tabPage != null)
            {
                Form form = tabPage.MdiChild.FindForm();
                switch (form.Name)
                {
                    case "frmGoiMonTaiQuay":
                        frmGoiMonTaiQuay frmGoiMonTaiQuay = (frmGoiMonTaiQuay)form;
                        if (checkInsertUpdateKH == true)
                        {
                            frmGoiMonTaiQuay.loadKhachHang();
                            checkInsertUpdateKH = false;
                        }
                        if (checkDeleteKH == true)
                        {
                            frmGoiMonTaiQuay.loadKhachHang();
                            checkDeleteKH = false;
                        }
                        if (checkUpdateMon == true)
                        {
                            frmGoiMonTaiQuay.firstLoadDataGridDSMon();
                            checkUpdateMon = false;
                        }
                        if (checkUpdateNhomMon == true)
                        {
                            frmGoiMonTaiQuay.loadNhomMon();
                            checkUpdateNhomMon = false;
                        }
                        break;
                }
            }
        }

        //private void xtraTabbedMdiManager1_SelectedPageChanged(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Change");
        //}
    }
}

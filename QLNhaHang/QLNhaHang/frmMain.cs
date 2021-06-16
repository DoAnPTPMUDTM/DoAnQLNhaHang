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
    public partial class frmMain : Form
    {
        
        public frmMain()
        {
            InitializeComponent();
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
        private void barBtnGoiMonTaiQuay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGoiMonTaiQuay frm = new frmGoiMonTaiQuay();
            frm.Name = "frmGoiMonTaiQuay";
            showForm(frm);
        }

        private void barBtnGoiMonTaiBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmGoiMonTaiBan frmGoiMonTaiBan = new frmGoiMonTaiBan();
            frmGoiMonTaiBan.Name = "frmGoiMonTaiBan";
            frmGoiMonTaiBan.ShowDialog(this);
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
    }
}

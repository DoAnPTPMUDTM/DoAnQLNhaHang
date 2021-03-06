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
    public partial class frmNhomMonMonAn : Form
    {
        NhomMonBLLDAL nhomMonBLLDAL = new NhomMonBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        ConfigImage configImage = new ConfigImage();
        public frmNhomMonMonAn()
        {
            InitializeComponent();
        }

        private void frmNhomMonMonAn_Load(object sender, EventArgs e)
        {
            loadDataNhomMon();
            loadDataMonAn();
        }

        private void loadDataNhomMon()
        {
            NhomMon nhomTatCa = new NhomMon();
            nhomTatCa.MaNhom = 0;
            nhomTatCa.TenNhom = "Tất cả";
            //
            List<NhomMon> lstNhomMon = nhomMonBLLDAL.getDataNhomMon();
            lstNhomMon.Insert(0, nhomTatCa);
            var nhomMons = from nm in lstNhomMon
                           select new
                           {
                               MaNhom = nm.MaNhom,
                               TenNhom = nm.TenNhom
                           };
            gridViewNhomMon.DataSource = nhomMons.ToList();

        }
        private void loadDataMonAn()
        {
            // Mã món, tên món, đvt, giá km, giá gốc, mã km
            monBLLDAL = new MonBLLDAL();
            var monAns = from mon in monBLLDAL.getDataMon()
                         select new
                         {
                             MaMon = mon.MaMon,
                             TenMon = mon.TenMon,
                             MaNhom = mon.NhomMon.TenNhom,
                             DVT = mon.DonViTinh.TenDVT,
                             Anh = loadImageMon(mon.Anh),//
                             GiaGoc = mon.GiaGoc,
                             GiaKM = mon.GiaKM,
                             //MaKM = mon.KhuyenMai == null ? 0 : mon.KhuyenMai.TyLe.Value
                             MaKM = mon.KhuyenMai.TyLe
                         };
            gridViewMonAn.DataSource = monAns.ToList();
        }

        public Image loadImageMon(string fileName)
        {
            try
            {
                if (Image.FromFile(configImage.GetProjectLinkDirectory() + configImage.imgAnhMon + fileName) == null)// Kiem tra file not found khong hoat dong?// 
                {
                    return QLNhaHang.Properties.Resources.monan;
                }
                else
                {
                    return Image.FromFile(configImage.GetProjectLinkDirectory() + configImage.imgAnhMon + fileName);
                }
            }
            catch
            {
                return QLNhaHang.Properties.Resources.monan;
            }
        }

        //private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        //{
        //    if (e.RowHandle >= 0)
        //    {
        //        if (e.RowHandle % 2 == 0)
        //        {
        //            e.Appearance.BackColor = Color.FromArgb(150, Color.FromArgb(0, 192, 192));
        //            //e.Appearance.BackColor = Color.FromArgb(150, Color.LightBlue);
        //            e.Appearance.BackColor2 = Color.White;
        //        }
        //        else
        //        {
        //            e.Appearance.BackColor = Color.White;
        //        }
        //    }

        //}

        private void loadDataMonAnByMaNhom(int maNhom)
        {
            monBLLDAL = new MonBLLDAL();
            var monAns = from mon in monBLLDAL.getDataMonByNhomMon(maNhom)
                         from nhom in nhomMonBLLDAL.getDataNhomMon()
                         where mon.MaNhom == nhom.MaNhom
                         select new
                         {
                             MaMon = mon.MaMon,
                             TenMon = mon.TenMon,
                             DVT = mon.DonViTinh.TenDVT,
                             Anh = loadImageMon(mon.Anh),
                             MaNhom = nhom.TenNhom,
                             GiaGoc = mon.GiaGoc,
                             GiaKM = mon.GiaKM,
                             MaKM = mon.KhuyenMai.TyLe
                         };
            gridViewMonAn.DataSource = monAns.ToList();
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string maNhom = gridView1.GetFocusedRowCellValue("MaNhom").ToString();
            if (maNhom == null)
            {
                loadDataMonAn();
            }
            else if (maNhom.Equals("0"))
            {
                loadDataMonAn();
            }
            else
            {
                loadDataMonAnByMaNhom(int.Parse(maNhom));
            }

        }

        private void barButtonThemNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemNhomMon frmThemNhomMon = new frmThemNhomMon();
            frmThemNhomMon.OnUpdate += FrmThemNhomMon_OnUpdate;
            frmThemNhomMon.Name = "frmThemNhomMon";
            frmThemNhomMon.ShowDialog(this);
        }

        private void FrmThemNhomMon_OnUpdate(object sender, EventArgs e, string tenNhomMon)
        {
            try
            {
                NhomMon nhomMon = new NhomMon();
                nhomMon.TenNhom = tenNhomMon;
                nhomMonBLLDAL.insertNhomMon(nhomMon);
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNhomMon();
                UpdateNhomMon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadDataNhomMon();
        }

        private void barButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string maNhom = gridView1.GetFocusedRowCellValue("MaNhom").ToString();
            if (maNhom == null)
            {
                return;
            }
            if (maNhom.Equals("0"))
            {
                MessageBox.Show("Xóa nhóm món thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                nhomMonBLLDAL.deleteNhomMon(int.Parse(maNhom));
                MessageBox.Show("Xóa nhóm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNhomMon();
                UpdateNhomMon();
            }
            catch
            {
                MessageBox.Show("Xóa nhóm món thất bại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void barButtonSuaNhom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
            // {
            string maNhom = gridView1.GetFocusedRowCellValue("MaNhom").ToString();


            if (maNhom == null)
            {
                loadDataMonAn();
                return;
            }
            if (maNhom.Equals("0"))
            {
                return;
            }


            NhomMon nhomMon = nhomMonBLLDAL.getNhomMonByMaMon(int.Parse(maNhom));
            if (nhomMon == null)
            {
                MessageBox.Show("Không tìm thấy nhóm món", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmSuaNhomMon frmSuaNhomMon = new frmSuaNhomMon(nhomMon.TenNhom, nhomMon.MaNhom);
            frmSuaNhomMon.Name = "frmSuaNhomMon";
            frmSuaNhomMon.OnUpdate += FrmSuaNhomMon_OnUpdate;


            //
            frmSuaNhomMon.ShowDialog(this);
            //}
            // catch(Exception ex)
            //{
            //MessageBox.Show();//ex
            //}

        }

        private void FrmSuaNhomMon_OnUpdate(object sender, EventArgs e, string tenNhom, int maNhom)
        {
            try
            {
                monBLLDAL = new MonBLLDAL();
                nhomMonBLLDAL.updateNhomMon(maNhom, tenNhom);
                MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataNhomMon();
                UpdateNhomMon();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonThemMon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThemMonAn frmThemMonAn = new frmThemMonAn();
            frmThemMonAn.OnUpdateMonAn += FrmThemMonAn_OnUpdateMonAn;
            frmThemMonAn.Name = "frmThemMonAn";
            frmThemMonAn.ShowDialog(this);
        }

        private void FrmThemMonAn_OnUpdateMonAn(object sender, EventArgs eventArgs, Mon mon, string fileLocal, string fileApp)
        {
            monBLLDAL = new MonBLLDAL();
            bool checkSaveImg = false;
            try
            {
                File.Copy(fileLocal, fileApp);
                checkSaveImg = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu ảnh không thành công: " + ex.Message);
                return;
            }
            if (checkSaveImg)
            {
                try
                {
                    monBLLDAL.insertMonAn(mon);
                    MessageBox.Show("Thêm món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDataMonAn();
                    UpdateMon();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm món không thành công: " + ex.Message);
                    if (File.Exists(fileApp))
                    {
                        File.Delete(fileApp);
                    }
                }
            }
        }


        //private void FrmThemMonAn_OnUpdateMonAn(object sender, EventArgs eventArgs, string tenMon, string nhomMon, string khuyenMai, double giaGoc, double giaKM, string donViTinh, string imgMonAn)
        //{
        //    try
        //    {
        //        Mon mon = new Mon();
        //        mon.TenMon = tenMon;
        //        mon.MaNhom = int.Parse(nhomMon);
        //        //if (khuyenMai)
        //        //{
        //        //    mon.MaKM = 0;
        //        //}
        //        //else
        //        //{
        //        if(int.Parse(khuyenMai) == 0)
        //        {
        //            mon.MaKM = null;
        //        }
        //        else
        //        {
        //            mon.MaKM = int.Parse(khuyenMai);
        //        }
        //        //}
        //        mon.GiaGoc = (decimal)giaGoc;
        //        mon.GiaKM = (decimal)giaKM;
        //        //mon.MaDVT = donViTinh;
        //        mon.Anh = imgMonAn;
        //        monBLLDAL.insertMonAn(mon);
        //        MessageBox.Show("Thêm món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        loadDataMonAn();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void barButtonSuaMon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string maMon = layoutView1.GetFocusedRowCellValue("MaMon").ToString();
            if (maMon == null)
            {
                loadDataMonAn();
                return;
            }
            //try
            //{
            Mon mon = monBLLDAL.getMonByMaMon(int.Parse(maMon));
            if (mon == null)
            {
                MessageBox.Show("Không tìm thấy món ăn", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmSuaMonAn frmSuaMonAn = new frmSuaMonAn(mon.MaMon, mon.TenMon, mon.MaNhom.ToString(), mon.MaKM.ToString(), mon.MaDVT.ToString(), (double)mon.GiaGoc, (double)mon.GiaKM, mon.Anh);
            frmSuaMonAn.OnUpdateMonAn += FrmSuaMonAn_OnUpdateMonAn; ;
            frmSuaMonAn.Name = "frmSuaMonAn";
            frmSuaMonAn.ShowDialog(this);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }

        private void FrmSuaMonAn_OnUpdateMonAn(object sender, EventArgs eventArgs, int maMon, string tenMon, string nhomMon, string khuyenMai, string donViTinh, double giaGoc, double giaKM, string imgMonAn, string fileLocal, string fileApp, bool check)
        {
            //MessageBox.Show(maMon + " - " + tenMon + " - " + nhomMon + " - " + khuyenMai + " - " + donViTinh + " - " + giaGoc + " - " + giaKM + " - " + imgMonAn + " - " + fileLocal + " - " + fileApp + " - " + check.ToString());
            monBLLDAL = new MonBLLDAL();
            bool checkSaveImg = false;
            if (check)
            {
                try
                {
                    File.Copy(fileLocal, fileApp);
                    checkSaveImg = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu ảnh không thành công: " + ex.Message);
                    return;
                }
            }
            if (check)
            {
                if (checkSaveImg)
                {
                    //try
                    //{
                    monBLLDAL.updateMonAn(maMon, int.Parse(nhomMon), tenMon, int.Parse(donViTinh), imgMonAn, giaGoc, giaKM, int.Parse(khuyenMai));
                    MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDataMonAn();
                    UpdateMon();
                    //}
                    //catch (Exception ex)
                    //{
                    // MessageBox.Show("Sửa món không thành công" + ex.Message);
                    //if (File.Exists(fileApp))
                    //{
                    //File.Delete(fileApp);
                    //}
                }
            }
            else
            {
                //try
                //{
                monBLLDAL.updateMonAnNoImg(maMon, int.Parse(nhomMon), tenMon, int.Parse(donViTinh), giaGoc, giaKM, int.Parse(khuyenMai));
                MessageBox.Show("Sửa thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataMonAn();
                UpdateMon();
                //}
                //catch (Exception ex)
                //{
                // MessageBox.Show("Sửa món không thành công" + ex.Message);
                //}
            }
        }


        private void barButtonXoaMon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string maMon = layoutView1.GetFocusedRowCellValue("MaMon").ToString();
            if (maMon == null)
            {
                return;
            }
            try
            {
                int iMaMon = int.Parse(maMon);
                if (monBLLDAL.ktKhoaNgoaiMon(iMaMon))
                {
                    MessageBox.Show("Hiện tại bạn không thể xoá món này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                monBLLDAL.deleteMonAn(iMaMon);
                MessageBox.Show("Xóa món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataMonAn();
                UpdateMon();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Xóa món ăn thất bại " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonRefeshMon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadDataMonAn();
        }
        public delegate void StatusUpdateHandler(object sender, EventArgs e);
        public event StatusUpdateHandler OnUpdateMon, OnUpdateNhomMon;

        private void UpdateMon()
        {
            EventArgs args = new EventArgs();
            OnUpdateMon?.Invoke(this, args);
        }
        private void UpdateNhomMon()
        {
            EventArgs args = new EventArgs();
            OnUpdateNhomMon?.Invoke(this, args);
        }
        private void gridViewMonAn_Click(object sender, EventArgs e)
        {

        }
    }
}

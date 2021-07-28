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
using QLNhaHang.Reports;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Enums;

namespace QLNhaHang
{
    public partial class frmGoiMonTaiBan : Form
    {
        BanBLLDAL banBLLDAL = new BanBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        GoiMonTaiBanBLLDAL goiMonTaiBanBLLDAL = new GoiMonTaiBanBLLDAL();
        List<Ban> lstBan;
        public frmGoiMonTaiBan()
        {
            InitializeComponent();
            lstBan = new List<Ban>();
        }

        private void btnHuyBoFrmGoiMonTaiBan_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }


        //

        public delegate void StatusUpdateHandler(object sender, EventArgs e, int maBan);
        public event StatusUpdateHandler OnUpdateStatus;

        //When button is clicked, this is trigged
        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    //In here, you now trigger your custom event

        //}


        private void UpdateGMTB(int maBan)
        {
            //Create arguments.  You should also have custom one, or else return EventArgs.Empty();
            EventArgs args = new EventArgs();

            //Call any listeners
            OnUpdateStatus?.Invoke(this, args, maBan);

        }
        public void loadBan()
        {
            banBLLDAL = new BanBLLDAL();
            lstBan = banBLLDAL.getBanGoiMonTaiBan();
            imgLstBoxBan.Items.Clear();
            if (lstBan == null || (lstBan != null && lstBan.Count == 0))
            {
                return;
            }
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            this.imgLstBoxBan.ImageList = imageList;
            int i = 0;
            foreach (Ban ban in lstBan)
            {
                imageList.Images.Add(Properties.Resources.bancokhach);
                this.imgLstBoxBan.Items.Add(ban.TenBan, i);
                i++;
            }
            this.imgLstBoxBan.ColumnWidth = 130;
        }
        private void frmGoiMonTaiBan_Load(object sender, EventArgs e)
        {
            loadBan();
            startListenner();
        }

        private void imgLstBoxBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int soBan = imgLstBoxBan.SelectedIndex;
                if (soBan >= 0)
                {
                    goiMonTaiBanBLLDAL = new GoiMonTaiBanBLLDAL();
                    monBLLDAL = new MonBLLDAL();
                    lbBan.Text = lstBan[soBan].TenBan;
                    List<GoiMonTaiBan> lst = goiMonTaiBanBLLDAL.getGMTBByMaBan(lstBan[soBan].MaBan);
                    //if (lst == null || (lst != null && lst.Count == 0))
                    //{
                    //    return;
                    //}
                    gridControlDSGoiMon.DataSource = (from c in lst
                                                      select new
                                                      {
                                                          MaGoiMon = c.MaGoiMon,
                                                          MaMon = c.MaMon,
                                                          TenMon = monBLLDAL.getTenMonByMaMon(c.MaMon.Value),
                                                          SoLuong = c.SoLuong,
                                                          GhiChu = c.GhiChu
                                                      }).ToList();
                }
                else
                {
                    gridControlDSGoiMon.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Click bàn - Exception: " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInCheBien_Click(object sender, EventArgs e)
        {
            int soBan = imgLstBoxBan.SelectedIndex;
            if (soBan >= 0)
            {
                goiMonTaiBanBLLDAL = new GoiMonTaiBanBLLDAL();
                List<GoiMonTaiBan> lst = goiMonTaiBanBLLDAL.getGMTBByMaBan(lstBan[soBan].MaBan);
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    return;
                }
                List<InCheBien> lstInCheBien = new List<InCheBien>();
                foreach (GoiMonTaiBan g in lst)
                {
                    InCheBien inCheBien = new InCheBien(g.MaMon.Value, monBLLDAL.getTenByMa(g.MaMon.Value), g.SoLuong.Value, g.GhiChu);
                    lstInCheBien.Add(inCheBien);
                }
                frmReportInCheBien frm = new frmReportInCheBien(lstInCheBien, lstBan[soBan].TenBan);
                frm.ShowDialog(this);
            }
        }
        SqlTableDependency<GoiMonTaiBan> dep;
        public void startListenner()
        {
            string stringConnection = Properties.Settings.Default.ChuoiKetNoi;
            var mapper = new ModelToTableMapper<GoiMonTaiBan>();
            mapper.AddMapping(c => c.MaHD, "MaHD");

            dep = new SqlTableDependency<GoiMonTaiBan>(stringConnection, "GoiMonTaiBan", mapper: mapper);
            dep.OnChanged += Dep_OnChanged;
            dep.Start();
        }

        private void Dep_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<GoiMonTaiBan> e)
        {
            try
            {
                if (e.ChangeType == ChangeType.Insert || e.ChangeType == ChangeType.Update)
                {
                    GoiMonTaiBan changedEntity = e.Entity;
                    if (changedEntity.TinhTrang == 0)
                    {
                        refesh();
                    }
                }
            }
            catch
            {

            }
        }
        private void refesh()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                int soBanHT = imgLstBoxBan.SelectedIndex;
                if (soBanHT >= 0)
                {
                    loadBan();
                    imgLstBoxBan.SelectedIndex = soBanHT;
                }
                else
                {
                    loadBan();
                }
            }));
        }
        private void btnGhiNhan_Click_1(object sender, EventArgs e)
        {
            try
            {
                goiMonTaiBanBLLDAL = new GoiMonTaiBanBLLDAL();
                int soBan = imgLstBoxBan.SelectedIndex;
                if (soBan >= 0)
                {
                    string messeage = "Rất tiếc" + Environment.NewLine;
                    
                    if (goiMonTaiBanBLLDAL.ktGhiNhanGMTB(lstBan[soBan].MaBan,ref messeage))
                    {
                        MessageBox.Show(messeage, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }    
                    goiMonTaiBanBLLDAL.ghiNhanGMTB(lstBan[soBan].MaBan);
                    loadBan();
                    UpdateGMTB(0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ghi nhận - Exception: " + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("MaGoiMon") == null)
            {
                return;
            }
            goiMonTaiBanBLLDAL = new GoiMonTaiBanBLLDAL();
            goiMonTaiBanBLLDAL.delete(int.Parse(gridView1.GetFocusedRowCellValue("MaGoiMon").ToString()));
            loadBan();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                btnXoa.Enabled = false;
            }
            else
            {
                btnXoa.Enabled = true;
            }
        }

        private void frmGoiMonTaiBan_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        //
    }
}

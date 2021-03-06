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
    public partial class frmPhanQuyen : Form
    {
        NhomNguoiDungBLLDAL nhomNguoiDungBLLDAL = new NhomNguoiDungBLLDAL();
        PhanQuyenBLLDAL phanQuyenBLLDAL = new PhanQuyenBLLDAL();
        ManHinhBLLDAL manHinhBLLDAL = new ManHinhBLLDAL();
        
        public frmPhanQuyen()
        {
            InitializeComponent();
            this.dtgvNhomNguoiDung.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvNhomNguoiDung.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));
            this.dtgvPhanQuyen.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvPhanQuyen.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));
            //
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            loadDataNhomNguoiDung();
            //loadDataPhanQuyen();
        }
        public void loadDataNhomNguoiDung()
        {
            var nguoiDungs = from nnd in nhomNguoiDungBLLDAL.getDataNhomNguoiDung()
                             select new
                             {
                                 MaNhom = nnd.MaNhom,
                                 TenNhom = nnd.TenNhom,
                                 GhiChu = nnd.GhiChu
                             };
            dtgvNhomNguoiDung.DataSource = nguoiDungs.ToList();
        }
         
        private void loadDataPhanQuyen(int maNhom)
        {
            var phanQuyens = from pq in phanQuyenBLLDAL.getDataPhanQuyen()
                             from mh in manHinhBLLDAL.getDataManHinh()
                             from nnd in nhomNguoiDungBLLDAL.getDataNhomNguoiDung()
                             where pq.MaMH == mh.MaMH && nnd.MaNhom == maNhom
                             select new
                             {
                                 MaMH = pq.MaMH,
                                 TenMH = mh.TenMH,
                                 CoQuyen = pq.CoQuyen.Value
                             };
            dtgvPhanQuyen.DataSource = phanQuyens.ToList();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //dynamic row = dtgvPhanQuyen.CurrentRow.DataBoundItem;
            //if(row == null)
            //{
            //    return;
            //}
            ////int temp;
            //try
            //{
            //    foreach (DataGridViewRow item in dtgvPhanQuyen.Rows)
            //    {

            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            try
            {
                string maNhom = dtgvNhomNguoiDung.CurrentRow.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(maNhom))
                {
                    return;
                }
                foreach (DataGridViewRow row in dtgvPhanQuyen.Rows)
                {
                    string maMH = row.Cells[0].Value.ToString();
                    if (string.IsNullOrEmpty(maMH))
                    {
                        continue;
                    }
                    DataGridViewCheckBoxCell cell = row.Cells[2] as DataGridViewCheckBoxCell;
                    int quyen = 0;
                    if (cell.Value != null)
                    {
                        if (Convert.ToBoolean(cell.Value))
                        {
                            quyen = 1;
                        }
                        else
                        {
                            quyen = 0;
                        }
                    }
                    else
                    {
                        quyen = 0;
                    }
                    phanQuyenBLLDAL.updatePhanQuyen(int.Parse(maNhom), int.Parse(maMH), quyen);
                }
                loadDataPhanQuyenByMaNhom(int.Parse(maNhom));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgvPhanQuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int temp;
            //try
            //{
            //    //DataGridViewCheckBoxCell check = dtgvPhanQuyen.CurrentRow.Cells[2] as DataGridViewCheckBoxCell;

            //    //
            //    if (dtgvPhanQuyen.CurrentRow.Cells[2].Value != null && int.TryParse(dtgvPhanQuyen.CurrentRow.Cells[2].Value.ToString(), out temp))
            //    {
            //        dtgvPhanQuyen.CurrentRow.Cells[2].Value = false;
            //        dtgvPhanQuyen.CurrentRow.Cells[2].Value = null;
            //        MessageBox.Show(dtgvPhanQuyen.CurrentRow.Cells[2].Value.ToString());
            //        //row.CoQuyen = false;
            //    }
            //    else if (dtgvPhanQuyen.CurrentRow.Cells[2].Value == null)
            //    {
            //        dtgvPhanQuyen.CurrentRow.Cells[2].Value = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            

        }

        private void dtgvNhomNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dynamic row = dtgvNhomNguoiDung.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            int maNhom = row.MaNhom;
            //loadDataPhanQuyen(maNhom);
            loadDataPhanQuyenByMaNhom(maNhom);
        }
        public void loadDataPhanQuyenByMaNhom(int maNhom)
        {
            dtgvPhanQuyen.DataSource = null;
            dtgvPhanQuyen.DataSource = phanQuyenBLLDAL.getPhanQuyensByMaNhom(maNhom);
        }
    }
}

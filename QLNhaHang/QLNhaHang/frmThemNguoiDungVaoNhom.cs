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
    public partial class frmThemNguoiDungVaoNhom : Form
    {
        NhomNguoiDungBLLDAL nhomNguoiDungBLLDAL = new NhomNguoiDungBLLDAL();
        NguoiDungBLLDAL nguoiDungBLLDAL = new NguoiDungBLLDAL();
        NguoiDungNhomNguoiDungBLLDAL nguoiDungNhomNguoiDungBLLDAL = new NguoiDungNhomNguoiDungBLLDAL();
        public frmThemNguoiDungVaoNhom()
        {
            InitializeComponent();
            this.dtgvNguoiDung.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvNguoiDung.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));
            this.dtgvNhomNguoiDung.ColumnHeadersDefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Bold));
            this.dtgvNhomNguoiDung.DefaultCellStyle.Font = (new Font("Tahoma", 10, FontStyle.Regular));
        }

        private void frmThemNguoiDungVaoNhom_Load(object sender, EventArgs e)
        {
            loadCbbNhomNguoiDung();
            loadDataNguoiDung();
        }

        private void loadCbbNhomNguoiDung()
        {
            cbbNhomNguoiDung.DataSource = nhomNguoiDungBLLDAL.getDataNhomNguoiDung();
            cbbNhomNguoiDung.ValueMember = "MaNhom";
            cbbNhomNguoiDung.DisplayMember = "TenNhom";
        }

        private void loadDataNguoiDung()
        {
            //mã nd, họ tên, giới tính, sdt, địa chỉ, email, tên đn, mk, hoạt động
            var nguoiDungs = from nd in nguoiDungBLLDAL.getDataNguoiDung()
                             select new
                             {
                                 MaND = nd.MaND,
                                 HoTen = nd.HoTen,
                                 TenDN = nd.TenDN,
                                 MatKhau = nd.MatKhau,
                                 GioiTinh = nd.GioiTinh,
                                 Email = nd.Email,
                                 SDT = nd.SDT,
                                 DiaChi = nd.DiaChi,
                                 HoatDong = nd.HoatDong
                             };
            dtgvNguoiDung.DataSource = nguoiDungs.ToList();
        }

        private void loadDataNhomNguoiDung(int maNhom)
        {
            var nhomNguoiDungs = from ndnnd in nguoiDungNhomNguoiDungBLLDAL.getNhomNguoiDungByMaNhom(maNhom)
                                 select new
                                 {
                                     TenDN = ndnnd.TenDN,
                                     MaNhom = ndnnd.MaNhom,
                                     GhiChu = ndnnd.GhiChu
                                 };
            dtgvNhomNguoiDung.DataSource = nhomNguoiDungs.ToList();
        }
        private void cbbNhomNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maNhom;
            bool result = int.TryParse(cbbNhomNguoiDung.SelectedValue.ToString(), out maNhom);
            try
            {
                loadDataNhomNguoiDung(maNhom);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dynamic row = dtgvNguoiDung.CurrentRow.DataBoundItem;
            if(row == null)
            {
                return;
            }
            try
            {
                int maNhom;
                bool result = int.TryParse(cbbNhomNguoiDung.SelectedValue.ToString(), out maNhom);
                string tenDN = row.TenDN.ToString();
                if (!nguoiDungNhomNguoiDungBLLDAL.kTraTrungMaNhom(maNhom, tenDN))
                {
                    
                    NguoiDungNhomNguoiDung nguoiDungNhomNguoiDung = new NguoiDungNhomNguoiDung();
                    nguoiDungNhomNguoiDung.TenDN = row.TenDN;
                    nguoiDungNhomNguoiDung.MaNhom = maNhom;
                    nguoiDungNhomNguoiDung.GhiChu = "";
                    nguoiDungNhomNguoiDungBLLDAL.insertnguoiDungNhomNguoiDung(nguoiDungNhomNguoiDung);
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDataNhomNguoiDung(maNhom);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, trùng mã nhóm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maNhom;
            bool result = int.TryParse(cbbNhomNguoiDung.SelectedValue.ToString(), out maNhom);
            int demND = nguoiDungNhomNguoiDungBLLDAL.getNhomNguoiDungByMaNhom(maNhom).Count();
            MessageBox.Show(demND.ToString());
            try
            {
                foreach (DataGridViewRow item in dtgvNhomNguoiDung.Rows)
                {
                    if (demND == 0)
                    {
                        return;
                    }    
                    string tenDN = item.Cells[0].Value.ToString();
                    nguoiDungNhomNguoiDungBLLDAL.deleteNguoiDungNhomNguoiDung(tenDN);
                    demND--;
                }
                loadDataNguoiDung();
                loadDataNhomNguoiDung(maNhom);
            }
            catch
            {
                MessageBox.Show("Xóa thất bại", "Thông báo");
            }
            
        }
    }
}

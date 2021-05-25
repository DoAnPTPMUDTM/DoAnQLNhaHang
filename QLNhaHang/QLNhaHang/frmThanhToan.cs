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
    public partial class frmThanhToan : Form
    {
        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        BanBLLDAL banBLLDAL = new BanBLLDAL();
        KhachHangBLLDAL khachHangBLLDAL = new KhachHangBLLDAL();
        private double tongTien, tienGiam, thanhTien;
        private int diemCong, diemTru;
        private int maHD;

        public frmThanhToan()
        {
            InitializeComponent();
        }
        public frmThanhToan(int maHD, double tongTien, double tienGiam, double thanhTien, int diemCong, int diemTru)
        {
            InitializeComponent();
            this.tongTien = tongTien;
            this.tienGiam = tienGiam;
            this.thanhTien = thanhTien;
            this.diemCong = diemCong;
            this.diemTru = diemTru;
            this.maHD = maHD;
            txtTienNhan.Focus();
        }


        public delegate void StatusUpdateHandler(object sender, EventArgs e, int maHD, double tongTien, double tienGiam, double thanhTien, int diemCong, int diemTru, double tienNhan, double tienThua);
        public event StatusUpdateHandler OnUpdateStatus;

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            txtTongTien.Text = String.Format("{0:0,00}", this.tongTien);
            txtTienGiam.Text = String.Format("{0:0,00}", this.tienGiam);
            txtThanhTien.Text = String.Format("{0:0,00}", this.thanhTien);
            HoaDon hd = hoaDonBLLDAL.getHoaDonByMaHD(this.maHD);
            if(hd != null)
            {
                Ban ban = banBLLDAL.getBanByMaBan(hd.MaBan.Value);
                if(ban != null)
                {
                    lbTenBan.Text = ban.TenBan;
                }
            }
        }


        private void txtTienNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtTienNhan_TextChanged(object sender, EventArgs e)
        {

            //int VisibleTime = 1000;  //in milliseconds
            //
            string tienNhan = txtTienNhan.Text;
            if (String.IsNullOrEmpty(tienNhan))
            {
                lbTienNhan.Text = "";
            }
            else
            {
                double tn = double.Parse(txtTienNhan.Text);
                double tt = tn - this.thanhTien;
                if(tn > 999)
                {
                    lbTienNhan.Text = String.Format("{0:0,00}", tn);
                }
                if (tt > 0)
                {
                    txtTienThua.Text = String.Format("{0:0,00}", tt);
                }
                else
                {
                    txtTienThua.Text = "0";
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
           // UpdateStatus();
        }

        //When button is clicked, this is trigged
        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    //In here, you now trigger your custom event

        //}


        private void UpdateStatus(int maHD, double tongTien, double tienGiam, double thanhTien, int diemCong, int diemTru, double tienNhan, double tienThua)
        {
            //Create arguments.  You should also have custom one, or else return EventArgs.Empty();
            EventArgs args = new EventArgs();

            //Call any listeners
            OnUpdateStatus?.Invoke(this, args, maHD,  tongTien,  tienGiam,  thanhTien,  diemCong,  diemTru,  tienNhan,  tienThua);

        }

        private void btnThanhToanVaInHoaDon_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTienNhan.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            double tn = double.Parse(txtTienNhan.Text);
            double tt = tn - this.thanhTien;
            if(tt < 0)
            {
                MessageBox.Show("Tiền nhận phải lớn hơn hoặc bằng thành tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            //banBLLDAL.capNhatTTDongBan(hd.MaBan.Value);                       
            UpdateStatus(this.maHD, this.tongTien, this.tienGiam,this.thanhTien, this.diemCong,this.diemTru, tn,tt);
            this.Close();
        }
    }
}

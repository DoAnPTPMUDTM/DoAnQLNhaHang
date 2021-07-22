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
using DevExpress.XtraCharts;
using QLNhaHang.Classes;

namespace QLNhaHang
{
    public partial class frmDoanhThu : Form
    {
        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        public frmDoanhThu()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtpkNgayBD.Value.Date > dtpkNgayKT.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtpkNgayBD.Value.Date > DateTime.Now.Date || dtpkNgayKT.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày phải nhỏ hơn hoặc bằng ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbbLoaiBaoCao.SelectedIndex < 0)
            {
                return;
            }
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            Series series = chartControl1.GetSeriesByName("Series 1");
            if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("1"))
            {
                diagram.AxisX.Title.Text = "Ngày";
                diagram.AxisY.Title.Text = "Doanh Thu (VNĐ)";
                if (hoaDonBLLDAL.getTKTheoNgay(dtpkNgayBD.Value, dtpkNgayKT.Value) == null ||(hoaDonBLLDAL.getTKTheoNgay(dtpkNgayBD.Value, dtpkNgayKT.Value) != null && hoaDonBLLDAL.getTKTheoNgay(dtpkNgayBD.Value, dtpkNgayKT.Value).Count == 0))
                {
                    MessageBox.Show("Không tìm thấy dữ liệu thống kê trong thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    series.DataSource = null;
                    gridControl1.DataSource = null;
                    txtTongDoanhThu.Text = "0";
                    txtTongTienGiam.Text = "0";
                    return;
                }
                List<Chart> data = hoaDonBLLDAL.getTKTheoNgay(dtpkNgayBD.Value, dtpkNgayKT.Value);                              
                series.DataSource = data;
                series.ArgumentDataMember = "X";
                series.ValueScaleType = ScaleType.Numerical;
                series.ValueDataMembers.AddRange(new string[] { "Y" });
                //
                txtTongTienGiam.Text = "0";
                txtTongDoanhThu.Text = string.Format("{0:0,0}", data.Sum(s => s.Y));
                //
                gridColumn1.Caption = "Ngày";
                gridColumn1.FieldName = "X";
                gridColumn2.Caption = "Số hoá đơn";
                gridColumn2.FieldName = "SL";
                gridColumn3.Caption = "Doanh thu";
                gridColumn3.FieldName = "Y";
                gridControl1.DataSource = data;
            }
            else if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("2"))
            {
                diagram.AxisX.Title.Text = "Tháng";
                diagram.AxisY.Title.Text = "Doanh Thu (VNĐ)";
                if (hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value) == null || (hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value) != null && hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value).Count == 0))//
                {
                    MessageBox.Show("Không tìm thấy dữ liệu thống kê trong thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    series.DataSource = null;
                    gridControl1.DataSource = null;
                    txtTongDoanhThu.Text = "0";
                    txtTongTienGiam.Text = "0";
                    return;
                }
                List<Chart> data = hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value);
                series.DataSource = data;
                series.ArgumentDataMember = "X";
                series.ValueScaleType = ScaleType.Numerical;
                series.ValueDataMembers.AddRange(new string[] { "Y" });
                series.Label.Font = new Font("Tahoma", 12.0f);
                //
                txtTongTienGiam.Text = "0";
                txtTongDoanhThu.Text = String.Format("{0:0,0}", data.Sum(s => s.Y));
                //
                gridColumn1.Caption = "Tháng";
                gridColumn1.FieldName = "X";
                gridColumn2.Caption = "Số hoá đơn";
                gridColumn2.FieldName = "SL";
                gridColumn3.Caption = "Doanh thu";
                gridColumn3.FieldName = "Y";
                gridControl1.DataSource = data;

            }
            else if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("3"))
            {
                diagram.AxisX.Title.Text = "Món";
                diagram.AxisY.Title.Text = "Doanh Thu (VNĐ)";
                if (monBLLDAL.getTKMonTheo(dtpkNgayBD.Value, dtpkNgayKT.Value) == null || (monBLLDAL.getTKMonTheo(dtpkNgayBD.Value, dtpkNgayKT.Value) != null && monBLLDAL.getTKMonTheo(dtpkNgayBD.Value, dtpkNgayKT.Value).Count == 0))
                {
                    MessageBox.Show("Không tìm thấy dữ liệu thống kê trong thời gian này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    series.DataSource = null;
                    gridControl1.DataSource = null;
                    txtTongDoanhThu.Text = "0";
                    txtTongTienGiam.Text = "0";
                    return;
                }
                List<Chart> data = monBLLDAL.getTKMonTheo(dtpkNgayBD.Value, dtpkNgayKT.Value);
                
                series.DataSource = data;
                series.ArgumentDataMember = "X";
                series.ValueScaleType = ScaleType.Numerical;
                series.ValueDataMembers.AddRange(new string[] { "Y" });
                //
                double tongGiam = hoaDonBLLDAL.tinhTongGiam(dtpkNgayBD.Value, dtpkNgayKT.Value);
                txtTongTienGiam.Text = String.Format("{0:0,0}", tongGiam);
                txtTongDoanhThu.Text = String.Format("{0:0,0}", data.Sum(s => s.Y) - tongGiam);
                //
                gridColumn1.Caption = "Món";
                gridColumn1.FieldName = "X";
                gridColumn2.Caption = "Số lượng bán";
                gridColumn2.FieldName = "SL";
                gridColumn3.Caption = "Doanh thu";
                gridColumn3.FieldName = "Y";
                gridControl1.DataSource = data;
            }


                //series.ValueScaleType = ScaleType.Numerical;
                //series.ArgumentDataMember = "X";

                //MessageBox.Show(hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value).Count + "");
                //int dayBD = dtpkNgayBD.Value.Date.Day;
                //if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("1"))
                //{

                //}
                //if (dtpkNgayBD.Value.Date > dtpkNgayKT.Value.Date)
                //{
                //    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //if(cbbLoaiBaoCao.SelectedIndex < 0)
                //{
                //    return;
                //}
                //var data = 

            }
        public void loadCbbLoaiBC()
        {
            cbbLoaiBaoCao.DataSource = BaoCao.getLoaiBaoCao();
            cbbLoaiBaoCao.DisplayMember = "TenBaoCao";
            cbbLoaiBaoCao.ValueMember = "MaBaoCao";
        }

        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
            loadCbbLoaiBC();
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            // Customize the appearance of the X-axis title.
            diagram.AxisX.Title.TextColor = Color.Red;
            diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            // Customize the appearance of the Y-axis title.
            diagram.AxisY.Title.TextColor = Color.Blue;
            diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
        }

        private void cbbLoaiBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dtpkNgayBD.Value.Date > dtpkNgayKT.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtpkNgayBD.Value.Date > DateTime.Now.Date || dtpkNgayKT.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày phải nhỏ hơn hoặc bằng ngày hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbbLoaiBaoCao.SelectedIndex < 0)
            {
                return;
            }
            ExcelExport excel = new ExcelExport();
            SaveFileDialog saveFile = new SaveFileDialog();
            if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("1"))
            {
                if (hoaDonBLLDAL.getTKTheoNgay(dtpkNgayBD.Value, dtpkNgayKT.Value).Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất");
                    return;
                }
                List<Chart> data = hoaDonBLLDAL.getTKTheoNgay(dtpkNgayBD.Value, dtpkNgayKT.Value);
                string tong = data.Sum(s => s.Y).ToString();
                string tieuDe = "Thống Kê Báo Cáo Doanh Thu Theo Ngày";
                string ngayBD = dtpkNgayBD.Value.Date.ToString("dd/MM/yyyy");
                string ngayKT = dtpkNgayKT.Value.Date.ToString("dd/MM/yyyy");
                string path = string.Empty;
                excel.ExportThongKeThang(data, ref path, false, tong, ngayBD, ngayKT, tieuDe, "Ngày", "Số Hoá Đơn", "Doanh Thu");
                if (!string.IsNullOrEmpty(path) && MessageBox.Show("Bạn có muốn mở file không?", "Thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(path);
                }

            }
            else if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("2"))
            {
                if (hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value).Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất");
                    return;
                }
                List<Chart> data = hoaDonBLLDAL.getTKTheoThang(dtpkNgayBD.Value, dtpkNgayKT.Value);
                string tong = data.Sum(s => s.Y).ToString();
                string tieuDe = "Thống Kê Báo Cáo Doanh Thu Theo Tháng";
                string ngayBD = dtpkNgayBD.Value.Date.ToString("dd/MM/yyyy");
                string ngayKT = dtpkNgayKT.Value.Date.ToString("dd/MM/yyyy");
                string path = string.Empty;
                excel.ExportThongKeThang(data, ref path, false, tong, ngayBD, ngayKT, tieuDe,"Tháng","Số Hoá Đơn","Doanh Thu");
                if (!string.IsNullOrEmpty(path) && MessageBox.Show("Bạn có muốn mở file không?", "Thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
            else if (cbbLoaiBaoCao.SelectedValue.ToString().Equals("3"))
            {
                if (monBLLDAL.getTKMonTheo(dtpkNgayBD.Value, dtpkNgayKT.Value).Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất");
                    return;
                }
                double tongGiam = hoaDonBLLDAL.tinhTongGiam(dtpkNgayBD.Value, dtpkNgayKT.Value);
                List<Chart> data = monBLLDAL.getTKMonTheo(dtpkNgayBD.Value, dtpkNgayKT.Value);
                double tong = data.Sum(s => s.Y);
                double tongCong = tong - tongGiam;               
                string ngayBD = dtpkNgayBD.Value.Date.ToString("dd/MM/yyyy");
                string ngayKT = dtpkNgayKT.Value.Date.ToString("dd/MM/yyyy");
                string path = string.Empty;
                excel.ExportThongKeMon(data, ref path, false, tong.ToString(), tongGiam.ToString(), tongCong.ToString(), ngayBD, ngayKT);
                if (!string.IsNullOrEmpty(path) && MessageBox.Show("Bạn có muốn mở file không?", "Thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
            
        }
        //public static List<DateTime> GetDates(int year, int month)
        //{
        //    return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
        //                     .Select(day => new DateTime(year, month, day)) // Map each day to a date
        //                     .ToList(); // Load dates into a list
        //}
    }
}

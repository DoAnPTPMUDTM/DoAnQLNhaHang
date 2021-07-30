using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class HoaDonBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public HoaDonBLLDAL()
        {

        }

        public List<Chart> getTKTheoThang(DateTime ngayBD, DateTime ngayKT)
        {

            List<Chart> lstChart = new List<Chart>();
            var data = db.HoaDons.Where(h => h.Ngay.Value.Date >= ngayBD.Date && h.Ngay.Value.Date <= ngayKT.Date && h.TinhTrang == 1).GroupBy(x => new { x.Ngay.Value.Month, x.Ngay.Value.Year }).Select(x => new
            {
                X = "Tháng " + x.Key.Month + "/" + x.Key.Year,
                Y = x.Sum(s => s.ThanhTien),
                SL = x.Count()
            });
            foreach (var item in data)
            {
                Chart chart = new Chart((double)item.Y, item.X.ToString(), item.SL);
                lstChart.Add(chart);
            }
            return lstChart;

        }
        public List<Chart> getTKTheoNgay(DateTime ngayBD, DateTime ngayKT)
        {

            List<Chart> lstChart = new List<Chart>();
            var data = db.HoaDons.Where(h => h.Ngay.Value.Date >= ngayBD.Date && h.Ngay.Value.Date <= ngayKT.Date && h.TinhTrang == 1).GroupBy(x => new { x.Ngay.Value.Day, x.Ngay.Value.Month, x.Ngay.Value.Year }).Select(x => new
            {
                X = x.Key.Day + "/" + x.Key.Month + "/" + x.Key.Year,
                Y = x.Sum(s => s.ThanhTien),
                SL = x.Count()
            }).ToList();
            foreach (var item in data)
            {
                Chart chart = new Chart((double)item.Y.Value, item.X.ToString(), item.SL);
                lstChart.Add(chart);
            }
            return lstChart;

        }
        public DataTable getTKTheoThang2(DateTime ngayBD, DateTime ngayKT)
        {
            var data = db.HoaDons.Where(h => h.Ngay >= ngayBD.Date && h.Ngay <= ngayKT.Date).GroupBy(x => new { x.Ngay.Value.Month, x.Ngay.Value.Year }).Select(x => new
            {
                X = x.Key.Month + "/" + x.Key.Year,
                Y = x.Sum(s => s.ThanhTien)
            });
            DataTable table = new DataTable("Table1");
            table.Columns.Add("Argument", typeof(string));
            table.Columns.Add("Value", typeof(double));
            DataRow row = null;
            //foreach (var item in data)
            //{
            //    row = table.NewRow();
            //    row["Argument"] = item.X.ToString();
            //    row["Value"] = item.Y;
            //    table.Rows.Add(row);
            //}
            for (int i = 1; i <= 5; i++)
            {
                row = table.NewRow();
                row["Argument"] = "a" + i;
                row["Value"] = i;
                table.Rows.Add(row);
            }
            return table;
        }
        public List<HoaDon> getDataHoaDon()
        {
            return db.HoaDons.ToList<HoaDon>();
        }
        //
        public List<HoaDon> getDataHoaDonTT()
        {
            return db.HoaDons.Where(t => t.TinhTrang == 1).OrderByDescending(h => h.MaHD).ToList();
        }
        public void themHoaDon(HoaDon hoaDon)
        {
            db.HoaDons.InsertOnSubmit(hoaDon);
            db.SubmitChanges();
        }

        public HoaDon getHoaDonByMaBanCoKhach(int maBan)
        {
            return db.HoaDons.Where(h => h.MaBan == maBan && h.TinhTrang == 0).FirstOrDefault();
        }
        public void updateTTKH(int maHD, int? maKH)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd != null)
            {
                hd.MaKH = maKH;
                db.SubmitChanges();
            }
        }
        public void clearTTKH(int maHD)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd != null)
            {
                if (hd.MaKH != null)
                {
                    hd.MaKH = null;
                    db.SubmitChanges();
                }
            }
        }
        public void thanhToan(int maHD, double tongTien, double tienGiam, double thanhTien)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd != null && hd.TinhTrang == 0)
            {
                hd.TongTien = (decimal)tongTien;
                hd.TienGiam = (decimal)tienGiam;
                hd.ThanhTien = (decimal)thanhTien;
                hd.TinhTrang = 1;
                db.SubmitChanges();
            }

        }
        public HoaDon getHoaDonByMaHD(int maHD)
        {
            return db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
        }

        public void updateKHVL(int maHD)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd != null)
            {
                if (hd.MaKH != 0)
                {
                    hd.MaKH = 0;
                    db.SubmitChanges();
                }
            }
        }

        public void deleteHDByMaHD(int maHD)
        {
            bool checkHDGMTB = db.GoiMonTaiBans.Where(g => g.MaHD == maHD).Count() > 0;
            if (checkHDGMTB)
            {
                db.GoiMonTaiBans.Where(g => g.MaHD == maHD).ToList().ForEach(g => g.MaHD = maHD);
                db.SubmitChanges();
            }
            else
            {
                HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
                if (hd != null)
                {
                    db.HoaDons.DeleteOnSubmit(hd);
                    db.SubmitChanges();
                }
            }
            
        }

        public void updateMaBanDoiBan(int maHD, int maBan)
        {
            HoaDon hoaDon = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hoaDon != null)
            {
                hoaDon.MaBan = maBan;
                db.SubmitChanges();
            }

        }
        public double tinhTongGiam(DateTime ngayBD, DateTime ngayKT)
        {
            decimal? tong = db.HoaDons.Where(h => h.Ngay.Value.Date >= ngayBD.Date && h.Ngay.Value.Date <= ngayKT.Date).Sum(s => s.TienGiam);
            if (tong.HasValue)
            {
                return (double)tong;
            }
            return 0;
        }
        public bool ktTinhTrangHD(int maHD)
        {
            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd == null)
                return false;
            return true;
        }
        public int getMaBanByMaHD(int maHD)
        {

            HoaDon hd = db.HoaDons.Where(h => h.MaHD == maHD).FirstOrDefault();
            if (hd == null)
            {
                return -1;
            }
            return hd.MaBan.Value;

        }
        public int[][] getDataSet()
        {
            List<int[]> data = new List<int[]>();
            foreach (HoaDon hd in db.HoaDons.ToList())
            {
                List<int> lst = db.CTHDs.Where(c => c.MaHD == hd.MaHD && hd.TinhTrang == 1).Select(c => c.MaMon).ToList();
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    continue;
                }
                data.Add(lst.OrderBy(c => c).ToArray());
            }
            return data.ToArray();
        }
        public int[][] getDataSeNewt(int[] newData)
        {
            List<int[]> data = new List<int[]>();
            foreach (HoaDon hd in db.HoaDons.ToList())
            {
                List<int> lst = db.CTHDs.Where(c => c.MaHD == hd.MaHD && hd.TinhTrang == 1).Select(c => c.MaMon).ToList();
                if (lst == null || (lst != null && lst.Count == 0))
                {
                    continue;
                }
                data.Add(lst.OrderBy(c => c).ToArray());
            }
            data.Add(newData);
            return data.ToArray();
        }

        //public List<Chart> getTKTheoNgay(DateTime ngayBD, DateTime ngayKT)
        //{
        //    List<Chart> lstChart = new List<Chart>();
        //    var data = db.HoaDons.Where(h => h.Ngay >= ngayBD.Date && h.Ngay <= ngayKT.Date).GroupBy(x => new {x.Ngay.Value.Day,  x.Ngay.Value.Month, x.Ngay.Value.Year }).Select(x => new
        //    {
        //        X = x.Key.Day + "/" + x.Key.Month + "/" + x.Key.Year,
        //        Y = x.Sum(s => s.ThanhTien)
        //    });
        //    foreach (var item in data)
        //    {
        //        Chart chart = new Chart((double)item.Y, item.X);
        //        lstChart.Add(chart);
        //    }
        //    return lstChart;
        //}
    }
}

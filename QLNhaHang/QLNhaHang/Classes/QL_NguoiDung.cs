using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang.Classes
{
   public class QL_NguoiDung
    {
        public QL_NguoiDung()
        {

        }
        public int checkConfig()
        {
            if (Properties.Settings.Default.ChuoiKetNoi == string.Empty)
            {
                return 1; //Chuỗi cấu hình rỗng, không tồn tại
            }
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ChuoiKetNoi);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return 0;// Kết nối thành công chuỗi cấu hình hợp lệ
            }
            catch
            {
                return 2;//Chuỗi cấu hình không hợp lệ
            }
        }

        public int checkUser(string userName, string password)
        {
            string query = "select * from NguoiDung where TenDN = '" + userName + "' and MatKhau = '" + password + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, Properties.Settings.Default.ChuoiKetNoi);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return 0;//Tên đăng nhập, mật khẩu không chính xác hoặc không tồn tại tên đăng nhập này
            }
            else if (dt.Rows[0][8] == null || dt.Rows[0][8].ToString() == "False")
            {
                return 1;// Tài khoản bị khóa
            }
            return 2;// Đăng nhập thành công
        }

        //

        public DataTable getServerName()
        {
            DataTable dt = new DataTable();
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            return dt;
        }
        public DataTable getDbName(string serverName, string id, string pass)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select name from sys.Databases", "Data source = " + serverName + ";Initial Catalog = master;" + "User ID = " + id + ";pwd = " + pass);
            da.Fill(dt);
            return dt;
        }
        public void saveConfig(string serverName, string databaseName, string id, string pass)
        {
            Properties.Settings.Default.ChuoiKetNoi = "Data source = " + serverName + ";Initial Catalog = " + databaseName + ";User ID = " + id + ";pwd = " + pass;
            Properties.Settings.Default.Save();
        }

    }
}

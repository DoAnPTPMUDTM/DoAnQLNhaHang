using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class StringConnection
    {
        public void saveStringConnection(string strConn)
        {
            Properties.Settings.Default.ChuoiKetNoi = strConn;
            Properties.Settings.Default.Save();
        }
        public static string getStringConnection()
        {
            return Properties.Settings.Default.ChuoiKetNoi;
        }
    }
}

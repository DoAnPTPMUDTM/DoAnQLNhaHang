using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class Chart
    {
        public double Y { get; set; }
        public string X { get; set; }
        public int  SL { get; set; }
        public string STT { get; set; }
        public int Ma { get; set; }
        public Chart()
        {

        }

        public Chart(double y, string x)
        {
            Y = y;
            X = x;
        }

        public Chart(double y, string x, int sL)
        {
            Y = y;
            X = x;
            SL = sL;
        }

        public Chart(double y, string x, int sL, int ma)
        {
            Y = y;
            X = x;
            SL = sL;
            Ma = ma;
        }
    }
}

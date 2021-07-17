using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class CTPNBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext();
        public CTPNBLLDAL()
        {

        }
        public List<CTPN> getCTPNByMaPN(int maPN)
        {
            return db.CTPNs.Where(t => t.MaPN == maPN).ToList();
        }
        public void insertCTPN(CTPN cTPN)
        {
            if(cTPN != null)
            {
                db.CTPNs.InsertOnSubmit(cTPN);
                db.SubmitChanges();
            }
        }
    }
}

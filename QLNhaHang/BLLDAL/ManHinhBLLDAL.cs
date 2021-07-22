using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLDAL
{
    public class ManHinhBLLDAL
    {
        QuanLyNhaHangDataContext db = new QuanLyNhaHangDataContext(StringConnection.getStringConnection());
        public ManHinhBLLDAL()
        {

        }
        public List<ManHinh> getDataManHinh()
        {
            return db.ManHinhs.ToList<ManHinh>();
        }
        public void insertManHinh(ManHinh mh)
        {
            if(mh != null)
            {
                db.ManHinhs.InsertOnSubmit(mh);
                db.SubmitChanges();
            }
        }
        public void updateManHinh(int maMH, string tenMH)
        {
            ManHinh mh = db.ManHinhs.Where(t => t.MaMH == maMH).FirstOrDefault();
            if(mh != null)
            {
                mh.TenMH = tenMH;
                db.SubmitChanges();
            }
        }
        public void deleteManHinh(int maMH)
        {
            ManHinh mh = db.ManHinhs.Where(t => t.MaMH == maMH).FirstOrDefault();
            if(mh != null)
            {
                db.ManHinhs.DeleteOnSubmit(mh);
                db.SubmitChanges();
            }
        }
    }
}

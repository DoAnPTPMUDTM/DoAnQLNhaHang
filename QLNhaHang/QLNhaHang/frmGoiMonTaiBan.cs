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
    public partial class frmGoiMonTaiBan : Form
    {
        public frmGoiMonTaiBan()
        {
            InitializeComponent();
        }

        private void btnHuyBoFrmGoiMonTaiBan_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnGhiNhan_Click(object sender, EventArgs e)
        {
            UpdateStatus();
        }
        //

        public delegate void StatusUpdateHandler(object sender, EventArgs e);
        public event StatusUpdateHandler OnUpdateStatus;

        //When button is clicked, this is trigged
        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    //In here, you now trigger your custom event
            
        //}


        private void UpdateStatus()
        {
            //Create arguments.  You should also have custom one, or else return EventArgs.Empty();
            EventArgs args = new EventArgs();

            //Call any listeners
            OnUpdateStatus?.Invoke(this, args);

        }

        private void frmGoiMonTaiBan_Load(object sender, EventArgs e)
        {
            KhachHangBLLDAL k = new KhachHangBLLDAL();
            searchLookUpEdit1.Properties.DataSource = k.getDataKhachHang();
            searchLookUpEdit1.Properties.DisplayMember = "TenKH";
            searchLookUpEdit1.Properties.ValueMember = "MaKH";
            searchLookUpEdit1.EditValue = 0;
        }

        //
    }
}

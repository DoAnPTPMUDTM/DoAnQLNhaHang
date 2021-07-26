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
    public partial class frmDoiBan : Form
    {
        BanBLLDAL banBLLDAL = new BanBLLDAL();
        List<Ban> lstBan;
        private int maBan;
        int soBan = -1;
        public frmDoiBan()
        {
            InitializeComponent();
        }
        public frmDoiBan(int maBan)
        {
            InitializeComponent();
            this.maBan = maBan;
        }

        public delegate void StatusUpdateHandler(object sender, EventArgs e,int maBanCu, int maBanMoi, int soBan);
        public event StatusUpdateHandler OnUpdateStatusDoiBan;
        private void UpdateStatusDoiBan(int maBanCu, int maBanMoi, int soBan)
        {
            //Create arguments.  You should also have custom one, or else return EventArgs.Empty();
            EventArgs args = new EventArgs();

            //Call any listeners
            OnUpdateStatusDoiBan?.Invoke(this, args,maBanCu, maBanMoi, soBan);

        }
        public void loadBan()
        {
            lstBan = banBLLDAL.getDataBan();
            imgLstBoxDoiBan.Items.Clear();
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64);
            this.imgLstBoxDoiBan.ImageList = imageList;
            int i = 0;
            foreach (Ban ban in lstBan)
            {
                if (ban.TrangThai == 0)
                {
                    imageList.Images.Add(Properties.Resources.bantrong);
                }
                else
                {
                    imageList.Images.Add(Properties.Resources.bancokhach);
                }
                this.imgLstBoxDoiBan.Items.Add(ban.TenBan, i);
                i++;
            }
            this.imgLstBoxDoiBan.ColumnWidth = 130;
        }

        private void frmDoiBan_Load(object sender, EventArgs e)
        {
            loadBan();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            soBan = imgLstBoxDoiBan.SelectedIndex;
            if (soBan >= 0)
            {
                //lstBan[soBan].TenBan;
                if(lstBan[soBan].MaBan == this.maBan)
                {
                    MessageBox.Show("Hiện tại bạn đang ở " + lstBan[soBan].TenBan, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int trangThai = banBLLDAL.ktTinhTrangBan(lstBan[soBan].MaBan);
                if (trangThai == 0)
                {
                    Ban ban = banBLLDAL.getBanByMaBan(this.maBan);
                    if(ban == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin bàn hiện tại!");
                        return;
                    }
                    DialogResult res = MessageBox.Show("Bạn có muốn chuyển từ " + ban.TenBan  + " sang " + lstBan[soBan].TenBan + Environment.NewLine + "Toàn bộ hoá đơn " + ban.TenBan + " sẽ được chuyển sang " + lstBan[soBan].TenBan, "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        UpdateStatusDoiBan(this.maBan, lstBan[soBan].MaBan, soBan);
                        this.Close();
                    }
                    if (res == DialogResult.No)
                    {
                        return;
                    }
                }
                if (trangThai == 1)
                {
                    MessageBox.Show("Bàn này đã có khách" + Environment.NewLine + "Vui lòng chọn bàn trống để đổi(chuyển) bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

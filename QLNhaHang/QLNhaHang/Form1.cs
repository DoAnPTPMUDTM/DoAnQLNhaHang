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
using QLNhaHang.Classes;
namespace QLNhaHang
{
    public partial class Form1 : Form
    {
        MonBLLDAL monBLLDAL = new MonBLLDAL();
        ConfigImage configImage = new ConfigImage();
        NhomMonBLLDAL nhomMonBLLDAL = new NhomMonBLLDAL();
        public Form1()
        {
            InitializeComponent();
            //
            //pictureEdit1.Image = Image.FromFile(configImage.GetProjectLinkDirectory() + configImage.imgAnhMon + monBLLDAL.getDataMon()[2].Anh);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.DisplayMember = "TenMon";
            lookUpEdit1.Properties.ValueMember = "MaMon";
            lookUpEdit1.Properties.DataSource = nhomMonBLLDAL.getDataNhomMon();
        }
    }
}

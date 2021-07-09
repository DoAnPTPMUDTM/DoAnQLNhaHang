using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaHang
{
    public partial class frmSaoLuu : Form
    {
        public frmSaoLuu()
        {
            InitializeComponent();
        }

        private void frmSaoLuu_Load(object sender, EventArgs e)
        {
            //string stringConnection = Properties.Settings.Default.ChuoiKetNoi;
            string stringConnection = "";
            if (String.IsNullOrEmpty(stringConnection))
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSaoLuu.Enabled = false;
                return;
            }
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ChuoiKetNoi);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                conn.Close();
                btnSaoLuu.Enabled = true;
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(stringConnection);
                string server = builder.DataSource;
                string databaseName = builder.InitialCatalog;
                string userName = builder.UserID;
                string password = builder.Password;
                //
                txtDatabaseName.Text = databaseName;
                txtUserName.Text = userName;
                txtServerName.Text = server;
            }
            catch
            {
                btnSaoLuu.Enabled = false;
                MessageBox.Show("Chuỗi kết nối không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaoLuu_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(@"Data Source=.;Initial Catalog=QuanLyNhaHang;User ID=sa;Password=123");
            string server = builder.DataSource;
            string databaseName = builder.InitialCatalog;
            string userName = builder.UserID;
            string password = builder.Password;
            //
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "BackupDatabase" + databaseName + "_" + DateTime.Now.ToString("dd-MM-yy_hh-mm-ss-tt");
            sfd.Filter = "Database backup file (*.bak)|*.bak";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString();
                try
                {
                    progressBarSaoLuu.Visible = true;
                    lbPercent.Visible = true;
                    lbStatus.Visible = true;
                    progressBarSaoLuu.Position = 0;
                    progressBarSaoLuu.Properties.PercentView = true;
                    Server dbServer = new Server(new ServerConnection(server, userName, password));
                    Backup dbBackup = new Backup()
                    {
                        Action = BackupActionType.Database,
                        Database = databaseName
                    };
                    BackupDeviceItem deviceItem = new BackupDeviceItem(localFilePath, DeviceType.File);
                    dbBackup.Devices.Add(deviceItem);
                    dbBackup.Incremental = false;
                    dbBackup.Initialize = true;
                    dbBackup.Checksum = true;
                    dbBackup.ContinueAfterError = true;
                    dbBackup.PercentComplete += DbBackup_PercentComplete;
                    dbBackup.Complete += DbBackup_Complete;
                    dbBackup.SqlBackupAsync(dbServer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DbBackup_Complete(object sender, ServerMessageEventArgs e)
        {
            if (e.Error != null)
            {
                lbStatus.Invoke((MethodInvoker)delegate
                {
                    lbStatus.Text = e.Error.Message;
                });
            }
        }

        private void DbBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            progressBarSaoLuu.Invoke((MethodInvoker)delegate
            {
                progressBarSaoLuu.Position = e.Percent;
                progressBarSaoLuu.Update();
            });
            lbStatus.Invoke((MethodInvoker)delegate
            {
                lbPercent.Text = e.Percent + "%";
            });
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

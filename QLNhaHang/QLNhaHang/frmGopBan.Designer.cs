
namespace QLNhaHang
{
    partial class frmGopBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgLstBoxGopBan = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgLstBoxGopBan)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLstBoxGopBan
            // 
            this.imgLstBoxGopBan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLstBoxGopBan.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgLstBoxGopBan.Appearance.Options.UseFont = true;
            this.imgLstBoxGopBan.ItemAutoHeight = true;
            this.imgLstBoxGopBan.ItemPadding = new System.Windows.Forms.Padding(3);
            this.imgLstBoxGopBan.Location = new System.Drawing.Point(8, 32);
            this.imgLstBoxGopBan.MultiColumn = true;
            this.imgLstBoxGopBan.Name = "imgLstBoxGopBan";
            this.imgLstBoxGopBan.Size = new System.Drawing.Size(953, 462);
            this.imgLstBoxGopBan.TabIndex = 1;
            // 
            // btnThucHien
            // 
            this.btnThucHien.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThucHien.Appearance.Options.UseFont = true;
            this.btnThucHien.Location = new System.Drawing.Point(342, 500);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(116, 42);
            this.btnThucHien.TabIndex = 2;
            this.btnThucHien.Text = "Thực Hiện";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyBo.Appearance.Options.UseFont = true;
            this.btnHuyBo.Location = new System.Drawing.Point(474, 500);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(110, 42);
            this.btnHuyBo.TabIndex = 2;
            this.btnHuyBo.Text = "Huỷ Bỏ";
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(961, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Gộp Bàn";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmGopBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuyBo);
            this.Controls.Add(this.btnThucHien);
            this.Controls.Add(this.imgLstBoxGopBan);
            this.Name = "frmGopBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGopBan";
            this.Load += new System.EventHandler(this.frmGopBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLstBoxGopBan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ImageListBoxControl imgLstBoxGopBan;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private System.Windows.Forms.Label label1;
    }
}
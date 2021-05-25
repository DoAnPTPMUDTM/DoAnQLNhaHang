
namespace QLNhaHang
{
    partial class frmDoiBan
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
            this.imgLstBoxDoiBan = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgLstBoxDoiBan)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLstBoxDoiBan
            // 
            this.imgLstBoxDoiBan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLstBoxDoiBan.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgLstBoxDoiBan.Appearance.Options.UseFont = true;
            this.imgLstBoxDoiBan.ItemAutoHeight = true;
            this.imgLstBoxDoiBan.ItemPadding = new System.Windows.Forms.Padding(3);
            this.imgLstBoxDoiBan.Location = new System.Drawing.Point(8, 32);
            this.imgLstBoxDoiBan.MultiColumn = true;
            this.imgLstBoxDoiBan.Name = "imgLstBoxDoiBan";
            this.imgLstBoxDoiBan.Size = new System.Drawing.Size(953, 462);
            this.imgLstBoxDoiBan.TabIndex = 1;
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
            this.label1.Text = "Đổi Bàn(Chuyển Bàn)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDoiBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 550);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuyBo);
            this.Controls.Add(this.btnThucHien);
            this.Controls.Add(this.imgLstBoxDoiBan);
            this.Name = "frmDoiBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDoiBan";
            this.Load += new System.EventHandler(this.frmDoiBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLstBoxDoiBan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ImageListBoxControl imgLstBoxDoiBan;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private System.Windows.Forms.Label label1;
    }
}
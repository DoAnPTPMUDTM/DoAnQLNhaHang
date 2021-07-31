using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPI.Models
{
    public partial class QuanLyNhaHangContext : DbContext
    {
        public QuanLyNhaHangContext()
        {
        }

        public QuanLyNhaHangContext(DbContextOptions<QuanLyNhaHangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ban> Bans { get; set; }
        public virtual DbSet<Cthd> Cthds { get; set; }
        public virtual DbSet<Ctpn> Ctpns { get; set; }
        public virtual DbSet<DinhLuong> DinhLuongs { get; set; }
        public virtual DbSet<DonViTinh> DonViTinhs { get; set; }
        public virtual DbSet<GoiMonTaiBan> GoiMonTaiBans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<LoaiMatHang> LoaiMatHangs { get; set; }
        public virtual DbSet<ManHinh> ManHinhs { get; set; }
        public virtual DbSet<MatHang> MatHangs { get; set; }
        public virtual DbSet<Mon> Mons { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NguoiDungNhomNguoiDung> NguoiDungNhomNguoiDungs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhomMon> NhomMons { get; set; }
        public virtual DbSet<NhomNguoiDung> NhomNguoiDungs { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=QuanLyNhaHang;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ban>(entity =>
            {
                entity.HasKey(e => e.MaBan);

                entity.ToTable("Ban");

                entity.Property(e => e.TenBan).HasMaxLength(50);

                entity.Property(e => e.ViTri).HasMaxLength(50);
            });

            modelBuilder.Entity<Cthd>(entity =>
            {
                entity.HasKey(e => new { e.MaHd, e.MaMon });

                entity.ToTable("CTHD");

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTHD_HD");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTHD_Mon");
            });

            modelBuilder.Entity<Ctpn>(entity =>
            {
                entity.HasKey(e => new { e.MaPn, e.MaMh });

                entity.ToTable("CTPN");

                entity.Property(e => e.MaPn).HasColumnName("MaPN");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ThanhTien)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.Ctpns)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTPN_MatHang");

                entity.HasOne(d => d.MaPnNavigation)
                    .WithMany(p => p.Ctpns)
                    .HasForeignKey(d => d.MaPn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CTPN_PhieuNhap");
            });

            modelBuilder.Entity<DinhLuong>(entity =>
            {
                entity.HasKey(e => new { e.MaMon, e.MaMh });

                entity.ToTable("DinhLuong");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.Property(e => e.QuyDoi).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.DinhLuongs)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_DinhLuong_MatHang");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.DinhLuongs)
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_DinhLuong_Mon");
            });

            modelBuilder.Entity<DonViTinh>(entity =>
            {
                entity.HasKey(e => e.MaDvt);

                entity.ToTable("DonViTinh");

                entity.Property(e => e.MaDvt).HasColumnName("MaDVT");

                entity.Property(e => e.TenDvt)
                    .HasMaxLength(100)
                    .HasColumnName("TenDVT");
            });

            modelBuilder.Entity<GoiMonTaiBan>(entity =>
            {
                entity.HasKey(e => e.MaGoiMon);

                entity.ToTable("GoiMonTaiBan");

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.GoiMonTaiBans)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK_GoiMonTaiBan_HoaDon");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.GoiMonTaiBans)
                    .HasForeignKey(d => d.MaMon)
                    .HasConstraintName("FK_GoiMonTaiBan_Mon");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd).HasColumnName("MaHD");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.Ngay).HasColumnType("datetime");

                entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TienGiam).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TongTien).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaBanNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaBan)
                    .HasConstraintName("FK_HoaDon_Ban");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HoaDon_KhachHang");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_HoaDon_NguoiDung");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.DiemTichLuy).HasDefaultValueSql("((0))");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKm)
                    .HasName("PL_KhuyenMai");

                entity.ToTable("KhuyenMai");

                entity.Property(e => e.MaKm).HasColumnName("MaKM");

                entity.Property(e => e.TenKm)
                    .HasMaxLength(100)
                    .HasColumnName("TenKM");
            });

            modelBuilder.Entity<LoaiMatHang>(entity =>
            {
                entity.HasKey(e => e.MaLoaiMh);

                entity.ToTable("LoaiMatHang");

                entity.Property(e => e.MaLoaiMh).HasColumnName("MaLoaiMH");

                entity.Property(e => e.TenLoaiMh)
                    .HasMaxLength(50)
                    .HasColumnName("TenLoaiMH");
            });

            modelBuilder.Entity<ManHinh>(entity =>
            {
                entity.HasKey(e => e.MaMh);

                entity.ToTable("ManHinh");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.Property(e => e.TenMh)
                    .HasMaxLength(50)
                    .HasColumnName("TenMH");
            });

            modelBuilder.Entity<MatHang>(entity =>
            {
                entity.HasKey(e => e.MaMh);

                entity.ToTable("MatHang");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.Property(e => e.MaDvt).HasColumnName("MaDVT");

                entity.Property(e => e.MaLoaiMh).HasColumnName("MaLoaiMH");

                entity.Property(e => e.SoLuongTon).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TenMh)
                    .HasMaxLength(50)
                    .HasColumnName("TenMH");

                entity.HasOne(d => d.MaDvtNavigation)
                    .WithMany(p => p.MatHangs)
                    .HasForeignKey(d => d.MaDvt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatHang_DonViTinh");

                entity.HasOne(d => d.MaLoaiMhNavigation)
                    .WithMany(p => p.MatHangs)
                    .HasForeignKey(d => d.MaLoaiMh)
                    .HasConstraintName("FK_MatHang_LoaiMH");
            });

            modelBuilder.Entity<Mon>(entity =>
            {
                entity.HasKey(e => e.MaMon);

                entity.ToTable("Mon");

                entity.Property(e => e.Anh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GiaGoc).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.GiaKm)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("GiaKM");

                entity.Property(e => e.MaDvt).HasColumnName("MaDVT");

                entity.Property(e => e.MaKm)
                    .HasColumnName("MaKM")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TenMon).HasMaxLength(100);

                entity.HasOne(d => d.MaDvtNavigation)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.MaDvt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mon_DonViTinh");

                entity.HasOne(d => d.MaKmNavigation)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.MaKm)
                    .HasConstraintName("FK_Mon_KhuyenMai");

                entity.HasOne(d => d.MaNhomNavigation)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.MaNhom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mon_NhomMon");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd);

                entity.ToTable("NguoiDung");

                entity.Property(e => e.MaNd).HasColumnName("MaND");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh).HasMaxLength(3);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenDn)
                    .HasMaxLength(50)
                    .HasColumnName("TenDN");
            });

            modelBuilder.Entity<NguoiDungNhomNguoiDung>(entity =>
            {
                entity.HasKey(e => new { e.MaNd, e.MaNhom })
                    .HasName("PK_NDNhomND");

                entity.ToTable("NguoiDungNhomNguoiDung");

                entity.Property(e => e.MaNd).HasColumnName("MaND");

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.NguoiDungNhomNguoiDungs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NDNhomND_ND");

                entity.HasOne(d => d.MaNhomNavigation)
                    .WithMany(p => p.NguoiDungNhomNguoiDungs)
                    .HasForeignKey(d => d.MaNhom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NDNhomND_NhomND");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNcc)
                    .HasMaxLength(50)
                    .HasColumnName("TenNCC");
            });

            modelBuilder.Entity<NhomMon>(entity =>
            {
                entity.HasKey(e => e.MaNhom);

                entity.ToTable("NhomMon");

                entity.Property(e => e.Anh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenNhom).HasMaxLength(100);
            });

            modelBuilder.Entity<NhomNguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNhom)
                    .HasName("PK_NhomND");

                entity.ToTable("NhomNguoiDung");

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.TenNhom).HasMaxLength(50);
            });

            modelBuilder.Entity<PhanQuyen>(entity =>
            {
                entity.HasKey(e => new { e.MaNhom, e.MaMh });

                entity.ToTable("PhanQuyen");

                entity.Property(e => e.MaMh).HasColumnName("MaMH");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.PhanQuyens)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanQuyen_ManHinh");

                entity.HasOne(d => d.MaNhomNavigation)
                    .WithMany(p => p.PhanQuyens)
                    .HasForeignKey(d => d.MaNhom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanQuyen_NhomND");
            });

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.HasKey(e => e.MaPn);

                entity.ToTable("PhieuNhap");

                entity.Property(e => e.MaPn).HasColumnName("MaPN");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.Ngay).HasColumnType("date");

                entity.Property(e => e.TongTien).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.PhieuNhaps)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_PhieuNhap_NCC");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.PhieuNhaps)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_PhieuNhap_NguoiDung");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

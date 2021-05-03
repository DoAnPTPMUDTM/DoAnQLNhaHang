﻿USE MASTER;
GO
CREATE DATABASE QuanLyNhaHang;
GO
USE QuanLyNhaHang;
GO


CREATE TABLE KhuyenMai
(
	MaKM INT IDENTITY(1,1),
	TenKM NVARCHAR(100),
	TyLe FLOAT,
	NgayBD DATE,
	NgayKT DATE,
	CONSTRAINT PL_KhuyenMai PRIMARY KEY(MAKM)
)
CREATE TABLE NhomMon
(
	MaNhom INT IDENTITY(1,1),
	TenNhom NVARCHAR(100),
	Anh VARCHAR(50) NULL,
	CONSTRAINT PK_NhomMon PRIMARY KEY(MaNhom)
)
CREATE TABLE Mon
(
	MaMon INT IDENTITY(1,1),
	MaNhom INT,
	TenMon NVARCHAR(100),
	DVT  NVARCHAR(50),
	Anh VARCHAR(50),
	GiaGoc DECIMAL(18, 0),
	GiaKM DECIMAL(18, 0),
	MaKM INT NULL,
	CONSTRAINT PK_Mon PRIMARY KEY(MaMon),
	CONSTRAINT FK_Mon_KhuyenMai FOREIGN KEY(MaKM) REFERENCES KhuyenMai(MaKM)
)	
CREATE TABLE Ban
(
	MaBan INT IDENTITY(1,1),
	TenBan NVARCHAR(50),
	ViTri NVARCHAR(50),
	TrangThai INT,
	CONSTRAINT PK_Ban PRIMARY KEY(MaBan)
)
CREATE TABLE KhachHang
(
	MaKH INT IDENTITY(1,1),
	TenKH NVARCHAR(50),
	DiaChi NVARCHAR(50),
	SDT VARCHAR(11),
	TongThanhTien DECIMAL(18,0),
	DiemTichLuy INT,
	CONSTRAINT PK_KhachHang PRIMARY KEY(MaKH)
)
CREATE TABLE NguoiDung
(
	MaND INT IDENTITY(1,1),
	HoTen NVARCHAR(50),
	GioiTinh NVARCHAR(3),
	SDT VARCHAR(11),
	DiaChi NVARCHAR(50),
	Email VARCHAR(50),
	TenDN NVARCHAR(50),
	MatKhau NVARCHAR(50),
	HoatDong INT,
	CONSTRAINT PK_NguoiDung PRIMARY KEY(MaND)

)
CREATE TABLE HoaDon
(
	MaHD INT IDENTITY(1,1),
	MaBan INT,
	MaNV INT,
	MaKH INT NULL,
	HoTen NVARCHAR(50),
	Ngay DATE,
	TongTien DECIMAL(18,0),
	CONSTRAINT PK_HoaDon PRIMARY KEY(MaHD),
	CONSTRAINT FK_HoaDon_Ban FOREIGN KEY(MaBan) REFERENCES Ban(MaBan),
	CONSTRAINT FK_HoaDon_NguoiDung FOREIGN KEY(MaNV) REFERENCES NguoiDung(MaND),
	CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY(MaKH) REFERENCES KhachHang(MaKH)
)
CREATE TABLE CTHD
(
	MaHD INT,
	MaMon INT,
	SoLuong INT,
	DonGia DECIMAL(18,0),
	ThanhTien DECIMAL(18,0),
	GhiChu NVARCHAR(50) NULL,
	CONSTRAINT PK_CTHD PRIMARY KEY(MaHD,MaMon),
	CONSTRAINT FK_CTHD_HD FOREIGN KEY(MaHD) REFERENCES HoaDon(MaHD),
	CONSTRAINT FK_CTHD_Mon FOREIGN KEY(MaMon) REFERENCES Mon(MaMon)
)
CREATE TABLE NhaCungCap
(
	MaNCC INT IDENTITY(1,1),
	TenNCC NVARCHAR(50),
	DiaChi NVARCHAR(50),
	SDT VARCHAR(11),
	CONSTRAINT PK_NhaCungCap PRIMARY KEY(MaNCC)

)
CREATE TABLE LoaiMatHang
(
	MaLoaiMH INT IDENTITY(1,1),
	TenLoaiMH NVARCHAR(50),
	CONSTRAINT PK_LoaiMatHang PRIMARY KEY(MaLoaiMH)
)
CREATE TABLE MatHang
(
	MaMH INT IDENTITY(1,1),
	TenMH NVARCHAR(50),
	DVT NVARCHAR(50),
	MaLoaiMH INT,
	CONSTRAINT PK_MatHang PRIMARY KEY(MaMH),
	CONSTRAINT FK_MatHang_LoaiMH FOREIGN KEY(MaLoaiMH) REFERENCES LoaiMatHang(MaLoaiMH)
)
CREATE TABLE PhieuNhap
(
	MaPN INT IDENTITY(1,1),
	MaNV INT,
	MaNCC INT,
	Ngay DATE,
	TongTien DECIMAL(18,0),
	CONSTRAINT PK_PhieuNhap PRIMARY KEY(MaPN),
	CONSTRAINT FK_PhieuNhap_NguoiDung FOREIGN KEY(MaNV) REFERENCES NguoiDung(MaND),
	CONSTRAINT FK_PhieuNhap_NCC FOREIGN KEY(MaNCC) REFERENCES NhaCungCap(MaNCC)
)
CREATE TABLE CTPN
(
	MaPN INT,
	MaMH INT,
	SoLuong INT,
	DonGia DECIMAL(18,0),
	ThanhTien DECIMAL(18,0) DEFAULT 0,
	CONSTRAINT PK_CTPN PRIMARY KEY(MaPN,MaMH),
	CONSTRAINT FK_CTPN_PhieuNhap FOREIGN KEY(MaPN) REFERENCES PhieuNhap(MaPN),
	CONSTRAINT FK_CTPN_MatHang FOREIGN KEY(MaMH) REFERENCES MatHang(MaMH)
)
CREATE TABLE ManHinh
(
	MaMH INT IDENTITY(1,1),
	TenMH NVARCHAR(50),
	CONSTRAINT PK_ManHinh PRIMARY KEY(MaMH)
)
CREATE TABLE NhomNguoiDung
(
	MaNhom INT IDENTITY(1,1),
	TenNhom NVARCHAR(50),
	GhiChu NVARCHAR(50),
	CONSTRAINT PK_NhomND PRIMARY KEY(MaNhom)
)
CREATE TABLE NguoiDungNhomNguoiDung
(
	TenDN NVARCHAR(50),
	MaNhom INT,
	GhiChu NVARCHAR(50),
	CONSTRAINT PK_NDNhomND PRIMARY KEY(TenDN,MaNhom),
	CONSTRAINT FK_NDNhomND_NhomND FOREIGN KEY(MaNhom) REFERENCES NhomNguoiDung(MaNhom)
)
CREATE TABLE PhanQuyen
(
	MaNhom INT,
	MaMH INT,
	CoQuyen INT,
	CONSTRAINT PK_PhanQuyen PRIMARY KEY(MaNhom, MaMH),
	CONSTRAINT FK_PhanQuyen_ManHinh FOREIGN KEY(MaMH) REFERENCES ManHinh(MaMH),
	CONSTRAINT FK_PhanQuyen_NhomND FOREIGN KEY(MaNhom) REFERENCES NhomNguoiDung(MaNhom)
)
--Table bổ sung


--Trigger


--Procedure
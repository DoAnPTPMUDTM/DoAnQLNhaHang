USE MASTER;
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
	CONSTRAINT PL_KhuyenMai PRIMARY KEY(MAKM)
)
CREATE TABLE NhomMon
(
	MaNhom INT IDENTITY(1,1),
	TenNhom NVARCHAR(100),
	Anh VARCHAR(50) NULL,
	CONSTRAINT PK_NhomMon PRIMARY KEY(MaNhom)
)
CREATE TABLE DonViTinh
(
	MaDVT INT IDENTITY(1,1),
	TenDVT NVARCHAR(100),
	GhiChu NVARCHAR(MAX) NULL,
	CONSTRAINT PK_DonViTinh PRIMARY KEY(MaDVT)
)
CREATE TABLE Mon
(
	MaMon INT IDENTITY(1,1),
	MaNhom INT,
	MaDVT INT,
	TenMon NVARCHAR(100),
	Anh VARCHAR(50),
	GiaGoc DECIMAL(18, 0),
	GiaKM DECIMAL(18, 0),
	MaKM INT DEFAULT 0,
	CONSTRAINT PK_Mon PRIMARY KEY(MaMon),
	CONSTRAINT FK_Mon_KhuyenMai FOREIGN KEY(MaKM) REFERENCES KhuyenMai(MaKM),
	CONSTRAINT FK_Mon_NhomMon FOREIGN KEY(MaNhom) REFERENCES NhomMon(MaNhom),
	CONSTRAINT FK_Mon_DonViTinh FOREIGN KEY(MaDVT) REFERENCES DonViTinh(MaDVT)
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
	DiemTichLuy INT DEFAULT 0
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
	HoatDong BIT NOT NULL,
	CONSTRAINT PK_NguoiDung PRIMARY KEY(MaND)

)

CREATE TABLE HoaDon
(
	MaHD INT IDENTITY(1,1),
	MaBan INT,
	MaNV INT,
	MaKH INT NULL,
	Ngay DATETIME,
	TongTien DECIMAL(18,0),
	TienGiam DECIMAL(18,0),
	ThanhTien DECIMAL(18,0),
	TinhTrang INT,
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
	MaDVT INT,
	TenMH NVARCHAR(50),
	MaLoaiMH INT,
	CONSTRAINT PK_MatHang PRIMARY KEY(MaMH),
	CONSTRAINT FK_MatHang_LoaiMH FOREIGN KEY(MaLoaiMH) REFERENCES LoaiMatHang(MaLoaiMH),
	CONSTRAINT FK_MatHang_DonViTinh FOREIGN KEY(MaDVT) REFERENCES DonViTinh(MaDVT)
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
	MaND INT,
	MaNhom INT,
	GhiChu NVARCHAR(50),
	CONSTRAINT PK_NDNhomND PRIMARY KEY(MaND,MaNhom),
	CONSTRAINT FK_NDNhomND_NhomND FOREIGN KEY(MaNhom) REFERENCES NhomNguoiDung(MaNhom),
	CONSTRAINT FK_NDNhomND_ND FOREIGN KEY(MaND) REFERENCES NguoiDung(MaND)
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
CREATE TABLE GoiMonTaiBan
(
	MaGoiMon INT IDENTITY(1,1),
	MaHD INT, 
	MaMon INT,
	SoLuong INT,
	TinhTrang INT,
	GhiChu NVARCHAR(255) NULL,
	CONSTRAINT PK_GoiMonTaiBan PRIMARY KEY(MaGoiMon),
	CONSTRAINT FK_GoiMonTaiBan_HoaDon FOREIGN KEY(MaHD) REFERENCES HoaDon(MaHD),
	CONSTRAINT FK_GoiMonTaiBan_Mon FOREIGN KEY(MaMon) REFERENCES Mon(MaMon)
)
--Table bổ sung

--Trigger
GO
CREATE TRIGGER CapNhatKMUpdate
ON Mon
FOR UPDATE
AS
IF UPDATE(MaKM)
BEGIN
	IF ((SELECT MaKM FROM inserted) IS NULL)
		BEGIN
			UPDATE Mon
			SET GiaKM = GiaGoc
			WHERE MaMon = (SELECT MaMon FROM inserted)
		END
	ELSE
		BEGIN
			UPDATE Mon
			SET GiaKM = GiaGoc - (GiaGoc*((SELECT TyLe FROM KhuyenMai WHERE MaKM = (SELECT MaKM FROM inserted))))
			WHERE MaMon = (SELECT MaMon FROM inserted)
		END
END
---------------------------------------------------
GO
CREATE TRIGGER CapNhatKMInsert
ON Mon
FOR INSERT
AS
BEGIN
	IF ((SELECT MaKM FROM inserted) IS NULL)
		BEGIN
			UPDATE Mon
			SET GiaKM = GiaGoc
			WHERE MaMon = (SELECT MaMon FROM inserted)
		END
	ELSE
		BEGIN
			UPDATE Mon
			SET GiaKM = GiaGoc - (GiaGoc*((SELECT TyLe FROM KhuyenMai WHERE MaKM = (SELECT MaKM FROM inserted))))
			WHERE MaMon = (SELECT MaMon FROM inserted)
		END
END
---------------------------------------------------
GO
CREATE TRIGGER CapNhatTTCTHD
ON CTHD
FOR INSERT
AS
BEGIN
	UPDATE CTHD
	SET ThanhTien = (SELECT SoLuong FROM inserted) * (SELECT DonGia FROM inserted)
	WHERE MaHD = (SELECT MaHD FROM inserted) AND MaMon = (SELECT MaMon FROM inserted)
END
GO
-------------------------------------------------
GO
CREATE TRIGGER CapNhatSLCTHD
ON CTHD
FOR UPDATE
AS
BEGIN
	IF UPDATE(SoLuong)
	BEGIN
		UPDATE CTHD
		SET ThanhTien = (SELECT SoLuong FROM inserted) * (SELECT DonGia FROM inserted)
		WHERE MaHD = (SELECT MaHD FROM inserted) AND MaMon = (SELECT MaMon FROM inserted)
	END
END
GO
--Procedure
/*
GO
CREATE PROC CapNhatKMHangNgay
AS
	DECLARE @MaMon INT, @MaKM INT
	DECLARE @GiaGoc FLOAT
	DECLARE CS_MonKM CURSOR
	DYNAMIC
	FOR
		SELECT MaMon ,GiaGoc, MaKM FROM Mon WHERE MaKM IS NOT NULL
	OPEN CS_MonKM
	FETCH NEXT FROM CS_MonKM
	INTO @MaMon,@GiaGoc,@MaKM
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		IF GETDATE() > (SELECT NgayKT FROM KhuyenMai WHERE MaKM = @MaKM)
		BEGIN
			UPDATE Mon
			SET MaKM = NULL --Tu goi trigger
			WHERE MaMon = @MaMon
		END
		FETCH NEXT FROM CS_MonKM
		INTO @MaMon,@GiaGoc,@MaKM
	END
CLOSE CS_MonKM
*/
-----------------------------------------------------------------------------------
--BẢNG KHUYẾN MÃI(MaKM, TenKM, TyLe, NgayBD, NgayKT)
SET DATEFORMAT DMY;
SET IDENTITY_INSERT KhuyenMai ON
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe) VALUES(0,N'Không khuyến mãi',0);
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe) VALUES(1,N'Khuyến mãi giảm 2%',0.02);
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe) VALUES(2,N'Khuyến mãi giảm 5%',0.05);
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe) VALUES(3,N'Khuyến mãi giảm 10%',0.1);
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe) VALUES(4,N'Khuyến mãi giảm 15%',0.15); 
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe) VALUES(5,N'Khuyến mãi giảm 20%',0.2);
SET IDENTITY_INSERT KhuyenMai OFF
SELECT * FROM KhuyenMai
-----------------------------------------------------------------------------------
--BẢNG ĐƠN VỊ TÍNH(MaDVT, TenDVT, GhiChu)
SET IDENTITY_INSERT DonViTinh ON
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(1,N'Bát');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(2,N'Nồi');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(3,N'Đĩa');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(4,N'Chén');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(5,N'Cái');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(6,N'Lon');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(7,N'Thùng');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(8,N'Chai');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(9,N'Hủ');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(10,N'Bó');
INSERT INTO DonViTinh(MaDVT, TenDVT) VALUES(11,N'Kg');
SET IDENTITY_INSERT DonViTinh OFF
------------------------------------------------------------------------------------
--BẢNG NHÓM MÓN(MaNhom, TenNhom, Anh)
SET IDENTITY_INSERT NhomMon ON
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(1,N'MÓN KHAI VỊ','1.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(2,N'HẢI SẢN','4.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(3,N'GÀ - VỊT','3.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(4,N'MÓN LẨU','2.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(5,N'ĐỒ UỐNG','5.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(6,N'MÓN TRÁNG MIỆNG','6.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(7,N'MÓN BÒ','6.png');
SET IDENTITY_INSERT NhomMon OFF
select * from NhomMon
------------------------------------------------------------------------------------
--BẢNG MÓN(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM)
SET IDENTITY_INSERT Mon ON
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (1,1,1,N'Súp măng Tây cua','1.jpg',45000,45000,1);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (2,1,1,N'Súp hải sản Ngọc Bích','2.jpg',60000,60000,2);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (3,1,3,N'Gỏi tôm kiểu Thái','3.jpg',70000,70000,3);
----------
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (4,3,2,N'Gà hầm ngũ quả','4.jpg',120000,120000,3);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (5,3,3,N'Gà quay Hương Liễu và bánh bao','5.jpg',150000,150000,3);
--- thêm 3 món gà, 3 món lẩu, 3 đồ uống, 2 món tráng miệng 
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (6,3,2,N'Gà tiềm thuốc Bắc','6.jpg',600000,600000,3);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (7,3,3,N'Gà nấu Lagu','7.jpg',600000,600000,2);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (8,3,3,N'Gà hấp lá chanh','8.png',250000,250000,1);
----------
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (9,4,2,N'Lẩu cá kèo','9.png',650000,650000,3);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (10,4,2,N'Lẩu cá chép','10.png',450000,450000,2);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (11,4,2,N'Lẩu Lươn','11.png',450000,450000,2);
----------
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (12,5,6,N'Bò Húc','12.jpg',14000,14000,1);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (13,5,6,N'Bia 333','13.jpg',13000,13000,1);
----------
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (14,6,5,N'Panna Cotta','14.jpg',25000,25000,1);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (15,6,8,N'Đá bào mật trái cây','15.png',20000,20000,1);
----Hải sản nhóm 2
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (16,2,3,N'Cá đuối 2 món','16.png',1200000,1200000,3);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (17,2,3,N'Cá điêu hồng hấp Hồng Kông','17.png',500000,500000,2);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (18,2,3,N'Cà ri cua','18.jpg',1200000,1200000,4);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (19,2,3,N'Tôm càng xanh','19.png',600000,600000,5);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (20,2,3,N'Mực ống','20.png',500000,500000,2);
---Mỗi loại 1 món
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (21,1,3,N'Chả cá hấp Đông Cô cải thìa','21.jpg',450000,450000,4);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (22,4,2,N'Lẩu hải sản','22.png',550000,550000,5);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (23,4,2,N'Lẩu thác lác','23.png',600000,600000,5);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (24,5,8,N'Bia Heineken','24.jpg',20000,20000,0);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (25,5,8,N'Rượu Vodka Smirnoff Red','25.jpg',230000,230000,0);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (26,6,1,N'Bingsu trái cây','26.jpg',45000,45000,1);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (27,6,3,N'Bánh Flan','27.jpg',20000,20000,1);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (28,7,3,N'Bắp bò nước mắm','28.jpg',500000,500000,2);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (29,7,3,N'Bò cuộn nướng phô mai','29.jpg',500000,500000,3);
INSERT INTO Mon(MaMon, MaNhom, MaDVT, TenMon, Anh, GiaGoc, GiaKM, MaKM) VALUES  (30,7,3,N'Bò nấu Lagu','30.jpg',500000,500000,1);
----------
SET IDENTITY_INSERT Mon OFF
select * from Mon
------------------------------------------------------------------------------------
--BẢNG BÀN(MaBan, TenBan, ViTri, TrangThai) 0 là chưa/ 1 là đặt
SET IDENTITY_INSERT Ban ON
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (1,N'Bàn 01',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (2,N'Bàn 02',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (3,N'Bàn 03',N'Tầng 2',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (4,N'Bàn 04',N'Tầng 2',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (5,N'Bàn 05',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (6,N'Bàn 06',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (7,N'Bàn 07',N'Tầng 2',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (8,N'Bàn 08',N'Tầng 2',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (9,N'Bàn 09',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (10,N'Bàn 10',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (11,N'Bàn 11',N'Tầng 2',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (12,N'Bàn 12',N'Tầng 2',0);
SET IDENTITY_INSERT Ban OFF
select * from Ban
------------------------------------------------------------------------------------
--BẢNG KHÁCH HÀNG(MaKH, TenKH, DiaChi, SDT, TongThanhTien, DiemTichLuy) ---100K ĐƯỢC 10 ĐIỂM?
SET IDENTITY_INSERT KhachHang ON
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, DiemTichLuy) VALUES(0,N'Khách Vãng Lai',NULL,NULL,0); 
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, DiemTichLuy) VALUES(1,N'Nguyễn Đức Thanh Mai','TP HCM','0901682813',100); 
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, DiemTichLuy) VALUES(2,N'Nguyễn Kim Anh Thư','TP HCM','0901682812',60);
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, DiemTichLuy) VALUES(3,N'Tô Khánh Vân','TP HCM','0901682811',30); 
SET IDENTITY_INSERT KhachHang OFF
select * from KhachHang
------------------------------------------------------------------------------------
--BẢNG NGƯỜI DÙNG(MaND, HoTen, GioiTinh, SDT, DiaChi, Email, TenDN, MatKhau, HoatDong)
SET IDENTITY_INSERT NguoiDung ON
INSERT INTO NguoiDung(MaND, HoTen, GioiTinh, SDT, DiaChi, Email, TenDN, MatKhau, HoatDong) VALUES(1,N'Phạm Hồng Sơn','Nam','0372454734','TP HCM','sonpham.031195@gmail.com','hongson','123456',1);
INSERT INTO NguoiDung(MaND, HoTen, GioiTinh, SDT, DiaChi, Email, TenDN, MatKhau, HoatDong) VALUES(2,N'Nguyễn Đức Thanh Vy',N'Nữ','0901682813','TP HCM','thanhvyela@gmail.com','thanhvy','123456',1);
SET IDENTITY_INSERT NguoiDung OFF
select * from NguoiDung
 
---------------------------------Không insert cái này nha----------------------------------------------
/*
--HÓA ĐƠN(MaHD, MaBan, MaNV, MaKH, HoTen, Ngay, TongTien, TienNhan, TienThua, TinhTrang) --0: Đã thanh toán/1: Chưa Thanh Toán
SET IDENTITY_INSERT HoaDon ON
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien, TienNhan, TienThua,TinhTrang) VALUES(1,1,2,1,GETDATE(),136500,0,136500,500000,363500,1); -- ĂN MÓN SỐ 1 VÀ SỐ 4
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien, TienGiam,ThanhTien,TienNhan, TienThua,TinhTrang) VALUES(2,3,2,3,GETDATE(),147000,0,147000,147000,0,1); --ĂN MÓN SỐ 2 VÀ SỐ 4
SET IDENTITY_INSERT HoaDon OFF
select * from HoaDon
------------------------------------------------------------------------------------
--CHI TIẾT HÓA ĐƠN(MaHD,MaMon, SoLuong, DonGia, ThanhTien, GhiChu) -- MÓN 1 GIẢM 10%, MÓN 2-- GIẢM 15%, MÓN 3,4,5 GIẢM 20%
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(1,1,1,45000,40500);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(1,4,1,120000,96000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(2,2,1,60000,51000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(2,4,1,120000,96000);
select * from CTHD
------------------------------------------------------------------------------------
*/
-----------------------------------------------------------------
--HÓA ĐƠN(MaHD, MaBan, MaNV, MaKH, HoTen, Ngay, TongTien, TienNhan, TienThua, TinhTrang)
select * from HoaDon
select * from Ban
select * from CTHD
select * from Mon
SET DATEFORMAT DMY;
SET IDENTITY_INSERT HoaDon ON
--tháng 01/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(1,1,2,1,'10/01/2020',136500,0,136500,1); 
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(2,3,2,3,'14/01/2020',147000,0,147000,1); 
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(3,3,2,3,'22/01/2020',920000,0,920000,1);
--tháng 02/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(4,4,2,3,'11/02/2020',675000,0,675000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(5,5,2,3,'14/02/2020',2778000,0,2778000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(6,3,2,3,'22/02/2020',5410500,0,5410500,1);
--tháng 03/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(7,6,2,3,'02/03/2020',12130000,0,12130000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(8,3,2,3,'16/03/2020',855000,0,855000,1);
--tháng 04/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(9,8,2,3,'10/04/2020',277500,0,277500,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(10,7,2,3,'23/04/2020',4230000,0,4230000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(11,8,2,3,'30/04/2020',135000,0,135000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(12,4,2,3,'15/04/2020',6160000,0,6160000,1);
--tháng 05/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(13,3,2,3,'11/05/2020',440000,0,440000,1);
--tháng 06/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(14,3,2,3,'27/06/2020',12525000,0,12525000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(15,4,2,3,'08/06/2020',692100,0,692100,1);
--tháng 07/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(16,3,2,3,'21/07/2020',19500000,0,19500000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(17,10,2,3,'04/07/2020',13350000,0,13350000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(18,2,2,3,'22/07/2020',12130000,0,12130000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(19,5,2,3,'27/07/2020',6160000,0,6160000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(20,3,2,3,'31/07/2020',4140000,0,4140000,1);
--tháng 08/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(21,3,2,3,'17/08/2020',13445000,0,13445000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(22,3,2,3,'09/08/2020',63000,0,63000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(23,7,2,3,'13/08/2020',2778000,0,2778000,1);
--tháng 09/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(24,8,2,3,'22/09/2020',12130000,0,12130000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(25,4,2,3,'14/09/2020',675000,0,675000,1);
--tháng 10/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(26,5,2,3,'20/10/2020',4230000,0,4230000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(27,3,2,3,'27/10/2020',675000,0,675000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(28,3,2,3,'30/10/2020',4417500,0,4417500,1);
--tháng 11/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(29,2,2,3,'03/11/2020',692100,0,692100,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(30,3,2,3,'05/11/2020',19500000,0,19500000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(31,6,2,3,'17/11/2020',5410500,0,5410500,1);
--tháng 12/2020
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(32,3,2,3,'30/12/2020',3850000,0,3850000,1);
---------------
--tháng 01/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(33,4,2,3,'04/01/2021',5300000,0,5300000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(34,5,2,3,'09/01/2021',4230000,0,4230000,1);
--tháng 02/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(35,3,2,3,'11/02/2021',12130000,0,12130000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(36,3,2,3,'15/02/2021',2778000,0,2778000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(37,3,2,3,'07/02/2021',3023400,0,3023400,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(38,2,2,3,'25/02/2021',3850000,0,3850000,1);
--tháng 03/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(39,3,2,3,'14/03/2021',13350000,0,13350000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(40,3,2,3,'31/03/2021',10150000,0,10150000,1);
--tháng 04/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(41,3,2,3,'14/04/2021',5300000,0,5300000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(42,4,2,3,'18/04/2021',440000,0,440000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(43,8,2,3,'28/04/2021',6930000,0,6930000,1);

--tháng 05/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(44,3,2,3,'19/05/2021',19500000,0,19500000,1);
--tháng 06/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(45,3,2,3,'20/06/2021',16650000,0,16650000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(46,3,2,3,'21/06/2021',4140000,0,4140000,1);
--tháng 07/2021
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(47,3,2,3,'24/07/2021',2778000,0,2778000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(48,3,2,3,'14/07/2021',10650000,0,10650000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(49,3,2,3,'10/07/2021',6160000,0,6160000,1);
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, Ngay, TongTien,TienGiam,ThanhTien,TinhTrang) 
VALUES(50,3,2,3,'18/05/2020',4230000,0,4230000,1); --bosung
--------
SET IDENTITY_INSERT HoaDon OFF
SELECT * FROM HoaDon
----------------------------------------------------------------
--CHI TIẾT HÓA ĐƠN(MaHD,MaMon, SoLuong, DonGia, ThanhTien, GhiChu)
---
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(1,1,1,45000,40500);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(1,4,1,120000,120000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(2,2,1,60000,60000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(2,4,1,120000,120000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(3,19,1,480000,480000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(3,20,1,475000,475000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(4,5,1,135000,135000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(4,6,1,540000,540000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(5,7,3,570000,1710000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(5,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(5,19,2,480000,960000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(6,7,5,44100,220500);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(6,19,5,480000,2400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(6,27,10,20000,200000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(7,18,10,44100,441000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(7,24,10,20000,200000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(7,9,4,585000,2340000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(8,11,2,427500,855000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(9,1,5,44100,220500);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(9,2,1,57000,57000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(10,3,5,63000,315000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(10,4,1,108000,108000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(10,5,5,135000,675000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(10,6,5,540000,2700000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(11,5,1,135000,135000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(12,9,5,585000,2925000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(12,7,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(12,24,5,20000,100000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(12,2,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(13,22,1,440000,440000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(14,16,5,1080000,5400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(14,17,15,475000,7125000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(15,3,1,63000,63000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(15,9,1,585000,585000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(15,26,1,44100,44100);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(16,2,1,57000,57000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(16,9,1,585000,585000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(16,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(17,10,20,427500,8550000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(17,19,10,480000,4800000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(18,18,10,44100,441000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(18,24,10,20000,200000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(18,9,4,585000,2340000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(19,9,5,585000,2925000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(19,7,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(19,24,5,20000,100000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(19,2,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(20,3,5,63000,315000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(20,21,10,382500,3825000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(21,14,10,24500,245000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(21,16,10,1080000,10800000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(21,23,5,480000,2400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(22,3,1,63000,63000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(23,7,3,570000,1710000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(23,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(23,19,2,480000,960000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(24,18,10,44100,441000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(24,24,10,20000,200000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(24,9,4,585000,2340000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(25,5,1,135000,135000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(25,6,1,540000,540000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(26,3,5,63000,315000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(26,4,1,108000,108000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(26,5,5,135000,675000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(26,6,5,540000,2700000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(27,5,1,135000,135000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(27,6,1,540000,540000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(28,7,4,570000,2280000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(28,10,5,427500,2137500);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(29,3,1,63000,63000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(29,9,1,585000,585000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(29,26,1,44100,44100);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(30,2,1,57000,57000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(30,9,1,585000,585000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(30,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(31,7,5,44100,220500);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(31,19,5,480000,2400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(31,27,10,20000,200000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(32,6,5,540000,2700000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(32,25,5,230000,1150000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(33,8,10,245000,2450000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(33,7,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(34,3,5,63000,315000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(34,4,1,108000,108000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(34,5,5,135000,675000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(34,6,5,540000,2700000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(35,18,10,44100,441000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(35,24,10,20000,200000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(35,9,4,585000,2340000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(36,7,3,570000,1710000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(36,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(36,19,2,480000,960000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(37,13,10,12740,127400);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(37,27,10,19600,196000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(37,6,5,540000,2700000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(38,6,5,540000,2700000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(38,25,5,230000,1150000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(39,10,20,427500,8550000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(39,19,10,480000,4800000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(40,20,10,475000,4750000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(40,6,10,540000,5400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(41,8,10,245000,2450000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(41,7,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(42,22,1,440000,440000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(43,4,10,108000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(43,9,10,585000,5850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(44,2,1,57000,57000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(44,9,1,585000,585000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(44,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(45,16,5,1080000,5400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(45,9,10,585000,5850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(45,6,10,540000,5400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(46,3,5,63000,315000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(46,21,10,382500,3825000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(47,7,3,570000,1710000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(47,16,1,1080000,1080000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(48,25,5,230000,1150000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(48,18,5,1020000,5100000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(48,22,10,440000,4400000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(49,9,5,585000,2925000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(49,7,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(49,24,5,20000,100000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(49,2,5,570000,2850000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(50,3,5,63000,315000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(50,4,1,108000,108000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(50,5,5,135000,675000);
INSERT INTO CTHD(MaHD,MaMon, SoLuong, DonGia, ThanhTien) VALUES(50,6,5,540000,2700000);
SELECT * FROM CTHD
--NHÀ CUNG CẤP(MaNCC, TenNCC, DiaChi, SDT)
SET IDENTITY_INSERT NhaCungCap ON
INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT) VALUES (1,N'Phạm Hồng Nhung',N'TPHCM','0367695027');
INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT) VALUES (2,N'Nguyễn Thanh Trúc',N'TPHCM','0367695027');
INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT) VALUES (3,N'Phạm Kim Út',N'TPHCM','0367695027');
SET IDENTITY_INSERT NhaCungCap OFF
select * from NhaCungCap
------------------------------------------------------------------------------------
--BẢNG LOẠI MẶT HÀNG(MaLoaiMH,TenLoaiMH)
SET IDENTITY_INSERT LoaiMatHang ON
INSERT INTO LoaiMatHang(MaLoaiMH,TenLoaiMH) VALUES (1,N'Nguyên liệu rau');
INSERT INTO LoaiMatHang(MaLoaiMH,TenLoaiMH) VALUES (2,N'Nguyên liệu củ quả');
INSERT INTO LoaiMatHang(MaLoaiMH,TenLoaiMH) VALUES (3,N'Nguyên liệu hải sản tươi sống');
INSERT INTO LoaiMatHang(MaLoaiMH,TenLoaiMH) VALUES (4,N'Nguyên liệu thịt tươi');
INSERT INTO LoaiMatHang(MaLoaiMH,TenLoaiMH) VALUES (5,N'Đồ uống các loại');
SET IDENTITY_INSERT LoaiMatHang OFF
select * from LoaiMatHang
------------------------------------------------------------------------------------
--BẢNG MẶT HÀNG(MaMH, TenMH, DVT,GiaMH, MaLoaiMH)
/*
SET IDENTITY_INSERT MatHang  ON
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (1,N'Xà lách','kg',40000,1); --40k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (2,N'Hành tây','kg',21000,1); --21k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (3,N'Cải thìa','kg',25000,1); --25k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (4,N'Cần tây','kg',50000,1); --50k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (5,N'Cà chua','kg',25000,2); --25k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (6,N'Cà rốt','kg',17000,2); --17k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (7,N'Cà tím','kg',30000,2); --30k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (8,N'Mực','kg',250000,3); --250k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (9,N'Tôm','kg',150000,3); --150k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (10,N'Thịt vịt','kg',48000,4); --48k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (11,N'Thịt gà','kg',28000,4); --28k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (12,N'Bia 333',N'Thùng',235000,5); --235k/thùng
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (13,N'Bò húc',N'Lốc',65000,5); --65k/lốc
SET IDENTITY_INSERT MatHang OFF
select * from MatHang
*/

SET IDENTITY_INSERT MatHang  ON
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (1,N'Xà lách',11,1); --40k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (2,N'Hành tây',11,1); --21k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (3,N'Cải thìa',11,1); --25k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (4,N'Cần tây',11,1); --50k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (5,N'Cà chua',11,2); --25k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (6,N'Cà rốt',11,2); --17k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (7,N'Cà tím',11,2); --30k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (8,N'Mực',11,3); --250k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (9,N'Tôm',11,3); --150k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (10,N'Thịt vịt',11,4); --48k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (11,N'Thịt gà',11,4); --28k/kg
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (12,N'Bia 333',7,5); --235k/thùng
INSERT INTO MatHang(MaMH, TenMH, MaDVT, MaLoaiMH) VALUES (13,N'Bò húc',7,5); --65k/lốc
SET IDENTITY_INSERT MatHang OFF
select * from MatHang

------------------------------------------------------------------------------------
--BẢNG PHIẾU NHẬP(MaPN, MaNV, MaNCC, Ngay, TongTien)
SET IDENTITY_INSERT PhieuNhap  ON
INSERT INTO PhieuNhap(MaPN, MaNV, MaNCC, Ngay, TongTien) VALUES(1,2,1,GETDATE(),810000);
INSERT INTO PhieuNhap(MaPN, MaNV, MaNCC, Ngay, TongTien) VALUES(2,2,1,GETDATE(),400000);
SET IDENTITY_INSERT PhieuNhap  OFF
select * from PhieuNhap
------------------------------------------------------------------------------------
--BẢNG CT PHIẾU NHẬP(MaPN, MaMH, SoLuong, DonGia, ThanhTien)
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (1,1,2,40000,80000);
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (1,9,3,150000,450000);
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (1,11,10,28000,280000);
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (2,5,2,100000,200000);
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (2,6,10,20000,200000);
select * from CTPN
------------------------------------------------------------------------------------

--BẢNG MÀN HÌNH(MaMH, TenMH)
SET IDENTITY_INSERT ManHinh  ON
INSERT INTO ManHinh(MaMH, TenMH) VALUES(1,N'Gọi món tại quầy');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(2,N'Gọi món tại bàn');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(3,N'Quản lý khách hàng');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(4,N'Quản lý món ăn, nhóm món ăn');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(5,N'Quản lý khuyến mãi');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(6,N'Quản lý bàn ăn');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(7,N'Thống kê doanh thu');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(8,N'Quản lý người dùng');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(9,N'Quản lý nhóm người dùng');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(10,N'Quản lý màn hình');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(11,N'Thêm người dùng vào nhóm');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(12,N'Phân quyền');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(13,N'Sao lưu dữ liệu');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(14,N'Thông tin tài khoản');
SET IDENTITY_INSERT ManHinh  OFF
select * from ManHinh
------------------------------------------------------------------------------------
--BẢNG NHÓM NGDÙNG(MaNhom, TenNhom, GhiChu)
SET IDENTITY_INSERT NhomNguoiDung  ON
INSERT INTO NhomNguoiDung(MaNhom, TenNhom) VALUES (1,N'Nhân viên');
INSERT INTO NhomNguoiDung(MaNhom, TenNhom) VALUES (2,N'Quản lý');
SET IDENTITY_INSERT NhomNguoiDung  OFF
select * from NhomNguoiDung
------------------------------------------------------------------------------------
--BẢNG NGƯỜI DÙNG NHÓM NGƯỜI DÙNG(MaND, MaNhom, GhiChu)
INSERT INTO NguoiDungNhomNguoiDung(MaND, MaNhom) VALUES (1,1);
INSERT INTO NguoiDungNhomNguoiDung(MaND, MaNhom) VALUES (1,2);
select * from NguoiDungNhomNguoiDung
------------------------------------------------------------------------------------
--BẢNG PHÂN QUYỀN(MaNhom, MaMH, CoQuyen) CoQuyen-- 0: Quản lý, 1: nhân viên
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,1,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,2,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,3,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,4,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,5,0);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,6,0);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,7,0);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,8,0);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,1,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,2,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,3,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,4,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,5,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,6,1);
select * from PhanQuyen



------------------------------------------------------------------------------------END------------------------------------------------------------------------------------



---------------------------Command Test Code
SELECT * FROM NhomMon
SELECT * FROM NguoiDung
SELECT * FROM Ban
SELECT * FROM Mon
SELECT * FROM HoaDon
SELECT * FROM CTHD
SELECT * FROM KhachHang
SELECT * FROM Ban
SELECT * FROM KhuyenMai
SELECT * FROM NguoiDungNhomNguoiDung
SELECT * FROM ManHinh
SELECT * FROM HoaDon
SELECT * FROM CTHD
SELECT * FROM GoiMonTaiBan
SELECT * FROM CTPN
SELECT * FROM PhieuNhap
SELECT * FROM NhomNguoiDung
SELECT * FROM MatHang
-------------------------------
INSERT INTO CTHD VALUES (3,1,1,45000,45000,NULL)
INSERT INTO CTHD VALUES (3,4,1,120000,120000,NULL)
INSERT INTO CTHD VALUES (3,2,1,60000,60000,NULL)

INSERT INTO CTHD VALUES (4,1,1,45000,45000,NULL)
INSERT INTO CTHD VALUES (4,4,1,120000,120000,NULL)

INSERT INTO CTHD VALUES (5,2,1,60000,60000,NULL)
INSERT INTO CTHD VALUES (5,4,2,60000,120000,NULL)
INSERT INTO CTHD VALUES (5,1,5,45000,225000,NULL)
INSERT INTO CTHD VALUES (5,6,5,10000,225000,NULL)

delete from PhanQuyen
select * from PhanQuyen
DELEte from CTHD
DELEte from HoaDon
SELECT CTHD.MaMon FROM CTHD
GROUP BY CTHD.MaMon
-------------
SELECT ManHinh.TenMH, PhanQuyen.CoQuyen FROM ManHinh, PhanQuyen WHERE ManHinh.MaMH = PhanQuyen.MaMH AND MaNhom = 1
SELECT ManHinh.TenMH, PhanQuyen.CoQuyen FROM ManHinh, PhanQuyen WHERE ManHinh.MaMH = PhanQuyen.MaMH AND MaNhom = 2
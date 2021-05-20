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
	CONSTRAINT FK_Mon_KhuyenMai FOREIGN KEY(MaKM) REFERENCES KhuyenMai(MaKM),
	CONSTRAINT FK_Mon_NhomMon FOREIGN KEY(MaNhom) REFERENCES NhomMon(MaNhom)
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
	HoatDong BIT NOT NULL,
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
	TienNhan DECIMAL(18,0),
	TienThua DECIMAL(18,0),
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
CREATE TABLE GoiMonTaiBan
(
	MaBan INT,
	MaMon INT,
	SoLuong INT,
	CONSTRAINT PK_GoiMonTaiBan PRIMARY KEY(MaBan,MaMon),
	CONSTRAINT FK_GoiMonTaiBan_Ban FOREIGN KEY(MaBan) REFERENCES Ban(MaBan),
	CONSTRAINT FK_GoiMonTaiBan_Mon FOREIGN KEY(MaMon) REFERENCES Mon(MaMon)
)
--Table bổ sung


--Trigger
GO
CREATE TRIGGER CapNhatKM
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
		UPDATE Mon
		SET GiaKM = GiaGoc - (GiaGoc*((SELECT TyLe FROM KhuyenMai WHERE MaKM = (SELECT MaKM FROM inserted))))
		WHERE MaMon = (SELECT MaMon FROM inserted)
END
--Procedure

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
-----------------------------------------------------------------------------------
--BẢNG KHUYẾN MÃI(MaKM, TenKM, TyLe, NgayBD, NgayKT)
SET DATEFORMAT DMY;
SET IDENTITY_INSERT KhuyenMai ON
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe, NgayBD, NgayKT) VALUES(1,N'Khuyến mãi giảm 10%',0.1,GETDATE(),'20-6-2021');
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe, NgayBD, NgayKT) VALUES(2,N'Khuyến mãi giảm 15%',0.15,GETDATE(),'13-5-2021'); 
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe, NgayBD, NgayKT) VALUES(3,N'Khuyến mãi giảm 20%',0.2,GETDATE(),'16-5-2021');
INSERT INTO KhuyenMai(MaKM, TenKM, TyLe, NgayBD, NgayKT) VALUES(4,N'Khuyến mãi giảm 5%',0.05,GETDATE(),'20-7-2021');
SET IDENTITY_INSERT KhuyenMai OFF
SELECT * FROM KhuyenMai
------------------------------------------------------------------------------------
--BẢNG NHÓM MÓN(MaNhom, TenNhom, Anh)
SET IDENTITY_INSERT NhomMon ON
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(1,N'MÓN KHAI VỊ','1.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(2,N'HẢI SẢN','4.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(3,N'GÀ - VỊT','3.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(4,N'MÓN LẨU','2.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(5,N'ĐỒ UỐNG','5.png');
INSERT INTO NhomMon(MaNhom, TenNhom, Anh) VALUES(6,N'MÓN TRÁNG MIỆNG','6.png');
SET IDENTITY_INSERT NhomMon OFF
select * from NhomMon
------------------------------------------------------------------------------------
--BẢNG MÓN(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM)
SET IDENTITY_INSERT Mon ON
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (1,1,N'Súp măng Tây cua',N'Bát','1.jpg',45000,45000,1);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (2,1,N'Súp hải sản Ngọc Bích',N'Bát','2.jpg',60000,60000,2);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (3,1,N'Gỏi tôm kiểu Thái',N'Đĩa','3.jpg',70000,70000,3);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (4,3,N'Gà hầm ngũ quả',N'Nồi','4.jpg',120000,120000,3);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (5,3,N'Gà quay Hương Liễu và bánh bao',N'Đĩa','5.jpg',150000,150000,3);
------5 món ăn bên trên là Vy lấy từ hình ở website nhà hàng cưới. còn dưới này là nhà hàng 241 test thử ảnh coi zo form oke hok hé.
--- thêm 3 món gà, 3 món lẩu, 3 đồ uống, 2 món tráng miệng 
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (6,3,N'Gà tiềm thuốc Bắc',N'Nồi','6.jpg',600000,600000,3);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (7,3,N'Gà nấu Lagu',N'Đĩa','7.jpg',600000,600000,2);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (8,3,N'Gà hấp lá chanh',N'Đĩa','8.png',250000,250000,1);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (9,4,N'Lẩu cá kèo',N'Nồi','9.png',650000,650000,3);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (10,4,N'Lẩu cá chép',N'Nồi','10.png',450000,450000,2);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (11,4,N'Lẩu Lươn',N'Nồi','11.png',450000,450000,2);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (12,5,N'Bò Húc',N'Lon','12.jpg',14000,14000,1);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (13,5,N'Bò Húc',N'Lon','13.jpg',13000,13000,1);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (14,6,N'Panna Cotta',N'Cái','14.jpg',25000,25000,1);
INSERT INTO Mon(MaMon, MaNhom, TenMon, DVT, Anh, GiaGoc, GiaKM, MaKM) VALUES  (15,6,N'Đá bào mật trái cây',N'Hủ','15.png',20000,20000,1);
SET IDENTITY_INSERT Mon OFF
select * from Mon
------------------------------------------------------------------------------------
--BẢNG BÀN(MaBan, TenBan, ViTri, TrangThai) 0 là chưa/ 1 là đặt
SET IDENTITY_INSERT Ban ON
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (1,N'Bàn 01',N'Tầng 1',1);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (2,N'Bàn 02',N'Tầng 1',0);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (3,N'Bàn 03',N'Tầng 2',1);
INSERT INTO Ban(MaBan, TenBan, ViTri, TrangThai) VALUES (4,N'Bàn 04',N'Tầng 2',0);
SET IDENTITY_INSERT Ban OFF
select * from Ban
------------------------------------------------------------------------------------
--BẢNG KHÁCH HÀNG(MaKH, TenKH, DiaChi, SDT, TongThanhTien, DiemTichLuy) ---100K ĐƯỢC 10 ĐIỂM?
SET IDENTITY_INSERT KhachHang ON
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, TongThanhTien, DiemTichLuy) VALUES(1,N'Thanh Mai','TPHCM','0901682813',1000000,100); 
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, TongThanhTien, DiemTichLuy) VALUES(2,N'Anh Thư','TPHCM','0901682813',600000,60);
INSERT INTO KhachHang(MaKH, TenKH, DiaChi, SDT, TongThanhTien, DiemTichLuy) VALUES(3,N'Khánh Vân','TPHCM','0901682813',300000,30); 
SET IDENTITY_INSERT KhachHang OFF
select * from KhachHang
------------------------------------------------------------------------------------
--BẢNG NGƯỜI DÙNG(MaND, HoTen, GioiTinh, SDT, DiaChi, Email, TenDN, MatKhau, HoatDong)
SET IDENTITY_INSERT NguoiDung ON
INSERT INTO NguoiDung(MaND, HoTen, GioiTinh, SDT, DiaChi, Email, TenDN, MatKhau, HoatDong) VALUES(1,N'Phạm Hồng Sơn','Nam','0372454734','TP HCM','sonpham.031195@gmail.com','hongson','123456',1);
INSERT INTO NguoiDung(MaND, HoTen, GioiTinh, SDT, DiaChi, Email, TenDN, MatKhau, HoatDong) VALUES(2,N'Nguyễn Đức Thanh Vy',N'Nữ','0901682813','TP HCM','thanhvyela@gmail.com','thanhvy','123456',1);
SET IDENTITY_INSERT NguoiDung OFF
select * from NguoiDung
 
------------------------------------------------------------------------------------
--HÓA ĐƠN(MaHD, MaBan, MaNV, MaKH, HoTen, Ngay, TongTien, TienNhan, TienThua, TinhTrang) --0: Đã thanh toán/1: Chưa Thanh Toán
SET IDENTITY_INSERT HoaDon ON
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, HoTen, Ngay, TongTien, TienNhan, TienThua) VALUES(1,1,2,1,N'Thanh Mai',GETDATE(),136500,500000,363500); -- ĂN MÓN SỐ 1 VÀ SỐ 4
INSERT INTO HoaDon(MaHD, MaBan, MaNV, MaKH, HoTen, Ngay, TongTien, TienNhan, TienThua) VALUES(2,3,2,3,N'Khánh Vân',GETDATE(),147000,147000,0); --ĂN MÓN SỐ 2 VÀ SỐ 4
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
--NHÀ CUNG CẤP(MaNCC, TenNCC, DiaChi, SDT)
SET IDENTITY_INSERT NhaCungCap ON
INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT) VALUES (1,N'Hồng Nhung',N'TPHCM','0367695027');
INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT) VALUES (2,N'Thanh Trúc',N'TPHCM','0367695027');
INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, SDT) VALUES (3,N'Kim Út',N'TPHCM','0367695027');
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
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (1,N'Xà lách','kg',1); --40k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (2,N'Hành tây','kg',1); --21k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (3,N'Cải thìa','kg',1); --25k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (4,N'Cần tây','kg',1); --50k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (5,N'Cà chua','kg',2); --25k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (6,N'Cà rốt','kg',2); --17k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (7,N'Cà tím','kg',2); --30k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (8,N'Mực','kg',3); --250k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (9,N'Tôm','kg',3); --150k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (10,N'Thịt vịt','kg',4); --48k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (11,N'Thịt gà','kg',4); --28k/kg
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (12,N'Bia 333',N'Thùng',5); --235k/thùng
INSERT INTO MatHang(MaMH, TenMH, DVT, MaLoaiMH) VALUES (13,N'Bò húc',N'Lốc',5); --65k/lốc
SET IDENTITY_INSERT MatHang OFF
select * from MatHang
------------------------------------------------------------------------------------
--BẢNG PHIẾU NHẬP(MaPN, MaNV, MaNCC, Ngay, TongTien)
-- Ví dụ nhân viên Vy mã 2 nhập 2 kg xà lách, 3 kg Tôm và 10kg thịt gà
SET IDENTITY_INSERT PhieuNhap  ON
INSERT INTO PhieuNhap(MaPN, MaNV, MaNCC, Ngay, TongTien) VALUES(1,2,1,GETDATE(),810000);
SET IDENTITY_INSERT PhieuNhap  OFF
select * from PhieuNhap
------------------------------------------------------------------------------------
--BẢNG CT PHIẾU NHẬP(MaPN, MaMH, SoLuong, DonGia, ThanhTien)
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (1,1,2,40000,80000);
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (1,9,3,150000,450000);
INSERT INTO CTPN(MaPN, MaMH, SoLuong, DonGia, ThanhTien) VALUES (1,11,10,28000,280000);
select * from CTPN
------------------------------------------------------------------------------------
--BẢNG MÀN HÌNH(MaMH, TenMH)
SET IDENTITY_INSERT ManHinh  ON
INSERT INTO ManHinh(MaMH, TenMH) VALUES(1,N'Gọi món');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(2,N'Quản lý khách hàng');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(3,N'Quản lý món ăn');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(4,N'Quản lý khuyến mãi');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(5,N'Quản lý bàn ăn');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(6,N'Thống kê doanh thu');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(7,N'Quản lý người dùng');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(8,N'Quản lý nhóm người dùng');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(9,N'Quản lý màn hình');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(10,N'Thêm người dùng vào nhóm');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(11,N'Phân quyền');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(12,N'Sao lưu dữ liệu');
INSERT INTO ManHinh(MaMH, TenMH) VALUES(13,N'Quản lý tài khoản');
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
--BẢNG NGƯỜI DÙNG NHÓM NGƯỜI DÙNG(TenDN, MaNhom, GhiChu)
INSERT INTO NguoiDungNhomNguoiDung(TenDN, MaNhom) VALUES ('thanhvy',1);
INSERT INTO NguoiDungNhomNguoiDung(TenDN, MaNhom) VALUES ('hongson',2);
select * from NguoiDungNhomNguoiDung
------------------------------------------------------------------------------------
--BẢNG PHÂN QUYỀN(MaNhom, MaMH, CoQuyen) CoQuyen-- 0: Quản lý, 1: nhân viên
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,1,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,2,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,3,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (1,7,1);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,4,0);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,5,0);
INSERT INTO PhanQuyen(MaNhom, MaMH, CoQuyen) VALUES (2,6,0);
select * from PhanQuyen
------------------------------------------------------------------------------------
--BẢNG GỌI MÓN TẠI BÀN(MaBan, MaHD, MaMon, SoLuong)  
-- ĂN MÓN SỐ 1 VÀ SỐ 4, MAHD 1 BÀN 01 
-- ĂN MÓN SỐ 2 VÀ SỐ 4, MAHD 2 BÀN 03 
INSERT INTO GoiMonTaiBan(MaBan, MaMon, SoLuong) VALUES (1,1,1);
INSERT INTO GoiMonTaiBan(MaBan, MaMon, SoLuong) VALUES (1,4,1);
INSERT INTO GoiMonTaiBan(MaBan, MaMon, SoLuong) VALUES (3,2,1);
INSERT INTO GoiMonTaiBan(MaBan, MaMon, SoLuong) VALUES (3,4,1);
select * from GoiMonTaiBan
------------------------------------------------------------------------------------
SELECT * FROM NguoiDung
------------------------------------------------------------------------------------END------------------------------------------------------------------------------------
USE [master]
GO

-- 1. Xóa Database cũ nếu tồn tại để tránh lỗi bảng/ràng buộc đã tồn tại
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QUANLYTHUVIEN')
BEGIN
	ALTER DATABASE [QUANLYTHUVIEN] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [QUANLYTHUVIEN];
END
GO

-- 2. Tạo DB không fix cứng đường dẫn để chạy được ở bất kỳ máy tính/phiên bản SSMS nào
CREATE DATABASE [QUANLYTHUVIEN]
GO

USE [QUANLYTHUVIEN]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ================= CREATE TABLES =================

CREATE TABLE [dbo].[TheLoai](
	[IDTheLoai] [nchar](10) NOT NULL,
	[TenTheLoai] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_TheLoai] PRIMARY KEY CLUSTERED ([IDTheLoai] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DauSach](
	[IDDauSach] [nchar](10) NOT NULL,
	[IDTheLoai] [nchar](10) NULL,
	[TenDauSach] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_DauSach] PRIMARY KEY CLUSTERED ([IDDauSach] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ThongTinSach](
	[IDSach] [nchar](10) NOT NULL,
	[TenSach] [nvarchar](50) NOT NULL,
	[TacGia] [nvarchar](50) NOT NULL,
	[NamXuatBan] [int] NOT NULL,
	[NhaXuatBan] [nvarchar](50) NOT NULL,
	[NgayNhap] [date] NOT NULL,
	[GiaBan] [money] NOT NULL,
	[GiaThue] [money] NOT NULL,
	[TinhTrang] [nvarchar](20) NOT NULL,
	[IDDauSach] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED ([IDSach] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TheDocGia](
	[IDDocGia] [nchar](10) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[NgayLap] [date] NOT NULL,
	[LoaiDocGia] [nvarchar](20) NOT NULL,
	[TienNo] [money] NOT NULL,
 CONSTRAINT [PK_DocGia] PRIMARY KEY CLUSTERED ([IDDocGia] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[HoSoNhanVien](
	[IDNhanVien] [nchar](10) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[DienThoai] [nvarchar](20) NOT NULL,
	[BangCap] [nvarchar](20) NOT NULL,
	[BoPhan] [nvarchar](20) NOT NULL,
	[ChucVu] [nvarchar](20) NOT NULL,
	[MatKhau] [varchar](50) NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED ([IDNhanVien] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PhieuMuon](
	[IDPhieuMuon] [nchar](10) NOT NULL,
	[IDNguoiMuon] [nchar](10) NOT NULL,
 CONSTRAINT [PK_DanhSachMuon] PRIMARY KEY CLUSTERED ([IDPhieuMuon] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ChiTietMuon](
	[IDChiTietMuon] [nchar](10) NOT NULL,
	[IDPhieuMuon] [nchar](10) NOT NULL,
	[IDSach] [nchar](10) NOT NULL,
	[NgayMuon] [date] NOT NULL,
	[HanTra] [date] NOT NULL,
	[NgayTra] [date] NULL,
	[TinhTrangTra] [nvarchar](50) NOT NULL,
	[TienPhat] [money] NULL,
 CONSTRAINT [PK_ChiTietMuon] PRIMARY KEY CLUSTERED ([IDChiTietMuon] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GhiNhanMatSach](
	[IDPhieuMatSach] [nchar](10) NOT NULL,
	[IDNguoiMuon] [nchar](10) NOT NULL,
	[IDSach] [nchar](10) NOT NULL,
	[NgayGhiNhan] [date] NOT NULL,
	[TienPhat] [money] NULL,
 CONSTRAINT [PK_GhiNhanMatSach] PRIMARY KEY CLUSTERED ([IDPhieuMatSach] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ThanhLySach](
	[IDSach] [nchar](10) NOT NULL,
	[NgayThanhLy] [date] NOT NULL,
	[LyDoThanhLy] [nvarchar](20) NOT NULL,
	[TienPhat] [money] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_ThanhLySach] PRIMARY KEY CLUSTERED ([IDSach] ASC)
) ON [PRIMARY]
GO


-- ================= INSERT DATA =================

INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (N'TL001     ', N'Khoa học tự nhiên')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (N'TL002     ', N'Khoa học xã hội')
INSERT [dbo].[TheLoai] ([IDTheLoai], [TenTheLoai]) VALUES (N'TL003     ', N'Ngoại ngữ')
GO

INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS001     ', N'TL001     ', N'Mathematics')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS002     ', N'TL001     ', N'Physics')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS003     ', N'TL001     ', N'Chemistry')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS004     ', N'TL001     ', N'Biology')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS005     ', N'TL002     ', N'History')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS006     ', N'TL002     ', N'Geography')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS007     ', N'TL003     ', N'English')
INSERT [dbo].[DauSach] ([IDDauSach], [IDTheLoai], [TenDauSach]) VALUES (N'DS008     ', N'TL002     ', N'Literature')
GO

INSERT [dbo].[ThongTinSach] ([IDSach], [TenSach], [TacGia], [NamXuatBan], [NhaXuatBan], [NgayNhap], [GiaBan], [GiaThue], [TinhTrang], [IDDauSach]) VALUES (N'1         ', N'lalal', N'hahahaha', 2000, N'ahha', CAST(N'2026-04-23' AS Date), 5.0000, 5.0000, N'Sẵn sàng', N'DS001     ')
INSERT [dbo].[ThongTinSach] ([IDSach], [TenSach], [TacGia], [NamXuatBan], [NhaXuatBan], [NgayNhap], [GiaBan], [GiaThue], [TinhTrang], [IDDauSach]) VALUES (N'3         ', N'g', N'g', 2014, N'a', CAST(N'1995-01-02' AS Date), 4.0000, 3.0000, N'Đang mượn', N'DS002     ')
GO

INSERT [dbo].[TheDocGia] ([IDDocGia], [HoTen], [NgaySinh], [DiaChi], [Email], [NgayLap], [LoaiDocGia], [TienNo]) VALUES (N'dg1       ', N'ahahahhah', CAST(N'2005-09-15' AS Date), N'a', N'a@gmail.com', CAST(N'2026-04-23' AS Date), N'Whitelist', 0.0000)
GO

INSERT [dbo].[HoSoNhanVien] ([IDNhanVien], [HoTen], [NgaySinh], [DiaChi], [DienThoai], [BangCap], [BoPhan], [ChucVu], [MatKhau]) VALUES (N'BGD0001   ', N'Nguyễn Văn A', CAST(N'2000-01-01' AS Date), N'a', N'0', N'Tú Tài', N'Ban Giám Đốc', N'Giám Đốc', N'a')
INSERT [dbo].[HoSoNhanVien] ([IDNhanVien], [HoTen], [NgaySinh], [DiaChi], [DienThoai], [BangCap], [BoPhan], [ChucVu], [MatKhau]) VALUES (N'TK0001    ', N'Đoàn Văn C', CAST(N'2000-03-03' AS Date), N'c', N'2', N'Đại Học', N'Thủ Kho', N'Nhân Viên', N'c')
INSERT [dbo].[HoSoNhanVien] ([IDNhanVien], [HoTen], [NgaySinh], [DiaChi], [DienThoai], [BangCap], [BoPhan], [ChucVu], [MatKhau]) VALUES (N'TQ0001    ', N'Nguyễn Lê Văn D', CAST(N'2000-04-04' AS Date), N'd', N'3', N'Đại Học', N'Thủ Quỹ', N'Nhân Viên', N'd')
INSERT [dbo].[HoSoNhanVien] ([IDNhanVien], [HoTen], [NgaySinh], [DiaChi], [DienThoai], [BangCap], [BoPhan], [ChucVu], [MatKhau]) VALUES (N'TT0001    ', N'Lê Thị B', CAST(N'2000-02-02' AS Date), N'b', N'1', N'Cao Đẳng', N'Thủ Thư', N'Nhân Viên', N'b')
GO

INSERT [dbo].[PhieuMuon] ([IDPhieuMuon], [IDNguoiMuon]) VALUES (N'1         ', N'dg1       ')
GO

INSERT [dbo].[ChiTietMuon] ([IDChiTietMuon], [IDPhieuMuon], [IDSach], [NgayMuon], [HanTra], [NgayTra], [TinhTrangTra], [TienPhat]) VALUES (N'1         ', N'1         ', N'1         ', CAST(N'2026-04-23' AS Date), CAST(N'2026-04-25' AS Date), CAST(N'2026-04-25' AS Date), N'Hỏng', 1000000.0000)
GO

INSERT [dbo].[ThanhLySach] ([IDSach], [NgayThanhLy], [LyDoThanhLy], [TienPhat]) VALUES (N'1         ', CAST(N'2026-04-23' AS Date), N'LostByUser', 5.0000)
GO


-- ================= ADD FOREIGN KEYS =================

-- Thêm khoá ngoại giữa DauSach và TheLoai (Lỗi bị bỏ sót ở bản gốc)
ALTER TABLE [dbo].[DauSach] ADD CONSTRAINT [FK_DauSach_TheLoai] FOREIGN KEY([IDTheLoai]) REFERENCES [dbo].[TheLoai] ([IDTheLoai])
GO

ALTER TABLE [dbo].[ThongTinSach] ADD CONSTRAINT [FK_ThongTinSach_DauSach] FOREIGN KEY([IDDauSach]) REFERENCES [dbo].[DauSach] ([IDDauSach])
GO

ALTER TABLE [dbo].[PhieuMuon] ADD CONSTRAINT [FK_DanhSachMuon_TheDocGia] FOREIGN KEY([IDNguoiMuon]) REFERENCES [dbo].[TheDocGia] ([IDDocGia])
GO

ALTER TABLE [dbo].[ChiTietMuon] ADD CONSTRAINT [FK_ChiTietMuon_PhieuMuon] FOREIGN KEY([IDPhieuMuon]) REFERENCES [dbo].[PhieuMuon] ([IDPhieuMuon])
GO

ALTER TABLE [dbo].[ChiTietMuon] ADD CONSTRAINT [FK_ChiTietMuon_ThongTinSach] FOREIGN KEY([IDSach]) REFERENCES [dbo].[ThongTinSach] ([IDSach])
GO

ALTER TABLE [dbo].[GhiNhanMatSach] ADD CONSTRAINT [FK_GhiNhanMatSach_TheDocGia] FOREIGN KEY([IDNguoiMuon]) REFERENCES [dbo].[TheDocGia] ([IDDocGia])
GO

ALTER TABLE [dbo].[GhiNhanMatSach] ADD CONSTRAINT [FK_GhiNhanMatSach_ThongTinSach] FOREIGN KEY([IDSach]) REFERENCES [dbo].[ThongTinSach] ([IDSach])
GO

ALTER TABLE [dbo].[ThanhLySach] ADD CONSTRAINT [FK_ThanhLySach_ThongTinSach] FOREIGN KEY([IDSach]) REFERENCES [dbo].[ThongTinSach] ([IDSach])
GO


-- ================= ADD CHECK CONSTRAINTS =================

ALTER TABLE [dbo].[HoSoNhanVien] ADD CONSTRAINT [CK_NhanVien_BangCap] CHECK (([BangCap]=N'Tiến Sĩ' OR [BangCap]=N'Thạc Sĩ' OR [BangCap]=N'Đại Học' OR [BangCap]=N'Cao Đẳng' OR [BangCap]=N'Trung Cấp' OR [BangCap]=N'Tú Tài'))
GO

ALTER TABLE [dbo].[HoSoNhanVien] ADD CONSTRAINT [CK_NhanVien_BoPhan] CHECK (([BoPhan]=N'Ban Giám Đốc' OR [BoPhan]=N'Thủ Quỹ' OR [BoPhan]=N'Thủ Kho' OR [BoPhan]=N'Thủ Thư'))
GO

ALTER TABLE [dbo].[HoSoNhanVien] ADD CONSTRAINT [CK_NhanVien_ChucVu] CHECK (([ChucVu]=N'Nhân Viên' OR [ChucVu]=N'Phó Phòng' OR [ChucVu]=N'Trưởng Phòng' OR [ChucVu]=N'Phó Giám Đốc' OR [ChucVu]=N'Giám Đốc'))
GO

ALTER TABLE [dbo].[TheDocGia] ADD CONSTRAINT [CK_DocGia_LoaiDocGia] CHECK (([LoaiDocGia]='Blacklist' OR [LoaiDocGia]='Whitelist' OR [LoaiDocGia]='Graylist'))
GO

ALTER TABLE [dbo].[TheDocGia] ADD CONSTRAINT [CK_DocGia_Tuoi] CHECK ((datediff(year,[NgaySinh],getdate())>=(18) AND datediff(year,[NgaySinh],getdate())<=(55)))
GO
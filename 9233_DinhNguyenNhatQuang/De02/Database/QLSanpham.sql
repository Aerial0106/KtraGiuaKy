Create database QLSanpham
Use QLSanPham
Go
Create table LoaiSanPham
(
	MaLoai char(2) primary key not null,
	TenLoai nvarchar (30)
);

Create table SanPham
(
	MaSP char(6) primary key not null,
	TenSP nvarchar(30),
	Ngaynhap Datetime,
	MaLoai char(2)
);

Go

insert into LoaiSanPham(MaLoai, TenLoai)
values
	(1, N'Bánh kẹo'),
	(2, N'Nước ngọt');

insert into SanPham(MaSP, TenSP, Ngaynhap, MaLoai)
values
	('SP0001', N'Bánh quy bơ sữa', '2014-08-20', 1),
	('SP0002', N'Pepsi', '2014-06-01', 2),
	('SP0003', N'Bánh ốc quế', '2015-01-06', 1)
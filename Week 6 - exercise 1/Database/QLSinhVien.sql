IF DB_ID(N'QLSinhVien') IS NULL CREATE DATABASE QLSinhVien;
GO
USE QLSinhVien;
GO
CREATE TABLE Khoa(MaKhoa nvarchar(20) PRIMARY KEY,TenKhoa nvarchar(100) NOT NULL);
CREATE TABLE Lop(MaLop nvarchar(20) PRIMARY KEY,TenLop nvarchar(100) NOT NULL,MaKhoa nvarchar(20) NOT NULL REFERENCES Khoa(MaKhoa));
CREATE TABLE SinhVien(MaSinhVien nvarchar(20) PRIMARY KEY,HoTen nvarchar(100) NOT NULL,NgaySinh date NOT NULL,MaLop nvarchar(20) NOT NULL REFERENCES Lop(MaLop));
CREATE TABLE MonHoc(MaMonHoc nvarchar(20) PRIMARY KEY,TenMonHoc nvarchar(100) NOT NULL);
CREATE TABLE Diem(MaSinhVien nvarchar(20) NOT NULL REFERENCES SinhVien(MaSinhVien),MaMonHoc nvarchar(20) NOT NULL REFERENCES MonHoc(MaMonHoc),Diem decimal(4,2) NOT NULL CHECK(Diem BETWEEN 0 AND 10),PRIMARY KEY(MaSinhVien,MaMonHoc));
GO
INSERT INTO Khoa VALUES ('CNTT',N'Công nghệ thông tin'),('KT',N'Kinh tế'),('NN',N'Ngoại ngữ'),('DL',N'Du lịch'),('QTKD',N'Quản trị kinh doanh');
INSERT INTO Lop VALUES ('L01',N'Công nghệ phần mềm','CNTT'),('L02',N'Hệ thống thông tin','CNTT'),('L03',N'Kế toán','KT'),('L04',N'Ngôn ngữ Anh','NN'),('L05',N'Du lịch','DL');
INSERT INTO SinhVien VALUES ('SV01',N'Nguyễn Văn An','2004-03-12','L01'),('SV02',N'Trần Thị Bình','2004-06-21','L02'),('SV03',N'Lê Minh Khang','2003-12-01','L03'),('SV04',N'Phạm Thảo Vy','2005-01-10','L04'),('SV05',N'Đỗ Anh Tuấn','2004-09-15','L05');
INSERT INTO MonHoc VALUES ('MH01',N'Lập trình C#'),('MH02',N'Cơ sở dữ liệu'),('MH03',N'Cấu trúc dữ liệu'),('MH04',N'Mạng máy tính'),('MH05',N'Toán rời rạc');
INSERT INTO Diem VALUES ('SV01','MH01',8.5),('SV02','MH02',7),('SV03','MH03',9),('SV04','MH04',6.5),('SV05','MH05',8);

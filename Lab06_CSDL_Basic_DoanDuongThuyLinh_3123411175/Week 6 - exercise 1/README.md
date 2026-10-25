# TH06a – Tương tác cơ sở dữ liệu

Bốn project Windows Forms (.NET 8) thực hành ADO.NET và SQL Server.

## Cách chạy
1. Mở `Database/QLSinhVien.sql` bằng SQL Server Management Studio và chạy trên SQL Server của bạn (script dành cho database mới).
2. Sửa `Db.cs` trong **mỗi project**: thay `localhost` bằng tên SQL Server instance của bạn. Mặc định Windows Authentication, `TrustServerCertificate=True` dành cho môi trường học tập cục bộ.
3. Mở `TH06a_TuongTacCSDL.sln` trong Visual Studio 2022, cài .NET 8 SDK và workload .NET desktop development. Restore NuGet, chọn startup project, nhấn F5.
4. Bấm một hàng trong bảng để tự điền dữ liệu lên form. Thêm / Sửa / Xóa sẽ tải lại bảng.

**Lưu ý:** SQL Server phải đang chạy. Không dùng kết nối SQL của ví dụ cho môi trường production. Dữ liệu mẫu chỉ dùng để thực hành. Mọi truy vấn sử dụng tham số SQL.

**Quy tắc:** Sinh viên: xóa theo mã và họ tên, cập nhật một hoặc nhiều trường. Điểm: khóa ghép mã sinh viên + mã môn học, điểm 0–10. Lớp: cập nhật một hoặc hai trường. Môn học: đầy đủ hai trường khi thêm/sửa, chỉ mã khi xóa.

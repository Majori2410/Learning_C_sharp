
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai1_QuanLySinhVien_06b;

public class MainForm : Form
{
    private readonly ComboBox cboLop = new();
    private readonly TextBox txtMaSinhVien = new();
    private readonly TextBox txtHoTen = new();
    private readonly TextBox txtNgaySinh = new();

    private readonly Button btnThem = new();
    private readonly Button btnXoa = new();
    private readonly Button btnSua = new();

    public MainForm()
    {
        InitializeUI();
        Shown += MainForm_Shown;
    }

    // 1. THIET KE GIAO DIEN
    private void InitializeUI()
    {
        Text = "Quản lý sinh viên - TH06b";
        ClientSize = new Size(460, 335);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        string[] labels =
        {
            "Lớp",
            "Mã sinh viên",
            "Họ tên",
            "Ngày sinh"
        };

        for (int i = 0; i < labels.Length; i++)
        {
            Label label = new Label
            {
                Text = labels[i],
                AutoSize = true,
                Location = new Point(30, 40 + i * 55)
            };

            Controls.Add(label);
        }

        cboLop.Location = new Point(160, 35);
        cboLop.Size = new Size(260, 27);
        cboLop.DropDownStyle = ComboBoxStyle.DropDownList;

        txtMaSinhVien.Location = new Point(160, 90);
        txtMaSinhVien.Size = new Size(260, 27);
        txtMaSinhVien.MaxLength = 50;

        txtHoTen.Location = new Point(160, 145);
        txtHoTen.Size = new Size(260, 27);
        txtHoTen.MaxLength = 100;

        txtNgaySinh.Location = new Point(160, 200);
        txtNgaySinh.Size = new Size(260, 27);
        txtNgaySinh.MaxLength = 10;
        txtNgaySinh.PlaceholderText = "dd/MM/yyyy";

        btnThem.Text = "Thêm";
        btnThem.Location = new Point(55, 265);
        btnThem.Size = new Size(90, 35);

        btnXoa.Text = "Xóa";
        btnXoa.Location = new Point(185, 265);
        btnXoa.Size = new Size(90, 35);

        btnSua.Text = "Sửa";
        btnSua.Location = new Point(315, 265);
        btnSua.Size = new Size(90, 35);

        btnThem.Click += btnThem_Click;
        btnXoa.Click += btnXoa_Click;
        btnSua.Click += btnSua_Click;

        Controls.AddRange(new Control[]
        {
            cboLop,
            txtMaSinhVien,
            txtHoTen,
            txtNgaySinh,
            btnThem,
            btnXoa,
            btnSua
        });
    }

    // 2. TAI MA LOP
    private void MainForm_Shown(object? sender, EventArgs e)
    {
        try
        {
            cboLop.Items.Clear();

            foreach (string maLop in Db.LayDanhSachMaLop())
            {
                cboLop.Items.Add(maLop);
            }

            cboLop.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 3. PHUONG THUC HO TRO
    private void LamMoi()
    {
        cboLop.SelectedIndex = -1;
        txtMaSinhVien.Clear();
        txtHoTen.Clear();
        txtNgaySinh.Clear();
        txtMaSinhVien.Focus();
    }

    private void ThongBao(string message)
    {
        MessageBox.Show(
            message,
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void CanhBao(string message)
    {
        MessageBox.Show(
            message,
            "Dữ liệu không hợp lệ",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    private void BaoLoi(Exception ex)
    {
        MessageBox.Show(
            ex.Message,
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private bool ThuNgaySinh(
        string text, out DateTime ngaySinh)
    {
        return DateTime.TryParseExact(
            text.Trim(),
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out ngaySinh);
    }

    // Kiem tra tuoi chi dua vao nam sinh
    private bool Du17Tuoi(DateTime ngaySinh)
    {
        return DateTime.Today.Year - ngaySinh.Year >= 17;
    }

    private bool SinhVienTonTai(string maSV)
    {
        const string sql = """
            SELECT MaSinhVien
            FROM SinhVien
            WHERE MaSinhVien = @MaSinhVien
            """;

        return Db.Exists(
            sql,
            new SqlParameter("@MaSinhVien", maSV));
    }

    private bool LopTonTai(string maLop)
    {
        const string sql = """
            SELECT MaLop
            FROM Lop
            WHERE MaLop = @MaLop
            """;

        return Db.Exists(
            sql,
            new SqlParameter("@MaLop", maLop));
    }

    // Kiem tra sinh vien da co diem hay chua
    private bool SinhVienDaCoDiem(string maSV)
    {
        const string sql = """
            SELECT MaSinhVien
            FROM Diem
            WHERE MaSinhVien = @MaSinhVien
            """;

        return Db.Exists(
            sql,
            new SqlParameter("@MaSinhVien", maSV));
    }

    // 4. CHUC NANG THEM
    private void btnThem_Click(object? sender, EventArgs e)
    {
        string maLop = cboLop.Text.Trim();
        string maSV = txtMaSinhVien.Text.Trim();
        string hoTen = txtHoTen.Text.Trim();
        string ngayNhap = txtNgaySinh.Text.Trim();

        if (string.IsNullOrWhiteSpace(maLop) ||
            string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(hoTen) ||
            string.IsNullOrWhiteSpace(ngayNhap))
        {
            CanhBao("Vui lòng nhập đầy đủ thông tin!");
            return;
        }

        if (!ThuNgaySinh(ngayNhap, out DateTime ngaySinh))
        {
            CanhBao("Ngày sinh phải có định dạng dd/MM/yyyy!");
            return;
        }

        if (!Du17Tuoi(ngaySinh))
        {
            CanhBao("Sinh viên phải từ 17 tuổi trở lên!");
            return;
        }

        try
        {
            if (SinhVienTonTai(maSV))
            {
                CanhBao("Mã sinh viên đã tồn tại!");
                return;
            }

            if (!LopTonTai(maLop))
            {
                CanhBao("Mã lớp không tồn tại!");
                return;
            }

            const string sql = """
                INSERT INTO SinhVien
                    (MaSinhVien, HoTen, NgaySinh, MaLop)
                VALUES
                    (@MaSinhVien, @HoTen, @NgaySinh, @MaLop)
                """;

            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", SqlDbType.Date)
                {
                    Value = ngaySinh
                },
                new SqlParameter("@MaLop", maLop)
            );

            if (result > 0)
            {
                ThongBao("Thêm sinh viên thành công!");
                LamMoi();
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 5. CHUC NANG XOA
    private void btnXoa_Click(object? sender, EventArgs e)
    {
        string maSV = txtMaSinhVien.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSV))
        {
            CanhBao("Mã sinh viên không được để trống!");
            return;
        }

        try
        {
            if (!SinhVienTonTai(maSV))
            {
                CanhBao("Không tìm thấy sinh viên cần xóa!");
                return;
            }

            // Khong xoa sinh vien dang duoc tham chieu
            if (SinhVienDaCoDiem(maSV))
            {
                CanhBao(
                    "Sinh viên đã có dữ liệu điểm. Không thể xóa!");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên {maSV}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            const string sql = """
                DELETE FROM SinhVien
                WHERE MaSinhVien = @MaSinhVien
                """;

            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV)
            );

            if (result > 0)
            {
                ThongBao("Xóa sinh viên thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Không tìm thấy sinh viên cần xóa!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 6. CHUC NANG SUA
    private void btnSua_Click(object? sender, EventArgs e)
    {
        string maLop = cboLop.Text.Trim();
        string maSV = txtMaSinhVien.Text.Trim();
        string hoTen = txtHoTen.Text.Trim();
        string ngayNhap = txtNgaySinh.Text.Trim();

        if (string.IsNullOrWhiteSpace(maLop) ||
            string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(hoTen) ||
            string.IsNullOrWhiteSpace(ngayNhap))
        {
            CanhBao("Vui lòng nhập đầy đủ thông tin!");
            return;
        }

        if (!ThuNgaySinh(ngayNhap, out DateTime ngaySinh))
        {
            CanhBao("Ngày sinh phải có định dạng dd/MM/yyyy!");
            return;
        }

        if (!Du17Tuoi(ngaySinh))
        {
            CanhBao("Sinh viên phải từ 17 tuổi trở lên!");
            return;
        }

        try
        {
            if (!SinhVienTonTai(maSV))
            {
                CanhBao("Không tìm thấy sinh viên cần sửa!");
                return;
            }

            if (!LopTonTai(maLop))
            {
                CanhBao("Mã lớp không tồn tại!");
                return;
            }

            const string sql = """
                UPDATE SinhVien
                SET HoTen = @HoTen,
                    NgaySinh = @NgaySinh,
                    MaLop = @MaLop
                WHERE MaSinhVien = @MaSinhVien
                """;

            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", SqlDbType.Date)
                {
                    Value = ngaySinh
                },
                new SqlParameter("@MaLop", maLop)
            );

            if (result > 0)
            {
                ThongBao("Sửa sinh viên thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Không tìm thấy sinh viên cần sửa!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }
}


using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BaiNC1_QuanLySinhVien;

public class MainForm : Form
{
    private readonly TextBox txtMaLop = new();
    private readonly TextBox txtMaSinhVien = new();
    private readonly TextBox txtHoTen = new();
    private readonly TextBox txtNgaySinh = new();

    private readonly Button btnThem = new();
    private readonly Button btnXoa = new();
    private readonly Button btnSua = new();

    public MainForm()
    {
        InitializeUI();
    }

    // 1. THIET KE GIAO DIEN
    private void InitializeUI()
    {
        Text = "Quản lý sinh viên";
        ClientSize = new Size(470, 340);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        string[] labels =
        {
            "Mã lớp",
            "Mã sinh viên",
            "Tên sinh viên",
            "Ngày sinh"
        };

        TextBox[] textBoxes =
        {
            txtMaLop,
            txtMaSinhVien,
            txtHoTen,
            txtNgaySinh
        };

        for (int i = 0; i < labels.Length; i++)
        {
            Label label = new Label
            {
                Text = labels[i],
                AutoSize = true,
                Location = new Point(35, 35 + i * 55)
            };

            textBoxes[i].Location =
                new Point(170, 30 + i * 55);

            textBoxes[i].Size = new Size(260, 27);

            Controls.Add(label);
            Controls.Add(textBoxes[i]);
        }

        txtNgaySinh.PlaceholderText = "dd/MM/yyyy";

        txtMaLop.MaxLength = 50;
        txtMaSinhVien.MaxLength = 50;
        txtHoTen.MaxLength = 100;
        txtNgaySinh.MaxLength = 10;

        btnThem.Text = "Thêm";
        btnThem.Location = new Point(65, 270);
        btnThem.Size = new Size(90, 35);

        btnXoa.Text = "Xóa";
        btnXoa.Location = new Point(190, 270);
        btnXoa.Size = new Size(90, 35);

        btnSua.Text = "Sửa";
        btnSua.Location = new Point(315, 270);
        btnSua.Size = new Size(90, 35);

        btnThem.Click += btnThem_Click;
        btnXoa.Click += btnXoa_Click;
        btnSua.Click += btnSua_Click;

        Controls.AddRange(new Control[]
        {
            btnThem, btnXoa, btnSua
        });

        AcceptButton = btnThem;
    }

    // 2. PHUONG THUC HO TRO
    private void LamMoi()
    {
        txtMaLop.Clear();
        txtMaSinhVien.Clear();
        txtHoTen.Clear();
        txtNgaySinh.Clear();
        txtMaLop.Focus();
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

    private bool ThuNgaySinh(string text, out DateTime ngaySinh)
    {
        return DateTime.TryParseExact(
            text,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out ngaySinh);
    }

    // 3. CHUC NANG THEM
    private void btnThem_Click(object? sender, EventArgs e)
    {
        string maLop = txtMaLop.Text.Trim();
        string maSV = txtMaSinhVien.Text.Trim();
        string hoTen = txtHoTen.Text.Trim();
        string ngayNhap = txtNgaySinh.Text.Trim();

        if (string.IsNullOrWhiteSpace(maLop) ||
            string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(hoTen) ||
            string.IsNullOrWhiteSpace(ngayNhap))
        {
            CanhBao("Vui lòng nhập đầy đủ bốn trường!");
            return;
        }

        if (!ThuNgaySinh(ngayNhap, out DateTime ngaySinh))
        {
            CanhBao("Ngày sinh phải có định dạng dd/MM/yyyy!");
            return;
        }

        string sql = """
            INSERT INTO SinhVien
                (MaSinhVien, HoTen, NgaySinh, MaLop)
            VALUES
                (@MaSinhVien, @HoTen, @NgaySinh, @MaLop)
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh",
                    System.Data.SqlDbType.Date)
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
            else
            {
                CanhBao("Không thể thêm sinh viên!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 4. CHUC NANG XOA
    private void btnXoa_Click(object? sender, EventArgs e)
    {
        string maLop = txtMaLop.Text.Trim();
        string maSV = txtMaSinhVien.Text.Trim();
        string hoTen = txtHoTen.Text.Trim();
        string ngayNhap = txtNgaySinh.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(hoTen))
        {
            CanhBao(
                "Phải nhập Mã sinh viên và Tên sinh viên!");
            return;
        }

        if (!string.IsNullOrWhiteSpace(maLop) ||
            !string.IsNullOrWhiteSpace(ngayNhap))
        {
            CanhBao(
                "Khi xóa, Mã lớp và Ngày sinh phải để trống!");
            return;
        }

        DialogResult confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa sinh viên {maSV}?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        string sql = """
            DELETE FROM SinhVien
            WHERE MaSinhVien = @MaSinhVien
              AND HoTen = @HoTen
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@HoTen", hoTen)
            );

            if (result > 0)
            {
                ThongBao("Xóa sinh viên thành công!");
                LamMoi();
            }
            else
            {
                CanhBao(
                    "Không tìm thấy sinh viên có mã và tên tương ứng!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 5. CHUC NANG SUA
    private void btnSua_Click(object? sender, EventArgs e)
    {
        string maLop = txtMaLop.Text.Trim();
        string maSV = txtMaSinhVien.Text.Trim();
        string hoTen = txtHoTen.Text.Trim();
        string ngayNhap = txtNgaySinh.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSV))
        {
            CanhBao("Mã sinh viên không được để trống!");
            return;
        }

        if (string.IsNullOrWhiteSpace(maLop) &&
            string.IsNullOrWhiteSpace(hoTen) &&
            string.IsNullOrWhiteSpace(ngayNhap))
        {
            CanhBao(
                "Phải nhập ít nhất một trường cần sửa!");
            return;
        }

        // Chỉ cập nhật những trường đã nhập
        var updates = new System.Collections.Generic.List<string>();

        var parameters =
            new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@MaSinhVien", maSV)
            };

        if (!string.IsNullOrWhiteSpace(maLop))
        {
            updates.Add("MaLop = @MaLop");

            parameters.Add(
                new SqlParameter("@MaLop", maLop));
        }

        if (!string.IsNullOrWhiteSpace(hoTen))
        {
            updates.Add("HoTen = @HoTen");

            parameters.Add(
                new SqlParameter("@HoTen", hoTen));
        }

        if (!string.IsNullOrWhiteSpace(ngayNhap))
        {
            if (!ThuNgaySinh(ngayNhap, out DateTime ngaySinh))
            {
                CanhBao(
                    "Ngày sinh phải có định dạng dd/MM/yyyy!");
                return;
            }

            updates.Add("NgaySinh = @NgaySinh");

            parameters.Add(
                new SqlParameter(
                    "@NgaySinh", System.Data.SqlDbType.Date)
                {
                    Value = ngaySinh
                });
        }

        string sql =
            $"UPDATE SinhVien SET {string.Join(", ", updates)} " +
            "WHERE MaSinhVien = @MaSinhVien";

        try
        {
            int result = Db.Execute(
                sql,
                parameters.ToArray());

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

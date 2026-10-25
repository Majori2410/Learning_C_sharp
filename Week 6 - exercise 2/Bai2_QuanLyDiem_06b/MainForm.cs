
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai2_QuanLyDiem_06b;

public class MainForm : Form
{
    private readonly ComboBox cboSinhVien = new();
    private readonly ComboBox cboMonHoc = new();
    private readonly TextBox txtDiem = new();

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
        Text = "Quản lý điểm - TH06b";
        ClientSize = new Size(440, 290);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        Label lblSinhVien = new Label
        {
            Text = "Sinh viên",
            AutoSize = true,
            Location = new Point(30, 43)
        };

        Label lblMonHoc = new Label
        {
            Text = "Môn học",
            AutoSize = true,
            Location = new Point(30, 98)
        };

        Label lblDiem = new Label
        {
            Text = "Điểm",
            AutoSize = true,
            Location = new Point(30, 153)
        };

        cboSinhVien.Location = new Point(150, 38);
        cboSinhVien.Size = new Size(250, 28);
        cboSinhVien.DropDownStyle =
            ComboBoxStyle.DropDownList;

        cboMonHoc.Location = new Point(150, 93);
        cboMonHoc.Size = new Size(250, 28);
        cboMonHoc.DropDownStyle =
            ComboBoxStyle.DropDownList;

        txtDiem.Location = new Point(150, 148);
        txtDiem.Size = new Size(250, 28);
        txtDiem.PlaceholderText = "Nhap diem";

        btnThem.Text = "Thêm";
        btnThem.Location = new Point(45, 215);
        btnThem.Size = new Size(90, 35);

        btnXoa.Text = "Xóa";
        btnXoa.Location = new Point(175, 215);
        btnXoa.Size = new Size(90, 35);

        btnSua.Text = "Sửa";
        btnSua.Location = new Point(305, 215);
        btnSua.Size = new Size(90, 35);

        btnThem.Click += btnThem_Click;
        btnXoa.Click += btnXoa_Click;
        btnSua.Click += btnSua_Click;

        Controls.AddRange(new Control[]
        {
            lblSinhVien, lblMonHoc, lblDiem,
            cboSinhVien, cboMonHoc, txtDiem,
            btnThem, btnXoa, btnSua
        });
    }

    // 2. TAI DU LIEU LEN COMBOBOX
    private void MainForm_Shown(object? sender, EventArgs e)
    {
        try
        {
            cboSinhVien.Items.Clear();
            cboMonHoc.Items.Clear();

            var sinhViens = Db.LayDanhSach(
                "SELECT MaSinhVien FROM SinhVien ORDER BY MaSinhVien");

            var monHocs = Db.LayDanhSach(
                "SELECT MaMonHoc FROM MonHoc ORDER BY MaMonHoc");

            foreach (string maSV in sinhViens)
                cboSinhVien.Items.Add(maSV);

            foreach (string maMH in monHocs)
                cboMonHoc.Items.Add(maMH);

            cboSinhVien.SelectedIndex = -1;
            cboMonHoc.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 3. CAC PHUONG THUC HO TRO
    private void LamMoi()
    {
        cboSinhVien.SelectedIndex = -1;
        cboMonHoc.SelectedIndex = -1;
        txtDiem.Clear();
        cboSinhVien.Focus();
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

    // Kiem tra diem la so va nam trong khoang (0, 10)
    private bool ThuDiem(string text, out decimal diem)
    {
        bool hopLe = decimal.TryParse(
            text.Trim().Replace(',', '.'),
            NumberStyles.AllowDecimalPoint |
            NumberStyles.AllowLeadingSign,
            CultureInfo.InvariantCulture,
            out diem);

        return hopLe && diem > 0 && diem < 10;
    }

    private SqlParameter TaoThamSoDiem(decimal diem)
    {
        return new SqlParameter("@Diem", SqlDbType.Decimal)
        {
            Precision = 10,
            Scale = 2,
            Value = diem
        };
    }

    private bool KiemTraHaiMa()
    {
        if (cboSinhVien.SelectedIndex < 0 ||
            cboMonHoc.SelectedIndex < 0)
        {
            CanhBao("Vui lòng chọn Sinh viên và Môn học!");
            return false;
        }

        return true;
    }

    // Kiem tra khoa ngoai: MaSinhVien
    private bool SinhVienTonTai(string maSV)
    {
        const string sql = """
            SELECT MaSinhVien FROM SinhVien
            WHERE MaSinhVien = @MaSinhVien
            """;

        return Db.Exists(
            sql,
            new SqlParameter("@MaSinhVien", maSV));
    }

    // Kiem tra khoa ngoai: MaMonHoc
    private bool MonHocTonTai(string maMH)
    {
        const string sql = """
            SELECT MaMonHoc FROM MonHoc
            WHERE MaMonHoc = @MaMonHoc
            """;

        return Db.Exists(
            sql,
            new SqlParameter("@MaMonHoc", maMH));
    }

    // Kiem tra khoa chinh ghep cua bang Diem
    private bool DiemTonTai(string maSV, string maMH)
    {
        const string sql = """
            SELECT MaSinhVien FROM Diem
            WHERE MaSinhVien = @MaSinhVien
              AND MaMonHoc = @MaMonHoc
            """;

        return Db.Exists(
            sql,
            new SqlParameter("@MaSinhVien", maSV),
            new SqlParameter("@MaMonHoc", maMH));
    }

    // 4. CHUC NANG THEM
    private void btnThem_Click(object? sender, EventArgs e)
    {
        if (!KiemTraHaiMa())
            return;

        string maSV = cboSinhVien.Text;
        string maMH = cboMonHoc.Text;
        string diemNhap = txtDiem.Text.Trim();

        if (string.IsNullOrWhiteSpace(diemNhap))
        {
            CanhBao("Điểm không được để trống!");
            return;
        }

        if (!ThuDiem(diemNhap, out decimal diem))
        {
            CanhBao("Điểm phải là số trong khoảng (0, 10)!");
            return;
        }

        try
        {
            // Kiem tra khoa ngoai
            if (!SinhVienTonTai(maSV) || !MonHocTonTai(maMH))
            {
                CanhBao("Mã sinh viên hoặc mã môn học không tồn tại!");
                return;
            }

            // Kiem tra trung khoa chinh
            if (DiemTonTai(maSV, maMH))
            {
                CanhBao(
                    "Sinh viên đã có điểm ở môn học này!");
                return;
            }

            const string sql = """
                INSERT INTO Diem
                    (MaSinhVien, MaMonHoc, Diem)
                VALUES
                    (@MaSinhVien, @MaMonHoc, @Diem)
                """;

            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@MaMonHoc", maMH),
                TaoThamSoDiem(diem)
            );

            if (result > 0)
            {
                ThongBao("Thêm điểm thành công!");
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
        if (!KiemTraHaiMa())
            return;

        string maSV = cboSinhVien.Text;
        string maMH = cboMonHoc.Text;

        try
        {
            if (!DiemTonTai(maSV, maMH))
            {
                CanhBao("Không tìm thấy bản ghi điểm cần xóa!");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa điểm này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            const string sql = """
                DELETE FROM Diem
                WHERE MaSinhVien = @MaSinhVien
                  AND MaMonHoc = @MaMonHoc
                """;

            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@MaMonHoc", maMH)
            );

            if (result > 0)
            {
                ThongBao("Xóa điểm thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Bản ghi điểm không còn tồn tại!");
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
        if (!KiemTraHaiMa())
            return;

        string maSV = cboSinhVien.Text;
        string maMH = cboMonHoc.Text;
        string diemNhap = txtDiem.Text.Trim();

        if (string.IsNullOrWhiteSpace(diemNhap))
        {
            CanhBao("Vui lòng nhập điểm cần sửa!");
            return;
        }

        if (!ThuDiem(diemNhap, out decimal diem))
        {
            CanhBao("Điểm phải là số trong khoảng (0, 10)!");
            return;
        }

        try
        {
            if (!DiemTonTai(maSV, maMH))
            {
                CanhBao("Không tìm thấy bản ghi điểm cần sửa!");
                return;
            }

            const string sql = """
                UPDATE Diem
                SET Diem = @Diem
                WHERE MaSinhVien = @MaSinhVien
                  AND MaMonHoc = @MaMonHoc
                """;

            int result = Db.Execute(
                sql,
                new SqlParameter("@MaSinhVien", maSV),
                new SqlParameter("@MaMonHoc", maMH),
                TaoThamSoDiem(diem)
            );

            if (result > 0)
            {
                ThongBao("Sửa điểm thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Không tìm thấy bản ghi điểm cần sửa!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }
}

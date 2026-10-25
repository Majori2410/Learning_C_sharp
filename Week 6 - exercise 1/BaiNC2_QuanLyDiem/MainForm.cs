
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BaiNC2_QuanLyDiem;

public class MainForm : Form
{
    private readonly TextBox txtMaSinhVien = new();
    private readonly TextBox txtMaMonHoc = new();
    private readonly TextBox txtDiem = new();

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
        Text = "Quản lý điểm";
        ClientSize = new Size(440, 290);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        string[] labels =
        {
            "Mã sinh viên",
            "Mã môn học",
            "Điểm"
        };

        TextBox[] textBoxes =
        {
            txtMaSinhVien,
            txtMaMonHoc,
            txtDiem
        };

        for (int i = 0; i < labels.Length; i++)
        {
            Label label = new Label
            {
                Text = labels[i],
                AutoSize = true,
                Location = new Point(30, 40 + i * 55)
            };

            textBoxes[i].Location =
                new Point(160, 35 + i * 55);

            textBoxes[i].Size = new Size(245, 27);

            Controls.Add(label);
            Controls.Add(textBoxes[i]);
        }

        txtMaSinhVien.MaxLength = 50;
        txtMaMonHoc.MaxLength = 50;
        txtDiem.MaxLength = 20;

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
            btnThem, btnXoa, btnSua
        });

        AcceptButton = btnThem;
    }

    // 2. PHUONG THUC HO TRO
    private void LamMoi()
    {
        txtMaSinhVien.Clear();
        txtMaMonHoc.Clear();
        txtDiem.Clear();

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

    // Kiem tra diem co phai la so hay khong
    private bool ThuDiem(string text, out decimal diem)
    {
        text = text.Trim();

        return decimal.TryParse(
            text.Replace(',', '.'),
            NumberStyles.AllowLeadingSign |
            NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture,
            out diem);
    }

    // Tao tham so SQL cho diem
    private SqlParameter TaoThamSoDiem(decimal diem)
    {
        return new SqlParameter("@Diem", SqlDbType.Decimal)
        {
            Precision = 10,
            Scale = 2,
            Value = diem
        };
    }

    // 3. CHUC NANG THEM
    private void btnThem_Click(object? sender, EventArgs e)
    {
        string maSV = txtMaSinhVien.Text.Trim();
        string maMH = txtMaMonHoc.Text.Trim();
        string diemNhap = txtDiem.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(maMH) ||
            string.IsNullOrWhiteSpace(diemNhap))
        {
            CanhBao("Vui lòng nhập đầy đủ ba trường!");
            return;
        }

        if (!ThuDiem(diemNhap, out decimal diem))
        {
            CanhBao("Điểm phải là một giá trị số hợp lệ!");
            return;
        }

        string sql = """
            INSERT INTO Diem
                (MaSinhVien, MaMonHoc, Diem)
            VALUES
                (@MaSinhVien, @MaMonHoc, @Diem)
            """;

        try
        {
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
            else
            {
                CanhBao("Không thể thêm điểm!");
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
        string maSV = txtMaSinhVien.Text.Trim();
        string maMH = txtMaMonHoc.Text.Trim();
        string diemNhap = txtDiem.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(maMH))
        {
            CanhBao(
                "Mã sinh viên và Mã môn học không được để trống!");
            return;
        }

        if (!string.IsNullOrWhiteSpace(diemNhap))
        {
            CanhBao("Khi xóa, trường Điểm phải để trống!");
            return;
        }

        DialogResult confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa điểm của sinh viên {maSV} " +
            $"ở môn học {maMH}?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        string sql = """
            DELETE FROM Diem
            WHERE MaSinhVien = @MaSinhVien
              AND MaMonHoc = @MaMonHoc
            """;

        try
        {
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
                CanhBao("Không tìm thấy bản ghi điểm cần xóa!");
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
        string maSV = txtMaSinhVien.Text.Trim();
        string maMH = txtMaMonHoc.Text.Trim();
        string diemNhap = txtDiem.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSV) ||
            string.IsNullOrWhiteSpace(maMH) ||
            string.IsNullOrWhiteSpace(diemNhap))
        {
            CanhBao("Vui lòng nhập đầy đủ ba trường!");
            return;
        }

        if (!ThuDiem(diemNhap, out decimal diem))
        {
            CanhBao("Điểm phải là một giá trị số hợp lệ!");
            return;
        }

        string sql = """
            UPDATE Diem
            SET Diem = @Diem
            WHERE MaSinhVien = @MaSinhVien
              AND MaMonHoc = @MaMonHoc
            """;

        try
        {
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

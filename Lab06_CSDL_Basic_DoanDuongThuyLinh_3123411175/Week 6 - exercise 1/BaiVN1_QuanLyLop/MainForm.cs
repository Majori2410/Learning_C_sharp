
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BaiVN1_QuanLyLop;

public class MainForm : Form
{
    private readonly TextBox txtMaKhoa = new();
    private readonly TextBox txtMaLop = new();
    private readonly TextBox txtTenLop = new();

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
        Text = "Quản lý lớp";
        ClientSize = new Size(440, 290);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        string[] labels =
        {
            "Mã khoa",
            "Mã lớp",
            "Tên lớp"
        };

        TextBox[] textBoxes =
        {
            txtMaKhoa,
            txtMaLop,
            txtTenLop
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

        txtMaKhoa.MaxLength = 50;
        txtMaLop.MaxLength = 50;
        txtTenLop.MaxLength = 200;

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

    // 2. CAC PHUONG THUC HO TRO
    private void LamMoi()
    {
        txtMaKhoa.Clear();
        txtMaLop.Clear();
        txtTenLop.Clear();

        txtMaKhoa.Focus();
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

    // 3. CHUC NANG THEM
    private void btnThem_Click(object? sender, EventArgs e)
    {
        string maKhoa = txtMaKhoa.Text.Trim();
        string maLop = txtMaLop.Text.Trim();
        string tenLop = txtTenLop.Text.Trim();

        if (string.IsNullOrWhiteSpace(maKhoa) ||
            string.IsNullOrWhiteSpace(maLop) ||
            string.IsNullOrWhiteSpace(tenLop))
        {
            CanhBao("Vui lòng nhập đầy đủ ba trường!");
            return;
        }

        string sql = """
            INSERT INTO Lop (MaLop, TenLop, MaKhoa)
            VALUES (@MaLop, @TenLop, @MaKhoa)
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaLop", maLop),
                new SqlParameter("@TenLop", tenLop),
                new SqlParameter("@MaKhoa", maKhoa)
            );

            if (result > 0)
            {
                ThongBao("Thêm lớp thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Không thể thêm lớp!");
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
        string maKhoa = txtMaKhoa.Text.Trim();
        string maLop = txtMaLop.Text.Trim();
        string tenLop = txtTenLop.Text.Trim();

        if (string.IsNullOrWhiteSpace(maLop))
        {
            CanhBao("Mã lớp không được để trống!");
            return;
        }

        if (!string.IsNullOrWhiteSpace(maKhoa) ||
            !string.IsNullOrWhiteSpace(tenLop))
        {
            CanhBao(
                "Khi xóa, Mã khoa và Tên lớp phải để trống!");
            return;
        }

        DialogResult confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa lớp {maLop}?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        string sql = """
            DELETE FROM Lop
            WHERE MaLop = @MaLop
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaLop", maLop)
            );

            if (result > 0)
            {
                ThongBao("Xóa lớp thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Không tìm thấy lớp cần xóa!");
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
        string maKhoa = txtMaKhoa.Text.Trim();
        string maLop = txtMaLop.Text.Trim();
        string tenLop = txtTenLop.Text.Trim();

        if (string.IsNullOrWhiteSpace(maLop))
        {
            CanhBao("Mã lớp không được để trống!");
            return;
        }

        if (string.IsNullOrWhiteSpace(maKhoa) &&
            string.IsNullOrWhiteSpace(tenLop))
        {
            CanhBao(
                "Phải nhập ít nhất Mã khoa hoặc Tên lớp cần sửa!");
            return;
        }

        // Chi cap nhat cac truong duoc nhap
        List<string> updates = new();

        List<SqlParameter> parameters = new()
        {
            new SqlParameter("@MaLop", maLop)
        };

        if (!string.IsNullOrWhiteSpace(maKhoa))
        {
            updates.Add("MaKhoa = @MaKhoa");

            parameters.Add(
                new SqlParameter("@MaKhoa", maKhoa));
        }

        if (!string.IsNullOrWhiteSpace(tenLop))
        {
            updates.Add("TenLop = @TenLop");

            parameters.Add(
                new SqlParameter("@TenLop", tenLop));
        }

        string sql =
            $"UPDATE Lop SET {string.Join(", ", updates)} " +
            "WHERE MaLop = @MaLop";

        try
        {
            int result = Db.Execute(
                sql,
                parameters.ToArray());

            if (result > 0)
            {
                ThongBao("Sửa lớp thành công!");
                LamMoi();
            }
            else
            {
                CanhBao("Không tìm thấy lớp cần sửa!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }
}

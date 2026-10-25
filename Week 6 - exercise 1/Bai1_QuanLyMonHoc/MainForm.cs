
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai1_QuanLyMonHoc;

public class MainForm : Form
{
    private readonly TextBox txtMaMonHoc = new();
    private readonly TextBox txtTenMonHoc = new();

    private readonly Button btnThem = new();
    private readonly Button btnXoa = new();
    private readonly Button btnSua = new();

    public MainForm()
    {
        InitializeUI();
    }

    // ==============================
    // 1. THIET KE GIAO DIEN
    // ==============================

    private void InitializeUI()
    {
        Text = "Quản lý môn học";
        ClientSize = new Size(400, 220);
        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        // Ma mon hoc
        Label lblMaMonHoc = new Label
        {
            Text = "Mã môn học",
            AutoSize = true,
            Location = new Point(25, 35)
        };

        txtMaMonHoc.Location = new Point(145, 30);
        txtMaMonHoc.Size = new Size(225, 27);
        txtMaMonHoc.MaxLength = 50;

        // Ten mon hoc
        Label lblTenMonHoc = new Label
        {
            Text = "Tên môn học",
            AutoSize = true,
            Location = new Point(25, 85)
        };

        txtTenMonHoc.Location = new Point(145, 80);
        txtTenMonHoc.Size = new Size(225, 27);
        txtTenMonHoc.MaxLength = 200;

        // Button Them
        btnThem.Text = "Thêm";
        btnThem.Location = new Point(45, 145);
        btnThem.Size = new Size(90, 35);

        // Button Xoa
        btnXoa.Text = "Xóa";
        btnXoa.Location = new Point(155, 145);
        btnXoa.Size = new Size(90, 35);

        // Button Sua
        btnSua.Text = "Sửa";
        btnSua.Location = new Point(265, 145);
        btnSua.Size = new Size(90, 35);

        // Dang ky su kien
        btnThem.Click += btnThem_Click;
        btnXoa.Click += btnXoa_Click;
        btnSua.Click += btnSua_Click;

        // Them control vao Form
        Controls.AddRange(new Control[]
        {
            lblMaMonHoc,
            txtMaMonHoc,
            lblTenMonHoc,
            txtTenMonHoc,
            btnThem,
            btnXoa,
            btnSua
        });
    }

    // ==============================
    // 2. CAC PHUONG THUC HO TRO
    // ==============================

    private void LamMoi()
    {
        txtMaMonHoc.Clear();
        txtTenMonHoc.Clear();
        txtMaMonHoc.Focus();
    }

    private void ThongBao(string message)
    {
        MessageBox.Show(
            message,
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void BaoLoi(Exception ex)
    {
        MessageBox.Show(
            ex.Message,
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    // ==============================
    // 3. CHUC NANG THEM
    // ==============================

    private void btnThem_Click(object? sender, EventArgs e)
    {
        string ma = txtMaMonHoc.Text.Trim();
        string ten = txtTenMonHoc.Text.Trim();

        if (string.IsNullOrWhiteSpace(ma) ||
            string.IsNullOrWhiteSpace(ten))
        {
            ThongBao(
                "Mã môn học và tên môn học không được để trống!");
            return;
        }

        string sql = """
            INSERT INTO MonHoc (MaMonHoc, TenMonHoc)
            VALUES (@MaMonHoc, @TenMonHoc)
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaMonHoc", ma),
                new SqlParameter("@TenMonHoc", ten)
            );

            if (result > 0)
            {
                ThongBao("Thêm môn học thành công!");
                LamMoi();
            }
            else
            {
                ThongBao("Thêm môn học thất bại!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // ==============================
    // 4. CHUC NANG XOA
    // ==============================

    private void btnXoa_Click(object? sender, EventArgs e)
    {
        string ma = txtMaMonHoc.Text.Trim();
        string ten = txtTenMonHoc.Text.Trim();

        if (string.IsNullOrWhiteSpace(ma))
        {
            ThongBao("Mã môn học không được để trống!");
            return;
        }

        if (!string.IsNullOrWhiteSpace(ten))
        {
            ThongBao(
                "Khi xóa, tên môn học phải để trống!");
            return;
        }

        DialogResult confirm = MessageBox.Show(
            $"Bạn có chắc muốn xóa môn học {ma}?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        string sql = """
            DELETE FROM MonHoc
            WHERE MaMonHoc = @MaMonHoc
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaMonHoc", ma)
            );

            if (result > 0)
            {
                ThongBao("Xóa môn học thành công!");
                LamMoi();
            }
            else
            {
                ThongBao("Không tìm thấy môn học cần xóa!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // ==============================
    // 5. CHUC NANG SUA
    // ==============================

    private void btnSua_Click(object? sender, EventArgs e)
    {
        string ma = txtMaMonHoc.Text.Trim();
        string ten = txtTenMonHoc.Text.Trim();

        if (string.IsNullOrWhiteSpace(ma) ||
            string.IsNullOrWhiteSpace(ten))
        {
            ThongBao(
                "Mã môn học và tên môn học không được để trống!");
            return;
        }

        string sql = """
            UPDATE MonHoc
            SET TenMonHoc = @TenMonHoc
            WHERE MaMonHoc = @MaMonHoc
            """;

        try
        {
            int result = Db.Execute(
                sql,
                new SqlParameter("@MaMonHoc", ma),
                new SqlParameter("@TenMonHoc", ten)
            );

            if (result > 0)
            {
                ThongBao("Sửa môn học thành công!");
                LamMoi();
            }
            else
            {
                ThongBao("Không tìm thấy môn học cần sửa!");
            }
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }
}

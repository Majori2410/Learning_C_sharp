
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiVN1_QuanLyCuaHang_06b;

// FORM CHINH
public class MainForm : Form
{
    public MainForm()
    {
        Text = "Quản lý cửa hàng";
        ClientSize = new Size(540, 330);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        MenuStrip menu = new MenuStrip();

        ToolStripMenuItem mnuTonKho =
            new ToolStripMenuItem("Tạo tồn kho");

        ToolStripMenuItem mnuSanPham =
            new ToolStripMenuItem("Thêm sản phẩm");

        ToolStripMenuItem mnuBanHang =
            new ToolStripMenuItem("Bán hàng");

        menu.Items.AddRange(new ToolStripItem[]
        {
            mnuTonKho, mnuSanPham, mnuBanHang
        });

        mnuTonKho.Click += (_, _) => TaoTonKho();

        mnuSanPham.Click += (_, _) =>
        {
            using var form = new FormSanPham();
            form.ShowDialog(this);
        };

        mnuBanHang.Click += (_, _) =>
        {
            using var form = new FormBanHang();
            form.ShowDialog(this);
        };

        Label title = new Label
        {
            Text = "QUẢN LÝ CỬA HÀNG",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        Controls.Add(title);
        Controls.Add(menu);
        MainMenuStrip = menu;
    }

    private void TaoTonKho()
    {
        try
        {
            int count = Db.TaoTonKho();

            MessageBox.Show(
                count > 0
                    ? $"Đã tạo tồn kho cho {count} sản phẩm."
                    : "Tồn kho hôm nay đã được tạo.",
                "Thông báo");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }
}

// FORM THEM SAN PHAM
public class FormSanPham : Form
{
    private readonly TextBox txtTenSP = new();

    public FormSanPham()
    {
        Text = "Thêm sản phẩm mới";
        ClientSize = new Size(380, 210);
        StartPosition = FormStartPosition.CenterParent;
        Font = new Font("Segoe UI", 10);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Label lblTenSP = new Label
        {
            Text = "Tên sản phẩm",
            AutoSize = true,
            Location = new Point(25, 50)
        };

        txtTenSP.Location = new Point(150, 45);
        txtTenSP.Size = new Size(200, 28);
        txtTenSP.MaxLength = 100;

        Button btnThem = new Button
        {
            Text = "Thêm",
            Location = new Point(85, 125),
            Size = new Size(90, 35)
        };

        Button btnThoat = new Button
        {
            Text = "Thoát",
            Location = new Point(215, 125),
            Size = new Size(90, 35)
        };

        btnThem.Click += BtnThem_Click;
        btnThoat.Click += (_, _) => Close();

        Controls.AddRange(new Control[]
        {
            lblTenSP, txtTenSP, btnThem, btnThoat
        });
    }

    private void BtnThem_Click(object? sender, EventArgs e)
    {
        string tenSP = txtTenSP.Text.Trim();

        if (string.IsNullOrWhiteSpace(tenSP))
        {
            MessageBox.Show("Vui lòng nhập tên sản phẩm!");
            return;
        }

        try
        {
            int maSP = Db.ThemSanPham(tenSP);

            MessageBox.Show(
                $"Thêm sản phẩm thành công! Mã SP: {maSP}");

            txtTenSP.Clear();
            txtTenSP.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }
}

// FORM BAN HANG
public class FormBanHang : Form
{
    private readonly TextBox txtMaHD = new();
    private readonly ComboBox cboSanPham = new();
    private readonly TextBox txtSoLuong = new();

    private readonly Button btnTaoHD = new();
    private readonly Button btnThem = new();
    private readonly Button btnHuy = new();
    private readonly Button btnThanhToan = new();

    private int? maHoaDon;

    public FormBanHang()
    {
        InitializeUI();
        Load += (_, _) => TaiSanPham();
    }

    private void InitializeUI()
    {
        Text = "Bán hàng";
        ClientSize = new Size(490, 420);
        StartPosition = FormStartPosition.CenterParent;
        Font = new Font("Segoe UI", 10);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        btnTaoHD.Text = "Tạo hóa đơn mới";
        btnTaoHD.Location = new Point(285, 25);
        btnTaoHD.Size = new Size(160, 35);

        GroupBox group = new GroupBox
        {
            Text = "Chi tiết hóa đơn",
            Location = new Point(35, 80),
            Size = new Size(410, 235)
        };

        Label lblMaHD = new Label
        {
            Text = "Mã hóa đơn",
            AutoSize = true,
            Location = new Point(25, 40)
        };

        Label lblSanPham = new Label
        {
            Text = "Sản phẩm",
            AutoSize = true,
            Location = new Point(25, 95)
        };

        Label lblSoLuong = new Label
        {
            Text = "Số lượng",
            AutoSize = true,
            Location = new Point(25, 150)
        };

        txtMaHD.Location = new Point(155, 35);
        txtMaHD.Size = new Size(220, 28);
        txtMaHD.ReadOnly = true;

        cboSanPham.Location = new Point(155, 90);
        cboSanPham.Size = new Size(220, 28);
        cboSanPham.DropDownStyle =
            ComboBoxStyle.DropDownList;

        txtSoLuong.Location = new Point(155, 145);
        txtSoLuong.Size = new Size(220, 28);

        btnThem.Text = "Thêm";
        btnThem.Location = new Point(155, 190);
        btnThem.Size = new Size(90, 32);

        btnHuy.Text = "Hủy";
        btnHuy.Location = new Point(285, 190);
        btnHuy.Size = new Size(90, 32);

        btnThanhToan.Text = "Thanh toán";
        btnThanhToan.Location = new Point(315, 335);
        btnThanhToan.Size = new Size(130, 35);

        group.Controls.AddRange(new Control[]
        {
            lblMaHD, lblSanPham, lblSoLuong,
            txtMaHD, cboSanPham, txtSoLuong,
            btnThem, btnHuy
        });

        Controls.AddRange(new Control[]
        {
            btnTaoHD, group, btnThanhToan
        });

        btnTaoHD.Click += BtnTaoHD_Click;
        btnThem.Click += BtnThem_Click;
        btnHuy.Click += BtnHuy_Click;
        btnThanhToan.Click += BtnThanhToan_Click;

        BatNhapLieu(false);
    }

    private void BatNhapLieu(bool enabled)
    {
        cboSanPham.Enabled = enabled;
        txtSoLuong.Enabled = enabled;
        btnThem.Enabled = enabled;
        btnHuy.Enabled = enabled;
        btnThanhToan.Enabled = enabled;
        btnTaoHD.Enabled = !enabled;
    }

    private void TaiSanPham()
    {
        try
        {
            cboSanPham.Items.Clear();

            foreach (var sp in Db.LayDanhSachSanPham())
            {
                cboSanPham.Items.Add(
                    new SanPhamItem(sp.MaSP, sp.TenSP));
            }

            cboSanPham.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }

    // Tao hoa don moi
    private void BtnTaoHD_Click(object? sender, EventArgs e)
    {
        try
        {
            maHoaDon = Db.TaoHoaDon();
            txtMaHD.Text = maHoaDon.ToString();

            BatNhapLieu(true);

            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Clear();

            MessageBox.Show(
                $"Đã tạo hóa đơn số {maHoaDon}!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }

    // Them san pham vao hoa don
    private void BtnThem_Click(object? sender, EventArgs e)
    {
        if (maHoaDon == null)
            return;

        if (cboSanPham.SelectedItem is not SanPhamItem sp)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm!");
            return;
        }

        if (!int.TryParse(
            txtSoLuong.Text.Trim(), out int soLuong)
            || soLuong <= 0)
        {
            MessageBox.Show(
                "Số lượng phải là số nguyên lớn hơn 0!");
            return;
        }

        try
        {
            Db.ThemChiTietHoaDon(
                maHoaDon.Value, sp.MaSP, soLuong);

            MessageBox.Show(
                "Thêm sản phẩm vào hóa đơn thành công!");

            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }

    // Huy hoa don
    private void BtnHuy_Click(object? sender, EventArgs e)
    {
        if (maHoaDon == null)
            return;

        DialogResult confirm = MessageBox.Show(
            "Bạn có chắc muốn hủy hóa đơn?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            Db.HuyHoaDon(maHoaDon.Value);
            KetThucHoaDon();

            MessageBox.Show("Đã hủy hóa đơn.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }

    // Thanh toan
    private void BtnThanhToan_Click(object? sender, EventArgs e)
    {
        if (maHoaDon == null)
            return;

        try
        {
            Db.ThanhToan(maHoaDon.Value);
            KetThucHoaDon();

            MessageBox.Show("Thanh toán thành công!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi");
        }
    }

    private void KetThucHoaDon()
    {
        maHoaDon = null;
        txtMaHD.Clear();
        txtSoLuong.Clear();
        cboSanPham.SelectedIndex = -1;
        BatNhapLieu(false);
    }

    // Hien thi ten san pham va luu ma tuong ung
    private sealed class SanPhamItem
    {
        public int MaSP { get; }
        public string TenSP { get; }

        public SanPhamItem(int maSP, string tenSP)
        {
            MaSP = maSP;
            TenSP = tenSP;
        }

        public override string ToString()
        {
            return $"{MaSP} - {TenSP}";
        }
    }
}


using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiNC1_DanhSachLop_06b;

public class MainForm : Form
{
    private readonly ListView lvDanhSachLop = new();
    private readonly Button btnHienThi = new();
    private readonly Button btnThoat = new();

    public MainForm()
    {
        InitializeUI();
    }

    // 1. THIET KE GIAO DIEN
    private void InitializeUI()
    {
        Text = "Danh sách Lớp";
        ClientSize = new Size(650, 480);
        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        // Button Hien thi du lieu
        btnHienThi.Text = "Hiển thị dữ liệu";
        btnHienThi.Location = new Point(25, 20);
        btnHienThi.Size = new Size(135, 35);

        btnHienThi.Click += btnHienThi_Click;

        // ListView danh sach lop
        lvDanhSachLop.Location = new Point(25, 70);
        lvDanhSachLop.Size = new Size(600, 335);

        lvDanhSachLop.View = View.Details;
        lvDanhSachLop.FullRowSelect = true;
        lvDanhSachLop.GridLines = true;
        lvDanhSachLop.MultiSelect = false;

        lvDanhSachLop.HideSelection = false;
        lvDanhSachLop.UseCompatibleStateImageBehavior = false;

        // Khai bao cac cot
        lvDanhSachLop.Columns.Add("STT", 60);
        lvDanhSachLop.Columns.Add("Mã lớp", 140);
        lvDanhSachLop.Columns.Add("Tên lớp", 255);
        lvDanhSachLop.Columns.Add("Mã khoa", 120);

        // Button Thoat
        btnThoat.Text = "Thoát";
        btnThoat.Location = new Point(525, 420);
        btnThoat.Size = new Size(100, 35);

        btnThoat.Click += btnThoat_Click;

        // Them controls vao Form
        Controls.AddRange(new Control[]
        {
            btnHienThi,
            lvDanhSachLop,
            btnThoat
        });
    }

    // 2. CHUC NANG HIEN THI DU LIEU
    private void btnHienThi_Click(object? sender, EventArgs e)
    {
        try
        {
            // Xoa du lieu cu truoc khi hien thi
            lvDanhSachLop.Items.Clear();

            var danhSach = Db.LayDanhSachLop();

            int stt = 1;

            foreach (var lop in danhSach)
            {
                ListViewItem item =
                    new ListViewItem(stt.ToString());

                item.SubItems.Add(lop.MaLop);
                item.SubItems.Add(lop.TenLop);
                item.SubItems.Add(lop.MaKhoa);

                lvDanhSachLop.Items.Add(item);

                stt++;
            }

            if (danhSach.Count == 0)
            {
                MessageBox.Show(
                    "Không có dữ liệu lớp học!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không thể tải danh sách lớp!\n" + ex.Message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    // 3. CHUC NANG THOAT
    private void btnThoat_Click(object? sender, EventArgs e)
    {
        Close();
    }
}

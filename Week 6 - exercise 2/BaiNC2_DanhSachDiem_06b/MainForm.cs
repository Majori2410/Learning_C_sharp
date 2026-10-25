
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BaiNC2_DanhSachDiem_06b;

public class MainForm : Form
{
    private readonly ComboBox cboMonHoc = new();
    private readonly Button btnLocDuLieu = new();

    private readonly ListView lvDanhSachDiem = new();

    private readonly TextBox txtTongDiem = new();

    public MainForm()
    {
        InitializeUI();
        Load += MainForm_Load;
    }

    // 1. THIET KE GIAO DIEN
    private void InitializeUI()
    {
        Text = "Danh sách sinh viên";
        ClientSize = new Size(760, 540);
        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        Font = new Font("Segoe UI", 10F);
        BackColor = SystemColors.Control;

        // Label Mon hoc
        Label lblMonHoc = new Label
        {
            Text = "Môn học",
            AutoSize = true,
            Location = new Point(35, 38)
        };

        // ComboBox Mon hoc
        cboMonHoc.Location = new Point(135, 32);
        cboMonHoc.Size = new Size(285, 28);
        cboMonHoc.DropDownStyle =
            ComboBoxStyle.DropDownList;

        // Button Loc du lieu
        btnLocDuLieu.Text = "Lọc dữ liệu";
        btnLocDuLieu.Location = new Point(455, 30);
        btnLocDuLieu.Size = new Size(115, 35);

        btnLocDuLieu.Click += btnLocDuLieu_Click;

        // ListView
        lvDanhSachDiem.Location = new Point(35, 85);
        lvDanhSachDiem.Size = new Size(690, 365);

        lvDanhSachDiem.View = View.Details;
        lvDanhSachDiem.FullRowSelect = true;
        lvDanhSachDiem.GridLines = true;
        lvDanhSachDiem.MultiSelect = false;

        lvDanhSachDiem.HideSelection = false;

        // Cac cot cua ListView
        lvDanhSachDiem.Columns.Add("STT", 55);
        lvDanhSachDiem.Columns.Add("Tên SV", 165);
        lvDanhSachDiem.Columns.Add("Ngày sinh", 125);
        lvDanhSachDiem.Columns.Add("Tên môn học", 225);
        lvDanhSachDiem.Columns.Add("Điểm", 100);

        // Label Tong diem
        Label lblTongDiem = new Label
        {
            Text = "Tổng điểm",
            AutoSize = true,
            Location = new Point(475, 477)
        };

        // TextBox Tong diem
        txtTongDiem.Location = new Point(575, 472);
        txtTongDiem.Size = new Size(150, 28);
        txtTongDiem.ReadOnly = true;
        txtTongDiem.TextAlign =
            HorizontalAlignment.Right;

        // Them controls vao Form
        Controls.AddRange(new Control[]
        {
            lblMonHoc,
            cboMonHoc,
            btnLocDuLieu,
            lvDanhSachDiem,
            lblTongDiem,
            txtTongDiem
        });
    }

    // 2. LOAD FORM
    private void MainForm_Load(object? sender, EventArgs e)
    {
        try
        {
            cboMonHoc.Items.Clear();

            // Lua chon rong dai dien cho tat ca mon hoc
            cboMonHoc.Items.Add("");

            var danhSachMonHoc =
                Db.LayDanhSachMonHoc();

            foreach (string maMonHoc in danhSachMonHoc)
            {
                cboMonHoc.Items.Add(maMonHoc);
            }

            // Mac dinh hien thi tat ca
            cboMonHoc.SelectedIndex = 0;

            HienThiDanhSach(null);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 3. HIEN THI DANH SACH VA TINH TONG DIEM
    private void HienThiDanhSach(string? maMonHoc)
    {
        var danhSach = Db.LayDanhSachDiem(maMonHoc);

        lvDanhSachDiem.BeginUpdate();

        try
        {
            lvDanhSachDiem.Items.Clear();

            decimal tongDiem = 0;
            int stt = 1;

            foreach (var item in danhSach)
            {
                ListViewItem row =
                    new ListViewItem(stt.ToString());

                row.SubItems.Add(item.HoTen);

                row.SubItems.Add(
                    item.NgaySinh.ToString("dd/MM/yyyy"));

                row.SubItems.Add(item.TenMonHoc);

                row.SubItems.Add(
                    item.Diem.ToString(
                        "0.##",
                        CultureInfo.GetCultureInfo("vi-VN")));

                lvDanhSachDiem.Items.Add(row);

                tongDiem += item.Diem;
                stt++;
            }

            txtTongDiem.Text = tongDiem.ToString(
                "0.##",
                CultureInfo.GetCultureInfo("vi-VN"));
        }
        finally
        {
            lvDanhSachDiem.EndUpdate();
        }
    }

    // 4. LOC DU LIEU THEO MON HOC
    private void btnLocDuLieu_Click(
        object? sender, EventArgs e)
    {
        try
        {
            string? maMonHoc = cboMonHoc.SelectedItem?
                .ToString();

            HienThiDanhSach(maMonHoc);
        }
        catch (Exception ex)
        {
            BaoLoi(ex);
        }
    }

    // 5. THONG BAO LOI
    private void BaoLoi(Exception ex)
    {
        MessageBox.Show(
            "Không thể tải dữ liệu!\n" + ex.Message,
            "Lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}

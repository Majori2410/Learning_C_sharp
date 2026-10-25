
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BaiNC2_DanhSachDiem_06b;

internal static class Db
{
    private const string ConnectionString =
        @"Server=(local)\SQLEXPRESS;" +
        @"Database=QLSinhVien;" +
        @"Integrated Security=True;" +
        @"TrustServerCertificate=True;";

    // Lay danh sach ma mon hoc cho ComboBox
    public static List<string> LayDanhSachMonHoc()
    {
        List<string> danhSach = new();

        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        const string sql = """
            SELECT MaMonHoc
            FROM MonHoc
            ORDER BY MaMonHoc
            """;

        using SqlCommand cmd = new SqlCommand(sql, conn);

        conn.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            danhSach.Add(
                reader["MaMonHoc"].ToString() ?? "");
        }

        return danhSach;
    }

    // Lay danh sach diem theo mon hoc
    // maMonHoc = null: hien thi tat ca
    public static List<DiemSinhVien> LayDanhSachDiem(
        string? maMonHoc = null)
    {
        List<DiemSinhVien> danhSach = new();

        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        const string sql = """
            SELECT
                SV.HoTen,
                SV.NgaySinh,
                MH.TenMonHoc,
                D.Diem
            FROM Diem AS D
            INNER JOIN SinhVien AS SV
                ON D.MaSinhVien = SV.MaSinhVien
            INNER JOIN MonHoc AS MH
                ON D.MaMonHoc = MH.MaMonHoc
            WHERE (@MaMonHoc IS NULL
                OR D.MaMonHoc = @MaMonHoc)
            ORDER BY SV.HoTen, MH.TenMonHoc
            """;

        using SqlCommand cmd = new SqlCommand(sql, conn);

        cmd.Parameters.Add(
            new SqlParameter("@MaMonHoc",
                System.Data.SqlDbType.NVarChar, 50)
            {
                Value = string.IsNullOrWhiteSpace(maMonHoc)
                    ? DBNull.Value
                    : maMonHoc
            });

        conn.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            DiemSinhVien item = new DiemSinhVien
            {
                HoTen = reader["HoTen"].ToString() ?? "",
                NgaySinh = Convert.ToDateTime(
                    reader["NgaySinh"]),
                TenMonHoc =
                    reader["TenMonHoc"].ToString() ?? "",
                Diem = Convert.ToDecimal(reader["Diem"])
            };

            danhSach.Add(item);
        }

        return danhSach;
    }
}

// Lop luu du lieu doc tu SQL Server
internal class DiemSinhVien
{
    public string HoTen { get; set; } = "";
    public DateTime NgaySinh { get; set; }
    public string TenMonHoc { get; set; } = "";
    public decimal Diem { get; set; }
}

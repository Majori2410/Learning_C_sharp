
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BaiNC1_DanhSachLop_06b;

internal static class Db
{
    private const string ConnectionString =
        @"Server=(local)\SQLEXPRESS;" +
        @"Database=QLSinhVien;" +
        @"Integrated Security=True;" +
        @"TrustServerCertificate=True;";

    // Lay danh sach lop tu SQL Server
    public static List<(string MaLop, string TenLop, string MaKhoa)>
        LayDanhSachLop()
    {
        var danhSach =
            new List<(string MaLop, string TenLop, string MaKhoa)>();

        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        const string sql = """
            SELECT MaLop, TenLop, MaKhoa
            FROM Lop
            ORDER BY MaLop
            """;

        using SqlCommand cmd = new SqlCommand(sql, conn);

        conn.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string maLop =
                reader["MaLop"].ToString() ?? "";

            string tenLop =
                reader["TenLop"].ToString() ?? "";

            string maKhoa =
                reader["MaKhoa"].ToString() ?? "";

            danhSach.Add((maLop, tenLop, maKhoa));
        }

        return danhSach;
    }
}

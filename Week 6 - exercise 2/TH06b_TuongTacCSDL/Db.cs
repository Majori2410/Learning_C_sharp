
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Bai1_QuanLySinhVien_06b;

internal static class Db
{
    private const string ConnectionString =
        @"Server=(local)\SQLEXPRESS;" +
        @"Database=QLSinhVien;" +
        @"Integrated Security=True;" +
        @"TrustServerCertificate=True;";

    public static int Execute(
        string sql,
        params SqlParameter[] parameters)
    {
        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);

        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    // Kiem tra ban ghi co ton tai hay khong
    public static bool Exists(
        string sql,
        params SqlParameter[] parameters)
    {
        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        using SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);

        conn.Open();

        using SqlDataReader reader = cmd.ExecuteReader();
        return reader.HasRows;
    }

    // Lay danh sach ma lop cho ComboBox
    public static List<string> LayDanhSachMaLop()
    {
        List<string> danhSach = new();

        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        const string sql = """
            SELECT MaLop
            FROM Lop
            ORDER BY MaLop
            """;

        using SqlCommand cmd = new SqlCommand(sql, conn);

        conn.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            danhSach.Add(
                reader["MaLop"].ToString() ?? "");
        }

        return danhSach;
    }
}


using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Bai2_QuanLyDiem_06b;

internal static class Db
{
    private const string ConnectionString =
        @"Server=(local)\SQLEXPRESS;" +
        @"Database=QLSinhVien;" +
        @"Integrated Security=True;" +
        @"TrustServerCertificate=True;";

    // Thuc thi INSERT, UPDATE, DELETE
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

    // Kiem tra ban ghi ton tai bang SqlDataReader
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

    // Doc mot cot du lieu de hien thi len ComboBox
    public static List<string> LayDanhSach(string sql)
    {
        List<string> danhSach = new();

        using SqlConnection conn =
            new SqlConnection(ConnectionString);

        using SqlCommand cmd = new SqlCommand(sql, conn);

        conn.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            danhSach.Add(
                reader.GetValue(0).ToString() ?? "");
        }

        return danhSach;
    }
}

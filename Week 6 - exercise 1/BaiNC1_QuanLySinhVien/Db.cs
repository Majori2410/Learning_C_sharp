
using Microsoft.Data.SqlClient;

namespace BaiNC1_QuanLySinhVien;

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
}

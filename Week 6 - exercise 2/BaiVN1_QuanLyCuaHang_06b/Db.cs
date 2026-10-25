
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BaiVN1_QuanLyCuaHang_06b;

internal static class Db
{
    private const string ConnectionString =
        @"Server=(local)\SQLEXPRESS;" +
        @"Database=QLCuaHang;" +
        @"Integrated Security=True;" +
        @"TrustServerCertificate=True;";

    private static SqlConnection TaoKetNoi()
    {
        return new SqlConnection(ConnectionString);
    }

    // 1. Tao ton kho cho ngay hien tai
    public static int TaoTonKho()
    {
        using var conn = TaoKetNoi();
        conn.Open();

        using var tran = conn.BeginTransaction(
            IsolationLevel.Serializable);

        try
        {
            // Moi san pham chi co mot ban ghi ton kho/ngay
            const string sql = """
                INSERT INTO TONKHO
                    (MaSP, NgayTon, SLDau, SLNhap, SLBan)
                SELECT
                    SP.MaSP,
                    CAST(GETDATE() AS DATE),
                    COALESCE(TK.SLDau + TK.SLNhap
                             - TK.SLBan, 0),
                    0,
                    0
                FROM SANPHAM SP
                OUTER APPLY
                (
                    SELECT TOP 1
                        SLDau, SLNhap, SLBan
                    FROM TONKHO
                    WHERE MaSP = SP.MaSP
                      AND NgayTon < CAST(GETDATE() AS DATE)
                    ORDER BY NgayTon DESC
                ) TK
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM TONKHO AS H WITH (UPDLOCK, HOLDLOCK)
                    WHERE H.MaSP = SP.MaSP
                      AND H.NgayTon = CAST(GETDATE() AS DATE)
                )
                """;

            using var cmd = new SqlCommand(sql, conn, tran);
            int soDong = cmd.ExecuteNonQuery();

            tran.Commit();
            return soDong;
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    // 2. Them san pham va ton kho ban dau
    public static int ThemSanPham(string tenSP)
    {
        using var conn = TaoKetNoi();
        conn.Open();

        using var tran = conn.BeginTransaction();

        try
        {
            const string sqlSP = """
                INSERT INTO SANPHAM (TenSP)
                OUTPUT INSERTED.MaSP
                VALUES (@TenSP)
                """;

            using var cmdSP = new SqlCommand(
                sqlSP, conn, tran);

            cmdSP.Parameters.AddWithValue("@TenSP", tenSP);

            int maSP = Convert.ToInt32(
                cmdSP.ExecuteScalar());

            const string sqlTK = """
                INSERT INTO TONKHO
                    (MaSP, NgayTon, SLDau, SLNhap, SLBan)
                VALUES
                    (@MaSP, CAST(GETDATE() AS DATE), 0, 0, 0)
                """;

            using var cmdTK = new SqlCommand(
                sqlTK, conn, tran);

            cmdTK.Parameters.AddWithValue("@MaSP", maSP);
            cmdTK.ExecuteNonQuery();

            tran.Commit();
            return maSP;
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    // 3. Lay danh sach san pham
    public static List<(int MaSP, string TenSP)>
        LayDanhSachSanPham()
    {
        var danhSach = new List<(int, string)>();

        using var conn = TaoKetNoi();

        using var cmd = new SqlCommand(
            "SELECT MaSP, TenSP FROM SANPHAM ORDER BY MaSP",
            conn);

        conn.Open();

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            danhSach.Add((
                Convert.ToInt32(reader["MaSP"]),
                reader["TenSP"].ToString() ?? ""
            ));
        }

        return danhSach;
    }

    // 4. Tao hoa don moi
    public static int TaoHoaDon()
    {
        using var conn = TaoKetNoi();
        conn.Open();

        // Phai tao ton kho truoc khi ban
        using (var check = new SqlCommand(
            """
            SELECT COUNT(*)
            FROM TONKHO
            WHERE NgayTon = CAST(GETDATE() AS DATE)
            """, conn))
        {
            if (Convert.ToInt32(check.ExecuteScalar()) == 0)
                throw new Exception(
                    "Chưa tạo tồn kho hôm nay!");
        }

        const string sql = """
            INSERT INTO HOADON
                (NgayTao, TinhTrangThanhToan)
            OUTPUT INSERTED.MaHD
            VALUES (GETDATE(), 0)
            """;

        using var cmd = new SqlCommand(sql, conn);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    // 5. Them san pham vao hoa don va cap nhat SLBan
    public static void ThemChiTietHoaDon(
        int maHD, int maSP, int soLuong)
    {
        if (soLuong <= 0)
            throw new ArgumentException(
                "Số lượng phải lớn hơn 0.");

        using var conn = TaoKetNoi();
        conn.Open();

        using var tran = conn.BeginTransaction();

        try
        {
            // Kiem tra hoa don chua thanh toan
            using (var check = new SqlCommand(
                """
                SELECT TinhTrangThanhToan
                FROM HOADON WITH (UPDLOCK, HOLDLOCK)
                WHERE MaHD = @MaHD
                  AND CAST(NgayTao AS DATE) =
                      CAST(GETDATE() AS DATE)
                """, conn, tran))
            {
                check.Parameters.AddWithValue("@MaHD", maHD);

                object? status = check.ExecuteScalar();

                if (status == null)
                    throw new Exception(
                        "Hóa đơn không tồn tại hoặc không thuộc hôm nay.");

                if (Convert.ToBoolean(status))
                    throw new Exception(
                        "Hóa đơn đã thanh toán.");
            }

            // Kiem tra ton kho va cong so luong ban
            const string sqlTonKho = """
                UPDATE TONKHO
                SET SLBan = SLBan + @SoLuong
                WHERE MaSP = @MaSP
                  AND NgayTon = CAST(GETDATE() AS DATE)
                  AND (SLDau + SLNhap - SLBan) > @SoLuong
                """;

            using (var cmdTK = new SqlCommand(
                sqlTonKho, conn, tran))
            {
                cmdTK.Parameters.AddWithValue(
                    "@MaSP", maSP);

                cmdTK.Parameters.AddWithValue(
                    "@SoLuong", soLuong);

                if (cmdTK.ExecuteNonQuery() == 0)
                    throw new Exception(
                        "Sản phẩm không có tồn kho hoặc số lượng không đủ.");
            }

            // Neu san pham da co trong hoa don, cong don
            const string sqlCT = """
                UPDATE CHITIET_HOADON
                SET SoLuong = SoLuong + @SoLuong
                WHERE MaHD = @MaHD AND MaSP = @MaSP;

                IF @@ROWCOUNT = 0
                BEGIN
                    INSERT INTO CHITIET_HOADON
                        (MaHD, MaSP, SoLuong)
                    VALUES (@MaHD, @MaSP, @SoLuong);
                END
                """;

            using (var cmdCT = new SqlCommand(
                sqlCT, conn, tran))
            {
                cmdCT.Parameters.AddWithValue(
                    "@MaHD", maHD);

                cmdCT.Parameters.AddWithValue(
                    "@MaSP", maSP);

                cmdCT.Parameters.AddWithValue(
                    "@SoLuong", soLuong);

                cmdCT.ExecuteNonQuery();
            }

            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    // 6. Huy hoa don chua thanh toan
    public static void HuyHoaDon(int maHD)
    {
        using var conn = TaoKetNoi();
        conn.Open();

        using var tran = conn.BeginTransaction();

        try
        {
            using (var check = new SqlCommand(
                """
                SELECT TinhTrangThanhToan
                FROM HOADON WITH (UPDLOCK, HOLDLOCK)
                WHERE MaHD = @MaHD
                  AND CAST(NgayTao AS DATE) =
                      CAST(GETDATE() AS DATE)
                """, conn, tran))
            {
                check.Parameters.AddWithValue("@MaHD", maHD);
                object? status = check.ExecuteScalar();

                if (status == null || Convert.ToBoolean(status))
                    throw new Exception(
                        "Không thể hủy hóa đơn này.");
            }

            // Tra lai so luong da ban vao ton kho
            const string sqlHoanKho = """
                UPDATE TK
                SET TK.SLBan = TK.SLBan - CT.SoLuong
                FROM TONKHO TK
                INNER JOIN CHITIET_HOADON CT
                    ON TK.MaSP = CT.MaSP
                WHERE CT.MaHD = @MaHD
                  AND TK.NgayTon = CAST(GETDATE() AS DATE)
                """;

            using (var cmd = new SqlCommand(
                sqlHoanKho, conn, tran))
            {
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SqlCommand(
                """
                DELETE FROM CHITIET_HOADON
                WHERE MaHD = @MaHD
                """, conn, tran))
            {
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.ExecuteNonQuery();
            }

            using (var cmd = new SqlCommand(
                "DELETE FROM HOADON WHERE MaHD = @MaHD",
                conn, tran))
            {
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.ExecuteNonQuery();
            }

            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    // 7. Thanh toan hoa don
    public static void ThanhToan(int maHD)
    {
        using var conn = TaoKetNoi();
        conn.Open();

        const string sql = """
            UPDATE HOADON
            SET TinhTrangThanhToan = 1
            WHERE MaHD = @MaHD
              AND TinhTrangThanhToan = 0
              AND EXISTS
              (
                  SELECT 1
                  FROM CHITIET_HOADON
                  WHERE MaHD = @MaHD
              )
            """;

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@MaHD", maHD);

        if (cmd.ExecuteNonQuery() == 0)
            throw new Exception(
                "Hóa đơn không hợp lệ, đã thanh toán hoặc chưa có sản phẩm.");
    }
}

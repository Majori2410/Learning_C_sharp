using System;
using System.Collections.Generic;

abstract class NhanVien
{
    protected string maNV;
    protected string hoTen;

    public virtual void Input()
    {
        Console.Write("Nhap ma nhan vien: ");
        maNV = Console.ReadLine();

        Console.Write("Nhap ho ten: ");
        hoTen = Console.ReadLine();
    }

    public abstract double TinhLuong();

    public virtual void Output()
    {
        Console.WriteLine($"Ma NV: {maNV}");
        Console.WriteLine($"Ho ten: {hoTen}");
        Console.WriteLine($"Luong: {TinhLuong():N0} VND");
    }
}

class NhanVienKinhDoanh : NhanVien
{
    private double luongCoBan;
    private int soHopDong;

    public override void Input()
    {
        base.Input();

        Console.Write("Nhap luong co ban: ");
        luongCoBan = double.Parse(Console.ReadLine());

        Console.Write("Nhap so hop dong: ");
        soHopDong = int.Parse(Console.ReadLine());
    }

    public override double TinhLuong()
    {
        return luongCoBan + soHopDong * 500000;
    }

    public override void Output()
    {
        Console.WriteLine("\nNHAN VIEN KINH DOANH");
        base.Output();

        Console.WriteLine($"Luong co ban: {luongCoBan:N0} VND");
        Console.WriteLine($"So hop dong: {soHopDong}");
    }
}

class NhanVienSanXuat : NhanVien
{
    private int soSanPham;

    public override void Input()
    {
        base.Input();

        Console.Write("Nhap so san pham: ");
        soSanPham = int.Parse(Console.ReadLine());
    }

    public override double TinhLuong()
    {
        double luong = soSanPham * 1000;

        if (soSanPham > 3000)
        {
            luong += luong * 0.05;
        }

        return luong;
    }

    public override void Output()
    {
        Console.WriteLine("\nNHAN VIEN SAN XUAT");
        base.Output();

        Console.WriteLine($"So san pham: {soSanPham}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Nhap so luong nhan vien: ");
        int n = int.Parse(Console.ReadLine());

        List<NhanVien> ds = new List<NhanVien>();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nNhap nhan vien thu {i + 1}:");
            Console.WriteLine("1. Nhan vien kinh doanh");
            Console.WriteLine("2. Nhan vien san xuat");

            Console.Write("Chon loai nhan vien: ");
            int loai = int.Parse(Console.ReadLine());

            NhanVien nv;

            if (loai == 1)
            {
                nv = new NhanVienKinhDoanh();
            }
            else
            {
                nv = new NhanVienSanXuat();
            }

            nv.Input();

            ds.Add(nv);
        }

        Console.WriteLine("\n===== DANH SACH NHAN VIEN =====");

        foreach (NhanVien nv in ds)
        {
            nv.Output();
        }
    }
}
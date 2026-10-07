using System;
using System.Collections.Generic;

class NhanVien
{
    private string hoTen;
    private double mucLuong;
    private int soNgayVang;

    public NhanVien()
    {
        hoTen = "";
        mucLuong = 0;
        soNgayVang = 0;
    }

    public NhanVien(string hoTen, double mucLuong, int soNgayVang)
    {
        this.hoTen = hoTen;
        this.mucLuong = mucLuong;
        this.soNgayVang = soNgayVang;
    }

    public void Input()
    {
        Console.Write("Nhap ho ten: ");
        hoTen = Console.ReadLine();

        Console.Write("Nhap muc luong: ");
        mucLuong = double.Parse(Console.ReadLine());

        Console.Write("Nhap so ngay vang: ");
        soNgayVang = int.Parse(Console.ReadLine());
    }

    public double TinhLuong()
    {
        return mucLuong - soNgayVang * 100000;
    }

    public void Output()
    {
        Console.WriteLine($"Ho ten: {hoTen}");
        Console.WriteLine($"Muc luong: {mucLuong:N0} VND");
        Console.WriteLine($"So ngay vang: {soNgayVang}");
        Console.WriteLine($"Luong thuc nhan: {TinhLuong():N0} VND");
    }
}

class PhongBan
{
    private List<NhanVien> ds;

    public PhongBan()
    {
        ds = new List<NhanVien>();
    }

    public void Input()
    {
        Console.Write("Nhap so luong nhan vien: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nNhap nhan vien thu {i + 1}:");

            NhanVien nv = new NhanVien();
            nv.Input();

            ds.Add(nv);
        }
    }

    public void Output()
    {
        Console.WriteLine("\nDANH SACH NHAN VIEN");

        for (int i = 0; i < ds.Count; i++)
        {
            Console.WriteLine($"\nNhan vien thu {i + 1}:");
            ds[i].Output();
        }
    }

    public double TongLuong()
    {
        double tong = 0;

        foreach (NhanVien nv in ds)
        {
            tong += nv.TinhLuong();
        }

        return tong;
    }
}

class Program
{
    static void Main(string[] args)
    {
        PhongBan pb = new PhongBan();

        pb.Input();
        pb.Output();

        Console.WriteLine(
            $"\nTong luong cua phong ban: {pb.TongLuong():N0} VND"
        );
    }
}
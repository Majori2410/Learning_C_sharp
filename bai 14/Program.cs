using System;

class NhanVien
{
    string hoTen;
    double mucLuong;
    int soNgayVang;

    public NhanVien()
    {
    }

    public void Nhap()
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

    public void Xuat()
    {
        Console.WriteLine("\nTHONG TIN NHAN VIEN");
        Console.WriteLine($"Ho ten: {hoTen}");
        Console.WriteLine($"Muc luong: {mucLuong:N0} VND");
        Console.WriteLine($"So ngay vang: {soNgayVang}");
        Console.WriteLine($"Luong thuc nhan: {TinhLuong():N0} VND");
    }
}

class Program
{
    static void Main(string[] args)
    {
        NhanVien nv = new NhanVien();

        nv.Nhap();
        nv.Xuat();
    }
}
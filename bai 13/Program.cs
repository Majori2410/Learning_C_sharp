using System;
using System.Security.Cryptography.X509Certificates;

class SinhVien
{
    string MaSV;
    string HoTen;
    string DiaChi;
    string SVNamThu;

    public SinhVien()
    {
    }

    public void Nhap()
    {
        Console.Write("Nhap ma sinh vien: ");
        MaSV = Console.ReadLine();

        Console.Write("Nhap ho ten cua sinh vien: ");
        HoTen = Console.ReadLine();

        Console.Write("Nhap dia chi cua sinh vien: ");
        DiaChi = Console.ReadLine();

        Console.Write("Sinh vien dang hoc nam thu: ");
        SVNamThu = Console.ReadLine();
    }

    public void Xuat()
    {
        Console.WriteLine($"\nTHONG TIN SINH VIEN:\n" +
            $"1. MSSV: {MaSV}\n" +
            $"2. Ho ten: {HoTen}\n" +
            $"3. Dia chi: {DiaChi}\n" +
            $"4. Sinh vien hoc nam thu {SVNamThu}");
    }

    static void Main (String[] arrgs)
    {
        SinhVien sv = new SinhVien();
        sv.Nhap();
        sv.Xuat();
    }
}
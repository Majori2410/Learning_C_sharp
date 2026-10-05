using System;

class DanhSachHoTen
{
    string[] hoTen;

    public void Nhap()
    {
        Console.Write("Nhap so luong nguoi: ");
        int n = int.Parse(Console.ReadLine());

        hoTen = new string[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Nhap ho ten nguoi thu {i + 1}: ");
            hoTen[i] = Console.ReadLine();
        }
    }

    public void SapXep()
    {
        Array.Sort(hoTen);
    }

    public void Xuat()
    {
        Console.WriteLine("\nDanh sach ho ten:");

        for (int i = 0; i < hoTen.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {hoTen[i]}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        DanhSachHoTen ds = new DanhSachHoTen();

        ds.Nhap();

        Console.WriteLine("\nDanh sach truoc khi sap xep:");
        ds.Xuat();

        ds.SapXep();

        Console.WriteLine("\nDanh sach sau khi sap xep tang dan:");
        ds.Xuat();
    }
}
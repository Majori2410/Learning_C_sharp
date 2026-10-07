using System;

class SinhVien : IComparable<SinhVien>
{
    private string hoTen;
    private double diem;

    public SinhVien()
    {
        hoTen = "";
        diem = 0;
    }

    public SinhVien(string hoTen, double diem)
    {
        this.hoTen = hoTen;
        this.diem = diem;
    }

    public void Input()
    {
        Console.Write("Nhap ho ten: ");
        hoTen = Console.ReadLine();

        Console.Write("Nhap diem: ");
        diem = double.Parse(Console.ReadLine());
    }

    public int CompareTo(SinhVien other)
    {
        return diem.CompareTo(other.diem);
    }

    public override string ToString()
    {
        return $"{hoTen} - {diem}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Nhap so luong sinh vien: ");
        int n = int.Parse(Console.ReadLine());

        SinhVien[] ds = new SinhVien[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nNhap sinh vien thu {i + 1}:");

            ds[i] = new SinhVien();
            ds[i].Input();
        }

        Console.WriteLine("\nDanh sach truoc khi sap xep:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }

        Array.Sort(ds);

        Console.WriteLine("\nDanh sach sau khi sap xep tang dan theo diem:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }
    }
}
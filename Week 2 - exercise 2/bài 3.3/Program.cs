using System;

delegate int Compare<T>(T a, T b);

class SinhVien
{
    public string HoTen { get; set; }
    public double Diem { get; set; }

    public SinhVien(string hoTen, double diem)
    {
        HoTen = hoTen;
        Diem = diem;
    }

    public override string ToString()
    {
        return $"{HoTen} - {Diem}";
    }
}

class Program
{
    static void MySort<T>(T[] a, Compare<T> compare)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            for (int j = i + 1; j < a.Length; j++)
            {
                if (compare(a[i], a[j]) > 0)
                {
                    T temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }
        }
    }

    static int CompareByDiem(SinhVien a, SinhVien b)
    {
        return a.Diem.CompareTo(b.Diem);
    }

    static int CompareByTen(SinhVien a, SinhVien b)
    {
        return string.Compare(a.HoTen, b.HoTen);
    }

    static void Main(string[] args)
    {
        SinhVien[] ds =
        {
            new SinhVien("Nguyen Van A", 8.5),
            new SinhVien("Tran Van B", 6.5),
            new SinhVien("Le Van C", 9.0)
        };

        Console.WriteLine("Danh sach ban dau:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }

        MySort(ds, CompareByDiem);

        Console.WriteLine("\nSap xep tang dan theo diem:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }

        MySort(ds, CompareByTen);

        Console.WriteLine("\nSap xep tang dan theo ho ten:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }
    }
}
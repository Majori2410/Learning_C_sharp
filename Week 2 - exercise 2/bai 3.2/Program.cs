using System;

interface IMyComparable
{
    int CompareTo(object other);
}

class SinhVien : IMyComparable
{
    private string hoTen;
    private double diem;

    public SinhVien(string hoTen, double diem)
    {
        this.hoTen = hoTen;
        this.diem = diem;
    }

    public int CompareTo(object other)
    {
        SinhVien sv = (SinhVien)other;

        return diem.CompareTo(sv.diem);
    }

    public override string ToString()
    {
        return $"{hoTen} - {diem}";
    }
}

class Program
{
    static void MySort(IMyComparable[] a)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[i].CompareTo(a[j]) > 0)
                {
                    IMyComparable temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        SinhVien[] ds =
        {
            new SinhVien("Nguyen Van A", 8.5),
            new SinhVien("Tran Van B", 6.5),
            new SinhVien("Le Van C", 9.0)
        };

        Console.WriteLine("Truoc khi sap xep:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }

        IMyComparable[] temp = ds;

        MySort(temp);

        Console.WriteLine("\nSau khi sap xep:");

        foreach (SinhVien sv in ds)
        {
            Console.WriteLine(sv);
        }
    }
}
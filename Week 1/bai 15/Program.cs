using System;
using System.Collections.Generic;

class MangSoNguyen
{
    int[] a;

    public void Nhap()
    {
        Console.Write("Nhap so phan tu n: ");
        int n = int.Parse(Console.ReadLine());

        a = new int[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i}] = ");
            a[i] = int.Parse(Console.ReadLine());
        }
    }

    public void Xuat()
    {
        Console.Write("Mang: ");

        for (int i = 0; i < a.Length; i++)
        {
            Console.Write($"{a[i]} ");
        }

        Console.WriteLine();
    }

    public void TimMaxMin(out int max, out int min)
    {
        max = a[0];
        min = a[0];

        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] > max)
            {
                max = a[i];
            }

            if (a[i] < min)
            {
                min = a[i];
            }
        }
    }

    private bool IsPrime(int n)
    {
        if (n < 2)
        {
            return false;
        }

        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    public int[] LayMangSoNguyenTo()
    {
        List<int> ds = new List<int>();

        for (int i = 0; i < a.Length; i++)
        {
            if (IsPrime(a[i]))
            {
                ds.Add(a[i]);
            }
        }

        return ds.ToArray();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MangSoNguyen mang = new MangSoNguyen();

        mang.Nhap();

        Console.WriteLine();
        mang.Xuat();

        mang.TimMaxMin(out int max, out int min);

        Console.WriteLine($"Gia tri lon nhat: {max}");
        Console.WriteLine($"Gia tri nho nhat: {min}");

        int[] mangNguyenTo = mang.LayMangSoNguyenTo();

        Console.Write("Cac so nguyen to: ");

        for (int i = 0; i < mangNguyenTo.Length; i++)
        {
            Console.Write($"{mangNguyenTo[i]} ");
        }

        Console.WriteLine();
    }
}
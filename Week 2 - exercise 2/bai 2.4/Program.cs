using System;
using System.Collections.Generic;

class MangHaiChieu
{
    private int[,] a;
    private int n;
    private int m;

    // Default constructor
    public MangHaiChieu()
    {
        n = 0;
        m = 0;
        a = new int[0, 0];
    }

    // Constructor co tham so
    public MangHaiChieu(int n, int m)
    {
        this.n = n;
        this.m = m;
        a = new int[n, m];
    }

    // Copy constructor
    public MangHaiChieu(MangHaiChieu other)
    {
        n = other.n;
        m = other.m;

        a = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                a[i, j] = other.a[i, j];
            }
        }
    }

    // Indexer
    public int this[int i, int j]
    {
        get
        {
            return a[i, j];
        }

        set
        {
            a[i, j] = value;
        }
    }

    // Nhap
    public void Input()
    {
        Console.Write("Nhap so hang n: ");
        n = int.Parse(Console.ReadLine());

        Console.Write("Nhap so cot m: ");
        m = int.Parse(Console.ReadLine());

        a = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"a[{i},{j}] = ");
                a[i, j] = int.Parse(Console.ReadLine());
            }
        }
    }

    // Xuat
    public void Output()
    {
        Console.WriteLine("\nMang hai chieu:");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"{a[i, j],5}");
            }

            Console.WriteLine();
        }
    }

    // Kiem tra so nguyen to
    private bool IsPrime(int x)
    {
        if (x < 2)
        {
            return false;
        }

        for (int i = 2; i * i <= x; i++)
        {
            if (x % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    // Tim cac so nguyen to
    public int[] TimSoNguyenTo()
    {
        List<int> ds = new List<int>();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (IsPrime(a[i, j]))
                {
                    ds.Add(a[i, j]);
                }
            }
        }

        return ds.ToArray();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MangHaiChieu mang = new MangHaiChieu();

        mang.Input();
        mang.Output();

        Console.WriteLine($"\nPhan tu a[0,0] = {mang[0, 0]}");

        int[] soNguyenTo = mang.TimSoNguyenTo();

        Console.Write("Cac so nguyen to trong mang: ");

        for (int i = 0; i < soNguyenTo.Length; i++)
        {
            Console.Write($"{soNguyenTo[i]} ");
        }

        Console.WriteLine();
    }
}
using System;
using System.Collections.Generic;

class DaySo
{
    private int[] a;

    // Default constructor
    public DaySo()
    {
        a = new int[0];
    }

    // Constructor có tham số n
    public DaySo(int n)
    {
        a = new int[n];
    }

    // Copy constructor
    public DaySo(DaySo other)
    {
        a = new int[other.a.Length];

        for (int i = 0; i < other.a.Length; i++)
        {
            a[i] = other.a[i];
        }
    }

    // Property Length
    public int Length
    {
        get { return a.Length; }
    }

    // Indexer
    public int this[int i]
    {
        get
        {
            return a[i];
        }

        set
        {
            a[i] = value;
        }
    }

    // Nhập dãy số
    public void Input()
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

    // Xuất dãy số
    public void Output()
    {
        Console.Write("Day so: ");

        for (int i = 0; i < a.Length; i++)
        {
            Console.Write($"{a[i]} ");
        }

        Console.WriteLine();
    }

    // Tìm các số chẵn
    public int[] TimSoChan()
    {
        List<int> dsChan = new List<int>();

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] % 2 == 0)
            {
                dsChan.Add(a[i]);
            }
        }

        return dsChan.ToArray();
    }
}

class Program
{
    static void Main(string[] args)
    {
        DaySo ds = new DaySo();

        ds.Input();

        Console.WriteLine();
        ds.Output();

        Console.WriteLine($"Phan tu dau tien: {ds[0]}");

        int[] mangChan = ds.TimSoChan();

        Console.Write("Cac so chan: ");

        for (int i = 0; i < mangChan.Length; i++)
        {
            Console.Write($"{mangChan[i]} ");
        }

        Console.WriteLine();
    }
}
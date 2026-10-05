using System;
using System.Collections.Generic;

class MangHaiChieu
{
    int[,] a;
    int n;
    int m;

    public void NhapKichThuoc()
    {
        Console.Write("Nhap so hang n: ");
        n = int.Parse(Console.ReadLine());

        Console.Write("Nhap so cot m: ");
        m = int.Parse(Console.ReadLine());

        a = new int[n, m];
    }

    public void SinhMang()
    {
        Random random = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                a[i, j] = random.Next(10, 101);
            }
        }
    }

    public void Xuat()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"{a[i, j],5}");
            }

            Console.WriteLine();
        }
    }

    public void TachChanLe(out int[] mangChan, out int[] mangLe)
    {
        List<int> dsChan = new List<int>();
        List<int> dsLe = new List<int>();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (a[i, j] % 2 == 0)
                {
                    dsChan.Add(a[i, j]);
                }
                else
                {
                    dsLe.Add(a[i, j]);
                }
            }
        }

        mangChan = dsChan.ToArray();
        mangLe = dsLe.ToArray();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MangHaiChieu mang = new MangHaiChieu();

        mang.NhapKichThuoc();
        mang.SinhMang();

        Console.WriteLine("\nMang duoc sinh ngau nhien:");
        mang.Xuat();

        mang.TachChanLe(out int[] mangChan, out int[] mangLe);

        Console.Write("\nMang cac so chan: ");
        for (int i = 0; i < mangChan.Length; i++)
        {
            Console.Write($"{mangChan[i]} ");
        }

        Console.Write("\nMang cac so le: ");
        for (int i = 0; i < mangLe.Length; i++)
        {
            Console.Write($"{mangLe[i]} ");
        }

        Console.WriteLine();
    }
}
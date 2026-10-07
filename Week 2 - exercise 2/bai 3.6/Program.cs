using System;
using System.Collections.Generic;

abstract class ThiSinh
{
    protected string sbd;
    protected string hoTen;
    protected double bai1;
    protected double bai2;
    protected double bai3;

    public virtual void Input()
    {
        Console.Write("Nhap so bao danh: ");
        sbd = Console.ReadLine();

        Console.Write("Nhap ho ten: ");
        hoTen = Console.ReadLine();

        Console.Write("Nhap diem bai 1: ");
        bai1 = double.Parse(Console.ReadLine());

        Console.Write("Nhap diem bai 2: ");
        bai2 = double.Parse(Console.ReadLine());

        Console.Write("Nhap diem bai 3: ");
        bai3 = double.Parse(Console.ReadLine());
    }

    public abstract double TinhTongDiem();

    public virtual void Output()
    {
        Console.WriteLine($"SBD: {sbd}");
        Console.WriteLine($"Ho ten: {hoTen}");
        Console.WriteLine($"Bai 1: {bai1}");
        Console.WriteLine($"Bai 2: {bai2}");
        Console.WriteLine($"Bai 3: {bai3}");
        Console.WriteLine($"Tong diem: {TinhTongDiem()}");
    }
}

class ThiSinhChuyen : ThiSinh
{
    private double tiengAnh;

    public override void Input()
    {
        base.Input();

        Console.Write("Nhap diem tieng Anh: ");
        tiengAnh = double.Parse(Console.ReadLine());
    }

    public override double TinhTongDiem()
    {
        double tong = bai1 + bai2 + bai3;

        if (tiengAnh >= 7 && tiengAnh <= 8)
        {
            tong += 1;
        }
        else if (tiengAnh >= 9 && tiengAnh <= 10)
        {
            tong += 2;
        }

        return tong;
    }

    public override void Output()
    {
        Console.WriteLine("\nTHI SINH CHUYEN");
        base.Output();
        Console.WriteLine($"Tieng Anh: {tiengAnh}");
    }
}

class ThiSinhSieuCup : ThiSinh
{
    private double csdl;

    public override void Input()
    {
        base.Input();

        Console.Write("Nhap diem CSDL: ");
        csdl = double.Parse(Console.ReadLine());
    }

    public override double TinhTongDiem()
    {
        return bai1 + bai2 + bai3 + csdl;
    }

    public override void Output()
    {
        Console.WriteLine("\nTHI SINH SIEU CUP");
        base.Output();
        Console.WriteLine($"CSDL: {csdl}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Nhap so luong thi sinh: ");
        int n = int.Parse(Console.ReadLine());

        List<ThiSinh> ds = new List<ThiSinh>();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nNhap thi sinh thu {i + 1}:");
            Console.WriteLine("1. Thi sinh Chuyen");
            Console.WriteLine("2. Thi sinh Sieu cup");

            Console.Write("Chon loai thi sinh: ");
            int loai = int.Parse(Console.ReadLine());

            ThiSinh ts;

            if (loai == 1)
            {
                ts = new ThiSinhChuyen();
            }
            else
            {
                ts = new ThiSinhSieuCup();
            }

            ts.Input();
            ds.Add(ts);
        }

        Console.WriteLine("\n===== KET QUA =====");

        foreach (ThiSinh ts in ds)
        {
            ts.Output();
        }
    }
}
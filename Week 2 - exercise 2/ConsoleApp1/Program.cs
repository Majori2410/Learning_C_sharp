using System;

class DonThuc
{
    // Field
    private double a;
    private int n;

    // Property
    public double A
    {
        get { return a; }
        set { a = value; }
    }

    public int N
    {
        get { return n; }
        set
        {
            if (value >= 0)
                n = value;
        }
    }

    // Default constructor
    public DonThuc()
    {
        a = 0;
        n = 0;
    }

    // Parameter constructor
    public DonThuc(double a, int n)
    {
        this.a = a;
        this.n = n;
    }

    // Input
    public void Input()
    {
        Console.Write("Nhap he so a: ");
        a = double.Parse(Console.ReadLine());

        do
        {
            Console.Write("Nhap so mu n (n >= 0): ");
            n = int.Parse(Console.ReadLine());
        }
        while (n < 0);
    }

    // Output
    public void Output()
    {
        Console.WriteLine($"P(x) = {a}x^{n}");
    }

    // Tính giá trị đơn thức tại x
    public double TinhGiaTri(double x)
    {
        return a * Math.Pow(x, n);
    }

    // Đạo hàm đơn thức
    public DonThuc DaoHam()
    {
        if (n == 0)
        {
            return new DonThuc(0, 0);
        }

        return new DonThuc(a * n, n - 1);
    }

    public override string ToString()
    {
        return $"{a}x^{n}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        DonThuc p = new DonThuc();

        p.Input();

        Console.WriteLine("\nDon thuc vua nhap:");
        p.Output();

        Console.Write("\nNhap x: ");
        double x = double.Parse(Console.ReadLine());

        double giaTri = p.TinhGiaTri(x);

        Console.WriteLine($"P({x}) = {giaTri}");

        DonThuc q = p.DaoHam();

        Console.WriteLine($"Dao ham P'(x) = {q}");
    }
}
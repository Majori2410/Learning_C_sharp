using System;

class Point
{
    // Field
    private double x;
    private double y;

    // Property
    public double X
    {
        get { return x; }
        set { x = value; }
    }

    public double Y
    {
        get { return y; }
        set { y = value; }
    }

    // Default constructor
    public Point()
    {
        x = 0;
        y = 0;
    }

    // Parameter constructor
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // Input
    public void Input()
    {
        Console.Write("Nhap x: ");
        x = double.Parse(Console.ReadLine());

        Console.Write("Nhap y: ");
        y = double.Parse(Console.ReadLine());
    }

    // Output
    public void Output()
    {
        Console.WriteLine($"({x}, {y})");
    }

    // ToString
    public override string ToString()
    {
        return $"({x}, {y})";
    }

    // A + B
    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    // A - B
    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y);
    }

    // -A
    public static Point operator -(Point a)
    {
        return new Point(-a.x, -a.y);
    }

    // Khoang cach - phuong thuc thanh vien
    public double Distance(Point other)
    {
        double dx = other.x - this.x;
        double dy = other.y - this.y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    // Khoang cach - phuong thuc tinh
    public static double Distance(Point a, Point b)
    {
        double dx = b.x - a.x;
        double dy = b.y - a.y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    // Trung diem - phuong thuc thanh vien
    public Point MidPoint(Point other)
    {
        return new Point(
            (this.x + other.x) / 2,
            (this.y + other.y) / 2
        );
    }

    // Trung diem - phuong thuc tinh
    public static Point MidPoint(Point a, Point b)
    {
        return new Point(
            (a.x + b.x) / 2,
            (a.y + b.y) / 2
        );
    }
}

class Program
{
    static void Main(string[] args)
    {
        Point A = new Point();
        Point B = new Point();

        Console.WriteLine("Nhap diem A:");
        A.Input();

        Console.WriteLine("\nNhap diem B:");
        B.Input();

        Console.WriteLine($"\nA = {A}");
        Console.WriteLine($"B = {B}");

        // Toan tu
        Console.WriteLine($"A + B = {A + B}");
        Console.WriteLine($"A - B = {A - B}");
        Console.WriteLine($"-A = {-A}");

        // Khoang cach
        Console.WriteLine(
            $"Khoang cach (method thanh vien): {A.Distance(B)}");

        Console.WriteLine(
            $"Khoang cach (method tinh): {Point.Distance(A, B)}");

        // Trung diem
        Console.WriteLine(
            $"Trung diem (method thanh vien): {A.MidPoint(B)}");

        Console.WriteLine(
            $"Trung diem (method tinh): {Point.MidPoint(A, B)}");
    }
}
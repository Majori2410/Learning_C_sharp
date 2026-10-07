using System;
using System.Collections;

class Point
{
    private double x;
    private double y;

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

    public Point()
    {
        x = 0;
        y = 0;
    }

    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public void Input()
    {
        Console.Write("Nhap x: ");
        x = double.Parse(Console.ReadLine());

        Console.Write("Nhap y: ");
        y = double.Parse(Console.ReadLine());
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}

class ArrayPoint
{
    private ArrayList dsPoint;

    public ArrayPoint()
    {
        dsPoint = new ArrayList();
    }

    // Indexer
    public Point this[int i]
    {
        get
        {
            return (Point)dsPoint[i];
        }

        set
        {
            dsPoint[i] = value;
        }
    }

    public int Count
    {
        get { return dsPoint.Count; }
    }

    public void Add(Point p)
    {
        dsPoint.Add(p);
    }

    public void Input()
    {
        Console.Write("Nhap so luong diem: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nNhap diem thu {i + 1}:");

            Point p = new Point();
            p.Input();

            dsPoint.Add(p);
        }
    }

    public void Output()
    {
        Console.WriteLine("\nDANH SACH DIEM");

        for (int i = 0; i < dsPoint.Count; i++)
        {
            Console.WriteLine($"Point[{i}] = {dsPoint[i]}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArrayPoint ds = new ArrayPoint();

        ds.Input();
        ds.Output();

        if (ds.Count > 0)
        {
            Console.WriteLine($"\nDiem dau tien: {ds[0]}");
        }
    }
}
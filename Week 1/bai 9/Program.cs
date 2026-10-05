using System;

class Program
{
    static void findMaxMin(double a, double b, double c, out double max, out double min)
    {
        max = a;

        if (max < b)
        {
            max = b;
        }
        
        if (max < c)
        {
            max = c;
        }

        min = a;

        if (min > b)
        {
            min = b;
        }

        if (min > c)
        {
            min = c;
        }
    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap cac so thap phan ma ban muon kiem tra: ");
        double a = double.Parse(Console.ReadLine());
        double b = double.Parse(Console.ReadLine());
        double c = double.Parse(Console.ReadLine());

        findMaxMin(a, b, c, out double MAX, out double MIN);

        Console.WriteLine($"Ta co so lon nhat la {MAX}, so nho nhat la {MIN}");
    }
}
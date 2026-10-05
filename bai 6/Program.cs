using System;

class Program
{
    static int findMax (int a, int b, int c)
    {
        int max = a;

        if (max < b)
        {
            max = b;
        }

        if (max < c)
        {
            max = c;
        }
        return max;
    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap cac gia tri ma ban muon so sanh: ");
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());

        
        int result = findMax(a, b, c);
        Console.Write($"So lon nhat trong ba so la: {result}");
    }
}
using System;

class program
{
    static bool isPrime (int n)
    {
        if (n < 2)
        {
            return false;
        }

        for (int i = 2; i < n; i++)
        {
            if (n % i == 0)
            {
                return false;
            }
        }
        return true;

    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap so ma ban muon kiem tra la so nguyen to hay khong: ");
        int s = int.Parse(Console.ReadLine());

        bool result = isPrime(s);

        if (result)
        {
            Console.WriteLine($"So {s} la so nguyen to.");
        }
        else
        {
            Console.WriteLine($"So {s} khong phai la so nguyen to.");
        }
    }
}
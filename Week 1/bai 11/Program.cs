using System;

class Program
{
    static string daoChuoi(string n)
    {
        string result = "";

        for (int i = n.Length - 1; i >= 0; i--)
        {
            result += n[i];
        }
        return result;
    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap chuoi ma ban muon dao: ");
        String s = Console.ReadLine();

        String kq = daoChuoi(s);

        Console.WriteLine($"Chuoi {s} sau khi dao la: {kq}");
    }
}
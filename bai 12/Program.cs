using System;

class Program
{
    static void xuLyChuoi (string s, out string lc, out string uc, out int countString)
    {
        lc = s.ToLower();

        uc = s.ToUpper();

        String[] splitString = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        countString = splitString.Length;
    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap chuoi ma ban muon xu ly: ");
        string n = Console.ReadLine();

        string lowercase;
        string upercase;
        int countWords;

        xuLyChuoi(n, out lowercase, out upercase, out countWords);

        Console.WriteLine(" ");
        Console.WriteLine("Chuoi cua ban sau khi xu ly:\n" +
            $"chu thuong: {lowercase}\n" +
            $"chu hoa: {upercase}\n" +
            $"So tu trong chuoi: {countWords}\n");
    }
}
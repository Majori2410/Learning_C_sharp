using System;

class Program
{
    static bool isPalindrome (String s)
    {
        for (int i = 0; i < s.Length / 2; i++)
        {
            if (s[i] != s[s.Length - 1 - i])
            {
                return false;
            }
        }
        return true;
    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap chuoi ma ban muon kiem tra co doi xung hay khong: ");
        string s = Console.ReadLine();

        bool Result = isPalindrome(s);

        if (Result)
        {
            Console.WriteLine($"Chuoi '{s}' la mot chuoi doi xung.");
        }
        else
        {
            Console.WriteLine($"Chuoi '{s}' khong phai la mot chuoi doi xung.");
        }
    }
}
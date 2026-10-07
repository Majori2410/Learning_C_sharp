using System;

public delegate void ChooseHandler(int choice);

class ConsoleMenu
{
    public event ChooseHandler Choose;

    public void Run()
    {
        int choice;

        do
        {
            Console.WriteLine("\nMENU");
            Console.WriteLine("1. Giai phuong trinh bac 2");
            Console.WriteLine("2. Gioi thieu");
            Console.WriteLine("0. Thoat");

            Console.Write("Thuc hien: ");
            choice = int.Parse(Console.ReadLine());

            if (choice != 0)
            {
                Choose?.Invoke(choice);
            }

        } while (choice != 0);
    }
}

class PTBac2Console
{
    public void XuLyLuaChon(int choice)
    {
        switch (choice)
        {
            case 1:
                GiaiPTBac2();
                break;

            case 2:
                Console.WriteLine(
                    "Chuong trinh giai phuong trinh bac 2."
                );
                break;

            default:
                Console.WriteLine("Lua chon khong hop le.");
                break;
        }
    }

    private void GiaiPTBac2()
    {
        Console.Write("Nhap a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Nhap b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Nhap c: ");
        double c = double.Parse(Console.ReadLine());

        if (a == 0)
        {
            if (b == 0)
            {
                if (c == 0)
                    Console.WriteLine("Phuong trinh vo so nghiem.");
                else
                    Console.WriteLine("Phuong trinh vo nghiem.");
            }
            else
            {
                double x = -c / b;

                Console.WriteLine(
                    $"Phuong trinh co mot nghiem: x = {x}"
                );
            }

            return;
        }

        double delta = b * b - 4 * a * c;

        if (delta < 0)
        {
            Console.WriteLine("Phuong trinh vo nghiem.");
        }
        else if (delta == 0)
        {
            double x = -b / (2 * a);

            Console.WriteLine(
                $"Phuong trinh co nghiem kep: x = {x}"
            );
        }
        else
        {
            double x1 =
                (-b - Math.Sqrt(delta)) / (2 * a);

            double x2 =
                (-b + Math.Sqrt(delta)) / (2 * a);

            Console.WriteLine($"x1 = {x1}");
            Console.WriteLine($"x2 = {x2}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConsoleMenu menu = new ConsoleMenu();

        PTBac2Console app = new PTBac2Console();

        menu.Choose += app.XuLyLuaChon;

        menu.Run();
    }
}
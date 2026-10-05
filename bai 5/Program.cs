double x = 0;
double y = 0;
bool isFilled = false;
String Choice;

//Sử dụng do while để menu được in ra ít nhất 1 lần để bắt đầu chương trình menu
do
{
    Console.Write(
    "MENU\n" +
    "1. Nhap hai gia tri so thuc cho x va y\n" +
    "2. Tinh x^y\n" +
    "3. Tinh can bac 2 cua x va y\n" +
    "4. Thoat\n" +
    "Chon chuc nang: "
    );
    Choice = Console.ReadLine();

    //1
    if (Choice == "1")
    {
        Console.Write("Nhap x: ");
        x = double.Parse(Console.ReadLine());

        Console.Write("Nhap y: ");
        y = double.Parse(Console.ReadLine());

        Console.WriteLine("");
        isFilled = true;
    }

    //2
    else if (Choice == "2")
    {
        if (!isFilled)
        {
            Console.WriteLine("-> Ban chua nhap gia tri x va y de tinh x^y\n");
        }
        else
        {
            double result = 1;

            int count = 0;
            while (count < y)
            {
                result = result * x;
                count++;
            }
            Console.WriteLine($"-> Ket qua cua x mu y la {result}\n");
        }
    }

    //3
    else if (Choice == "3")
    {
        if (!isFilled)
        {
            Console.WriteLine("->Ban chua nhap gia tri x va y de tinh can bac 2 cua chung\n");
        }
        else
        {
            double kq1 = Math.Sqrt(x);
            double kq2 = Math.Sqrt(y);

            Console.WriteLine($"-> Can bac hai cua x la {kq1}, can bac 2 cua y la {kq2}\n");
        }
    }

    else if (Choice == "4")
    {
        Console.WriteLine("Ket thuc chuong trinh.");
        return;
    }
} while (Choice != "4");


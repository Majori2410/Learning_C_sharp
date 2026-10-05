Console.Write("Nhap so nguyen x: ");

if (int.TryParse(Console.ReadLine(), out int x))
{
    Console.WriteLine("x hop le.");
}
else
{
    Console.WriteLine("x khong hop le, vui long nhap lai");
    return;
}

Console.Write("Nhap so nguyen y: ");
if (int.TryParse(Console.ReadLine(), out int y))
{
    Console.WriteLine("y hop le.");
}
else
{
    Console.WriteLine("y khong hop le");
    return;
}

int count = 0;
int kq = 1;

while (count < y)
{
    kq *= x;
    count++;
}

Console.WriteLine($"ket qua {x} mu {y} la {kq}");


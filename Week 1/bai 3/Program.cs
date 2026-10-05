Console.WriteLine("Nhap so nguyen x: ");
int x = int.Parse(Console.ReadLine());

Console.WriteLine("Nhap so nguyen y: ");
int y = int.Parse(Console.ReadLine());

int count = 0;
int result = 1;

while (count < y)
{
   result = result * x;
    count++;
}

Console.WriteLine($"Ket qua {x} mu {y} la {result}");

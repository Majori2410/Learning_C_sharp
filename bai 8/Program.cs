using System;

class Program
{
    //sử dụng void thay vì double vì method này không cần một giá trị trả về cụ thể. nó thay đổi trực tiếp hai biến được truyền vào bằng ref.
    static void hoanVi(ref double a, ref double b)
    {
        double temp = a;
        a = b;
        b = temp;
    }

    static void Main(String[] args)
    {
        Console.WriteLine("Nhap hai so ma ban muon hoan doi gia tri: ");
        double x = double.Parse(Console.ReadLine());
        double y = double.Parse(Console.ReadLine());

        hoanVi(ref x, ref y);

        Console.WriteLine($"Sau khi thuc hien hoan vi, ta co x = {x}, y = {y}");
    }
}

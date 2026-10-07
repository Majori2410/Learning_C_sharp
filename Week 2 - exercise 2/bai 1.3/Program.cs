using System;

class Person
{
    // Field
    private string id;
    private string name;
    private int yob;
    private int yod;

    // Default constructor
    public Person()
    {
        id = "";
        name = "";
        yob = 0;
        yod = 0;
    }

    // Copy constructor
    public Person(Person other)
    {
        id = other.id;
        name = other.name;
        yob = other.yob;
        yod = other.yod;
    }

    // Input
    public void Input()
    {
        Console.Write("Nhap ID: ");
        id = Console.ReadLine();

        Console.Write("Nhap ho ten: ");
        name = Console.ReadLine();

        Console.Write("Nhap nam sinh: ");
        yob = int.Parse(Console.ReadLine());

        Console.Write("Nhap nam mat (0 neu con song): ");
        yod = int.Parse(Console.ReadLine());
    }

    // IsLiving
    public bool IsLiving()
    {
        return yod == 0;
    }

    // Output
    public void Output()
    {
        Console.WriteLine("\nTHONG TIN PERSON");
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"Ho ten: {name}");
        Console.WriteLine($"Nam sinh: {yob}");
        Console.WriteLine($"Nam mat: {yod}");

        if (IsLiving())
        {
            Console.WriteLine("Trang thai: Con song");
        }
        else
        {
            Console.WriteLine("Trang thai: Da mat");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Person p1 = new Person();

        p1.Input();
        p1.Output();

        // Test copy constructor
        Person p2 = new Person(p1);

        Console.WriteLine("\nPerson sao chep:");
        p2.Output();
    }
}
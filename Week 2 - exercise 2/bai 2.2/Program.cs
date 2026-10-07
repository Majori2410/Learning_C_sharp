using System;

class Person
{
    private string id;
    private string name;
    private int yob;
    private int yod;

    public Person()
    {
        id = "";
        name = "";
        yob = 0;
        yod = 0;
    }

    public Person(Person other)
    {
        id = other.id;
        name = other.name;
        yob = other.yob;
        yod = other.yod;
    }

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

    public bool IsLiving()
    {
        return yod == 0;
    }

    public void Output()
    {
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"Ho ten: {name}");
        Console.WriteLine($"Nam sinh: {yob}");
        Console.WriteLine($"Nam mat: {yod}");
        Console.WriteLine($"Trang thai: {(IsLiving() ? "Con song" : "Da mat")}");
    }

    class PersonList
    {
        private List<Person> ds;

        // Default constructor
        public PersonList()
        {
            ds = new List<Person>();
        }

        // Copy constructor
        public PersonList(PersonList other)
        {
            ds = new List<Person>();

            foreach (Person p in other.ds)
            {
                ds.Add(new Person(p));
            }
        }

        // Add
        public void Add(Person x)
        {
            ds.Add(x);
        }

        // Input
        public void Input()
        {
            Console.Write("Nhap so luong nguoi: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nNhap nguoi thu {i + 1}:");

                Person p = new Person();
                p.Input();

                ds.Add(p);
            }
        }

        // Output
        public void Output()
        {
            Console.WriteLine("\nDANH SACH PERSON");

            for (int i = 0; i < ds.Count; i++)
            {
                Console.WriteLine($"\nNguoi thu {i + 1}:");
                ds[i].Output();
            }
        }

        // Tra ve danh sach nhung nguoi con song
        public PersonList LivingPeople()
        {
            PersonList result = new PersonList();

            foreach (Person p in ds)
            {
                if (p.IsLiving())
                {
                    result.Add(new Person(p));
                }
            }

            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PersonList list = new PersonList();

            list.Input();

            Console.WriteLine("\n===== DANH SACH TAT CA =====");
            list.Output();

            PersonList living = list.LivingPeople();

            Console.WriteLine("\n===== DANH SACH NGUOI CON SONG =====");
            living.Output();
        }
    }
}
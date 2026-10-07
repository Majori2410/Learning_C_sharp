using System;

class PhanSo
{
    private int tu;
    private int mau;

    public int Tu
    {
        get { return tu; }
        set { tu = value; }
    }

    public int Mau
    {
        get { return mau; }
        set
        {
            if (value != 0)
                mau = value;
        }
    }

    // Default constructor
    public PhanSo()
    {
        tu = 0;
        mau = 1;
    }

    // Constructor co tham so
    public PhanSo(int tu, int mau)
    {
        if (mau == 0)
            throw new ArgumentException("Mau so khong duoc bang 0.");

        this.tu = tu;
        this.mau = mau;

        RutGon();
    }

    // Copy constructor
    public PhanSo(PhanSo other)
    {
        tu = other.tu;
        mau = other.mau;
    }

    private int UCLN(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);

        while (b != 0)
        {
            int r = a % b;
            a = b;
            b = r;
        }

        return a;
    }

    private void RutGon()
    {
        int ucln = UCLN(tu, mau);

        if (ucln != 0)
        {
            tu /= ucln;
            mau /= ucln;
        }

        if (mau < 0)
        {
            tu = -tu;
            mau = -mau;
        }
    }

    public override string ToString()
    {
        return $"{tu}/{mau}";
    }

    // Toan tu mot ngoi +
    public static PhanSo operator +(PhanSo a)
    {
        return new PhanSo(a.tu, a.mau);
    }

    // Toan tu mot ngoi -
    public static PhanSo operator -(PhanSo a)
    {
        return new PhanSo(-a.tu, a.mau);
    }

    // Cong
    public static PhanSo operator +(PhanSo a, PhanSo b)
    {
        int tuMoi = a.tu * b.mau + b.tu * a.mau;
        int mauMoi = a.mau * b.mau;

        return new PhanSo(tuMoi, mauMoi);
    }

    // Tru
    public static PhanSo operator -(PhanSo a, PhanSo b)
    {
        int tuMoi = a.tu * b.mau - b.tu * a.mau;
        int mauMoi = a.mau * b.mau;

        return new PhanSo(tuMoi, mauMoi);
    }

    // Nhan
    public static PhanSo operator *(PhanSo a, PhanSo b)
    {
        return new PhanSo(
            a.tu * b.tu,
            a.mau * b.mau
        );
    }

    // Chia
    public static PhanSo operator /(PhanSo a, PhanSo b)
    {
        if (b.tu == 0)
            throw new DivideByZeroException();

        return new PhanSo(
            a.tu * b.mau,
            a.mau * b.tu
        );
    }

    // So sanh
    public static bool operator >(PhanSo a, PhanSo b)
    {
        return a.tu * b.mau > b.tu * a.mau;
    }

    public static bool operator <(PhanSo a, PhanSo b)
    {
        return a.tu * b.mau < b.tu * a.mau;
    }

    public static bool operator >=(PhanSo a, PhanSo b)
    {
        return a.tu * b.mau >= b.tu * a.mau;
    }

    public static bool operator <=(PhanSo a, PhanSo b)
    {
        return a.tu * b.mau <= b.tu * a.mau;
    }

    public static bool operator ==(PhanSo a, PhanSo b)
    {
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.tu == b.tu && a.mau == b.mau;
    }

    public static bool operator !=(PhanSo a, PhanSo b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is PhanSo other)
        {
            return this == other;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(tu, mau);
    }
}

class Program
{
    static void Main(string[] args)
    {
        PhanSo a = new PhanSo(1, 2);
        PhanSo b = new PhanSo(3, 4);

        Console.WriteLine($"a = {a}");
        Console.WriteLine($"b = {b}");

        Console.WriteLine($"+a = {+a}");
        Console.WriteLine($"-a = {-a}");

        Console.WriteLine($"a + b = {a + b}");
        Console.WriteLine($"a - b = {a - b}");
        Console.WriteLine($"a * b = {a * b}");
        Console.WriteLine($"a / b = {a / b}");

        Console.WriteLine($"a > b: {a > b}");
        Console.WriteLine($"a < b: {a < b}");
        Console.WriteLine($"a >= b: {a >= b}");
        Console.WriteLine($"a <= b: {a <= b}");
        Console.WriteLine($"a == b: {a == b}");
        Console.WriteLine($"a != b: {a != b}");
    }
}
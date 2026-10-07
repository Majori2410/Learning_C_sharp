using System;

class SinhVien
{
    // Field
    private string hoTen;
    private int namSinh;

    // Property
    public string HoTen
    {
        get { return hoTen; }
        set { hoTen = value; }
    }

    public int NamSinh
    {
        get { return namSinh; }
        set { namSinh = value; }
    }

    // Default constructor
    public SinhVien()
    {
        hoTen = "";
        namSinh = 0;
    }

    // Parameter constructor
    public SinhVien(string hoTen, int namSinh)
    {
        this.hoTen = hoTen;
        this.namSinh = namSinh;
    }

    // Method nhập
    public void Nhap()
    {
        Console.Write("Nhap ho ten sinh vien: ");
        hoTen = Console.ReadLine();

        Console.Write("Nhap nam sinh: ");
        namSinh = int.Parse(Console.ReadLine());
    }

    // Method tính tuổi
    public int TinhTuoi()
    {
        int namHienTai = DateTime.Now.Year;
        return namHienTai - namSinh;
    }

    // Method xuất
    public void Xuat()
    {
        Console.WriteLine("\nTHONG TIN SINH VIEN");
        Console.WriteLine($"Ho ten: {hoTen}");
        Console.WriteLine($"Nam sinh: {namSinh}");
        Console.WriteLine($"Tuoi: {TinhTuoi()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        SinhVien sv = new SinhVien();

        sv.Nhap();
        sv.Xuat();
    }
}
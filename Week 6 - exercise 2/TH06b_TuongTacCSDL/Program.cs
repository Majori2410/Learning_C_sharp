using System;
using System.Windows.Forms;

namespace Bai1_QuanLySinhVien_06b;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
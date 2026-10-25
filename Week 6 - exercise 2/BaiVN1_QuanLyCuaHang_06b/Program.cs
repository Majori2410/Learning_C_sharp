
using System;
using System.Windows.Forms;

namespace BaiVN1_QuanLyCuaHang_06b;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}


using System;
using System.Windows.Forms;

namespace BaiNC1_DanhSachLop_06b;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

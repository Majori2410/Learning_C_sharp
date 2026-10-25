
using System;
using System.Windows.Forms;

namespace BaiNC2_DanhSachDiem_06b;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

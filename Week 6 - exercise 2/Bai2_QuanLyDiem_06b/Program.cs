
using System;
using System.Windows.Forms;

namespace Bai2_QuanLyDiem_06b;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

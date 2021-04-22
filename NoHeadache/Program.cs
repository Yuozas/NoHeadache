using System;
using System.Windows.Forms;

namespace NoHeadache
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            LowLevelKeyboardHookHandler.Run();
            Application.Run(new App());
            LowLevelKeyboardHookHandler.Stop();
        }
    }
}
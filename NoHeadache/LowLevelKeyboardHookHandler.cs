using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NoHeadache
{
    public static class LowLevelKeyboardHookHandler
    {
        #region Dll Method Imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcess lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hhk, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion Dll Method Imports

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static readonly LowLevelKeyboardProcess process = OnHooked;
        private static IntPtr hookID = IntPtr.Zero;

        public static void Run() => hookID = SetHook(process);

        public static void Stop() => UnhookWindowsHookEx(hookID);

        private static IntPtr SetHook(LowLevelKeyboardProcess proc) => SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);

        private delegate int LowLevelKeyboardProcess(int nCode, int wParam, IntPtr lParam);

        private static int OnHooked(int nCode, int wParam, IntPtr lParam)
        {
            if (wParam == WM_KEYDOWN && ShiftHandler.TryShift((Keys)Marshal.ReadInt32(lParam)))
                return 1;
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
    }
}
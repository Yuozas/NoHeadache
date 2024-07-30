using System.Runtime.InteropServices;

namespace LowLevelKeyboardHookLibrary;

public static partial class LowLevelKeyboardHookHandler
{
#region Dll Method Imports
	
	[LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExW", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	private static partial IntPtr SetWindowsHookEx
	(
			int                     idHook,
			LowLevelKeyboardProcess lpfn,
			IntPtr                  hMod,
			uint                    dwThreadId
	);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool UnhookWindowsHookEx(IntPtr hhk);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	private static partial int CallNextHookEx
	(
			IntPtr hhk,
			int    nCode,
			int    wParam,
			IntPtr lParam
	);
	
	[LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	private static partial IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
	
#endregion Dll Method Imports
	
	private const           int                     WH_KEYBOARD_LL = 13;
	private const           int                     WM_KEYDOWN     = 0x0100;
	private static readonly LowLevelKeyboardProcess Process        = OnHooked;
	private static          IntPtr                  _hookId        = IntPtr.Zero;
	
	public static void Run() => _hookId = SetHook(Process);
	
	public static void Stop() => UnhookWindowsHookEx(_hookId);
	
	private static IntPtr SetHook(LowLevelKeyboardProcess proc) => SetWindowsHookEx(WH_KEYBOARD_LL,
	                                                                                proc,
	                                                                                GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule?.ModuleName
	                                                                                             ?? throw new("Failed to get main module name")),
	                                                                                0);
	
	private delegate int LowLevelKeyboardProcess(int nCode, int wParam, IntPtr lParam);
	
	private static int OnHooked(int nCode, int wParam, IntPtr lParam)
	{
		if (wParam == WM_KEYDOWN && ShiftHandler.TryShift((Keys)Marshal.ReadInt32(lParam)))
			return 1;
		
		return CallNextHookEx(_hookId, nCode, wParam, lParam);
	}
}
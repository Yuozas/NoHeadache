using LowLevelKeyboardHookLibrary.Models;
using LowLevelKeyboardHookLibrary.Utils;

namespace LowLevelKeyboardHookLibrary;

public static partial class LowLevelKeyboardHookHandler
{
#region Dll Method Imports
	
	[LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExW", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	private static partial IntPtr SetWindowsHookEx(
			int                     idHook,
			LowLevelKeyboardProcess lpfn,
			IntPtr                  hMod,
			uint                    dwThreadId
	);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool UnhookWindowsHookEx(IntPtr hhk);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	private static partial int CallNextHookEx(
			IntPtr hhk,
			int    nCode,
			int    wParam,
			IntPtr lParam
	);
	
	[LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
	private static partial IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
	private static partial IntPtr GetKeyboardLayout(uint idThread);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
	private static partial uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
	
	[LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
	private static partial IntPtr GetForegroundWindow();
	
#endregion Dll Method Imports
	
	private const           int                     WH_KEYBOARD_LL = 13;
	private const           int                     WM_KEYDOWN     = 0x0100;
	private const           int                     WM_KEYUP       = 0x0101;
	private static readonly LowLevelKeyboardProcess Process        = OnHooked;
	private static          IntPtr                  _hookId        = IntPtr.Zero;
	
	private static List<(Keys key, bool isInjected)> _pressedKeys = [];
	// Available keyboard layouts with their names and identifiers
	public static readonly IReadOnlyCollection<KeyboardLayout> AvailableKeyboardLayouts =
	[
		new("US", 0x0409),
		new("UK", 0x0809),
		new("French", 0x040C),
		new("German", 0x0407),
		// Add more keyboard layouts as needed
	];
	
	private static int[] _desiredKeyboardLayoutIds = [0x0409]; // Default to US English layout
	
	public static void Run(params int[] desiredLayoutIds)
	{
		if (desiredLayoutIds.Length > 0)
			_desiredKeyboardLayoutIds = desiredLayoutIds;
		
		_hookId = SetHook(Process);
	}
	
	public static void Stop() => UnhookWindowsHookEx(_hookId);
	
	private static IntPtr SetHook(LowLevelKeyboardProcess proc) => SetWindowsHookEx(WH_KEYBOARD_LL,
	                                                                                proc,
	                                                                                GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule?.ModuleName
	                                                                                             ?? throw new Exception("Failed to get main module name")),
	                                                                                0);
	
	private delegate int LowLevelKeyboardProcess(int nCode, int wParam, IntPtr lParam);
	
	private static int OnHooked(int nCode, int wParam, IntPtr lParam)
	{
		if (!IsDesiredKeyboardLayout() || nCode < 0)
			return CallNextHookEx(_hookId, nCode, wParam, lParam);
		
		var vkCode       = Marshal.ReadInt32(lParam);
		var key          = (Keys)vkCode;
		var keyboardInfo = Marshal.PtrToStructure<KeyboardDllHookStruct>(lParam);
		var isInjected   = KeyboardHookUtils.IsInjected(keyboardInfo.flags);
		
		switch (wParam)
		{
			case WM_KEYDOWN:
				_pressedKeys.Add((key, isInjected));
				break;
			case WM_KEYUP:
				_pressedKeys = _pressedKeys.Where(pair => !(pair.key == key && pair.isInjected == isInjected)).ToList();
				break;
		}
		
		if (wParam is WM_KEYDOWN && (ShiftHandler.TryNegateRegularShift(_pressedKeys, key) || ShiftHandler.TryShift(_pressedKeys, key)))
			return 1;
		
		return CallNextHookEx(_hookId, nCode, wParam, lParam);
	}
	
	private static bool IsDesiredKeyboardLayout()
	{
		var foregroundWindow = GetForegroundWindow();
		var   threadId = GetWindowThreadProcessId(foregroundWindow, out _);
		
		var keyboardLayout  = GetKeyboardLayout(threadId);
		var currentLayoutId = keyboardLayout.ToInt32() & 0xFFFF;
		return _desiredKeyboardLayoutIds.Any(id => id == currentLayoutId);
	}
}
namespace LowLevelKeyboardHookLibrary.Models;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardDllHookStruct
{
	public uint   vkCode;      // Virtual-key code
	public uint   scanCode;    // Hardware scan code
	public uint   flags;       // Flags
	public uint   time;        // Timestamp for the message
	public IntPtr dwExtraInfo; // Additional information associated with the message
}
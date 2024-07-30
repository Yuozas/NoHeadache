namespace LowLevelKeyboardHookLibrary.Utils;

public static class KeyboardHookUtils
{
	private const int LLKHF_INJECTED = 0x00000010;
	
	public static bool IsInjected(uint flags) => (flags & LLKHF_INJECTED) != 0;
}
namespace LowLevelKeyboardHookLibrary.Utils;

public static class KeyUtils
{
	public static bool SymbolKeyboardPartIsPressed(Keys key) => key is >= Keys.D0 and <= Keys.D9;
	public static bool ShiftIsPressed(Keys              key) => key is Keys.ShiftKey or Keys.LShiftKey or Keys.RShiftKey;
}
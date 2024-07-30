namespace LowLevelKeyboardHookLibrary;

static class ShiftHandler
{
	public static bool TryShift(Keys key) => key switch
	{
		>= Keys.D0 and <= Keys.D9 => HandleShiftKey(key),
		_                         => false
	};
	
	private static bool HandleShiftKey(Keys key)
	{
		KeyPoster.ShiftKey(key);
		return true;
	}
}
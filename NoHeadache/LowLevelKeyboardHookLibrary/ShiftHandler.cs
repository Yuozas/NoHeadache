namespace LowLevelKeyboardHookLibrary;

static class ShiftHandler
{
	public static bool TryShift(Keys key) => key switch
	{
		Keys.D0 or Keys.D1 or Keys.D2 or Keys.D3 or Keys.D4 or Keys.D5 or Keys.D6 or Keys.D7 or Keys.D8 or Keys.D9
				=> HandleShiftKey(key),
		_ => false
	};
	
	private static bool HandleShiftKey(Keys key)
	{
		KeyPoster.ShiftKey(key);
		return true;
	}
}
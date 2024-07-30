namespace LowLevelKeyboardHookLibrary;

public static partial class KeyPoster
{
#region Dll Method Imports
	
	[LibraryImport("user32.dll")]
	private static partial void keybd_event
	(
			byte bVk,
			byte bScan,
			int  dwFlags,
			int  dwExtraInfo
	);
	
#endregion Dll Method Imports
	
	public static void ShiftKey(Keys key)
	{
		PressKey(Keys.ShiftKey);
		PressReleaseKey(key);
		ReleaseKey(Keys.ShiftKey);
	}
	
	private static void PressReleaseKey(Keys key)
	{
		PressKey(key);
		ReleaseKey(key);
	}
	
	private static void PressKey(Keys key) => keybd_event((byte)key, 0, 1 | 0, 0);
	
	private static void ReleaseKey(Keys key) => keybd_event((byte)key, 0, 1 | 2, 0);
}
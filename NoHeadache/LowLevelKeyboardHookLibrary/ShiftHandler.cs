using LowLevelKeyboardHookLibrary.Utils;

namespace LowLevelKeyboardHookLibrary;

static class ShiftHandler
{
	public static bool TryShift(IEnumerable<(Keys key, bool isInjected)> pressedKeys, Keys key) => KeyUtils.SymbolKeyboardPartIsPressed(key) && HandleShiftKey(pressedKeys, key);
	
	public static bool TryNegateRegularShift
			(IReadOnlyCollection<(Keys key, bool isInjected)> pressedKeys, Keys key) => KeyUtils.SymbolKeyboardPartIsPressed(key)
			                                                                         && pressedKeys.Any(pressedKey => !pressedKey.isInjected && KeyUtils.ShiftIsPressed(pressedKey.key))
			                                                                         && NegateRegularShift(pressedKeys, key);
	
	private static bool HandleShiftKey(IEnumerable<(Keys key, bool isInjected)> pressedKeys, Keys key)
	{
		var symbolPressedByUser = pressedKeys.Any(pressedKey => !pressedKey.isInjected && KeyUtils.SymbolKeyboardPartIsPressed(pressedKey.key));
		if(symbolPressedByUser)
			KeyPoster.ShiftKey(key);
		return symbolPressedByUser;
	}
	
	private static bool NegateRegularShift(IEnumerable<(Keys key, bool isInjected)> pressedKeys, Keys key)
	{
		KeyPoster.ReleaseShiftPressKey(key, pressedKeys.Select(p => p.key).Where(KeyUtils.ShiftIsPressed).ToArray());
		return true;
		// TODO: negate shift.
		// return false;
	}
}
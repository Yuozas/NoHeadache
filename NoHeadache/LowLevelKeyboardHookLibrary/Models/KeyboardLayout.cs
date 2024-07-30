namespace LowLevelKeyboardHookLibrary.Models;

public class KeyboardLayout(string name, int id)
{
	public string Name { get; } = name;
	public int    Id   { get; } = id;
}
namespace WinForms;

using LowLevelKeyboardHookLibrary;

static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		LowLevelKeyboardHookHandler.Run();
		Application.Run(new MainForm());
		LowLevelKeyboardHookHandler.Stop();
	}
}
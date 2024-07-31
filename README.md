# NoHeadache

**NoHeadache** is a lightweight background utility for Windows that simplifies typing by automatically converting number keys (D0-D9) to their corresponding symbols (e.g., 1 to !, 2 to @) without needing to press the Shift key. 

It’s perfect for users who frequently type special characters and want to streamline their workflow.

Affected Keys:

![Affected keys](https://github.com/user-attachments/assets/52101b5a-5799-4871-bb7d-8ec640638d22)

## Features

- Runs in the background with no setup required
- **Automatic Key Conversion**: When you press any number key (D0-D9), `NoHeadache` automatically applies the Shift functionality, converting it to the corresponding symbol. For example:
  - Pressing `1` results in `!`
  - Pressing `2` results in `@`
  - And so on...

- **Default Behavior with Shift**: If the Shift key is pressed, the number keys (D0-D9) function as usual, allowing for normal input without conversion.

- **Language-Sensitive Operation**: Currently, the app only operates with the US keyboard layout. If another language layout is detected, the app remains inactive, ensuring it doesn’t interfere with your typing.

## Installation

1. Download the latest release ZIP file from the [Releases](https://github.com/Yuozas/NoHeadache/releases) page.
2. Extract the contents of the ZIP file to your desired location.
3. Run the `.exe` file to start the application.

No additional installation or building of the solution is required.

## Usage

1. Launch `.exe`.
2. The application will run in the background, listening for D0-D9 key presses.
3. Type normally, and the app will automatically convert number keys to their shifted versions.

## Technical Details

- **Platform**: Windows
- **Framework**: .NET 8
- **Dependencies**: Utilizes `user32.dll` and `kernel32.dll` from the Windows API
- **User Interface**: WinForms

## Known Issues

- Some Windows 11 applications (e.g., the new Notepad) may not detect language changes correctly, causing unexpected behavior.

## Planned Features

- Language selection dropdown to specify which languages the app should work with
- Enable/disable checkbox for quick toggling of the app's functionality
- Configuring which keys should be affected allows for custom shift behavior. This feature is handy for users programming on keyboards with UK language layouts and similar configurations.
- Single executable file for easier distribution
- Both portable and installer editions

## Technical Details

- Developed for Windows using .NET 8
- Utilizes Windows-specific DLLs: `user32.dll` and `kernel32.dll`
- Implemented as a WinForms application

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue if you have suggestions or improvements.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

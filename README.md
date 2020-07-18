# gameboy-printer-emulator-client
A client for the GameBoy Printer emulator written in C#

![Preview](https://i.imgur.com/dzW44Gd.gif)

## Usage
You'll need the following:
- A Game Boy (any model that supports DMG games) with a Game Boy Camera Cartridge
- An Arduino or similar with [Arduino Gameboy Printer Emulator](https://github.com/mofosyne/arduino-gameboy-printer-emulator) flashed on it
- A spare Game Boy Link cable (see above for pinout reference)

Got that? That's great:
- Snap all the pictures you want to "print"
- Connect the GameBoy to the Arduino using the Link Cable
- Connect the Arduino to your PC via USB (make sure you have the drivers installed)
- Download the [latest release](https://github.com/LucaCorigliano/gameboy-printer-emulator-client/releases/latest)
- Open it and select the correct COM port for your Arduino (Baud Rate is set to 115200 by default)
- Click Connect
- Start the printing from the Game Boy
- Enjoy!

## Contributing
As you'll see the code is quite ugly as well as the User Interface. It'll be great to have a cleanup (which I'll probably do in the future) and a UI overhaul.

Maybe you could add custom Palette loading and something like that.

(Oh yeah! Error Handling would be great as well!)

## Credits
- [Arduino Gameboy Printer Emulator](https://github.com/mofosyne/arduino-gameboy-printer-emulator)
- [Huderlem](https://www.huderlem.com/demos/gameboy2bpp.html) For the GameBoy Tile decoding code!
- [Lospec](https://lospec.com/) for the Palettes

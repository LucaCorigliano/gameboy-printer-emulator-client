using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameboy_printer_windows
{
    public static class Constants
    {
        /// <summary>
        /// How much should the program read from serial
        /// </summary>
        public const int SERIAL_BUFFER_LEN = 512;
        /// <summary>
        /// Arduino serial baud rate
        /// </summary>
        public const int SERIAL_BAUD_RATE = 115200;


        /// <summary>
        /// How wide is a Game Boy tile
        /// </summary>
        public const int TILE_PIXEL_WIDTH = 8;
        /// <summary>
        /// How tall is a Game Boy tile
        /// </summary>
        public const int TILE_PIXEL_HEIGHT = 8;
        /// <summary>
        /// How many tiles per screen line
        /// </summary>
        public const int TILES_PER_LINE = 20;
    }
}

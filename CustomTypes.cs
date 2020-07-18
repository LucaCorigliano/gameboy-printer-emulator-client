using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace gameboy_printer_windows
{
    /// <summary>
    /// Defines a custom game boy palette (4 colours and a name)
    /// </summary>
    public struct Palette
    {
        public string name;
        public uint[] palette;
        public override string ToString()
        {
            return name;
        }
    };

    /// <summary>
    /// For rapid deserialization from json
    /// </summary>
    public struct StringPalette
    {
        public string name;
        public string[] palette;
        public override string ToString()
        {
            return name;
        }
    };

    /// <summary>
    /// Printing status
    /// </summary>
    public enum PrintStatus
    {
        /// <summary>
        /// Serial Port not connected
        /// </summary>
        NOT_CONNECTED,
        /// <summary>
        /// Serial Port connected, idle
        /// </summary>
        CONNECTED,
        /// <summary>
        /// Receiving data
        /// </summary>
        PRINTING,
        /// <summary>
        /// Data received
        /// </summary>
        DONE
    };

}

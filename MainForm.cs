
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace gameboy_printer_windows
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Printing status
        /// </summary>
        private enum PrintStatus
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

        /// <summary>
        /// Defines a custom game boy palette (4 colours and a name)
        /// </summary>
        private struct Palette
        {
            public string name;
            public uint[] palette;
            public override string ToString()
            {
                return name;
            }
        };

        /// <summary>
        /// Available palettes
        /// Credit: https://lospec.com/
        /// </summary>
        private Palette[] m_Palettes =  {

                new Palette
                {
                    name = "Original DMG",
                    palette = new uint[]
                    {
                        0xFFE0F8D0,
                        0xFF88C070,
                        0xFF346856,
                        0xFF081820
                    }
                },
                new Palette
                {
                    name = "Original DMG Blue",
                    palette = new uint[]
                    {
                        0xFFd1d0f8,
                        0xFF7074c0,
                        0xFF343968,
                        0xFF0a0820
                    }
                },
                new Palette
                {
                    name = "Original DMG Red",
                    palette = new uint[]
                    {
                        0xFFf8d0d0,
                        0xFFc07070,
                        0xFF683434,
                        0xFF200808
                    }
                },
                new Palette
                {
                    name = "Original DMG Grayscale",
                    palette = new uint[]
                    {
                        0xFFf7f7f7,
                        0xFFbfbfbf,
                        0xFF696969,
                        0xFF212121
                    }
                },
                new Palette
                {
                    name = "Ice Cream by Kerrie Lake",
                    palette = new uint[]
                    {
                        0xFFfff6d3,
                        0xFFf9a875,
                        0xFFeb6b6f,
                        0xFF7c3f58
                    }
                },
                new Palette
                {
                    name = "Kirokaze Gameboy by Kirokaze",
                    palette = new uint[]
                    {
                        0xFFe2f3e4,
                        0xFF94e344,
                        0xFF46878f,
                        0xFF332c50
                    }
                },
                new Palette
                {
                    name = "Rustic GB by Kerrie Lake",
                    palette = new uint[]
                    {
                        0xFFedb4a1,
                        0xFFa96868,
                        0xFF764462,
                        0xFF2c2137
                    }
                },
                new Palette
                {
                    name = "Mist GB by Kerrie Lake",
                    palette = new uint[]
                    {
                        0xFFc4f0c2,
                        0xFF5ab9a8,
                        0xFF1e606e,
                        0xFF2d1b00
                    }
                },
                new Palette
                {
                    name = "AYY4 by Polyducks",
                    palette = new uint[]
                    {
                        0xFFf1f2da,
                        0xFFffce96,
                        0xFFff7777,
                        0xFF00303b
                    }
                },
                new Palette
                {
                    name = "SpaceHaze by WildLeoKnight",
                    palette = new uint[]
                    {
                        0xFFf8e3c4,
                        0xFFcc3495,
                        0xFF6b1fb1,
                        0xFF0b0630,

                    }
                },
                new Palette
                {
                    name = "Wish GB by Kerrie Lake",
                    palette = new uint[]
                    {
                        0xFF8be5ff,
                        0xFF608fcf ,
                        0xFF7550e8 ,
                        0xFF622e4c ,

                    }
                },

        };




        // Hidden stuff, use properties
        private bool mh_bIsConnected = false;
        private PrintStatus mh_PrintStatus = PrintStatus.NOT_CONNECTED;

        private PrintStatus m_PrintStatus
        {
            get
            {
                return mh_PrintStatus;
            }
            set
            {
                SetStatus(string.Format("Status: {0}!", value.ToString()));
                mh_PrintStatus = value;
            }
        }
        private bool m_bIsConnected
        {
            get
            {
                return mh_bIsConnected;
            }
            set
            {
                comboSerialPort.Enabled = !value;
                btnSerialConnect.Text = value ? "Disconnect" : "Connect";
                m_PrintStatus = value ? PrintStatus.CONNECTED : PrintStatus.NOT_CONNECTED;
                mh_bIsConnected = value;
            }
        }
        private List<string> m_ListAvailablePortNames;
        private SerialPort m_ConnectedSerialPort;

        private const int SERIAL_BUFFER_LEN = 512;
        private const int TILE_PIXEL_WIDTH = 8;
        private const int TILE_PIXEL_HEIGHT = 8;
        private const int TILES_PER_LINE = 20; // Gameboy Printer Tile Constant
        private byte[] m_SerialBuffer = new byte[SERIAL_BUFFER_LEN];



        private List<byte[]> m_Tiles = new List<byte[]>();




        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateSerialPorts()
        {
            if (m_bIsConnected)
                return;
            m_ListAvailablePortNames = SerialPort.GetPortNames().ToList();
        }

        private void MainForm_OnLoad(object sender, EventArgs e)
        {
            UpdateSerialPorts();
            comboSerialPort.DataSource = m_ListAvailablePortNames;
            comboColorPalette.DataSource = m_Palettes;
            comboMagnify.SelectedIndex = 1;

        }

        private void comboSerialPort_DropDown(object sender, EventArgs e)
        {
            UpdateSerialPorts();
        }

        private void btnSerialConnect_Click(object sender, EventArgs e)
        {
            if (m_bIsConnected)
            {
                m_ConnectedSerialPort.Close();
                m_bIsConnected = false;
            }
            else
            {
                m_ConnectedSerialPort = new SerialPort((string)comboSerialPort.SelectedItem, 115200);
                m_ConnectedSerialPort.ReadTimeout = 500;
                m_ConnectedSerialPort.DataReceived += OnSerialDataReceived;
                m_ConnectedSerialPort.NewLine = "\n";
                //m_ConnectedSerialPort.ReceivedBytesThreshold = SERIAL_BUFFER_LEN;
                m_ConnectedSerialPort.Open();
                m_bIsConnected = true;
            }

        }

        string m_strDecodedBuffer;
        private void OnSerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var len = m_ConnectedSerialPort.Read(m_SerialBuffer, 0, SERIAL_BUFFER_LEN);

            string buffer = m_strDecodedBuffer + Encoding.Default.GetString(m_SerialBuffer, 0, len);

            m_strDecodedBuffer = "";

            var lines = buffer.Split('\n');

            for (int i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                OnLineReceived(line.Trim());
            }
            m_strDecodedBuffer = lines[lines.Length - 1];






        }

        private void OnLineReceived(string line)
        {
            LogData(line);
            ProcessData(line);
        }



        // I still don't get how a delegate should help with safe multithreading, but sure.
        private delegate void TextDelegate(string text);
        private void LogData(string text)
        {
            if (txtSerialLog.InvokeRequired)
            {
                var d = new TextDelegate(LogData);
                txtSerialLog.Invoke(d, new object[] { text });
            }
            else
            {
                txtSerialLog.AppendText(text + "\r\n");
            }
        }
        private void SetStatus(string text)
        {
            if (lblStatus.InvokeRequired)
            {
                var d = new TextDelegate(SetStatus);
                lblStatus.Invoke(d, new object[] { text });
            }
            else
            {
                lblStatus.Text = text;
            }
        }

        private delegate void RefreshImageDelegate();
        private void RefreshPreview()
        {
            if (picPanel.InvokeRequired)
            {
                var d = new RefreshImageDelegate(RefreshPreview);
                picPanel.Invoke(d);
            }
            else
            {
                Bitmap b = UpdateBitmap();


                picPanel.BackgroundImage = b;
            }
        }


        private void ProcessData(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            // NOW, I KNOW I SHOULD DO JSON PARSING BUT IT'S REALLY UNNECESSARY HERE
            if (text.StartsWith("!"))
            {
                var json = text.Substring(1);
                Console.WriteLine("[DBG] Parsing as json line {0}", json);
                dynamic jsondata = JValue.Parse(json);

                switch ((string)jsondata.command)
                {
                    case "INIT":
                        Console.WriteLine("[DBG] Print starting! {0}", jsondata.command);
                        m_PrintStatus = PrintStatus.PRINTING;
                        m_Tiles.Clear();
                        break;
                    case "DATA":
                        if (jsondata.more == 0)
                        {
                            Console.WriteLine("[DBG] Print ended! {0}", jsondata.command);
                            m_PrintStatus = PrintStatus.DONE;

                        }

                        break;
                    default:
                        Console.WriteLine("[DBG] Unknown command {0}", jsondata.command);
                        break;

                }
                return;
            }
            else if (text.StartsWith("#"))
            {
                Console.WriteLine("[DBG] Ignoring line {0}", text);
                return;
            }

            var bytes = text
                .Split(' ')                               // Split into items 
                .Select(item => Convert.ToByte(item, 16)) // Convert each item into byte
                .ToArray();                               // Materialize as array

            // Convert to a single tile
            // Convert to bitmap
            m_Tiles.Add(decode(bytes));
            RefreshPreview();




        }

        private byte[] decode(byte[] bytes)
        {


            var pixels = new byte[TILE_PIXEL_WIDTH * TILE_PIXEL_HEIGHT];
            for (var j = 0; j < TILE_PIXEL_HEIGHT; j++)
            {
                for (byte i = 0; i < TILE_PIXEL_WIDTH; i++)
                {
                    var hiBit = (bytes[j * 2 + 1] >> (7 - i)) & 1;
                    var loBit = (bytes[j * 2] >> (7 - i)) & 1;
                    pixels[j * TILE_PIXEL_WIDTH + i] = (byte)((hiBit << 1) | loBit);


                }
            }
            return pixels;
        }






        Bitmap UpdateBitmap()
        {
            int magnification = new int[] { 1, 2, 4 }[ Math.Max(0, comboMagnify.SelectedIndex)];


            int imageWidth = TILES_PER_LINE * TILE_PIXEL_WIDTH;
            int imageHeight = Math.Max(1, (int)Math.Ceiling((double)m_Tiles.Count / TILES_PER_LINE)) * TILE_PIXEL_HEIGHT;

            uint[,] imageData = new uint[imageHeight, imageWidth];

            int offsetX = 0;
            int offsetY = 0;

            uint[] palette = new uint[4];

            Array.Copy(((Palette)comboColorPalette.SelectedItem).palette, palette, 4);


            if (chkInvertPalette.Checked)
                Array.Reverse(palette);

          

            foreach (var tile in m_Tiles)
            {
                int i = 0;
                for (int y = 0; y < TILE_PIXEL_HEIGHT; y++)
                {
                    for (int x = 0; x < TILE_PIXEL_WIDTH; x++)
                        imageData[offsetY + y, offsetX + x] = palette[tile[i++]];

                }
                offsetX += TILE_PIXEL_WIDTH;
                if (offsetX >= TILE_PIXEL_WIDTH * TILES_PER_LINE)
                {
                    offsetX = 0;
                    offsetY += TILE_PIXEL_HEIGHT;
                }
            }


            // To byte array
            byte[] imageDataBytes = new byte[imageData.Length * sizeof(uint)];
            Buffer.BlockCopy(imageData, 0, imageDataBytes, 0, imageDataBytes.Length);

            var bmp = new Bitmap(imageWidth, imageHeight, PixelFormat.Format32bppArgb);

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
                                                            bmp.Width,
                                                            bmp.Height),
                                                ImageLockMode.WriteOnly,
                                                bmp.PixelFormat);

            IntPtr pNative = bmpData.Scan0;
            Marshal.Copy(imageDataBytes, 0, pNative, imageDataBytes.Length);

            bmp.UnlockBits(bmpData);

            if (magnification > 1)
                bmp = ResizeBitmap(bmp, magnification);


            return bmp;
        }

        private Bitmap ResizeBitmap(Bitmap src, int magnification) {
            int dstWidth = src.Width * magnification;
            int dstHeight = src.Height * magnification;
            Bitmap result = new Bitmap(dstWidth, dstHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(src, 0, 0, dstWidth, dstHeight);
            }
            return result;
        }




        private void comboColorPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void chkInvertPalette_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreview();

        }

        private void comboMagnify_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap | *.bmp|JPEG | *.jpg|PNG | *.png";
            sfd.DefaultExt = "bmp";

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imageFormat = ImageFormat.Bmp;
                var extension = Path.GetExtension(sfd.FileName);
                
                switch (extension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                }

                Bitmap bmp = UpdateBitmap();
                bmp.Save(sfd.FileName, imageFormat);
            }
        }
    }
}
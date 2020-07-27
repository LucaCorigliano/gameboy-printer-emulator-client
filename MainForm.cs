
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using static gameboy_printer_windows.Constants;
using static gameboy_printer_windows.ImageProcessing;


namespace gameboy_printer_windows
{
    public partial class MainForm : Form
    {


        #region Hidden by Properties
        private bool mh_bIsConnected = false;
        private PrintStatus mh_PrintStatus = PrintStatus.NOT_CONNECTED;
        #endregion

       
        private PrintStatus m_PrintStatus
        {
            get
            {
                return mh_PrintStatus;
            }
            set
            {


                SetStatus(value);
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
        private List<string> m_listAvailablePortNames;
        private SerialPort m_connectedSerialPort;
        private byte[] m_serialBuffer = new byte[SERIAL_BUFFER_LEN];
        private List<byte[]> m_listTiles = new List<byte[]>();
        private List<Palette> m_listPalettes;
        private Bitmap m_currentBitmap;






        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateSerialPorts()
        {
            comboSerialPort.DataSource = null;
            m_listAvailablePortNames = SerialPort.GetPortNames().ToList();
            comboSerialPort.DataSource = m_listAvailablePortNames;
        }
        private void UpdatePalettes()
        {
            comboColorPalette.DataSource = null;
            try
            {
                m_listPalettes = PaletteLoader.FetchPalettes("palette.json");
            } catch (FileNotFoundException)
            {
                MessageBox.Show("Could not find palette.json", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            comboColorPalette.DataSource = m_listPalettes;
        }

        private void onFormOpening(object sender, EventArgs e)
        {
            // Fetch serial ports
            UpdateSerialPorts();

            // Fetch palettes
            UpdatePalettes();

            // 2x looks fine
            comboMagnify.SelectedIndex = 1;

            // Default status
            m_PrintStatus = PrintStatus.NOT_CONNECTED;

        }

        private void onSerialPortDropdown(object sender, EventArgs e)
        {
            UpdateSerialPorts();
        }

        private void onConnect(object sender, EventArgs e)
        {
            if (m_bIsConnected)
            {
                // Disconnect
                m_connectedSerialPort.Close();
                m_bIsConnected = false;
            }
            else
            {
                // Connect
                m_connectedSerialPort = new SerialPort((string)comboSerialPort.SelectedItem, SERIAL_BAUD_RATE);
                m_connectedSerialPort.ReadTimeout = 500;
                m_connectedSerialPort.DataReceived += onSerialDataReceived;
                m_connectedSerialPort.NewLine = "\n";
                m_connectedSerialPort.Open();
                m_bIsConnected = true;
            }

        }

        string m_strDecodedBuffer;
        private void onSerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // This method makes sure that we only parse full lines
            // WARNING: This method *might* skip the last line of data, but it doesn't really matter.
            // TODO Still clean this up


            // Fetch as much data as we can store 
            var len = m_connectedSerialPort.Read(m_serialBuffer, 0, SERIAL_BUFFER_LEN);
            // Append to what we still have
            string buffer = m_strDecodedBuffer + Encoding.Default.GetString(m_serialBuffer, 0, len);
            // Clear the buffer for now
            m_strDecodedBuffer = "";
            // Split by newline
            var lines = buffer.Split('\n');
            // This discards the last line and processes any line that was sent in full
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i];
                onSerialDataReceived_FullLine(line.Trim());
            }
            // Store the unfinished line for safekeeping
            m_strDecodedBuffer = lines[lines.Length - 1];
        }

        private void onSerialDataReceived_FullLine(string line)
        {
            LogData(line);
            ProcessData(line);
        }


        private void ProcessData(string text)
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(text)) return;

            // Nono, we have proper json parsing here.
            // Lines that start with ! are treated as JSON data
            if (text.StartsWith("!"))
            {
                var json = text.Substring(1);
                Console.WriteLine("[DBG] Parsing as json line {0}", json);
                // This is so cool.
                dynamic jsondata = JValue.Parse(json);

                switch ((string)jsondata.command)
                {
                    // INIT = Print just started. So we clear any older tile buffer we had
                    case "INIT":
                        Console.WriteLine("[DBG] Print starting! {0}", jsondata.command);
                        m_PrintStatus = PrintStatus.PRINTING;
                        checkRemoveBorder.Checked = false;
                        m_listTiles.Clear();
                     
                        break;
                    // DATA = with more = 1 it means we still have data to receive, more = 0 means we are done
                    case "DATA":
                        if (jsondata.more == 0)
                        {
                            Console.WriteLine("[DBG] Print ended! {0}", jsondata.command);
                            m_PrintStatus = PrintStatus.DONE;

                        }
                        break;
                    // We don't care for other commands in this program
                    default:
                        Console.WriteLine("[DBG] Unknown command {0}", jsondata.command);
                        break;

                }
                return;
            }
            // Lines that start with # are treated like comments and ignored
            else if (text.StartsWith("#"))
            {
                Console.WriteLine("[DBG] Ignoring line {0}", text);
                return;
            }

            // If we reach this point we SHOULD have a full tile that we can store in byte array
            var bytes = text
                .Split(' ')                               // Split into items 
                .Select(item => Convert.ToByte(item, 16)) // Convert each item into byte
                .ToArray();                               // Materialize as array

           
            // We now convert these bytes in a single tile
            m_listTiles.Add(GameBoyTileToPixels(bytes));

            // This is optional but we also refresh the preview so the user can see the printer feed in real time
            RefreshImage();
        }

 



        // I still don't get how a delegate should help with safe multithreading, but sure.
        private delegate void TextDelegate(string text);
        private void LogData(string text)
        {
            // This just appends all the data we receive to the logbox
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






        private delegate void SetStatusDelegate(PrintStatus statue);
        private void SetStatus(PrintStatus status)
        {
            if (lblStatus.InvokeRequired)
            {
                var d = new SetStatusDelegate(SetStatus);
                lblStatus.Invoke(d, new object[] { status });
            }
            else
            {
       
                checkRemoveBorder.Enabled = status == PrintStatus.DONE;
               // comboColorPalette.Enabled = status == PrintStatus.DONE;
               // checkInvertPalette.Enabled = status == PrintStatus.DONE;
                comboMagnify.Enabled = status == PrintStatus.DONE;
                btnSave.Enabled = status == PrintStatus.DONE;

                lblStatus.Text = string.Format("Status: {0}", status.ToString());
            }
        }

        private delegate void RefreshImageDelegate();
        private void RefreshImage()
        {
            if (picPanel.InvokeRequired)
            {
                var d = new RefreshImageDelegate(RefreshImage);
                picPanel.Invoke(d);
            }
            else
            { 
                // Generate base image
                m_currentBitmap = GameBoyPixelsToImage(m_listTiles, (Palette)comboColorPalette.SelectedItem);

                // Handle cropping
                if (checkRemoveBorder.Checked)
                {
                    const int toRemove = TILE_PIXEL_WIDTH * 2;
                    m_currentBitmap = CropBitmap(m_currentBitmap,
                        new Rectangle
                        {

                            X = toRemove,
                            Y = toRemove,
                            Width = m_currentBitmap.Width - toRemove * 2,
                            Height = m_currentBitmap.Height - toRemove * 2
                        }
                    );
                }
                // Handle magnification
                int magnification = new int[] { 1, 2, 4 }[Math.Max(0, comboMagnify.SelectedIndex)];
                if (magnification > 1)
                    m_currentBitmap = ResizeBitmap(m_currentBitmap, magnification);





                picPanel.BackgroundImage = m_currentBitmap;
            }
        }








        






        private void comboColorPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }

        private void chkInvertPalette_CheckedChanged(object sender, EventArgs e)
        {
            RefreshImage();

        }

        private void comboMagnify_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }
        private void checkRemoveBorder_CheckedChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RefreshImage();
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

                m_currentBitmap.Save(sfd.FileName, imageFormat);
            }
        }

  
    }
}
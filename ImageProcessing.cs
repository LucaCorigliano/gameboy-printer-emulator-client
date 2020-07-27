using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static gameboy_printer_windows.Constants;

namespace gameboy_printer_windows
{
    public static class ImageProcessing
    {
        public static byte[] GameBoyTileToPixels(byte[] bytes)
        {
            if (bytes.Length != 16)
                throw new ArgumentException("A GameBoy tile is exactly 16 bytes long");

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

        public static Bitmap GameBoyPixelsToImage(List<byte[]> tiles, Palette palette)
        {
            //


            int imageWidth = TILES_PER_LINE * TILE_PIXEL_WIDTH;
            int imageHeight = Math.Max(1, (int)Math.Ceiling((double)tiles.Count / TILES_PER_LINE)) * TILE_PIXEL_HEIGHT;

            uint[,] imageData = new uint[imageHeight, imageWidth];

            int offsetX = 0;
            int offsetY = 0;

 





            foreach (var tile in tiles)
            {
                int i = 0;
                for (int y = 0; y < TILE_PIXEL_HEIGHT; y++)
                {
                    for (int x = 0; x < TILE_PIXEL_WIDTH; x++)
                        imageData[offsetY + y, offsetX + x] = palette.palette[tile[i++]];

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

         



            return bmp;
        }

        public static Bitmap ResizeBitmap(Bitmap src, int magnification)
        {
            int dstWidth = src.Width * magnification;
            int dstHeight = src.Height * magnification;
            Bitmap result = new Bitmap(dstWidth, dstHeight);

            var dstRect = new Rectangle(0, 0, dstWidth+ (1 * magnification), dstHeight+(1*magnification));
            var srcRect = new Rectangle(0, 0, src.Width, src.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(src, dstRect, srcRect, GraphicsUnit.Pixel);

            }
            return result;
        }

        public static Bitmap CropBitmap(Bitmap src, Rectangle rect)
        {
            Bitmap bmpImage = new Bitmap(src);
            return bmpImage.Clone(rect, bmpImage.PixelFormat);

        }
    }
}

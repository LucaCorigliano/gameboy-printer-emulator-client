using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameboy_printer_windows
{
    public static class PaletteLoader
    {
        


        public static List<Palette> FetchPalettes(string fileName)
        {
            List<Palette> retList = new List<Palette>();

            var sr = new StreamReader(fileName);
            var jsonData = sr.ReadToEnd();
            sr.Close();

            List<StringPalette> tempList = JsonConvert.DeserializeObject<List<StringPalette>>(jsonData);


            foreach(var tempPalette in tempList)
            {

                if (tempPalette.palette.Length < 4)
                {
                    Console.WriteLine("[ERR] Palette {0} doesn't have 4 colors.", tempPalette.name);
                    continue;
                }

                retList.Add(new Palette
                {
                    name = tempPalette.name,
                    palette = new[]
                    {
                        Convert.ToUInt32(tempPalette.palette[0], 16),
                        Convert.ToUInt32(tempPalette.palette[1], 16),
                        Convert.ToUInt32(tempPalette.palette[2], 16),
                        Convert.ToUInt32(tempPalette.palette[3], 16)
                    }
                });

            }
            return retList;
        }
    }
}

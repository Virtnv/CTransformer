using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTransformer
{
    class BinFileObject
    {
        private const int headerSize = 640;
        private readonly ushort measureCount;
        private byte[] headerByteArray = new byte[headerSize];
        public BinFileHeader binFileHeader;
        private StringBuilder sb = new StringBuilder();

        public BinFileObject(string pathToBinFile)
        {
            try
            {
                using (FileStream fs = File.Open(pathToBinFile, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Read(headerByteArray, 0, headerSize);
                    binFileHeader = (BinFileHeader)Serializer.RawDeserialize(headerByteArray, 0, typeof(BinFileHeader));
                    Console.WriteLine(binFileHeader.ToString());
                    measureCount = GetMeasureCount(binFileHeader);
                    int fileOffset = (2 * this.measureCount) + 6;
                    byte[] b = new byte[fileOffset];
                    fs.Seek(headerSize, SeekOrigin.Begin);
                    fs.Read(b, 0, b.Length);

                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        //Console.WriteLine($"{ConvertByteArrayToDateTime(b)}\t{ConvertByteArrayToString(b)}");
                        
                        //sb.AppendLine(BitConverter.ToString(b));
                    }

                    fs.Close();

                }
            }
            catch (Exception)
            {
                throw new Exception("Pizda v constructore!");
            }
        }

        private static ushort ConvertDateTime(byte b, int millenium = 0)
        // convert byte[] to date and time parts
        {
            return (ushort)(10 * (b / 16) + (b & 0x0f) + millenium);
        }

        public static DateTime ConvertByteArrayToDateTime(byte[] b)
        {
            string tempString = "";
            for (int i = 0; i < 6; i++)
            {
                tempString += b[i].ToString();
            }
            
            ushort seconds = ConvertDateTime(b[0]);
            ushort minutes = ConvertDateTime(b[1]);
            ushort hours = ConvertDateTime(b[2]);
            ushort days = ConvertDateTime(b[3]);
            ushort months = ConvertDateTime(b[4]);
            ushort year = ConvertDateTime(b[5], 2000);
            string dateTime = ($"{year.ToString()}/{months.ToString()}/{days.ToString()} {hours.ToString()}:{minutes.ToString()}:{seconds.ToString()}");

            DateTime dt;
            DateTime.TryParse(dateTime, out dt);
            return dt;
        }

        public string ConvertByteArrayToString(byte[] b)
        {
            string resultString = "";
            double dansr = 0f;
            short value = 0;
            float floatValue = 0f;
            for (int i = 0; i < measureCount-1; i++)
            {
                if (this.binFileHeader.onemr == 1 && i == 0)
                {
                    dansr = 3.6 * (b[6] + 256 * b[7] - 133 * (~this.binFileHeader.reserved1)) / (this.binFileHeader.kemr * 60);
                    resultString = $"{dansr.ToString()}\t";
                }
                value = (short)(b[6 + 2 * i] + 256 * b[7 + 2 * i]);
                floatValue = value / 50f;
                resultString += $"{floatValue.ToString()}\t";
            }
            return resultString;
        }

        public static ushort GetMeasureCount(BinFileHeader bfh)
        {
            ushort temp = 0;
            if (bfh.onemr == 1) temp++;
            for (int i = 0; i < 16; i++)
            {
                if ((bfh.ust[i] & 0x0f) != 0) temp++; //TO DO: сделай функцию ЕБА!!!
                if ((bfh.ust[i] & 0x10) != 0) temp++;
                if ((bfh.osn[i] & 0x0f) != 0) temp++;
                if ((bfh.osn[i] & 0x10) != 0) temp++;
                if ((bfh.osn[i] & 0x20) != 0) temp++;
                if ((bfh.dop[i] & 0x0f) != 0) temp++;
                if ((bfh.dop[i] & 0x10) != 0) temp++;
                if ((bfh.dop[i] & 0x20) != 0) temp++;
            }
            return temp;
        }
        
    }
}

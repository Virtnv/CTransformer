using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CTransformer
{
    class BinFileObject
    {
        //private const int headerSize = 640;
        //private byte[] headerByteArray = new byte[headerSize];

        private readonly int headerSize; // размер заголовка bin файла
        private readonly ushort measureCount;
        private byte[] headerByteArray; // заголовок файла массив байт !!!!!! Equals!!!!!

        public BinFileHeader binFileHeader; // заголовок файла Структура
        public List<DataEntry> dataEntryList; // список Записей(дата...массив данных)

        //private StringBuilder sb = new StringBuilder(); 

        public BinFileObject(string pathToBinFile)
        {
            try
            {
                using (FileStream fs = File.Open(pathToBinFile, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    headerSize = Marshal.SizeOf(typeof(BinFileHeader));
                    headerByteArray = new byte[headerSize];

                    fs.Read(headerByteArray, 0, headerSize);
                    binFileHeader = (BinFileHeader)Serializer.RawDeserialize(headerByteArray, 0, typeof(BinFileHeader));
                    dataEntryList = new List<DataEntry>();
                    //Console.WriteLine(binFileHeader.ToString());
                    measureCount = GetMeasureCount(binFileHeader);

                    int fileOffset = (2 * this.measureCount) + 6;
                    byte[] b = new byte[fileOffset];
                    fs.Seek(headerSize, SeekOrigin.Begin);
                    //fs.Read(b, 0, b.Length);
                    string dateString = "";
                    int z = 0;
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        ConvertByteArrayToDateTime(b, out dateString);

                        //dataEntryList.Add(new DataEntry($"{dateString} {ConvertByteArrayToString(b)}"));
                        dataEntryList.Add(new DataEntry($"{dateString} {ConvertByteArrayToString(b)}"));


                        //sb.AppendLine(BitConverter.ToString(b));
                        //if (z++ > 10) break;
                    }
                    //DateTime dt1 = new DateTime(2018,07,26);
                    //var sel = from c in dataEntryList where c.dt > dt1 select c;
                    //foreach (var item in sel)
                    //{
                    //    Console.WriteLine($"{item.dt}\t{item.data[1]}\t{item.data[3]} ");
                    //}
                    //Console.WriteLine(this.dataEntryList[1].ToString());
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

        public static DateTime ConvertByteArrayToDateTime(byte[] b, out string dateTime)
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
            dateTime = ($"{year.ToString()}/{months.ToString()}/{days.ToString()} {hours.ToString()}:{minutes.ToString()}:{seconds.ToString()}");

            DateTime dt;
            DateTime.TryParse(dateTime, out dt);
            return dt;
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
            var koeff1 = 133 * (~this.binFileHeader.reserved1);
            var koeff2 = (this.binFileHeader.kemr * 60);
            
            for (int i = 0, z = 0; i < measureCount-1; i++)
            {
                if (this.binFileHeader.onemr == 1 && i == 0)
                {
                    //dansr = 3.6 * (b[6] + 256 * b[7] - 133 * (~this.binFileHeader.reserved1)) / (this.binFileHeader.kemr * 60);
                    dansr = 3.6 * (b[6] + 256 * b[7] - koeff1) / koeff2;
                    resultString = $"{dansr.ToString()}\t";
                }
                z = 6 + 2 * i;
                //value = (short)(b[6 + 2 * i] + 256 * b[7 + 2 * i]);
                value = (short)(b[z] + 256 * b[z + 1]);

                floatValue = value / 50f;
                resultString += $"{floatValue.ToString()}\t";
            }
            return resultString;
        }

        public float[] ConvertByteArrayToFloatArray(byte[] b)
        {
            float[] floatArray = new float[measureCount-1];
            float dansr = 0f;
            short value = 0;
            float floatValue = 0f;
            for (int i = 0; i < measureCount - 1; i++)
            {
                if (this.binFileHeader.onemr == 1 && i == 0)
                {
                    dansr = (short)(3.6 * (b[6] + 256 * b[7] - 133 * (~this.binFileHeader.reserved1)) / (this.binFileHeader.kemr * 60));
                    floatArray[i] = dansr;
                }
                value = (short)(b[6 + 2 * i] + 256 * b[7 + 2 * i]);
                floatArray[i] = value / 50f;
            }
            return floatArray;
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
                if ((bfh.dop[i] & 0x0f) != 0) temp++;
                if ((bfh.dop[i] & 0x10) != 0) temp++;
            }
            return temp;
        }

        public static void Save(string fileName, BinFileObject bfo)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(fileName))
                {
                    foreach (var item in bfo.dataEntryList)
                    {
                        writer.WriteLine($"{item.ToString()}");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}

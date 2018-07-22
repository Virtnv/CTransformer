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
                    measureCount = GetMeasureCount(binFileHeader);
                    int fileOffset = (2 * this.measureCount) + 6;
                    byte[] b = new byte[fileOffset];
                    fs.Seek(headerSize, SeekOrigin.Begin);
                    fs.Read(b, 0, b.Length);

                    //while (fs.Read(b, 0, b.Length) > 0)
                    {
                        sb.AppendLine(BitConverter.ToString(b));
                    }

                    Console.WriteLine(sb);
                    fs.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Pizda v constructore!");
            }
        }

        public static ushort GetMeasureCount(BinFileHeader bfh)
        {
            ushort temp = 0;
            if (bfh.onemr == 1) temp++;
            for (int i = 0; i < 16; i++)
            {
                if ((bfh.ust[i] & 0x0f) != 0) temp++;
                if ((bfh.ust[i] & 0x10) != 0) temp++;
                if ((bfh.osn[i] & 0x0f) != 0) temp++;
                if ((bfh.osn[i] & 0x10) != 0) temp++;
                if ((bfh.dop[i] & 0x0f) != 0) temp++;
                if ((bfh.dop[i] & 0x10) != 0) temp++;
            }
            return temp;
        }
    }
}

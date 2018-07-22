using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CTransformer
{
    [StructLayout(LayoutKind.Explicit)]
    struct BinFileHeader
    {
        [FieldOffset(0)]
        public double dt0;
        [FieldOffset(8)]
        public Int16 number;
        [FieldOffset(10)]
        public Int16 version;
        [FieldOffset(12)]
        public byte tick;
        [FieldOffset(13)]
        public byte wperi;
        [FieldOffset(14)]
        public Int16 offset;
        [FieldOffset(16)]
        public double dts;
        [FieldOffset(24)]
        public byte onemr;
        [FieldOffset(25)]
        public byte reserved1;
        [FieldOffset(26)]
        public Int16 kemr;
        [FieldOffset(28)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] reserved2;
        [FieldOffset(128)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] ust;
        [FieldOffset(144)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] osn;
        [FieldOffset(160)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] dop;
        [FieldOffset(176)]
        public Int16 adrmdp;
        [FieldOffset(178)]
        public byte spmdp;
        [FieldOffset(179)]
        public byte reserved4;
        [FieldOffset(180)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] ustLineDescription;
        [FieldOffset(308)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] osnLineDescription;
        [FieldOffset(436)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] dopLineDescription;
        [FieldOffset(564)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76)]
        public byte[] reserved3;
    }

    class PanelEvaluated
    {
        public int measureCount = 0;
        public string ustFlowMeter;
        public StringBuilder sb = new StringBuilder();

        public int MeasureCount(BinFileHeader ph)
        {
            if (ph.onemr == 1) measureCount++;
            for (int i = 0; i < 16; i++)
            {
                if ((ph.ust[i] & 0x0f) != 0) measureCount++;
                if ((ph.ust[i] & 0x10) != 0) measureCount++;
                if ((ph.osn[i] & 0x0f) != 0) measureCount++;
                if ((ph.osn[i] & 0x10) != 0) measureCount++;
                if ((ph.dop[i] & 0x0f) != 0) measureCount++;
                if ((ph.dop[i] & 0x10) != 0) measureCount++;
            }
            return measureCount;
        }

        public void Read(string fileName, int offset)
        {
            int fileOffset = (2 * this.measureCount) + 6;
            byte[] b = new byte[fileOffset];
            using (FileStream fs = File.OpenRead(fileName))
            {
                //UTF8Encoding temp = new UTF8Encoding(true);
                fs.Seek(offset, SeekOrigin.Begin);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    sb.AppendLine(BitConverter.ToString(b));
                }
            }
        }

        public void PrintStringBuilder()
        {
            Console.WriteLine(sb);
        }
    }
}

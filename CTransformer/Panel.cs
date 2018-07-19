using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Transformer
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 8), Serializable]
    struct PanelHeader
    {
        [FieldOffset(0)]
        //public DateTime dt0;
        public byte[] dt0;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 640), Serializable]
    struct PanelHeader1
    {
        [FieldOffset(0)]
        //public DateTime dt0;
        public byte[] dt0;
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
        public DateTime dts;
        [FieldOffset(24)]
        public byte onemr;
        [FieldOffset(25)]
        public byte reserved1;
        [FieldOffset(26)]
        public Int16 kemr;

        [FieldOffset(28)]
        public byte[] reserved2;
        [FieldOffset(128)]
        public byte[] ust;
        [FieldOffset(144)]
        public byte[] osn;
        [FieldOffset(160)]
        public byte[] dop;

        [FieldOffset(176)]
        public Int16 adrmdb;
        [FieldOffset(178)]
        public byte spmdb;
        [FieldOffset(179)]
        public byte reserved4;

        [FieldOffset(180)]
        public byte[] ustLineDescription;
        [FieldOffset(308)]
        public byte[] osnLineDescription;
        [FieldOffset(436)]
        public byte[] dopLineDescription;
        [FieldOffset(564)]
        public byte[] reserved3;
        
    }

}

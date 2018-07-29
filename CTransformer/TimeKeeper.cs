using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTransformer
{
    static class TimeKeeper
    {
        public delegate string Del(byte[] b);
        public static string Keep(Del func, byte[] b)
        {
            Stopwatch sw = new Stopwatch();
            string result = "";
            sw.Start();
            result = func(b);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return result;
        }
    }
}

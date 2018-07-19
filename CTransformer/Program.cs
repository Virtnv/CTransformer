using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTransformer
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a file name to transform");
                return 1;
            }
            string fileName = args[0];
            string fileExtension = fileName.Substring(fileName.Length - 4);
            if(fileExtension != ".bin")
            {
                Console.WriteLine("Incorrect file format!");
                return 1;
            }
            byte[] b = new byte[640];
            try
            {
                using (FileStream fStream = File.OpenRead(Directory.GetCurrentDirectory() + $"\\{fileName}"))
                {
                    if (fStream.Length < 640)
                    {
                        Console.WriteLine("Too small file!");
                        return 1;
                    }
                    fStream.Read(b, 0, b.Length);
                    //ph = (PanelHeader)Serializer.RawDeserialize(b, 0, typeof(PanelHeader));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
                return 1;
            }
            

            Console.WriteLine("Everythiing OK!");
            Console.ReadLine();
            return 0;
        }
    }
}

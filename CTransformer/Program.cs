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
                Console.WriteLine("Uncorrect file format!");
                return 1;
            }

            using (FileStream fStream = File.OpenRead(Directory.GetCurrentDirectory() + "fileName"))
            {
                if (fStream.Length < 640)
                {
                    Console.WriteLine("Too small file!");
                    return 1;
                }
            }
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    FileInfo fInfo = new FileInfo(openFileDialog1.FileName);
            //    if (fInfo.Length < 640)
            //    {
            //        InfoBlock.Text += "\nFile size < 640 bytes!";
            //    }
            //    else
            //    {
            //        using (fStream = File.OpenRead(openFileDialog1.FileName))
            //        {
            //            //fStream.Read(b, 0, b.Length);
                        
                        //ph = (PanelHeader)Serializer.RawDeserialize(b, 0, typeof(PanelHeader));

                 


                //fileName.Substring(".bin");
                Console.WriteLine("Everythiing OK!");
            Console.ReadLine();
            return 0;
        }
    }
}

using System;
using System.IO;

namespace CTransformer
{
    class CommandLineParser // парсит аргументы командной строки
    {
        static public FileStream InputFileNameParse(string inputFileName)
        {
            string fileExt = Path.GetExtension(inputFileName);
            if (fileExt.Length == 0)
            {
                fileExt = "bin";
            }
            else if (fileExt != "bin")
            {
                Console.WriteLine("Incorrect file extension!");
                return null;

            }
            return null;
        }

        public static string[] Splitter(string stringToSplit)
        {
            stringToSplit = stringToSplit.Trim();
            string []split = stringToSplit.Split(new Char[] { ' ', '\t' });
            return split;
        }
            //Console.WriteLine($"{File.Exists(inputFileName)}");
            //// Save all needed types
            //string fullPath = Path.GetFullPath(inputFileName);
            //string directoryName = Path.GetDirectoryName(inputFileName);
            //string fileName = Path.GetFileName(inputFileName);
            //string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFileName);

            //string[] sa = { fullPath, directoryName, fileName, fileExt, fileNameWithoutExt };
            //foreach (var item in sa)
            //{
            //    Console.WriteLine($"{inputFileName} - {item}");
            //}

            //if (fileName.Length != 0 && fileExt == ".bin")
            //{
            //    try
            //    {
            //        FileStream fs = new FileStream(directoryName + @"\" + fileName, FileMode.Open);
            //    }
            //    catch (Exception e)
            //    {

            //        //throw;
            //    }
            //}

            //string path = inputFileName;
            //Console.WriteLine(Path.GetExtension(path));
        
    }
}

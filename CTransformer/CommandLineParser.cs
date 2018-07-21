using System;
using System.IO;
using System.Linq;

namespace CTransformer
{
    class CommandLineParser // парсит аргументы командной строки
    {
        static public FileStream InputFileNameParse(string inputFileName)
        {

            string fileExt = Path.GetExtension(inputFileName);
            string fileName = Path.GetFileName(inputFileName);

            // check file extention length and file name length
            if (fileExt.Length == 0 && fileName.Length != 0)
            {
                fileExt = "bin";
                inputFileName += "." + fileExt;
            }
            else if (fileExt != ".bin")
            {
                throw new Exception("Incorrect file ext!");
            }
            else if(fileName.Length == 0)
            {
                throw new Exception("Enter file name!");
            }

            // check if file exists
            string fullPath = Path.GetFullPath(inputFileName);
            if (!File.Exists(fullPath))
            {
                throw new Exception("Input File not found!");
            }
            FileStream fs = new FileStream(fullPath, FileMode.Open);
            return fs;
        }

        public static string[] Splitter(string stringToSplit)
        {
            stringToSplit = stringToSplit.Trim();
            string []split = stringToSplit.Split(new Char[] { ' ', '\t' });
            return split;
        }

        public static void KeyParser(string key)
        {
            if (CheckManagedChar(key))
            {
                string option = key.Remove(0, 1);
                option = 1.ToString();
                Strategies.keys.Contains(option);

            }
        }

        public static bool CheckManagedChar(string key)
        {
            if (key[0] == '-') return true;
            throw new Exception("Incorrect key char");
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

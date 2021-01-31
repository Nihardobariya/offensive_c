using System;
using System.Runtime.InteropServices;

namespace CopyFileFunction
{
    class Program
    {

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CopyFile(String lpExistingFileName, String lpNewFileName, bool bFailIfExists);


        static void Main(string[] args)
        {
            if(CopyFile("fileone.txt", "filetwo.txt", true))
            {
                Console.WriteLine("File successfully copied");
            }
            else
            {
                Console.WriteLine("File Copying stopped");
            }
        }
    }
}

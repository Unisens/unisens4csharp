using System;
using System.Diagnostics;

namespace org.unisens
{

    public class Version
    {

        public static void main(string[] args)
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo("Version.cs");

            Console.WriteLine(" ");
            Console.WriteLine("  This is Unisens Interface Version " + fvi.FileVersion);
            Console.WriteLine("  For more information, visit http://www.unisens.org");
        }
    }
}

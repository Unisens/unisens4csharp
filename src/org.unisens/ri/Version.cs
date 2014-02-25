
using System;
using System.Reflection;

namespace org.unisens.ri
{
    public class Version
    {

        public static void main(String[] args)
        {
            Assembly p = typeof(Version).Assembly;//.class.getPackage();
            Console.WriteLine(" ");
            Console.WriteLine("  This is Unisens Reference Implementation Version " + p.ImageRuntimeVersion);//.getImplementationVersion());
            Console.WriteLine("  For more information, visit http://www.unisens.org");
        }

        public static String getVersion()
        {
            Assembly p = typeof(Version).Assembly;//Version.class.getPackage();

            return p.ImageRuntimeVersion;//.getImplementationVersion(); 
        }
    }
}
using System;
using System.Windows.Forms;
using org.unisens.ri.io;

namespace UnisensTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.Run(new frmMain()); ;
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            BufferedFileHelper.CloseAll();
        }
    }
}

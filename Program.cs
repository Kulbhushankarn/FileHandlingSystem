using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace FileHandlingSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_MainForm());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MediaCenterVLCPlayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
                Environment.Exit(0);
            Form1 form = new Form1(args[0]);
            Application.Run(form);
        }
    }
}

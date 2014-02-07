using fyiReporting.RdlDesign.Netsis.Rdl.Designer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Netsis.Rdl.Designer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            var thread = System.Threading.Thread.CurrentThread;

            thread.CurrentCulture = new CultureInfo("tr-TR");
            thread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            thread.CurrentUICulture = thread.CurrentCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            (new ReportDesignerApp()).Run(args);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeForge_Desktop.Presentation.Forms;
using CodeForge_Desktop.Presentation.Forms.Admin;
using CodeForge_Desktop.Presentation.Forms.Student;

namespace CodeForge_Desktop
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
            Application.Run(new MainFormStudent());
        }
    }
}

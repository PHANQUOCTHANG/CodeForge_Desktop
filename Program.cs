using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Presentation.Forms;
using CodeForge_Desktop.Presentation.Forms.Student;
using CodeForge_Desktop.Presentation.Forms.Admin;
using System;
using System.Windows.Forms;

namespace CodeForge_Desktop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Login());
            Application.Run(new MainFormAdmin());

        }
    }
}
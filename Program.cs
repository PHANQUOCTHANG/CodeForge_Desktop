using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.Business.Services;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Presentation.Forms;
using CodeForge_Desktop.Presentation.Forms.Student;
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

            // --- Thiết lập Dependency Injection thủ công ---

            // 1. Đăng ký Repository
            IUserRepository userRepository = new UserRepository();

            // 2. Đăng ký Service, truyền Repository vào
            IAuthService authService = new AuthService(userRepository);

            // 3. Khởi chạy Form Login, truyền AuthService vào Constructor
            Login loginForm = new Login(authService);

            //Application.Run(loginForm);
            Application.Run(new MainFormStudent());

        }
    }
}
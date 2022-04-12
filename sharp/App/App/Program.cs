using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var fileStorage = new LocalStorage(""))
            {
                var userStorage = new UserStorage(fileStorage);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (userStorage.CurrentUser != null)
                {
                    Application.Run(new Form2(userStorage));
                }
                else
                {
                    Application.Run(new LogIn(userStorage));
                }
            }

        }
    }
}

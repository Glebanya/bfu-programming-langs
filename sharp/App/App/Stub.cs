using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Form2 : Form
    {
        private UserStorage userStorage;
        public Form2(UserStorage userStorage)
        {
            this.userStorage = userStorage;
            InitializeComponent();
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            userStorage.LogOut();
            Hide();
            var logIn = new LogIn(userStorage);
            logIn.ShowDialog();
        }
    }
}

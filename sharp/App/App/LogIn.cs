using System;
using System.Windows.Forms;

namespace App
{
    public partial class LogIn : Form
    {
        private UserStorage userStorage;
        public LogIn(UserStorage userStorage)
        {
            this.userStorage = userStorage;
            InitializeComponent();
            password.Text = "";
            password.PasswordChar = '*';
            password.MaxLength = 14;
        }

        private void signInClick(object sender, EventArgs e)
        {
            if (userName.Text.Length == 0)
            {
                MessageBox.Show("Invalid username");
                return;
            }

            if (password.Text.Length == 0)
            {
                MessageBox.Show("Invalid password");
                return;
            }

            if (!userStorage.LogIn(userName.Text, password.Text, out var user))
            {
                MessageBox.Show("Invalid password or username");
                return;
            }

            Hide();
            var stub = new Form2(userStorage);
            stub.ShowDialog();
        }

        private void registerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            var form = new Register(userStorage);
            form.ShowDialog();
        }
    }
}

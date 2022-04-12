using System;
using System.Windows.Forms;

namespace App
{
    public partial class Register : Form
    {
        private UserStorage userStorage;
        public Register(UserStorage userStorage)
        {
            this.userStorage = userStorage;
            InitializeComponent();
            password.Text = "";
            password.PasswordChar = '*';
            passwordConfirm.Text = "";
            passwordConfirm.PasswordChar = '*';
        }

        private void signup_Click(object sender, EventArgs e)
        {
            if (username.Text.Length == 0)
            {
                MessageBox.Show("Invalid username");
                return;
            }
            if (password.Text.Length == 0)
            {
                MessageBox.Show("Password is empty");
                return;
            }
            if (password.Text != passwordConfirm.Text)
            {
                MessageBox.Show("Passwords is not same");
                return;
            }
            if (firstName.Text.Length == 0)
            {
                MessageBox.Show("first name is empty");
                return;
            }
            if (lastName.Text.Length == 0)
            {
                MessageBox.Show("last name is empty");
                return;
            }

            if (!userStorage.Register(username.Text, password.Text, firstName.Text, lastName.Text))
            {
                MessageBox.Show("This username is not available");
                return;
            }
            Hide();
            var login = new LogIn(userStorage);
            login.ShowDialog();
        }
    }
}

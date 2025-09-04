using CommonUtils.Security;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonForms.Views
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        private SecureString _securePassword;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Username = txtUsername.Text;
            _securePassword = SecurePasswordHelper.ConvertToSecureString(txtPassword.Text);

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(txtPassword.Text))
            {
                XtraMessageBox.Show("Введите имя пользователя и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Генерируем хеш пароля с солью
            PasswordHash = GetPasswordHash();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string GetPasswordHash()
        {
            string password = SecurePasswordHelper.ConvertToString(_securePassword);
            return HashPassword(password, Username);
        }

        // Метод для хеширования пароля
        public static string HashPassword(string password, string salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(
                password,
                Encoding.UTF8.GetBytes(salt),
                10000,  // Количество итераций
                HashAlgorithmName.SHA256))
            {
                return Convert.ToBase64String(deriveBytes.GetBytes(256 / 8));
            }
        }
    }
}
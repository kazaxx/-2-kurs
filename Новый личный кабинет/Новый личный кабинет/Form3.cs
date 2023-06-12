using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Новый_личный_кабинет
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f2 = new Form1();
            f2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            string username = textBox3.Text;
            string message;
            if (password == "" && username == "")
            {
                message = "Заполните все поля";
            }
            else if (username == "")
            {
                message = "Логин обязателен к заполнению";
            }
            else if (password == "")
            {
                message = "Вы не ввели пароль";
            }
            else if (textBox1.Text != textBox2.Text)
            {
                message = "Пароли не совпадают";
            }
            else
            {
                message = IsPasswordValid(password) ? "Успешная регистрация" : "Данные не совпадают";
            }
            label4.Text = message;
            label4.ForeColor = message == "Успешная регистрация" ? Color.Green : Color.Red;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private bool IsPasswordValid(string password)
        {
            bool hasUppercase = false;
            bool hasSpecialChar = false;
            bool hasDigit = false;
            bool hasLowercase = false;

            if (password.Length < 6)
            {
                return false;
            }

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialChar = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
            }
            return hasUppercase && hasSpecialChar && hasDigit && hasLowercase;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;//или другой стиль с Fixed
            this.MaximizeBox = false;
        }
    }
}

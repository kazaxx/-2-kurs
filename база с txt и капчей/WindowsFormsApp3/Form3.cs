using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f2 = new Form1();
            f2.Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;//или другой стиль с Fixed
            this.MaximizeBox = false;
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(textBox1, "Для пароля необходимо использовать не менее 6 символов, включая заглавные и строчные буквы, цифры и специальные символы.");
            toolTip.SetToolTip(textBox2, "Для пароля необходимо использовать не менее 6 символов, включая заглавные и строчные буквы, цифры и специальные символы.");
            toolTip.SetToolTip(textBox3, "Введите ваш логин");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.passwordNew = textBox2.Text;
            Data.loginNew = textBox3.Text;
            bool isUserRegistered = Data.CheckLoginAndPasswordRegistr(textBox3.Text);
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
                textBox1.Clear();
                textBox2.Clear();
            }
            else if (isUserRegistered)
            {
                string message = IsPasswordValid(Data.passwordNew) ? "Пароль правильный" : "Пароль неправильный";
                if (message == "Пароль правильный")
                {
                    Data.LoadDataToFile();
                    MessageBox.Show("Успешная регистрация");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
                else if (message == "Пароль неправильный")
                {
                    MessageBox.Show(message);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
            else // пользователь уже существует
            {
                MessageBox.Show("Пользователь уже существует!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private bool IsPasswordValid(string passwordNew)
        {
            bool Uppercase = false;
            bool SpecialChar = false;
            bool Digit = false;
            bool Lowercase = false;

            if (passwordNew.Length < 6)
            {
                return false;
            }
            if (textBox1.Text == textBox2.Text)
            {
                foreach (char c in passwordNew)
                {
                    if (char.IsUpper(c))
                    {
                        Uppercase = true;
                    }
                    else if (!char.IsLetterOrDigit(c))
                    {
                        SpecialChar = true;
                    }
                    else if (char.IsDigit(c))
                    {
                        Digit = true;
                    }
                    else if (char.IsLower(c))
                    {
                        Lowercase = true;
                    }
                }
            }
            return Uppercase && SpecialChar && Digit && Lowercase;
        }
    }
}

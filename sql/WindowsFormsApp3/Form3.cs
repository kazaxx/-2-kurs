using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Reflection.Emit;
using md5_sql_hash;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class Form3 : Form
    {
        DataBase dataBase = new DataBase();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox3.Text;
            var password = textBox1.Text;
            var confirmPassword = textBox2.Text;

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Логин обязателен к регистрации");
                return;
            }
            if (password != confirmPassword)

            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (checkuser(login))
            {
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать больше 6 символов");
                return;
            }

            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Пароль должен содержать заглавную букву");
                return;
            }

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                MessageBox.Show("Пароль должен содержать специальный символ");
                return;
            }

            var hashedPassword = md5.hashPassword(password);

            string querystring = $"insert into register(login_user, password_user) values('{login}', '{hashedPassword}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetSqlConnection());

            dataBase.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

            dataBase.closeConnection();

            // Проверяем правильность ввода пароля
            string querystring2 = $"select user, login_user, password_user from register where login_user = '{login}'";

            SqlCommand command2 = new SqlCommand(querystring2, dataBase.GetSqlConnection());
            SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            if (table2.Rows.Count == 1)
            {
                var dbPassword = table2.Rows[0]["password_user"].ToString();

                if (hashedPassword != dbPassword)
                {
                    MessageBox.Show("Неправильный пароль");
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
        }
        private bool checkuser(string login)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{login}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

/************************
 * @
 * @
 ************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using md5_sql_hash;
using System.IO;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        DataBase dataBase = new DataBase();
        int counter = 0; // счетчик
        int counte = 30; // начальное значение - 10 секунд
        string timerValueFilePath = "timer_value.txt";
        string loginNew;
        string passwordNew;
        Form2 f2 = new Form2();
        Form3 f3 = new Form3();
        public Form1()
        {
            InitializeComponent(); button4.Image = Properties.Resources.close;
            this.StartPosition = FormStartPosition.CenterScreen; 
            if (File.Exists(timerValueFilePath))
            {
                string value = File.ReadAllText(timerValueFilePath);
                int intValue; if (int.TryParse(value, out intValue))
                {
                    counte = intValue;
                }
            }
            if (File.Exists(timerValueFilePath))
            {
                if (int.TryParse(File.ReadAllText(timerValueFilePath), out int savedCounte))
                {
                    counte = savedCounte;
                    StartTimer();
                }
                else
                {
                    File.Delete(timerValueFilePath);
                }
            }
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (counte > 0 && timer.Enabled)
            {
                File.WriteAllText(timerValueFilePath, counte.ToString());
            }
            else if (File.Exists(timerValueFilePath))
            {
                File.Delete(timerValueFilePath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginNew = textBox1.Text;
            passwordNew = textBox2.Text;

            if (String.IsNullOrEmpty(loginNew) || String.IsNullOrEmpty(passwordNew))
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
                counter++;

                if (counter == 3)
                {
                    label3.Visible = true;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    StartTimer();
                    counter = 0;
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
            else
            {
                string passwordHash = md5.hashPassword(passwordNew);
                string queryString = $"SELECT id_user, login_user, password_user, roll_user FROM register WHERE login_user = '{loginNew}' AND password_user = '{passwordHash}'";
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    string role = table.Rows[0]["roll_user"].ToString();

                    // Закрываем текущую форму

                    if (role == "admin")
                    {
                        Administrirovanie formAdmin = new Administrirovanie();
                        formAdmin.Show();
                        this.Visible = false; // Скрываем текущую форму
                        formAdmin.label1.Text = this.textBox1.Text;
                        formAdmin.label2.Text = this.textBox2.Text;
                    }
                    else if (role == "user")
                    {
                        Form5 formUser = new Form5();
                        formUser.Show();
                        this.Visible = false; // Скрываем текущую форму
                        formUser.label1.Text = this.textBox1.Text;
                        formUser.label2.Text = this.textBox2.Text;
                    }
                    else if (role == "manager")
                    {
                        Form7 formManager = new Form7();
                        formManager.Show();
                        this.Visible = false;
                        formManager.label1.Text = this.textBox1.Text;
                        formManager.label2.Text = this.textBox2.Text;
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная роль пользователя.");
                    }
                }
                else
                {
                    counter++;
                    if (counter == 3)
                    {
                        label3.Visible = true;
                        label3.Text = $"Вы ввели неправильные данные 3 раза. ";
                        button1.Enabled = false;
                        button2.Enabled = false;
                        StartTimer();
                        counter = 0;
                    }
                    else
                    {
                        MessageBox.Show("Данные введены неверно");
                        textBox1.Clear();
                        textBox2.Clear();
                    }
                }
            }
        }

        private void StartTimer()
        {
            // Настройка таймера
            timer.Interval = 1000;
            timer.Tick -= new EventHandler(timer1_Tick);
            timer.Tick += new EventHandler(timer1_Tick);
            button1.Enabled = false;
            button2.Enabled = false;
            // Отображаем сообщение
            label5.Visible = true;     
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counte--;
            if (counte <= 0)
            {
                timer.Stop();
                Form4 f4 = new Form4();
                f4.ShowDialog();
                label5.Visible = false;
                button1.Enabled = true;
                button2.Enabled = true;
                counte = 30;

                if (File.Exists(timerValueFilePath))
                {
                    File.Delete(timerValueFilePath);
                }
            }
            else
            {
                label5.Text = $"Вы ввели неправильные данные 3 раза. Ожидайте: {counte} секунд";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
            button4.Image = Properties.Resources.close;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DateTime now = DateTime.Now;
            {

                int hour = now.Hour;
                if (hour >= 6 && hour < 12)
                {
                    label4.Text = "Доброе утро!";
                }
                else if (hour >= 12 && hour < 18)
                {
                    label4.Text = "Добрый день!";
                }
                else if (hour >= 18 && hour < 24)
                {
                    label4.Text = "Добрый вечер!";
                }
                else
                {
                    label4.Text = "Доброй ночи!";
                }

            }
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
            button4.Image = Properties.Resources.open;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(); //Создаём новую форму
            f3.Show(); // Открываем форму
            this.Hide();
        }

        private string TimerValueFilePath
        {
            get { return Path.Combine(Application.StartupPath, "timer_value.txt"); }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Новый_личный_кабинет
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button5.Image = Properties.Resources.close;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                label4.ForeColor = Color.Red;
                label4.Text = ("Заполните все поля");
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                Form2 f2 = new Form2(); //Создаём новую форму
                f2.label1.Text = this.textBox1.Text; //Передаём данные из textBox1 в label1, но при этом у label1 в свойствах выставить public у modifiers
                f2.label2.Text = this.textBox2.Text;
                f2.Show(); // Открываем форму
                textBox1.Clear();
                textBox2.Clear();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f3 = new Form3();
            f3.Show();
            this.Hide();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            {
                int hour = now.Hour;
                if (hour >= 6 && hour < 12)
                {
                    label3.Text = "Доброе утро!";
                }
                else if (hour >= 12 && hour < 18)
                {
                    label3.Text = "Добрый день!";
                }
                else if (hour >= 18 && hour < 24)
                {
                    label3.Text = "Добрый вечер!";
                }
                else
                {
                    label3.Text = "Доброй ночи!";
                }
            }
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
            button5.Image = Properties.Resources.open;
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
            button5.Image = Properties.Resources.close;
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }
    }
}

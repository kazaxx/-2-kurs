using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            button4.Image = Properties.Resources.close;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string loginNew = textBox1.Text;
            string passwordNew = textBox2.Text;
            bool isUserRegistered = Data.CheckLoginAndPassword(textBox1.Text, textBox2.Text);
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
                textBox1.Clear();
                textBox2.Clear();
            }
            else if (isUserRegistered)
            {
                Form2 f2 = new Form2();
                f2.Show();
                f2.label1.Text = this.textBox1.Text;
                f2.label6.Text = this.textBox2.Text;
                this.Hide();
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Для авторизациии нужно зарегистрироваться!");
                textBox1.Clear();
                textBox2.Clear();
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

    }
}

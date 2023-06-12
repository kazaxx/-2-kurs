using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int sec = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            timer3.Start();
            timer2.Start();
            timer1.Stop();
            if (progressBar1.Value >= 10)
            {
                progressBar1.Value -= 10;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer3.Start();
            timer2.Stop();
            timer1.Start();
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 10;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 10;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 10)
            {
                progressBar1.Value -= 10;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer3.Stop();
                label1.Text = "Результат в секундах: " + sec;
            }
            if (progressBar1.Value == progressBar1.Minimum)
            {
                timer3.Stop();
                label1.Text = "Результат в секундах: " + sec;
                
            }
            sec++;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 15;
        }
    }
}

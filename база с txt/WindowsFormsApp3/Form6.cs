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

namespace WindowsFormsApp3
{
    public partial class Administrirovanie : Form
    {
        DataBase database = new DataBase();
        public Administrirovanie()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            f2.label1.Text = this.label1.Text;
            f2.label6.Text = this.label2.Text;
        }

        private void ReadSingleRows(IDataRecord record)
        {
            int? intVal = record.IsDBNull(0) ? (int?)null : record.GetInt32(0);
            string strVal1 = record.IsDBNull(1) ? null : record.GetString(1);
            string strVal2 = record.IsDBNull(2) ? null : record.GetString(2);
            string strVal3 = record.IsDBNull(3) ? null : record.GetString(3);

            dataGridView1.Rows.Add(intVal, strVal1, strVal2, strVal3);
        }


        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id_user", "ID");
            dataGridView1.Columns.Add("loqin_user", "Логин");
            dataGridView1.Columns.Add("password_user", "Пароль");
            dataGridView1.Columns.Add("roll_user", "Роль");
            var checkColumn = new DataGridViewCheckBoxColumn();
        }

        private void RefreshDataGrid()
        {
            dataGridView1.Rows.Clear();
            string queryString = $"SELECT * FROM register";
            SqlCommand comand = new SqlCommand(queryString, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = comand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRows(reader);
            }
            reader.Close();
            database.closeConnection();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            database.openConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                var role = dataGridView1.Rows[index].Cells[3].Value.ToString();
                var changeQuery = $"UPDATE register SET roll_user = '{role}' WHERE id_user = '{id}'";
                var command = new SqlCommand(changeQuery, database.GetSqlConnection());
                command.ExecuteNonQuery();
            }
            database.closeConnection();
            RefreshDataGrid();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            database.openConnection();
            var selectRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = Convert.ToInt32(dataGridView1.Rows[selectRowIndex].Cells[0].Value);
            var deleteQuery = $"DELETE FROM register WHERE id_user = '{id}'";
            var command = new SqlCommand(deleteQuery, database.GetSqlConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
            RefreshDataGrid();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Обработка изменения значения в ячейке DataGridView
            if (e.ColumnIndex == dataGridView1.Columns["roll_user"].Index && e.RowIndex >= 0)
            {
                var id = dataGridView1.Rows[e.RowIndex].Cells["id_user"].Value.ToString();
                var role = dataGridView1.Rows[e.RowIndex].Cells["roll_user"].Value.ToString();
                var updateQuery = $"UPDATE register SET roll_user = '{role}' WHERE id_user = '{id}'";

                database.openConnection();
                var command = new SqlCommand(updateQuery, database.GetSqlConnection());
                command.ExecuteNonQuery();
                database.closeConnection();
            }
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Form f2 = new Form1();
            f2.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
            f2.label1.Text = this.label1.Text;
            f2.label6.Text = this.label2.Text;
        }
    }
}
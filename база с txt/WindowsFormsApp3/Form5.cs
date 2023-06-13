using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form5 : Form
    {
        private DataBase db;

        public Form5()
        {
            InitializeComponent();
            db = new DataBase();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            db.openConnection();

            string query = "SELECT TOP 1 brand_tovar FROM tovar";
            SqlCommand command = new SqlCommand(query, db.GetSqlConnection());
            string result = command.ExecuteScalar().ToString();

            label1.Text = result;

            db.closeConnection();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    public static class Data // класс обработки данных
    {
        public static string loginNew { get; set; } // свойство для хранения логина
        public static string passwordNew { get; set; } // свойство для хранения пароля

        public static void LoadDataToFile() // метод запись в файл новых логина и пароля 
        {
            string createText = $"User:{loginNew}||Password:{passwordNew}" + Environment.NewLine;//создание шаблона записи в файл
            System.IO.File.AppendAllText(@"data.txt", createText);//запись в файл

        }
        public static bool CheckLoginAndPassword(string login, string password)
        {
            string filePath = @"data.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(@"data.txt");
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, $"^User:{login}\\W", RegexOptions.IgnoreCase);
                    if (match.Success && line.EndsWith($"Password:{password}"))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public static bool CheckLoginAndPasswordRegistr(string login)
        {
            string filePath = @"data.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(@"data.txt");
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, $"^User:{login}\\W", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        return false; // найден пользователь с указанным логином
                    }
                }
            }
            return true; // пользователь не найден в файле
        }

        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-JU35DKO\SQLEXPRESS;Initial Catalog=register;Integrated Security=true");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetSqlConnection()
        {
            return sqlConnection;
        }
    }
}


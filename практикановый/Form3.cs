﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace практикановый
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            InitializeConnection();
        }
        private MySqlConnection connection;
        private string server = "localhost";
        private string database = "sad";
        private string uid = "khviksa";
        private string password = "Vikavika777!";

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InitializeConnection()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kod = textBox1.Text;

            if (CheckKodInDatabase(kod))
            {
                // Если код верный, открываем следующую форму
                Form6 nextForm = new Form6();
                nextForm.Show();
                this.Hide(); // Скрываем текущую форму
            }
            else
            {
                // Если код неверный, выводим сообщение
                MessageBox.Show("Код неверный.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckKodInDatabase(string kod) //Отсутствие обработки исключений: В методе CheckKodInDatabase стоит добавить блок try-catch, чтобы отлавливать возможные ошибки при работе с базой данных.
        {
            string connectionString = "Server=localhost;Database=sad;User ID=khviksa;Password=Vikavika777!;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM v WHERE kod = @kod";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kod", kod);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}

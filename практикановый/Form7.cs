using System;
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
    public partial class Form7 : Form
    {
        private string kod;
        public Form7(string kod)
        {
            InitializeComponent();
            this.kod = kod;
            InitializeConnection();
            LoadData();
            LoadRaspis();
            LoadAttendance();
        }
        private MySqlConnection connection;
        private string server = "localhost";
        private string database = "sad";
        private string uid = "khviksa";
        private string password = "Vikavika777!";
        private void InitializeConnection()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }
        private void LoadData()
        {
            try
            {
                connection.Open(); // Открываем соединение
                string query = "SELECT * FROM r WHERE kod = @kod";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kod", kod);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем данные к DataGridView
                    dataGridView1.DataSource = dataTable; // Предполагается, что у вас есть DataGridView с именем dataGridView1
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
            finally
            {
                connection.Close(); // Закрываем соединение
            }
        }
        private void LoadRaspis()
        {
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM raspis", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView3.DataSource = dt;
            }
            finally
            {
                connection.Close();
            }
        }
        private void LoadAttendance()
        {
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM attendance", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            finally
            {
                connection.Close();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace практикановый
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            InitializeConnection();
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
            LoadV();
            LoadR();
        }
        private void LoadV()
        {
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM v", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            finally
            {
                connection.Close();
            }
        }
        private void LoadR()
        {
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM r", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string kod = textBox2.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(kod))
            {
                MessageBox.Show("Введите имя пользователя и код.");
                return;
            }

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO v (name, kod) VALUES (@name, @kod)", connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@kod", kod);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                LoadV();
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM v WHERE id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                    LoadV();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            string child = textBox4.Text;
            string kod = textBox5.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(child) || string.IsNullOrWhiteSpace(kod))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO r (name, child, kod) VALUES (@name, @child, @kod)", connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@child", child);
                cmd.Parameters.AddWithValue("@kod", kod);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int Id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);

                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM r WHERE id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                    LoadR();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.");
            }
        }
    }
}

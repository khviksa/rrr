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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            InitializeConnection();
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
        private void LoadRaspis()
        {
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM raspis", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
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
                dataGridView1.DataSource = dt;
            }
            finally
            {
                connection.Close();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string childName = comboBox1.SelectedItem?.ToString() ?? textBox1.Text.Trim();
            bool isPresent = checkBox1.Checked;

            if (string.IsNullOrEmpty(childName))
            {
                MessageBox.Show("Пожалуйста, введите имя ребёнка или выберите его из списка.");
                return;
            }

            try
            {
                connection.Open();
                string query = "INSERT INTO attendance (date, child_name, is_present) VALUES (@date, @childName, @isPresent)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@childName", childName);
                cmd.Parameters.AddWithValue("@isPresent", isPresent);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                LoadAttendance();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string subject = textBox2.Text;
            DateTime startTime = dateTimePicker2.Value;
            DateTime endTime = dateTimePicker3.Value;

            if (string.IsNullOrWhiteSpace(subject)) return;

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO raspis (subject, start_time, end_time) VALUES (@subject, @startTime, @endTime)", connection);
                cmd.Parameters.AddWithValue("@subject", subject);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                LoadRaspis();
                textBox2.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int scheduleId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM raspis WHERE id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", scheduleId);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                    LoadRaspis();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int scheduleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM attendance WHERE id = @id", connection);
                    cmd.Parameters.AddWithValue("@id", scheduleId);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                    LoadAttendance();
                }
            }
        }
    }
}
    


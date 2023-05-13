using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Zavd_XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txbName.Text == "")
            {
                MessageBox.Show("Заповніть всі поля.", "Помилка.");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = txbName.Text;
                dataGridView1.Rows[n].Cells[1].Value = txbSurname.Text;
                dataGridView1.Rows[n].Cells[2].Value = nmrAge.Value;
                dataGridView1.Rows[n].Cells[3].Value = cbxSex.Text;
                dataGridView1.Rows[n].Cells[4].Value = cbxProgrammer.Text;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = txbName.Text;
                dataGridView1.Rows[n].Cells[1].Value = txbSurname.Text;
                dataGridView1.Rows[n].Cells[2].Value = nmrAge.Value;
                dataGridView1.Rows[n].Cells[3].Value = cbxSex.Text;
                dataGridView1.Rows[n].Cells[4].Value = cbxProgrammer.Text;
            }
            else
            {
                MessageBox.Show("Виберіть рядок для редагування", "Помилка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Виберіть рядок для видалення", "Помилка");
            }
        }

        private void dataGridView1_MouseClick_1(object sender, MouseEventArgs e)
        {
            txbName.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txbSurname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            nmrAge.Value = n;
            cbxSex.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            cbxProgrammer.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблиця пуста", "Помилка");
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Очистіть поле перед завантаженням нового файлу", "Помилка");
            }
            else
            {
                if (File.Exists("D:\\Data.xml"))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml("D:\\Data.xml");

                    foreach (DataRow item in ds.Tables["Employee"].Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"];
                        dataGridView1.Rows[n].Cells[1].Value = item["Surname"];
                        dataGridView1.Rows[n].Cells[2].Value = item["Age"];
                        dataGridView1.Rows[n].Cells[3].Value = item["Sex"];
                        dataGridView1.Rows[n].Cells[4].Value = item["Programmer"];
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не знайдено", "Помилка");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Employee";
                dt.Columns.Add("Name");
                dt.Columns.Add("Surname");
                dt.Columns.Add("Age");
                dt.Columns.Add("Sex");
                dt.Columns.Add("Programmer");
                ds.Tables.Add(dt);

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    DataRow row = ds.Tables["Employee"].NewRow();
                    row["Name"] = r.Cells[0].Value;
                    row["Surname"] = r.Cells[1].Value;
                    row["Age"] = r.Cells[2].Value;
                    row["Sex"] = r.Cells[3].Value;
                    row["Programmer"] = r.Cells[4].Value;
                    ds.Tables["Employee"].Rows.Add(row);
                }
                ds.WriteXml("D:\\Data.xml");
                MessageBox.Show("XML файл успішно збережено", "Виконано");
            }
            catch
            {
                MessageBox.Show("Неможливо зберегти XML файл", "Помилка");
            }
        }
    }
}

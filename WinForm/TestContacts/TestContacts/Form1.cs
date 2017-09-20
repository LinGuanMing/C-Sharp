using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace TestContacts
{
    public partial class Form1 : Form
    {
        private List<Contact> _ContactList = new List<Contact>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=192.66.6.186;"
                                    + "Database=JDBDD;"
                                    + "User Id=sqlusr;"
                                    + "Password=1234;";
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = @"select Id,Name,Gender,Phone from Contacts ";

                sqlConn.Open();
                using (SqlDataReader sdr = sqlCmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        Contact contactData = null;
                        while (sdr.Read())
                        {
                            contactData = new Contact();
                            if (sdr[0] != DBNull.Value)
                            {
                                contactData.Id = sdr.GetInt32(0);
                            }
                            if (sdr[1] != DBNull.Value)
                            {
                                contactData.Name = sdr.GetString(1);
                            }
                            if (sdr[2] != DBNull.Value)
                            {
                                contactData.Gender = sdr.GetString(2);
                            }
                            if (sdr[3] != DBNull.Value)
                            {
                                contactData.Phone = sdr.GetString(3);
                            }
                            //加進清單
                            _ContactList.Add(contactData);
                        }
                    }
                }
                dataGridView1.DataSource = _ContactList;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string keyword = textBox1.Text.Trim();

            var keywordList = from c in _ContactList
                              where c.Name.Contains(keyword) || c.Phone.Contains(keyword)
                              select c;

            dataGridView1.DataSource = keywordList.ToList();
        }
        public class Contact
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string keyword = textBox1.Text.Trim();

                var keywordList = from c in _ContactList
                                  where c.Name.Contains(keyword) || c.Phone.Contains(keyword)
                                  select c;

                dataGridView1.DataSource = keywordList.ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form fm2 = new Form2();
            fm2.Visible = true;
            this.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            Application.Exit();
        }
    }
}

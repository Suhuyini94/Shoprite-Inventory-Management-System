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

namespace ShopriteMgtSys
{
    public partial class Unit : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");

        public Unit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand scmd1 = conn.CreateCommand();
            scmd1.CommandType = CommandType.Text;
            scmd1.CommandText = "select * from units where unit='"+textBox1.Text+"'";
            scmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(scmd1);
            sda1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            i = Convert.ToInt32(dt1.Rows.Count.ToString());
            if (i == 0)
            {
                SqlCommand scmd = conn.CreateCommand();
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "insert into units values('" + textBox1.Text + "')";
                textBox1.Text = "";
                scmd.ExecuteNonQuery();
                display_unit();
            }
            else
            {
                MessageBox.Show("This Unit is already added");
            }
        }

        private void Unit_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            display_unit();
        }

        public void display_unit()
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from units";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "delete from units where id= " + id + "";
            scmd.ExecuteNonQuery();

            display_unit();
        }
    }
}

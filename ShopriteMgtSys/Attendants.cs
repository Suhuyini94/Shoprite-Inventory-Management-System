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
    public partial class Attendants : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");
        public Attendants()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "insert into supplier values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"')";
            scmd.ExecuteNonQuery();

            textBox1.Text = ""; textBox2.Text = "";
            textBox3.Text = ""; textBox4.Text = "";

            data_grid();
            MessageBox.Show("Supplier added successfully");
        }

        private void Attendants_Load(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            data_grid();
            panel2.Visible = false;
        }

        public void data_grid()
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from supplier";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            int apdt;
            apdt = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from supplier where id=" + apdt+"";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            
            foreach(DataRow dr in dt.Rows)
            {
                textBox5.Text = dr["supplier_name"].ToString();
                textBox6.Text = dr["contact"].ToString();
                textBox7.Text = dr["address"].ToString();
                textBox8.Text = dr["city"].ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int del;
            del = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "delete from supplier where id=" + del+"";
            scmd.ExecuteNonQuery();

            data_grid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int apdt;
            apdt = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "update supplier set supplier_name='" + textBox5.Text+"', contact='"+textBox6.Text+"', address='"+textBox7.Text+"', city='"+textBox8.Text+"' where id=" + apdt + "";
            scmd.ExecuteNonQuery();

            panel2.Visible = false;
            data_grid();
        }
    }
}

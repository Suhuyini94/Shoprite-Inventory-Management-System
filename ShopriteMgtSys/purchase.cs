using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShopriteMgtSys
{
    public partial class purchase : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");

        public purchase()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from products where product_name= '"+comboBox1.Text+"'";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                label3.Text = dr["units"].ToString();
            }
        }

        public void fill_product_name()
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from products";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["product_name"].ToString());
            }
        }


        public void fill_supplier_name()
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from supplier";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["supplier_name"].ToString());
            }
        }
        private void purchase_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            fill_product_name();
            fill_supplier_name();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox3.Text = Convert.ToString(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            SqlCommand scmd1 = conn.CreateCommand();
            scmd1.CommandType = CommandType.Text;
            scmd1.CommandText = "select * from stock where product_name='"+comboBox1.Text+"'";
            scmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(scmd1);
            sda1.Fill(dt1);
            i = Convert.ToInt32(dt1.Rows.Count.ToString());

            if(i == 0)
            {
                SqlCommand scmd2 = conn.CreateCommand();
                scmd2.CommandType = CommandType.Text;
                scmd2.CommandText = "insert into purchase values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "', '" + comboBox2.Text + "', '" + comboBox3.Text + "', '" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "', '" + textBox4.Text + "')";
                scmd2.ExecuteNonQuery();

                SqlCommand scmd3 = conn.CreateCommand();
                scmd3.CommandType = CommandType.Text;
                scmd3.CommandText = "insert into stock values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "')";
                scmd3.ExecuteNonQuery();

            }
            else
            {
                SqlCommand scmd4 = conn.CreateCommand();
                scmd4.CommandType = CommandType.Text;
                scmd4.CommandText = "insert into purchase values('" + comboBox1.Text + "','" + textBox1.Text + "','" + label3.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "', '" + comboBox2.Text + "', '" + comboBox3.Text + "', '" + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + "', '" + textBox4.Text + "')";
                scmd4.ExecuteNonQuery();


                SqlCommand scmd5 = conn.CreateCommand();
                scmd5.CommandType = CommandType.Text;
                scmd5.CommandText = "update stock set product_qty=product_qty + " + textBox1.Text +" where product_name='"+comboBox1.Text+"' ";
                scmd5.ExecuteNonQuery();
            }
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
            comboBox1.Text = "";    comboBox2.Text = "";    comboBox3.Text = "";
            dateTimePicker1.Text = "";    dateTimePicker2.Text = "";


            MessageBox.Show("Purchase recorded successfully");
        }
    }
}

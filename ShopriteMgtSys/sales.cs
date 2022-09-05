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

namespace ShopriteMgtSys
{
    public partial class sales : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");

        public sales()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void sales_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            
                listBox1.Visible = true;

                listBox1.Items.Clear();
                SqlCommand scmd = conn.CreateCommand();
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "select * from stock where product_name like('" + textBox3.Text + "%')";
                scmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(scmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    listBox1.Items.Add(dr["product_name"].ToString());
                }

            
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;

                }

                if (e.KeyCode == Keys.Up)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex - 1;

                }

                if(e.KeyCode == Keys.Enter)
                {
                    textBox3.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    textBox4.Focus();
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}

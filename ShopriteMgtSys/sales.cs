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
        DataTable dt = new DataTable();
        int total = 0;
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
            dt.Clear();
            dt.Columns.Add("Product");
            dt.Columns.Add("Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Total");
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

        private void textBox4_Enter(object sender, EventArgs e)
        {
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select top 1 * from purchase where product_name= '"+textBox3.Text+"' order by id desc";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                textBox4.Text = dr["product_price"].ToString();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox6.Text = Convert.ToString(Convert.ToInt32(textBox4.Text) * Convert.ToInt32(textBox5.Text));
            }
            catch(Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand scmd1 = conn.CreateCommand();
            scmd1.CommandType = CommandType.Text;
            scmd1.CommandText = "select * from purchase where product_name= '" + textBox3.Text + "'";
            scmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda1 = new SqlDataAdapter(scmd1);
            sda1.Fill(dt1);

            foreach (DataRow dr1 in dt1.Rows)
            {
                stock = Convert.ToInt32(dr1["product_qty"].ToString());
            }

            if(Convert.ToInt32(textBox5.Text) > stock)
            {
                MessageBox.Show("This match value is not available");
            }
            else
            {
                DataRow dr = dt.NewRow();

                dr["Product"] = textBox3.Text;
                dr["Price"] = textBox4.Text;
                dr["Quantity"] = textBox5.Text;
                dr["Total"] = textBox6.Text;

                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                total = total + Convert.ToInt32(dr["Total"].ToString());
                label10.Text = total.ToString();
            }

            textBox3.Text = ""; textBox4.Text = "";
            textBox5.Text = ""; textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int total = 0;
                dt.Rows.RemoveAt(Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString()));

                foreach(DataRow dr1 in dt.Rows)
                {
                    total = total + Convert.ToInt32(dr1["Total"].ToString());
                    label10.Text = total.ToString();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string orderid = "";
            SqlCommand scmd1 = conn.CreateCommand();
            scmd1.CommandType = CommandType.Text;
            scmd1.CommandText = "insert into order_user values('"+textBox1.Text+"', '"+textBox2.Text+"', '"+comboBox1.Text+"', '"+dateTimePicker1.Value.ToString("dd-MM-yyyy")+"')";
            scmd1.ExecuteNonQuery();

            SqlCommand scmd2 = conn.CreateCommand();
            scmd2.CommandType = CommandType.Text;
            scmd2.CommandText = "select top 1 * from order_user order by id desc";
            scmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(scmd2);
            sda2.Fill(dt2);
            foreach(DataRow dr2 in dt2.Rows)
            {
                orderid = dr2["id"].ToString();
            }
            foreach(DataRow dr in dt.Rows)
            {
                int qty = 0;
                string pname = "";

                SqlCommand scmd3 = conn.CreateCommand();
                scmd3.CommandType = CommandType.Text;
                scmd3.CommandText = "insert into order_item values('"+orderid.ToString()+"', '" + dr["product"].ToString()+ "', '" + dr["price"].ToString()+"', '" + dr["quantity"].ToString()+"', '" + dr["total"].ToString()+"')";
                scmd3.ExecuteNonQuery();


                qty = Convert.ToInt32(dr["quantity"].ToString());
                pname = dr["product"].ToString();

                SqlCommand scmd4 = conn.CreateCommand();
                scmd4.CommandType = CommandType.Text;
                scmd4.CommandText = "update stock set product_qty=product_qty- "+qty+" where product_name='"+pname.ToString()+"'";
                 scmd4.ExecuteNonQuery();
            }

            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = "";
            label10.Text = ""; comboBox1.Text = "";

            dt.Clear();
            dataGridView1.DataSource = dt;

            MessageBox.Show("Record inserted successfully");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string orderid = "";
            SqlCommand scmd1 = conn.CreateCommand();
            scmd1.CommandType = CommandType.Text;
            scmd1.CommandText = "insert into order_user values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + comboBox1.Text + "', '" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "')";
            scmd1.ExecuteNonQuery();

            SqlCommand scmd2 = conn.CreateCommand();
            scmd2.CommandType = CommandType.Text;
            scmd2.CommandText = "select top 1 * from order_user order by id desc";
            scmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(scmd2);
            sda2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                orderid = dr2["id"].ToString();
            }
            foreach (DataRow dr in dt.Rows)
            {
                int qty = 0;
                string pname = "";

                SqlCommand scmd3 = conn.CreateCommand();
                scmd3.CommandType = CommandType.Text;
                scmd3.CommandText = "insert into order_item values('" + orderid.ToString() + "', '" + dr["product"].ToString() + "', '" + dr["price"].ToString() + "', '" + dr["quantity"].ToString() + "', '" + dr["total"].ToString() + "')";
                scmd3.ExecuteNonQuery();


                qty = Convert.ToInt32(dr["quantity"].ToString());
                pname = dr["product"].ToString();

                SqlCommand scmd4 = conn.CreateCommand();
                scmd4.CommandType = CommandType.Text;
                scmd4.CommandText = "update stock set product_qty=product_qty- " + qty + " where product_name='" + pname.ToString() + "'";
                scmd4.ExecuteNonQuery();
            }

            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = "";
            label10.Text = ""; comboBox1.Text = "";

            dt.Clear();
            dataGridView1.DataSource = dt;

            generate_bill gb = new generate_bill();
            gb.get_value(Convert.ToInt32(orderid.ToString()));
            gb.Show();
        }
    }
}

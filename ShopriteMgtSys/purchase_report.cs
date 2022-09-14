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
    public partial class purchase_report : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");
        string query = "";
        public purchase_report()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from purchase";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;


            foreach (DataRow dr in dt.Rows)
            {
                i = i + Convert.ToInt32(dr["product_total"].ToString());
            }

            label3.Text = i.ToString();
            query = "select * from purchase";
        }

        private void purchase_report_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string startdate, enddate;
            startdate = dateTimePicker1.Value.ToString("dd-MMM-yyyy");
            enddate = dateTimePicker2.Value.ToString("dd-MMM-yyyy");

            int i = 0;
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from purchase where purchase_date>= '"+startdate.ToString()+"' and purchase_date<='"+enddate.ToString()+"'";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;


            foreach (DataRow dr in dt.Rows)
            {
                i = i + Convert.ToInt32(dr["product_total"].ToString());
            }

            label3.Text = i.ToString();
            query = "select * from purchase where purchase_date>= '"+startdate.ToString()+"' and purchase_date<='"+enddate.ToString()+"'";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            generate_purchase_report gpr = new generate_purchase_report();
            gpr.get_value(query.ToString());
            gpr.Show();
        }
    }
}

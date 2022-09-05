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
    public partial class login : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");

        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from registration where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                MessageBox.Show("Invalid username or password!");
            }
            else
            {
                this.Hide();
                MDIParent1 mdi = new MDIParent1();
                mdi.Show();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

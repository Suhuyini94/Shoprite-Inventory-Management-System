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
    public partial class generate_bill : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");

        int j;
        int total = 0;
        public generate_bill()
        {
            InitializeComponent();
        }

        public void get_value(int i)
        {
            j = i;
        }

        private void generate_bill_Load(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            conn.Open();

            DataSet1 ds = new DataSet1();
            
            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "select * from order_user where id="+ j +"";
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(ds.DataTable1);


            SqlCommand scmd2 = conn.CreateCommand();
            scmd2.CommandType = CommandType.Text;
            scmd2.CommandText = "select * from order_item where order_id=" + j + "";
            scmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter(scmd2);
            sda2.Fill(ds.DataTable2);
            sda2.Fill(dt2);


            total = 0;
            foreach(DataRow dr2 in dt2.Rows)
            {
                total = total + Convert.ToInt32(dr2["total"].ToString());

            }

            CrystalReport1 myreport = new CrystalReport1();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("total", total.ToString());
            crystalReportViewer1.ReportSource = myreport;
        }
    }
}

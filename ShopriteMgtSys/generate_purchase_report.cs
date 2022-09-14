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
    public partial class generate_purchase_report : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHA'BAN\\source\\repos\\ShopriteMgtSys\\ShopriteMgtSys\\inventory.mdf\";Integrated Security=True");

        string j;
        int total = 0;
        public void get_value(string i)
        {
            j = i;
        }

        public generate_purchase_report()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void generate_purchase_report_Load(object sender, EventArgs e)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            DataSet2 ds = new DataSet2();

            SqlCommand scmd = conn.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = j;
            scmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(scmd);
            sda.Fill(ds.DataTable1);
            sda.Fill(dt);

            total = 0;
            foreach (DataRow dr in dt.Rows)
            {
                total = total + Convert.ToInt32(dr["product_total"].ToString());

            }

            CrystalReport2 myreport = new CrystalReport2();
            myreport.SetDataSource(ds);
            myreport.SetParameterValue("total", total.ToString());
            crystalReportViewer1.ReportSource = myreport;
        }
    }
}

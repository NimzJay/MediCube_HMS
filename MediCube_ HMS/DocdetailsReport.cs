using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace MediCube__HMS
{
    public partial class DocdetailsReport : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        ReportDocument cry1 = new ReportDocument();
        public DocdetailsReport()
        {
            InitializeComponent();
        }

        private void DocdetailsReport_Load(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Laleesha\Docde.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("docDetailsReport", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "TBLDoc");
            cry1.SetDataSource(st);
            docReport.ReportSource = cry1;
        }

        private void docbtn_Click(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Laleesha\Docde.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("getDocdetailsReport", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Name", doctxt.Text.Trim());
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "TBLDoc");
            cry1.SetDataSource(st);
            docReport.ReportSource = cry1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();   //moving from one window to another
            MediCube_Doctor ss1 = new MediCube_Doctor();
            ss1.Show();
        }
    }
}

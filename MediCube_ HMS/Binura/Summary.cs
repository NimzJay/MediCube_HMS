using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace MediCube__HMS.Binura
{
    public partial class Summary : UserControl
    {
        ReportDocument cry = new ReportDocument();
        
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Summary()
        {
            InitializeComponent();
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            cry.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Binura\Lab_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("labReport", sqlCon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "LAB_DATA");
            cry.SetDataSource(st);
            crystalReportViewer1.ReportSource = cry;
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            cry.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Binura\Lab_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("param_labReport", sqlCon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@name", txtName.Text.Trim());
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "LAB_DATA");
            cry.SetDataSource(st);
            crystalReportViewer1.ReportSource = cry;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

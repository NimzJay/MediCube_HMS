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

namespace MediCube__HMS.Mihiri
{
    public partial class InReports : UserControl
    {
        ReportDocument cry = new ReportDocument();
        ReportDocument cry1 = new ReportDocument();
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public InReports()
        {
            InitializeComponent();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Mihiri\InBill_Rpt.rpt");
            SqlDataAdapter sda1 = new SqlDataAdapter("param_ReportInBill", sqlCon);
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda1.SelectCommand.Parameters.AddWithValue("@Name", txtBill.Text.Trim());
            DataSet st1 = new System.Data.DataSet();
            sda1.Fill(st1, "DATA_INBILL");
            cry1.SetDataSource(st1);
            crystalReportInBill.ReportSource = cry1;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            cry.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Mihiri\InPatient_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("param_ReportInp", sqlCon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Name", txtIn.Text.Trim());
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "DATA_INP");
            cry.SetDataSource(st);
            crystalReportInPatient.ReportSource = cry;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            crystalReportInBill.Visible = false;
            crystalReportInPatient.Visible = true;
            btnIn.Visible = true;
            txtIn.Visible = true;
            txtBill.Visible = false;
            btnBill.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crystalReportInBill.Visible = true;
            crystalReportInPatient.Visible = false;
            btnIn.Visible = false;
            txtIn.Visible = false;
            txtBill.Visible = true;
            btnBill.Visible = true;
        }

        private void InReports_Load(object sender, EventArgs e)
        {
            cry.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Mihiri\InPatient_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("VieworSearch", sqlCon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "DATA_INP");
            cry.SetDataSource(st);
            crystalReportInPatient.ReportSource = cry;

            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Mihiri\InBill_Rpt.rpt");
            SqlDataAdapter sda1 = new SqlDataAdapter("viewBill", sqlCon);
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st1 = new System.Data.DataSet();
            sda1.Fill(st1, "DATA_INBILL");
            cry1.SetDataSource(st1);
            crystalReportInBill.ReportSource = cry1;

            btnIn.Visible = false;
            txtIn.Visible = false;
        }
    }
}

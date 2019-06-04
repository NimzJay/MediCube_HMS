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

namespace MediCube__HMS.Nimna
{
    public partial class Report : UserControl
    {
        ReportDocument cry = new ReportDocument();
        ReportDocument cry1 = new ReportDocument();

        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            cry.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Nimna\Medicine_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("mainViewMed", sqlcon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "MED_DATA");
            cry.SetDataSource(st);
            crystalReportMed.ReportSource = cry;

            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Nimna\Sales_Rpt.rpt");
            SqlDataAdapter sda1 = new SqlDataAdapter("incomeView", sqlcon);
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st1 = new System.Data.DataSet();
            sda1.Fill(st1, "BILL_DATA");
            cry1.SetDataSource(st1);
            crystalReportBill.ReportSource = cry1;

            label1.Visible = false;
            label2.Visible = false;
            btnDate.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            cry.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Nimna\Medicine_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("param_ReportMed", sqlcon);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@medName", textMedName.Text.Trim());
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "MED_DATA");
            cry.SetDataSource(st);
            crystalReportMed.ReportSource = cry;
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Nimna\Sales_Rpt.rpt");
            SqlDataAdapter sda1 = new SqlDataAdapter("incomeSearchView", sqlcon);
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda1.SelectCommand.Parameters.AddWithValue("@fdt", dateTimePicker1.Value);
            sda1.SelectCommand.Parameters.AddWithValue("@ldt", dateTimePicker2.Value);
            DataSet st1 = new System.Data.DataSet();
            sda1.Fill(st1, "BILL_DATA");
            cry1.SetDataSource(st1);
            crystalReportBill.ReportSource = cry1;
        }

        private void btnMed_Click(object sender, EventArgs e)
        {
            crystalReportBill.Visible = false;
            crystalReportMed.Visible = true;
            textMedName.Visible = true;
            btnName.Visible = true;
            label1.Visible = false;
            label2.Visible = false;
            btnDate.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            crystalReportBill.Visible = true;
            crystalReportMed.Visible = false;
            //crystalReportBill.BringToFront();
            textMedName.Visible = false;
            btnName.Visible = false;
            label1.Visible = true;
            label2.Visible = true;
            btnDate.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
        }
    }
}

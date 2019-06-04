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
    public partial class outPatienBillReport : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        ReportDocument cry1 = new ReportDocument();

        public outPatienBillReport()
        {
            InitializeComponent();
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
           
        }

        private void outPatienBillReport_Load(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Pavani\OutpatientBilRe.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("outPatientBRe", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "OUT_BILL_P_Report");
            cry1.SetDataSource(st);
            OutpatienBill.ReportSource = cry1;

        }

        private void obtn_Click(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Pavani\OutpatientBilRe.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("getOutBillReport", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Name", Otxt.Text.Trim());
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "OUT_BILL_P_Report");
            cry1.SetDataSource(st);
            OutpatienBill.ReportSource = cry1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Out_Patient ps = new MediCube_Out_Patient();
            ps.Show();
        }

        private void OutpatienBill_Load(object sender, EventArgs e)
        {

        }
    }
}

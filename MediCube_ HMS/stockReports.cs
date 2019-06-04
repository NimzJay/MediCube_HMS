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
    public partial class stockReports : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        ReportDocument cry1 = new ReportDocument();
        ReportDocument cry2 = new ReportDocument();
        ReportDocument cry3 = new ReportDocument();
        public stockReports()
        {
            InitializeComponent();
        }

        private void stockReports_Load(object sender, EventArgs e)
        {
            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Dakshika\Stock_Pharm_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("Stock_pharmacy_report", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "STOCK_PHARM");
            cry1.SetDataSource(st);
            stockPham.ReportSource = cry1;

            cry2.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Dakshika\stock_labo.rpt");
            SqlDataAdapter sda1 = new SqlDataAdapter("Stock_Lab_report", con);
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st1 = new System.Data.DataSet();
            sda1.Fill(st1, "STOCK_LAB");
            cry2.SetDataSource(st1);
            stockLab.ReportSource = cry2;

            cry3.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Dakshika\stcock_the.rpt");
            SqlDataAdapter sda2 = new SqlDataAdapter("Stock_The_report", con);
            sda2.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st2 = new System.Data.DataSet();
            sda2.Fill(st2, "STOCK_THE");
            cry3.SetDataSource(st2);
            stockThe.ReportSource = cry3;


        }

        private void pharmacy_Click(object sender, EventArgs e)
        {
            stockPham.Visible = true;
            stockLab.Visible = false;
            stockThe.Visible = false;

            phamtext.Visible = true;
            phabtn.Visible =true;

            labtxt.Visible = false;
            lbBtn.Visible = false;

            thetxt.Visible = false;
            thebtn.Visible = false;
            
        }

        private void threatre_Click(object sender, EventArgs e)
        {
            stockPham.Visible = false;
            stockLab.Visible = false;
            stockThe.Visible = true;

            phamtext.Visible = false;
            phabtn.Visible = false;

            labtxt.Visible = false;
            lbBtn.Visible = false;

            thetxt.Visible = true;
            thebtn.Visible = true;
            

        }

        private void lab_Click(object sender, EventArgs e)
        {
            stockPham.Visible = false;
            stockLab.Visible = true;
            stockThe.Visible = false;

            phamtext.Visible = false;
            phabtn.Visible = false;

            labtxt.Visible = true;
            lbBtn.Visible = true;

            thetxt.Visible = false;
            thebtn.Visible = false;
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cry2.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Dakshika\stock_labo.rpt");
            SqlDataAdapter sda1 = new SqlDataAdapter("getStock_lab", con);
            sda1.SelectCommand.Parameters.AddWithValue("@Name", labtxt.Text.Trim());
            sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet st1 = new System.Data.DataSet();
            sda1.Fill(st1, "STOCK_LAB");
            cry2.SetDataSource(st1);
            stockLab.ReportSource = cry2;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            cry1.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Dakshika\Stock_Pharm_Rpt.rpt");
            SqlDataAdapter sda = new SqlDataAdapter("getStock_pharm", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@Name", phamtext.Text.Trim());
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "STOCK_PHARM");
            cry1.SetDataSource(st);
            stockPham.ReportSource = cry1;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            cry3.Load(@"C:\Users\Hp\Desktop\MediCube_ HMS\MediCube_ HMS\Dakshika\stcock_the.rpt");
            SqlDataAdapter sda2 = new SqlDataAdapter("getStock_The", con);
            sda2.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda2.SelectCommand.Parameters.AddWithValue("@Name", thetxt.Text.Trim());
            DataSet st2 = new System.Data.DataSet();
            sda2.Fill(st2, "STOCK_THE");
            cry3.SetDataSource(st2);
            stockThe.ReportSource = cry3;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Stock s1 = new MediCube_Stock();
            s1.Show();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Stock s1 = new MediCube_Stock();
            s1.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Stock s1 = new MediCube_Stock();
            s1.Show();
        }
    }
}

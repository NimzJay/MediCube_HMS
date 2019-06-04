using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MediCube__HMS
{
    public partial class MediCube_Pharmacy : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public MediCube_Pharmacy()
        {
            InitializeComponent();
            sidePanel.Height = btnHome.Height;
            sidePanel.Top = btnHome.Top;
            p_Index1.BringToFront();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void MediCube_Pharmacy_Load(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnHome.Height;
            sidePanel.Top = btnHome.Top;
            p_Index1.BringToFront(); 
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnPOS.Height;
            sidePanel.Top = btnPOS.Top;
            pos1.BringToFront();
        }

        private void btnMed_Click(object sender, EventArgs e)
        {
            medView1.BringToFront();
            sidePanel.Height = btnMed.Height;
            sidePanel.Top = btnMed.Top; 
        }

        private void btnAddMed_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnAddMed.Height;
            sidePanel.Top = btnAddMed.Top;
            medInsert1.BringToFront();
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnIncome.Height;
            sidePanel.Top = btnIncome.Top;
            income1.BringToFront();
        }

        private void btnExpire_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnExpire.Height;
            sidePanel.Top = btnExpire.Top;
            expire_Alerts1.BringToFront();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnStock.Height;
            sidePanel.Top = btnStock.Top;
            stock_Alerts1.BringToFront();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnReports.Height;
            sidePanel.Top = btnReports.Top;
            report1.BringToFront();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnStaff.Height;
            sidePanel.Top = btnStaff.Top;
            p_staff1.BringToFront();
        }
    }
}

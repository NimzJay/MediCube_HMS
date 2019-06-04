using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MediCube__HMS.Nimna
{
    public partial class Income : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Income()
        {
            InitializeComponent();
        }

        void fillGridDataView()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("incomeView", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvIncome.DataSource = dtbl;

            sqlcon.Close();
        }

        private void Income_Load(object sender, EventArgs e)
        {
            try
            {
                dgvIncome.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

                fillGridDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void reload_Click(object sender, EventArgs e)
        {
            try
            {
                dgvIncome.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

                fillGridDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        void fillGridDataView2()
        {
            //load the set of records for the user entered parameters.
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("incomeSearchView", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@fdt", dateTimePicker1.Value);
            sqlDa.SelectCommand.Parameters.AddWithValue("@ldt", dateTimePicker2.Value);
            DataTable dtbl2 = new DataTable();
            sqlDa.Fill(dtbl2);
            dgvIncome.DataSource = dtbl2;

            sqlcon.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillGridDataView2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }

            if (dateTimePicker1.Value > DateTime.Today)
            {
                MessageBox.Show("Please Enter a valid Date!");
                dateTimePicker1.Value = DateTime.Today;
            }
        }
    }
}

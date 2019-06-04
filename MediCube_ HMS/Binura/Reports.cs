using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MediCube__HMS.Binura
{
    public partial class Reports : UserControl
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ReportsViewOrSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Patient_ID", SearchBox.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            sqlCon.Close();


        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Reports");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();


                SqlCommand sqlcmd = new SqlCommand("ReportsDelete", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Report_ID", textBox1.Text.Trim());
                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Succesfully");
                Reset();
                FillDataGridView();



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Report ID");
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Patient ID");
            }



            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (button7.Text == "Insert")
                {
                    SqlCommand sqlcmd = new SqlCommand("ReportsADD", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Add");
                    sqlcmd.Parameters.AddWithValue("@Report_ID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Reports_Category", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Date", Rdate.Value);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Save Succesfully");


                }
                else
                {
                    SqlCommand sqlcmd = new SqlCommand("ReportsADD", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlcmd.Parameters.AddWithValue("@Report_ID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Reports_Category", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Date", Rdate.Value);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Update Succesfully");


                }

                Reset();
                FillDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Reports");
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Reports");
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                Rdate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                button7.Text = "Update";
                button9.Enabled = true;

            }
        }

        void Reset()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = Rdate.Text = "";
            button7.Text = "Insert";
            button9.Enabled = false;

        }
    }
}

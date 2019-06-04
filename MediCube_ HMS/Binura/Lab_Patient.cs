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
    public partial class Lab_Patient : UserControl
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Lab_Patient()
        {
            InitializeComponent();
        }

        private void Lab_Patient_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Patient");
            }
        }

        void Reset()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = "";
            button7.Text = "Submit";
            button9.Enabled = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();


                SqlCommand sqlcmd = new SqlCommand("PatientDelete", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox1.Text.Trim());
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

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Patient");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Patient ID");
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Patient Name");
            }





            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (button7.Text == "Submit")
                {

                    SqlCommand sqlcmd = new SqlCommand("PatientADD", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Add");
                    sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_Name", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Phone_Number", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Sex", textBox4.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Age", textBox5.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Address", textBox6.Text);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Save Succesfully");
                }
                else
                {
                    SqlCommand sqlcmd = new SqlCommand("PatientADD", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_Name", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Phone_Number", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Sex", textBox4.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Age", textBox5.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Address", textBox6.Text);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("updated Succesfully");


                }

                Reset();
                FillDataGridView();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Patient");
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("PatientViewOrSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Patient_Name", SearchBox.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            sqlCon.Close();


        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                button7.Text = "Update";
                button9.Enabled = true;

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 0)
            {
                e.Handled = false;
            }

            else
            {
                MessageBox.Show("Please Enter Only Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Handled = true;
            }
        }

    }
}

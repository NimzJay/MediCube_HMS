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
    public partial class Test : UserControl
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("TestViewOrSearch", sqlCon);
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
                MessageBox.Show(ex.Message, "Error Message Patient");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Test ID");
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Patient ID");
            }


            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (button7.Text == "Submit")
                {
                    SqlCommand sqlcmd = new SqlCommand("TestADD", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Add");
                    sqlcmd.Parameters.AddWithValue("@Test_ID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_Name", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Date", Ldate.Value);
                    sqlcmd.Parameters.AddWithValue("@Lipid_Profile", textBox5.Text);
                    sqlcmd.Parameters.AddWithValue("@Bilirubin_Analysis", textBox6.Text);
                    sqlcmd.Parameters.AddWithValue("@Protein", textBox7.Text);
                    sqlcmd.Parameters.AddWithValue("@WIDAL_Test", textBox8.Text);
                    sqlcmd.Parameters.AddWithValue("@Other_Analysis", textBox9.Text);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Save Succesfully");
                }

                else
                {
                    SqlCommand sqlcmd = new SqlCommand("TestADD", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlcmd.Parameters.AddWithValue("@Test_ID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_ID", textBox2.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Patient_Name", textBox3.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Date", Ldate.Value);
                    sqlcmd.Parameters.AddWithValue("@Lipid_Profile", textBox5.Text);
                    sqlcmd.Parameters.AddWithValue("@Bilirubin_Analysis", textBox6.Text);
                    sqlcmd.Parameters.AddWithValue("@Protein", textBox7.Text);
                    sqlcmd.Parameters.AddWithValue("@WIDAL_Test", textBox8.Text);
                    sqlcmd.Parameters.AddWithValue("@Other_Analysis", textBox9.Text);
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Succesfully");

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

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();


                SqlCommand sqlcmd = new SqlCommand("TestDelete", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Test_ID", textBox1.Text.Trim());
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

        private void Refersh_Click(object sender, EventArgs e)
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
            textBox1.Text = textBox2.Text = textBox3.Text = Ldate.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = "";
            button7.Text = "Save";
            button10.Enabled = false;

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                Ldate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                button7.Text = "Update";
                button10.Enabled = true;

            }
        }
    }
}

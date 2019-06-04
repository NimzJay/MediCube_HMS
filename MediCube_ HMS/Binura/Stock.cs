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
    public partial class Stock : UserControl
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Lab Test Item");
                return;
            }

            if (invoicedate.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Invoice Date");
                return;
            }
            if (expirydate.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Expired Date");
                return;
            }
            if (InputDate.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Input Date");
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Quantity");
                return;
            }
            if (textBox6.Text == "")
            {
                MessageBox.Show("Validation Error-Enter Net_Amount");
                return;
            }


            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (button7.Text == "Insert")
                {
                    SqlCommand sqlcmd = new SqlCommand("StockAdd", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Add");
                    sqlcmd.Parameters.AddWithValue("@Lab_Test_Item", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Invoice_Date", invoicedate.Value);
                    sqlcmd.Parameters.AddWithValue("@Expiry_Date", expirydate.Value);
                    sqlcmd.Parameters.AddWithValue("@Input_Date", InputDate.Value);
                    sqlcmd.Parameters.AddWithValue("@Quantity", textBox5.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Net_Amount", textBox6.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Save Succesfully");

                }

                else
                {



                    SqlCommand sqlcmd = new SqlCommand("StockAdd", sqlCon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlcmd.Parameters.AddWithValue("@Lab_Test_Item", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Invoice_Date", invoicedate.Value);
                    sqlcmd.Parameters.AddWithValue("@Expiry_Date", expirydate.Value);
                    sqlcmd.Parameters.AddWithValue("@Input_Date", InputDate.Value);
                    sqlcmd.Parameters.AddWithValue("@Quantity", textBox5.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@Net_Amount", textBox6.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Succesfully");

                }

                Reset();
                FillDataGridView();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();


                SqlCommand sqlcmd = new SqlCommand("StockDelete", sqlCon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Lab_Test_Item", textBox1.Text.Trim());
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

        private void button10_Click(object sender, EventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            textBox1.Text = invoicedate.Text = expirydate.Text = InputDate.Text = textBox5.Text = textBox6.Text = "";
            button7.Text = "Insert";
            button9.Enabled = false;

        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Stock");
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message Stock");
            }
        }

        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("StockViewOrSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Lab_Test_Item", SearchBox.Text.Trim());
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
                invoicedate.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                expirydate.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                InputDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                button7.Text = "Update";
                button9.Enabled = true;

            }
        }

    }
}

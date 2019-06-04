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
    public partial class stockPharmacy : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int MedicineId = 0;
        public stockPharmacy()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Stock ss1 = new MediCube_Stock();
            ss1.Show();
        }

        private void insert_Click(object sender, EventArgs e)
        {
            if (textName.Text == "")
            {
                MessageBox.Show("Name validation error");
                return;
            }

            if (textCat.Text == "")
            {
                MessageBox.Show(" Category validation error");
                return;
            }

            if (textGen.Text == "")
            {
                MessageBox.Show("Generic name validation error");
                return;
            }

            if (textQu.Text == "")
            {
                MessageBox.Show("Quantity validation error");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (insert.Text == "Insert")
                {

                    SqlCommand cmd = new SqlCommand("Pharmacy", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mode", "Add");
                    cmd.Parameters.AddWithValue("@MedicineId ", 0);
                    cmd.Parameters.AddWithValue("@Name", textName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", textCat.Text.Trim());
                    cmd.Parameters.AddWithValue("@Store_Box", textSt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Generic_Name", textGen.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", textQu.Text.Trim());
                    cmd.Parameters.AddWithValue("@Expire_Date", dateTimePicker2.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert successfully");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Pharmacy", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mode", "Edit");
                    cmd.Parameters.AddWithValue("@MedicineId ", MedicineId);
                    cmd.Parameters.AddWithValue("@Name", textName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", textCat.Text.Trim());
                    cmd.Parameters.AddWithValue("@Store_Box", textSt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Generic_Name", textGen.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", textQu.Text.Trim());
                    cmd.Parameters.AddWithValue("@Expire_Date", dateTimePicker2.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update successfully");
                }
                Reset();
                DataView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                con.Close();
            }

        }
        void DataView()
        {
            if (
                
                
                con.State == ConnectionState.Closed)
                con.Open();



            SqlDataAdapter sqlData = new SqlDataAdapter("Seach", con);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@searchName", search1.Text.Trim());
            DataTable dt1 = new DataTable();
            sqlData.Fill(dt1);
            dataGridView1.DataSource = dt1;
            con.Close();


        }

        private void search_Click(object sender, EventArgs e)
        {
            try
            {
                DataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void delete_click_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("MedDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedicineId ", MedicineId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete successfully");
                Reset();
                DataView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                con.Close();
            }

        }
        void Reset()
        {
            textName.Text = textCat.Text = textSt.Text = textGen.Text = textQu.Text = dateTimePicker2.Text = "";
            insert.Text = "Insert";
            MedicineId = 0;
            delete_click.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void stockPharmacy_Load(object sender, EventArgs e)
        {
            Reset();
            DataView();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                MedicineId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                textName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textCat.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textSt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textGen.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textQu.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                int x = Int32.Parse(textQu.Text);
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                DateTime y = DateTime.Parse(dateTimePicker2.Text);
                insert.Text = "Update";
                delete_click.Enabled = true;

            }
        }
    }
}

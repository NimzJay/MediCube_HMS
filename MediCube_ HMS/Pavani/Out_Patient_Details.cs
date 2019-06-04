using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace MediCube__HMS
{
    public partial class Out_Patient_Details : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int PatientId = 0;
        public Out_Patient_Details()
        {
            InitializeComponent();
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            if (nameText.Text == "")
            {
                nameText.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameText.Focus();
                return;
            }
            if (ageText.Text == "") 
            {
                nameText.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameText.Focus();
                return;
            }
            if (conNumText.Text == "")
            {
                nameText.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameText.Focus();
                return;
            }
            {
                string pattern = "^[0-9]{10}$";
                if (Regex.IsMatch(conNumText.Text, pattern))
                {
                    errorProvider1.Clear();
                }
                else
                {
                    errorProvider1.SetError(this.conNumText, "Please provide a valid Contact Number");
                    return;
                }
            }
            if (addressText.Text == "")
            {
                nameText.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameText.Focus();
                return;
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (Insertbtn.Text == "Insert")
                {

                    SqlCommand sqlCmd = new SqlCommand("OutPDProc", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@PatientId", 0);
                    sqlCmd.Parameters.AddWithValue("@Name", nameText.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", ageText.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", conNumText.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", addressText.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Data inserted successfully");
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("OutPDProc", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@PatientId", PatientId);
                    sqlCmd.Parameters.AddWithValue("@Name", nameText.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", ageText.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", conNumText.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", addressText.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Data updated successfully");
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
                con.Close();
            }
        }
        void FillDataGridView()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("OutPViewSearch2", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Name", SearchTxt.Text.Trim());
            DataTable db = new DataTable();
            sqlDa.Fill(db);
            dgvPatients.DataSource = db;
            dgvPatients.Columns[0].Visible = false;
            con.Close();

        }

        private void nameText_TextChanged(object sender, EventArgs e)
        {
            nameText.BackColor = Color.White;
        }
        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            
        }
        void Reset()
        {
            nameText.Text = ageText.Text = conNumText.Text = addressText.Text = "";
            Insertbtn.Text = "Insert";
            PatientId = 0;
            Deletebtn.Enabled = false;
        }

        private void Restbtn_Click(object sender, EventArgs e)
        {
          
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

 

        private void ageText_TextChanged(object sender, EventArgs e)
        {
            ageText.BackColor = Color.White;
        }

 

        private void Out_Patient_Details_Load(object sender, EventArgs e)
        {
        
        }

        private void ageText_TextChanged_1(object sender, EventArgs e)
        {
            ageText.BackColor = Color.White;
        }

        private void conNumText_TextChanged_1(object sender, EventArgs e)
        {
            conNumText.BackColor = Color.White;
        }

        private void addressText_TextChanged_1(object sender, EventArgs e)
        {
            addressText.BackColor = Color.White;
        }

        private void dgvPatients_DoubleClick(object sender, EventArgs e)
        {

            if (dgvPatients.CurrentRow.Index != -1)
            {
                PatientId = Convert.ToInt32(dgvPatients.CurrentRow.Cells[0].Value.ToString());
                nameText.Text = dgvPatients.CurrentRow.Cells[1].Value.ToString();
                ageText.Text = dgvPatients.CurrentRow.Cells[2].Value.ToString();
                conNumText.Text = dgvPatients.CurrentRow.Cells[3].Value.ToString();
                addressText.Text = dgvPatients.CurrentRow.Cells[4].Value.ToString();
                Insertbtn.Text = "Update";
                Deletebtn.Enabled = true;
            }
        }

        private void Billbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            OutPatient_Bill ps = new OutPatient_Bill();
            ps.Show();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ps = new Form1();
            ps.Show();
        }

        private void Deletebtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand sqlCmd = new SqlCommand("OutPDeletetion", con);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@PatientId", PatientId);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Data deleted successfully");
                Reset();
                FillDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void Restbtn_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        private void Searchbtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Out_Patient ps = new MediCube_Out_Patient();
            ps.Show();
        }
    }
}

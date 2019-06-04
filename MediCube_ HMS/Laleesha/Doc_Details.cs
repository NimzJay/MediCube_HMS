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
    public partial class Doc_Details : Form
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int DocID = 0;
        public Doc_Details()
        {
            //SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            //int DocID = 0;
            InitializeComponent();
        }



        private void Doc_Details_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtnm.Text == "") //stop inserting null records
            {
                txtnm.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnm.Focus();
                return;

            }

            if (txtAge.Text == "")
            {
                txtAge.BackColor = Color.LightPink;
                MessageBox.Show("Age is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAge.Focus();
                return;
            }
            else
            {
                int outAge;
                if (!int.TryParse(txtAge.Text, out outAge))
                {
                    txtAge.BackColor = Color.LightPink;
                    MessageBox.Show("Age field should only have integers", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAge.Focus();
                    return;
                }

            }

            if (txtConNumber.Text == "")
            {
                txtConNumber.BackColor = Color.LightPink;
                MessageBox.Show("Contact Number is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConNumber.Focus();
                return;
            }
            else
            {
                string pattern = "^[0-9]{10}$";
                if (Regex.IsMatch(txtConNumber.Text, pattern))
                {
                    errorProvider2.Clear();
                }
                else
                {
                    errorProvider2.SetError(this.txtConNumber, "Please provide a valid Contact Number");
                    return;
                }
            }

            if (txtEmail.Text == "")
            {
                txtEmail.BackColor = Color.LightPink;
                MessageBox.Show("Email is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            else
            {
                string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                if (Regex.IsMatch(txtEmail.Text, pattern))
                {
                    errorProvider1.Clear();
                }
                else
                {
                    errorProvider1.SetError(this.txtEmail, "Please provide a valid email");
                    return;
                }
            }

            if (txtSpeciality.Text == "")
            {
                txtSpeciality.BackColor = Color.LightPink;
                MessageBox.Show("Speciality is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSpeciality.Focus();
                return;
            }
             try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

             
                if (btnInsert.Text == "Insert")
                {

                    SqlCommand sqlCmd = new SqlCommand("DocAddorEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@DocID", 0);
                    sqlCmd.Parameters.AddWithValue("@Name", txtnm.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", txtConNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Speciality", txtSpeciality.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Data inserted successfully");
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("DocAddorEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@DocID", DocID);
                    sqlCmd.Parameters.AddWithValue("@Name", txtnm.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", txtConNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Speciality", txtSpeciality.Text.Trim());
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
                sqlCon.Close();
            }
        }
         void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ContactViewOrSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@DoctorName", txtSearch.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvDoctors.DataSource = dtbl;
            dgvDoctors.Columns[0].Visible = false;
            sqlCon.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
        }
        private void dgvDoctors_DoubleClick(object sender, EventArgs e)
        {
           
        }

        void Reset()
        {
            txtnm.Text = txtAge.Text = txtConNumber.Text = txtEmail.Text = txtSpeciality.Text = "";
            btnInsert.Text = "Insert";
            DocID = 0;
            btnDelete.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSpeciality_TextChanged(object sender, EventArgs e)
        {
           // txtSpeciality.BackColor = Color.White;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtConNumber_TextChanged(object sender, EventArgs e)
        {
            txtConNumber.BackColor = Color.White;
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            txtAge.BackColor = Color.White;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            /* string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
             if (Regex.IsMatch(txtEmail.Text,pattern))
             {
                 errorProvider1.Clear();
             }
             else
             {
                 errorProvider1.SetError(this.txtEmail, "Please provide a valid email");
                 return;
             }*/

        }

        private void txtConNumber_Leave(object sender, EventArgs e)
        {
            /* string pattern = "^((0[0-9]{1}){1})((([ ][0-9]{3}){1}){3})$";
             if (Regex.IsMatch(txtConNumber.Text, pattern))
             {
                 errorProvider2.Clear();
             }
             else
             {
                 errorProvider2.SetError(this.txtConNumber, "Please provide a valid Contact Number");
                 return;
             }*/
        }
        
        private void btnhome_Click(object sender, EventArgs e)
        {
            this.Hide();   //moving from one window to another
            MediCube_Doctor ss1 = new MediCube_Doctor();
            ss1.Show();
        }

        private void txtnm_TextChanged(object sender, EventArgs e)
        {
            txtnm.BackColor = Color.White;

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.White;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSpeciality.BackColor = Color.White;
        }

        private void dgvDoctors_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDoctors.CurrentRow.Index != -1)
            {
                DocID = Convert.ToInt32(dgvDoctors.CurrentRow.Cells[0].Value.ToString());
                txtnm.Text = dgvDoctors.CurrentRow.Cells[1].Value.ToString();
                txtAge.Text = dgvDoctors.CurrentRow.Cells[2].Value.ToString();
                txtConNumber.Text = dgvDoctors.CurrentRow.Cells[3].Value.ToString();
                txtEmail.Text = dgvDoctors.CurrentRow.Cells[4].Value.ToString();
                txtSpeciality.Text = dgvDoctors.CurrentRow.Cells[5].Value.ToString();
                btnInsert.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("DoctorDeletion", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@DocID", DocID);
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

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
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

        private void btnlogin_Click_1(object sender, EventArgs e)
        {
            this.Hide();   //moving from one window to another
            MediCube_Doctor ss1 = new MediCube_Doctor();
            ss1.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            this.Hide();   //moving from one window to another
          DocdetailsReport ss1 = new DocdetailsReport();
            ss1.Show();
        }

        private void txtAge_TextChanged_1(object sender, EventArgs e)
        {
            txtAge.BackColor = Color.White;
        }

        private void txtEmail_TextChanged_1(object sender, EventArgs e)
        {
            txtEmail.BackColor = Color.White;
        }

        private void txtSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSpeciality.BackColor = Color.White;
        }

        private void btnhome_Click_1(object sender, EventArgs e)
        {
            this.Hide();   //moving from one window to another
            MediCube_Doctor ss1 = new MediCube_Doctor();
            ss1.Show();
        }
    }
} 

        

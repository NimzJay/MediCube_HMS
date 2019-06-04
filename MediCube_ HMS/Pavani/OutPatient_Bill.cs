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
    public partial class OutPatient_Bill : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int PatientId = 0;
        public OutPatient_Bill()
        {
            InitializeComponent();
        }

        private void Insertbtn_Click(object sender, EventArgs e)
        {
            
            if (nameTxt.Text == "")
            {
                nameTxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameTxt.Focus();
                return;
            }
            if (dtmtxt.Text == "")
            {
                dtmtxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtmtxt.Focus();
                return;
            }
            if (ConNumtxt.Text == "")
            {
                ConNumtxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConNumtxt.Focus();
                return;
            }

            if (Addresstxt.Text == "")
            {
                Addresstxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Addresstxt.Focus();
                return;
            }
            if (Agetxt.Text == "")
            {
                Agetxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Agetxt.Focus();
                return;
            }
            if (HosChartxt.Text == "")
            {
                HosChartxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HosChartxt.Focus();
                return;
            }
            if (ProfChartxt.Text == "")
            {
                ProfChartxt.BackColor = Color.LightPink;
                MessageBox.Show("Name is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProfChartxt.Focus();
                return;
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (Insertbtn.Text == "Insert")
                {

                    SqlCommand sqlCmd = new SqlCommand("OutPBill", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@PatientId", 0);
                    sqlCmd.Parameters.AddWithValue("@Name", nameTxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Date", dtmtxt.Value.Date);
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", ConNumtxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", Addresstxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", Agetxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Hospital_Charge", HosChartxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@profs_Charge", ProfChartxt.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Data inserted successfully");
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
                    sqlCmd.Parameters.AddWithValue("@Name", nameTxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", ConNumtxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", Addresstxt.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", Agetxt.Text.Trim());
                    
                }
                Reset1();
                FillDataGridView1();


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
            SqlDataAdapter sqlDa = new SqlDataAdapter("OutPViewSearch1", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Name", Searchtxt.Text.Trim());
            DataTable db = new DataTable();
            sqlDa.Fill(db);
            dgvBills.DataSource = db;
            dgvBills.Columns[0].Visible = false;
            con.Close();
        }
        void Reset()
        {
            nameTxt.Text = Agetxt.Text = ConNumtxt.Text = Addresstxt.Text = dtmtxt.Text = HosChartxt.Text = ProfChartxt.Text = "";
            Insertbtn.Text = "Insert";
            PatientId = 0;

        }
        void Reset1()
        {
            nameTxt.Text = Agetxt.Text = ConNumtxt.Text = Addresstxt.Text = "";
            Insertbtn.Text = "Insert";
            PatientId = 0;
        }
        void FillDataGridView1()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("OutPViewSearch2", con);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Name", Searchtxt.Text.Trim());
            DataTable db = new DataTable();
            sqlDa.Fill(db);
            dgvPatient.DataSource = db;
            dgvPatient.Columns[0].Visible = false;
            con.Close();
        }

        private void dgvPatient_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPatient.CurrentRow.Index != -1)
            {
                PatientId = Convert.ToInt32(dgvPatient.CurrentRow.Cells[0].Value.ToString());
                nameTxt.Text = dgvPatient.CurrentRow.Cells[1].Value.ToString();
                Agetxt.Text = dgvPatient.CurrentRow.Cells[2].Value.ToString();
                ConNumtxt.Text = dgvPatient.CurrentRow.Cells[3].Value.ToString();
                Addresstxt.Text = dgvPatient.CurrentRow.Cells[4].Value.ToString();


            }
        }

        private void dgvBills_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBills.CurrentRow.Index != 1)
            {
                PatientId = Convert.ToInt32(dgvPatient.CurrentRow.Cells[0].Value.ToString());
                nameTxt.Text = dgvBills.CurrentRow.Cells[1].Value.ToString();
                Agetxt.Text = dgvBills.CurrentRow.Cells[2].Value.ToString();
                ConNumtxt.Text = dgvBills.CurrentRow.Cells[3].Value.ToString();
                Addresstxt.Text = dgvBills.CurrentRow.Cells[4].Value.ToString();
                dtmtxt.Text = dgvBills.CurrentRow.Cells[5].Value.ToString();
                DateTime y = DateTime.Parse(dtmtxt.Text);
                HosChartxt.Text = dgvBills.CurrentRow.Cells[6].Value.ToString();
                int x = Int32.Parse(HosChartxt.Text);
                ProfChartxt.Text = dgvBills.CurrentRow.Cells[7].Value.ToString();
                int z = Int32.Parse(ProfChartxt.Text);

            }
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView1();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            try
            {
                FillDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Out_Patient_Details ps = new Out_Patient_Details();
            ps.Show();
        }

        private void OutPatient_Bill_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            outPatienBillReport ss1 = new outPatienBillReport();
            ss1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Out_Patient ss1 = new MediCube_Out_Patient();
            ss1.Show();
        }



       
        }
    }


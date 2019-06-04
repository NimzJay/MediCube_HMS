using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace MediCube__HMS.Nimna
{
    public partial class P_staff : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public P_staff()
        {
            InitializeComponent();
        }

        void fillGridDataView()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("mainViewStaff", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvStaff.DataSource = dtbl;

            sqlcon.Close();
        }

        private void P_staff_Load(object sender, EventArgs e)
        {
            try
            {
                dgvStaff.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

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
            SqlDataAdapter sqlDa = new SqlDataAdapter("viewSearchStaff", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@fName", txtSearch.Text.Trim());
            DataTable dtbl2 = new DataTable();
            sqlDa.Fill(dtbl2);
            dgvStaff.DataSource = dtbl2;
            //dgvMed.Columns[0].Visible = false;

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
        }

        private void reload_Click(object sender, EventArgs e)
        {
            try
            {
                dgvStaff.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

                fillGridDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void btnInsertOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //check whether all the fields are filled by the user
                if (txtEmail.Text == "" || txtUN.Text == "" || txtPW.Text == "" || txtLName.Text == "" || txtFName.Text == "" || txtNIC.Text == "" || txtPhone.Text == "" || txtGender.Text == "" || txtSal.Text == "")
                {
                    MessageBox.Show("Please Fill all the fields!");
                }

                System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z]*[a-zA-Z]$");
                if (!rEmail.IsMatch(txtEmail.Text))
                {
                    MessageBox.Show("Invalid Email!");
                }

                /*System.Text.RegularExpressions.Regex rPhone = new System.Text.RegularExpressions.Regex(@"^[0-9]{10}$");
                if (!rEmail.IsMatch(txtPhone.Text))
                {
                    MessageBox.Show("Invalid Phone Number!");
                }*/

                //check whether the expiry date is valid
                else if (pickerDOB.Value >= DateTime.Today || pickerHire.Value > DateTime.Today)
                {
                    MessageBox.Show("Please Enter a valid Date!");
                    pickerDOB.Value = DateTime.Today;
                    pickerHire.Value = DateTime.Today;
                }
                //else
                //{
                //    if (sqlcon.State == ConnectionState.Closed)
                //        sqlcon.Open();

                //    String query = "select count(*) from Medicine where medName = '" + txtMedName + "' ";
                //    SqlCommand cmd = new SqlCommand(query, sqlcon);
                //    SqlDataReader rd;
                //    rd = cmd.ExecuteReader();
                //    while (rd.Read())
                //    {
                //        if (rd.HasRows == true)
                //        {
                //            MessageBox.Show("Medicine " + txtMedName.Text + " already Exists!");
                //            Reset();
                //        }
                else
                {
                    try
                    {
                        if (sqlcon.State == ConnectionState.Closed)
                        {
                            //insert records to the database
                            sqlcon.Open();
                            if (btnInsertOrUpdate.Text == "Insert")
                            {
                                SqlCommand sqlcmd = new SqlCommand("insertEditStaff", sqlcon);
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.Parameters.AddWithValue("@mode", "Insert");
                                sqlcmd.Parameters.AddWithValue("@empNo", 0);
                                sqlcmd.Parameters.AddWithValue("@firstName", txtFName.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@lastName", txtLName.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@username", txtUN.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@password", txtPW.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@gender", txtGender.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@dob", pickerDOB.Value);
                                sqlcmd.Parameters.AddWithValue("@salary", txtSal.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@job", "Pharmacist");
                                sqlcmd.Parameters.AddWithValue("@hireDate", pickerHire.Value);
                                sqlcmd.ExecuteNonQuery();
                                MessageBox.Show("Inserted Successfully!");
                                //Reset();
                            }
                            else
                            {
                                SqlCommand sqlcmd = new SqlCommand("insertEditStaff", sqlcon);
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                                sqlcmd.Parameters.AddWithValue("@empNo", txtEmp.Text);
                                sqlcmd.Parameters.AddWithValue("@firstName", txtFName.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@lastName", txtLName.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@username", txtUN.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@password", txtPW.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@gender", txtGender.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@dob", pickerDOB.Value);
                                sqlcmd.Parameters.AddWithValue("@salary", txtSal.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@job", "Pharmacist");
                                sqlcmd.Parameters.AddWithValue("@hireDate", pickerHire.Value);
                                sqlcmd.ExecuteNonQuery();
                                MessageBox.Show("Updated Successfully!");
                                btnInsertOrUpdate.Text = "Insert";
                                Reset();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Message");
                    }
                    finally
                    {
                        sqlcon.Close();
                    }
                }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {

            }
        }

        private void Reset()
        {
            //clears all the typed data      
            txtEmail.Text = txtEmp.Text = txtUN.Text = txtPW.Text = txtLName.Text = txtFName.Text = txtNIC.Text = txtPhone.Text = txtGender.Text = txtSal.Text = "";
            pickerDOB.Value = DateTime.Today;
            pickerHire.Value = DateTime.Today;
        }

        private void dgvStaff_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnInsertOrUpdate.Text = "Update";
                //load the selected row data to txt boxes
                DataGridViewRow row = this.dgvStaff.Rows[e.RowIndex];
                txtEmp.Text = row.Cells["empNo"].Value.ToString();
                txtFName.Text = row.Cells["firstName"].Value.ToString();
                txtLName.Text = row.Cells["lastName"].Value.ToString();
                txtUN.Text = row.Cells["username"].Value.ToString();
                txtPW.Text = row.Cells["password"].Value.ToString();
                txtNIC.Text = row.Cells["nic"].Value.ToString();
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtGender.Text = row.Cells["gender"].Value.ToString();
                pickerDOB.Text = row.Cells["dob"].Value.ToString();
                txtSal.Text = row.Cells["salary"].Value.ToString();
                //txtJob.Text = row.Cells["job"].Value.ToString();
                pickerHire.Text = row.Cells["hireDate"].Value.ToString();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete a specific record from the database
            String query = "delete from Employee where empNo='" + this.txtEmp.Text + "' ";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            SqlDataReader myReader;
            try
            {
                sqlcon.Open();
                myReader = sqlcmd.ExecuteReader();
                MessageBox.Show("Deleted Successfully!");
                Reset();
                while (myReader.Read()) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z]*[a-zA-Z]$");
            if (!rEMail.IsMatch(txtEmail.Text))
            {
                MessageBox.Show("Invalid email address!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.SelectAll();
                e.Cancel = true;
            }
        }




    }
}

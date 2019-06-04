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
    public partial class MediCube_Employee : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public MediCube_Employee()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                //check whether all the fields are filled by the user
                if (txtName.Text == "" || txtNIC.Text == "" || txtID.Text == "" || txtPhone.Text == "" || txtGen.Text == "" || txtAge.Text == "" || txtSal.Text == "" || txtEmail.Text == "" || txtAddress.Text == "" || txtSal.Text == "")
                {
                    MessageBox.Show("Please Fill all the fields!");
                }
                else
                {
                    try
                    {
                        if (sqlcon.State == ConnectionState.Closed)
                        {
                            //insert records to the database
                            sqlcon.Open();
                            if (btnInsert.Text == "Insert")
                            {
                                SqlCommand sqlcmd = new SqlCommand("insertEditEmployee", sqlcon);
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.Parameters.AddWithValue("@mode", "Add");
                                sqlcmd.Parameters.AddWithValue("@NIc", txtNIC.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@EmpId", txtID.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Gender", txtGen.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@ContactNumber", txtPhone.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@ddress", txtAddress.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@asalary", txtSal.Text.Trim());

                                sqlcmd.ExecuteNonQuery();
                                MessageBox.Show("Inserted Successfully!");
                                //Reset();
                            }
                            else
                            {
                                SqlCommand sqlcmd = new SqlCommand("insertEditEmployee", sqlcon);
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.Parameters.AddWithValue("@mode", "Edit");
                                sqlcmd.Parameters.AddWithValue("@NIc", txtNIC.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@EmpId", txtID.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@Gender", txtGen.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@ContactNumber", txtPhone.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@ddress", txtAddress.Text.Trim());
                                sqlcmd.Parameters.AddWithValue("@asalary", txtSal.Text.Trim());

                                sqlcmd.ExecuteNonQuery();
                                MessageBox.Show("Updated Successfully!");
                                btnInsert.Text = "Insert";
                                //Reset();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Message");
                    }
                    finally
                    {
                        fillGridDataView();
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
        void fillGridDataView()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa3 = new SqlDataAdapter("HosEmployeeView", sqlcon);
            sqlDa3.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl3 = new DataTable();
            sqlDa3.Fill(dtbl3);
            dgvEmp.DataSource = dtbl3;

            sqlcon.Close();
        }

        void FillDataGridView2()
        {

            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("HosEmployee_SearchView", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            sqlDa.SelectCommand.Parameters.AddWithValue("@nic", txtNIC.Text.Trim());
            DataTable dtbl2 = new DataTable();
            sqlDa.Fill(dtbl2);
            dgvEmp.DataSource = dtbl2;

            sqlcon.Close();

        }

        private void btndel_Click(object sender, EventArgs e)
        {
            //delete a specific record from the database
            String query = "delete from Hos_Employee where NIC='" + this.txtNIC.Text + "' ";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            SqlDataReader myReader;
            try
            {
                sqlcon.Open();
                myReader = sqlcmd.ExecuteReader();
                MessageBox.Show("Deleted Successfully!");
                //Reset();
                //while (myReader.Read()) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                fillGridDataView();
                sqlcon.Close();
                
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        void Reset()
        {
            txtName.Text = txtNIC.Text = txtAddress.Text = txtAge.Text = txtEmail.Text = txtGen.Text = txtID.Text = txtPhone.Text = txtSal.Text = "";
            btnInsert.Text = "Submit";
            //Id = 0;
            btnReset.Enabled = false;
        }

        private void dgvEmp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnInsert.Text = "Update";
                //load the selected row data to txt boxes
                DataGridViewRow row = this.dgvEmp.Rows[e.RowIndex];

                txtNIC.Text = row.Cells["NIC"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtID.Text = row.Cells["EmpID"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();
                txtGen.Text = row.Cells["Gender"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["ContactNumber"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtSal.Text = row.Cells["salary"].Value.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            fillGridDataView();
        }

        private void MediCube_Employee_Load(object sender, EventArgs e)
        {
            fillGridDataView();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }
    }

}

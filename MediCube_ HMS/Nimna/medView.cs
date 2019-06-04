using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MediCube__HMS.Nimna
{
    public partial class medView : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public medView()
        {
            InitializeComponent();
        }

        void fillGridDataView()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("mainViewMed", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvMed.DataSource = dtbl;

            sqlcon.Close();
        }

        private void medView_Load(object sender, EventArgs e)
        {
            try
            {
                dgvMed.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

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
            SqlDataAdapter sqlDa = new SqlDataAdapter("viewSearchMed", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@medName", txtSearch.Text.Trim());
            DataTable dtbl2 = new DataTable();
            sqlDa.Fill(dtbl2);
            dgvMed.DataSource = dtbl2;
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
                dgvMed.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

                fillGridDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void dgvMed_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //load the selected row data to txt boxes
                DataGridViewRow row = this.dgvMed.Rows[e.RowIndex];
                txtMedId.Text = row.Cells["medID"].Value.ToString();
                txtMedName.Text = row.Cells["medName"].Value.ToString();
                txtMedDesc.Text = row.Cells["medDesc"].Value.ToString();
                txtMedBrand.Text = row.Cells["brand"].Value.ToString();
                pickerMedExp.Text = row.Cells["expDate"].Value.ToString();
                txtMedQty.Text = row.Cells["quantity"].Value.ToString();
                txtMedPrice.Text = row.Cells["price"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check whether all the fields are filled by the user
            if (txtMedName.Text == "" || txtMedDesc.Text == "" || txtMedBrand.Text == "" || pickerMedExp.Value == DateTime.Today || txtMedQty.Text == "" || txtMedPrice.Text == "")
            {
                MessageBox.Show("Please Fill all the fields!");
            }

            //check whether the expiry date is valid
            else if (pickerMedExp.Value < DateTime.Today)
            {
                MessageBox.Show("Please Enter a valid Date!");
                pickerMedExp.Value = DateTime.Today;
            }
            else
            {
                //update a specific record in the database
                String query = "update Medicine set medName='" + this.txtMedName.Text + "', medDesc='" + this.txtMedDesc.Text + "', brand='" + this.txtMedBrand.Text + "', expDate='" + this.pickerMedExp.Value + "', quantity='" + this.txtMedQty.Text + "', price='" + this.txtMedPrice.Text + "' where medID='" + this.txtMedId.Text + "' ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                SqlDataReader myReader;
                try
                {
                    sqlcon.Open();
                    myReader = sqlcmd.ExecuteReader();
                    MessageBox.Show("Updated Successfully!");
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
        }

        private void Reset()
        {
            //clears all the typed data
            txtMedId.Text = txtMedName.Text = txtMedDesc.Text = txtMedBrand.Text = txtMedQty.Text = txtMedPrice.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete a specific record from the database
            String query = "delete from Medicine where medID='" + this.txtMedId.Text + "' ";
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

        private void txtMedPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check whether the input contains only numbers
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                MessageBox.Show("Only numbers allowed!");
                e.Handled = true;
            }
        }

        private void txtMedQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check whether the input contains only numbers
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                MessageBox.Show("Only numbers allowed!");
                e.Handled = true;
            }
        }



    }
}

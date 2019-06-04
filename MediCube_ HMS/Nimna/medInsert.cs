using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MediCube__HMS
{
    public partial class medInsert : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public medInsert()
        {
            InitializeComponent();
        }

        private void medInsert_Load(object sender, EventArgs e)
        {
            
        }

        private void Reset()
        {
            //clear all the typed data
            txtMedName.Text = txtMedDesc.Text = txtMedBrand.Text = txtMedQty.Text = txtMedPrice.Text = "";
            pickerMedExp.Value = DateTime.Today;
            btnReset.Enabled = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //check whether all the fields are filled by the user
            if (txtMedName.Text == "" || txtMedDesc.Text == "" || txtMedBrand.Text == "" || pickerMedExp.Value == DateTime.Today || txtMedQty.Text == "" || txtMedPrice.Text == "")
            {
                MessageBox.Show("Please Fill all the fields!");
            }

            //check whether the expiry date is valid
            else if (pickerMedExp.Value <= DateTime.Today)
            {
                MessageBox.Show("Please Enter a valid Date!");
                pickerMedExp.Value = DateTime.Today;
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
                        SqlCommand sqlcmd = new SqlCommand("insertEditMed", sqlcon);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@mode", "Add");
                        sqlcmd.Parameters.AddWithValue("@medID", 0);
                        sqlcmd.Parameters.AddWithValue("@medName", txtMedName.Text.Trim());
                        sqlcmd.Parameters.AddWithValue("@medDesc", txtMedDesc.Text.Trim());
                        sqlcmd.Parameters.AddWithValue("@brand", txtMedBrand.Text.Trim());
                        sqlcmd.Parameters.AddWithValue("@expDate", pickerMedExp.Value);
                        sqlcmd.Parameters.AddWithValue("@quantity", txtMedQty.Text.Trim());
                        sqlcmd.Parameters.AddWithValue("@price", txtMedPrice.Text.Trim());
                        sqlcmd.ExecuteNonQuery();
                        MessageBox.Show("Saved Successfully!");
                        Reset();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
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

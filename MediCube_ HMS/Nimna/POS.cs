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
    public partial class POS : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public POS()
        {
            InitializeComponent();
            fill_listBox();
        }

        DataTable transDT = new DataTable();

        void fill_listBox()
        {
            String query = "select * from Medicine";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            SqlDataReader myReader;
            try
            {
                sqlcon.Open();
                myReader = sqlcmd.ExecuteReader();

                while (myReader.Read())
                {
                    string medname = myReader["medname"].ToString();
                    med_list.Items.Add(medname);

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

        void fillGridDataView()
        {
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("mainViewInv", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvInv.DataSource = dtbl;

            sqlcon.Close();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            //Specify columns for the Data Table
            transDT.Columns.Add("Medicine Name");
            transDT.Columns.Add("Unit Price");
            transDT.Columns.Add("Quantity");
            transDT.Columns.Add("Total");
        }

        private void med_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select * from Medicine where medName = '" + med_list.Text + "'";
            SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
            SqlDataReader myReader;
            try
            {
                sqlcon.Open();
                myReader = sqlcmd.ExecuteReader();

                while (myReader.Read())
                {
                    string medname = myReader["medname"].ToString();
                    string desc = myReader["medDesc"].ToString();
                    string brand = myReader["brand"].ToString();
                    string exp = myReader["expDate"].ToString();
                    string price = myReader["price"].ToString();
                    string stock = myReader["quantity"].ToString();

                    txtMedName.Text = medname;
                    txtDesc.Text = desc;
                    txtMedBrand.Text = brand;
                    pickerMedExp.Text = exp;
                    txtMedPrice.Text = price;
                    txtStock.Text = stock;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    //insert records to the database
                    sqlcon.Open();
                    SqlCommand sqlcm = new SqlCommand("updateInventory", sqlcon);
                    sqlcm.CommandType = CommandType.StoredProcedure;
                    sqlcm.Parameters.AddWithValue("@medName", txtMedName.Text);
                    sqlcm.Parameters.AddWithValue("@deduct", textQty.Text);
                    sqlcm.ExecuteNonQuery();
                    MessageBox.Show("Update Inventory Stock!");
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

            String medName = txtMedName.Text;
            int Rate = int.Parse(txtMedPrice.Text);
            int Qty = int.Parse(textQty.Text);
            double total = Rate * qty;

            //Add iems to data grid view
            transDT.Rows.Add(medName, Rate, Qty, total);

            //show data
            dgvInv.DataSource = transDT;

            Reset();

            grand = grand + total;
            txtGross.Text = grand.ToString("#0.00");
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (textCus.Text == "")
            {
                MessageBox.Show("Please Fill all the fields!");
            }

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    //insert records to the database
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand("insertBill", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@BillNo", 0);
                    sqlcmd.Parameters.AddWithValue("@idate", DateTime.Today);
                    sqlcmd.Parameters.AddWithValue("@sub", txtGross.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@disc", txtDisc.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@grand", txtGross.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@paid", textPaid.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@balance", txtReturn.Text.Trim());
                    sqlcmd.ExecuteNonQuery();
                    //object o = sqlcmd.ExecuteScalar();
                    MessageBox.Show("Bill Generated!");
                    //Reset();
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

        private void Reset()
        {
            //clear all the typed data
            txtMedName.Text = txtDesc.Text = txtMedBrand.Text = textQty.Text = txtMedPrice.Text = txtStock.Text = "";
            //pickerMedExp.Value = DateTime.Today;
        }

        private void textQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check whether the input contains only numbers
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                MessageBox.Show("Only numbers allowed!");
                e.Handled = true;
            }
        }

        double price, qty, disc = 0;
        double tot;
        double grand = 0, paid, change;

        private void textQty_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(txtMedPrice.Text, out price);
            if (!double.TryParse(textQty.Text, out qty))
            {
                txtSubTot.Text = "0.00";
                //txtGross.Text = "0.00";
                //textQty.BackColor = Color.Pink; //indicates wrong input
            }
            else
            {
                tot = (price * qty);
                txtSubTot.Text = tot.ToString("#0.00");
                //txtGross.Text = tot.ToString("#0.00");

            }
        }

        private void txtDisc_TextChanged(object sender, EventArgs e)
        {
            //double.TryParse(txtDisc.Text, out disc);
            if (!double.TryParse(txtDisc.Text, out disc))
            {
                //txtSubTot.Text = "0";
                txtGross.Text = grand.ToString("#0.00"); ;
                //txtDisc.BackColor = Color.Pink; //indicates wrong input
            }
            else
            {
                grand = grand - (grand * (disc / 100));
                //txtSubTot.Text = tot.ToString("#0.00");
                txtGross.Text = grand.ToString("#0.00");

            }
        }

        private void textPaid_TextChanged(object sender, EventArgs e)
        {
            double gross;
            if (!double.TryParse(textPaid.Text, out paid))
            {
                textPaid.Text = "";
                txtReturn.Text = "0";
                //txtGross.Text = txtSubTot.Text;
                textQty.BackColor = Color.Pink; //indicates wrong input
            }
            else
            {
                double.TryParse(txtGross.Text, out gross);
                change = paid - gross;
                txtReturn.Text = change.ToString("#0.00");
            }
        }

        private void txtDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check whether the input contains only numbers
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                MessageBox.Show("Only numbers allowed!");
                e.Handled = true;
            }
        }

        private void textPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check whether the input contains only numbers
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                MessageBox.Show("Only numbers allowed!");
                e.Handled = true;
            }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (sqlcon.State == ConnectionState.Closed)
                    {
                        //insert records to the database
                        sqlcon.Open();
                        SqlCommand sqlcm = new SqlCommand("updateInventory", sqlcon);
                        sqlcm.CommandType = CommandType.StoredProcedure;
                        sqlcm.Parameters.AddWithValue("@medName", txtMedName.Text);
                        sqlcm.Parameters.AddWithValue("@deduct", textQty.Text);
                        sqlcm.ExecuteNonQuery();
                        MessageBox.Show("Update Inventory Stock!");
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

                String medName = txtMedName.Text;
                int Rate = int.Parse(txtMedPrice.Text);
                int Qty = int.Parse(textQty.Text);
                double total = Rate * qty;

                //Add iems to data grid view
                transDT.Rows.Add(medName, Rate, Qty, total);

                //show data
                dgvInv.DataSource = transDT;

                Reset();

                grand = grand + total;
                txtGross.Text = grand.ToString("#0.00");
            }
        }      
    }
}

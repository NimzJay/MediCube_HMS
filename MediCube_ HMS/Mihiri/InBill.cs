using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace MediCube__HMS.Mihiri
{
    public partial class InBill : UserControl
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public InBill()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        void FillDataGridView()
        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("VieworSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            //sqlDa.SelectCommand.Parameters.AddWithValue("@Name", textBox12.Text.Trim());
            DataTable dtbl2 = new DataTable();
            sqlDa.Fill(dtbl2);
            dgvPat.DataSource = dtbl2;

            sqlCon.Close();

        }

        void FillDataGridView2()
        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("VieworSearch2", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Name", textBox12.Text.Trim());
            sqlDa.SelectCommand.Parameters.AddWithValue("@nic", textBox12.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgvPat.DataSource = dtbl;

            sqlCon.Close();

        }



        void FillDataGridView_bill()
        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa2 = new SqlDataAdapter("viewBill", sqlCon);
            sqlDa2.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl2 = new DataTable();
            sqlDa2.Fill(dtbl2);
            dgvBill.DataSource = dtbl2;

            sqlCon.Close();

        }

        private void InBill_Load(object sender, EventArgs e)
        {
            FillDataGridView();
            FillDataGridView_bill();
        }

        private void button2_Click(object sender, EventArgs e)
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
            try
            {
                dgvPat.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox11.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show("Some fields are EMPTY", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }
            dt1.Value = DateTime.Today;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd1 = new SqlCommand("inbillmiAddorEdit", sqlCon);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Parameters.AddWithValue("@mode", "Add");
                sqlCmd1.Parameters.AddWithValue("@BillNo", 0);
                sqlCmd1.Parameters.AddWithValue("@NIC", textBox2.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@Name", textBox3.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@AdmittedDate", textBox4.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@DischargeDate", dt1.Value);
                sqlCmd1.Parameters.AddWithValue("@RoomType", textBox6.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@RoomCharges", textBox7.Text.Trim());//Trim used to remove extra space from left and right side
                sqlCmd1.Parameters.AddWithValue("@FoodCharges", textBox8.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@LaboratoryCharges", textBox9.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@DoctorCharges", textBox11.Text.Trim());
                sqlCmd1.Parameters.AddWithValue("@PayableAmount", textBox10.Text.Trim());
                sqlCmd1.ExecuteNonQuery();
                MessageBox.Show("The Bill Created!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                FillDataGridView_bill();
                sqlCon.Close();
            }
        }

        void Reset()
        {
            textBox2.Text = textBox3.Text = textBox4.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox11.Text = textBox10.Text = "";
            button3.Text = "ADD";


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reset();
        }

        float room, food, lab, doc, tot;

        private void dgvPat_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPat.CurrentRow.Index != -1)
            {
                textBox2.Text = dgvPat.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dgvPat.CurrentRow.Cells["Name"].Value.ToString();
                textBox4.Text = dgvPat.CurrentRow.Cells[8].Value.ToString();
                textBox6.Text = dgvPat.CurrentRow.Cells[9].Value.ToString();

            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox11.Text, out doc))
            {
                MessageBox.Show("Enter a value.");
                textBox10.Text = "0.00";
            }
            else
            {
                room = float.Parse(textBox7.Text);
                food = float.Parse(textBox8.Text);
                lab = float.Parse(textBox9.Text);
                doc = float.Parse(textBox11.Text);
                tot = room + food + lab + doc;
                textBox10.Text = tot.ToString();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}

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

namespace MediCube__HMS.Mihiri
{
    public partial class Details : UserControl
    {
        String gender;
        //Add connection to the Application
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int Id = 0;
        public Details()
        {
            InitializeComponent();
        }

        void fillCombo()
        {
            string query = "select * from tbl_room where status = 'available' and roomType = '" + cmbRoom.Text + "'";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataReader myReader;
            try
            {
                sqlCon.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string rType = myReader["roomType"].ToString();
                    cmbRoom.Items.Add(rType);

                }
                myReader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Details_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand("Deletion", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@ID", Id);


                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
                Reset();
                FillDataGridView();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // to check weather the some text boxes are empty
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || dateTimePicker1.Text == "" || cmbRoom.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Some fields are EMPTY", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }


            try
            {
                if (sqlCon.State == ConnectionState.Closed) //Checking the connection
                    sqlCon.Open();

                if (button1.Text == "Submit")
                {
                    if (dateTimePicker1.Value < DateTime.Today)
                    {
                        MessageBox.Show("Please Enter a valid Date!");
                        dateTimePicker1.Value = DateTime.Today;
                    }

                    SqlCommand sqlCmd = new SqlCommand("SubmitorUpdate", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@ID", 0);
                    sqlCmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", textBox2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@NIC", textBox3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", textBox4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@email", textBox5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact", textBox6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@GuardianContact", textBox7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CurrentDate", dateTimePicker1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@RoomType", cmbRoom.Text.Trim());
                    //sqlCmd.Parameters.AddWithValue("@RoomNo", comboBox3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gender", comboBox2.Text.Trim());

                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Saved!");
                }
                else
                {

                    dateTimePicker1.Enabled = false;
                    SqlCommand sqlCmd = new SqlCommand("SubmitorUpdate", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@ID", Id);
                    sqlCmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Age", textBox2.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@NIC", textBox3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", textBox4.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@email", textBox5.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Contact", textBox6.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@GuardianContact", textBox7.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@CurrentDate", dateTimePicker1.Text.Trim());    //Trim helps to remove the unwanted space.
                    sqlCmd.Parameters.AddWithValue("@RoomTYpe", cmbRoom.Text.Trim());
                    //sqlCmd.Parameters.AddWithValue("@RoomNo", comboBox3.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gender", comboBox2.Text.Trim());

                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated!");
                }
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
            SqlDataAdapter sqlDa = new SqlDataAdapter("VieworSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            //sqlDa.SelectCommand.Parameters.AddWithValue("@Name", textBox8.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dataGridView1.DataSource = dtbl;

            sqlCon.Close();

        }

        void FillDataGridView2()
        {

            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("VieworSearch2", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Name", textBox8.Text.Trim());
            sqlDa.SelectCommand.Parameters.AddWithValue("@nic", textBox8.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dataGridView1.DataSource = dtbl;

            sqlCon.Close();

        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                cmbRoom.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                //comboBox3.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();

                button1.Text = "Update";
                button3.Enabled = true;
            }

        }

        void Reset()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = dateTimePicker1.Text = cmbRoom.Text = comboBox2.Text="";
            button1.Text = "Submit";
            Id = 0;
            button3.Enabled = false;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //validating text box only to have numbers.
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)  //Key enumaration for backspace is 8 in c#
            //In here we only can add numbers to this text box. 
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)  //Key enumaration for backspace is 8 in c#
            //In here we only can add numbers to this text box. 
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Validating text box only to have letters.
            try
            {
                if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar == 8) || (e.KeyChar == 32))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                    MessageBox.Show("Enter Letters ONLY!");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z]*[a-zA-Z]$");
            if (!rEMail.IsMatch(textBox5.Text))
            {
                MessageBox.Show("Invalid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.SelectAll();
                e.Cancel = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow digit + char + white space
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
            //else {
            //MessageBox.Show("Enter lettrs and numbers only");//This will show us a message box either we enter an number also
            //}
        }



    }
}
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
    public partial class stockTheare : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        int EquipmentsId = 0;
        public stockTheare()
        {
            InitializeComponent();
        }

        private void stockTheare_Load(object sender, EventArgs e)
        {
            Reset();
            DataView();
        }

        private void insert_Click(object sender, EventArgs e)
        {
            if (thName.Text == "")
            {
                MessageBox.Show("Name validation error");
                return;
            }
            if (theSto.Text == "")
            {
                MessageBox.Show("Store box validation error");
                return;
            }

            if (theCat.Text == "")
            {
                MessageBox.Show("Category validation error");
                return;
            }
            if (theQua.Text == "")
            {
                MessageBox.Show("Quantity validation error");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (insert.Text == "Insert")
                {

                    {
                        SqlCommand cmd = new SqlCommand("stockTheare", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mode", "Add");
                        cmd.Parameters.AddWithValue("@EquipmentsId ", 0);
                        cmd.Parameters.AddWithValue("@Name", thName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", theCat.Text.Trim());
                        cmd.Parameters.AddWithValue("@Store_Box", theSto.Text.Trim());
                        cmd.Parameters.AddWithValue("@Quantity", theQua.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Insert successfully");
                    }
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("stockTheare", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mode", "Edit");
                    cmd.Parameters.AddWithValue("@EquipmentsId ", EquipmentsId);
                    cmd.Parameters.AddWithValue("@Name", thName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", theCat.Text.Trim());
                    cmd.Parameters.AddWithValue("@Store_Box", theSto.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", theQua.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update successfully");

                }
                Reset();
                DataView();
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
        void DataView()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataAdapter sqlData = new SqlDataAdapter("stockTheareSearch", con);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@searchName", theSearch.Text.Trim());
            DataTable dt1 = new DataTable();
            sqlData.Fill(dt1);
            dataGridView1.DataSource = dt1;
            con.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }
        void Reset()
        {
            thName.Text = theCat.Text = theQua.Text = theSto.Text = "";
            insert.Text = "Insert";
            EquipmentsId = 0;
            delete.Enabled = false;

        }

        private void reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                EquipmentsId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                thName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                theSto.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                theCat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                theQua.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                int x = Int32.Parse(theQua.Text);
                insert.Text = "Update";
                delete.Enabled = true;

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("stockTheareDel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EquipmentsId ", EquipmentsId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete successfully");
                Reset();
                DataView();

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MediCube_Stock ss1 = new MediCube_Stock();
            ss1.Show();
        }

    }
}

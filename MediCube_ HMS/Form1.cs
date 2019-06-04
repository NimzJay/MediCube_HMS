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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (userText.Text == "")
            {
                userText.BackColor = Color.LightPink;
                MessageBox.Show("UserName is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userText.Focus();
                return;
            }
            if (passText.Text == "")
            {
                passText.BackColor = Color.LightPink;
                MessageBox.Show("Password is required", "validation error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userText.Focus();
                return;
            }
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            SqlDataAdapter ds = new SqlDataAdapter("Select Count(*) From Login where UserName ='" + userText.Text + "' and Password = '" + passText.Text + "'", con);
            DataTable dt = new DataTable();
            ds.Fill(dt);
           if ((userText.Text == "pavani") && (passText.Text == "pavani"))
            {
                this.Hide();
                MediCube_Out_Patient ps = new MediCube_Out_Patient();
                ps.Show();
            }
           else if((userText.Text=="daki")&&(passText.Text=="daki"))
            {
                this.Hide();
                MediCube_Stock ps = new MediCube_Stock();
                ps.Show();
            }
            else if ((userText.Text == "nim") && (passText.Text == "nim"))
            {
                this.Hide();
                MediCube_Pharmacy ps = new MediCube_Pharmacy();
                ps.Show();
            }
            else if ((userText.Text == "binura") && (passText.Text == "binura"))
            {
                this.Hide();
                MediCube_Lab ps = new MediCube_Lab();
                ps.Show();
            }
            else if ((userText.Text == "lale") && (passText.Text == "lale"))
            {
                this.Hide();
                MediCube_Doctor ps = new MediCube_Doctor();
                ps.Show();
            }
            else if ((userText.Text == "mihi") && (passText.Text == "mihi"))
            {
                this.Hide();
                MediCube_In_Patient ps = new MediCube_In_Patient();
                ps.Show();
            }
           else if ((userText.Text == "shamalki") && (passText.Text == "shamalki"))
            {
                this.Hide();
                MediCube_Employee ps = new MediCube_Employee();
                ps.Show();
            }
            else
            {
                MessageBox.Show("Please Check your username or password");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        }
    }


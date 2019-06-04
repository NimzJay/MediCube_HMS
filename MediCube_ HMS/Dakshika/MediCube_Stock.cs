using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MediCube__HMS
{
    public partial class MediCube_Stock : Form
    {
        public MediCube_Stock()
        {
            InitializeComponent();
        }

        private void pharmacy_Click(object sender, EventArgs e)
        {
            this.Hide();
            stockPharmacy ss1 = new stockPharmacy();
            ss1.Show();
        }

        private void lab_Click(object sender, EventArgs e)
        {
            this.Hide();
            stockLaboratory ss1 = new stockLaboratory();
            ss1.Show();
        }

        private void threatre_Click(object sender, EventArgs e)
        {
            this.Hide();
            stockTheare ss1 = new stockTheare();
            ss1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logout_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
           stockReports ss1 = new stockReports();
            ss1.Show();
        }
    }
}

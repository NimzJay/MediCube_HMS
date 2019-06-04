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
    public partial class MediCube_Out_Patient : Form
    {
        public MediCube_Out_Patient()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Out_Patient_Details ps = new Out_Patient_Details();
            ps.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            outPatienBillReport ps = new outPatienBillReport();
            ps.Show();
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            OutPatient_Bill ps = new OutPatient_Bill();
            ps.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ps = new Form1();
            ps.Show();
        }
    }
}

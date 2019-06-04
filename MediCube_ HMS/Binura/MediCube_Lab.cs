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
    public partial class MediCube_Lab : Form
    {
        public MediCube_Lab()
        {
            InitializeComponent();
            panel3.Height = button1.Height;
            panel3.Top = button1.Top;            
            dashboard1.BringToFront();
        }

        private void MediCube_Lab_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Height = button1.Height;
            panel3.Top = button1.Top;  
            dashboard1.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Height = button2.Height;
            panel3.Top = button2.Top;          
            stock1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Height = button4.Height;
            panel3.Top = button4.Top;
            reports1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Height = button5.Height;
            panel3.Top = button5.Top;
            test1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Height = button3.Height;
            panel3.Top = button3.Top;
            lab_Patient1.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel3.Height = button7.Height;
            panel3.Top = button7.Top;
            summary1.BringToFront();
        }

        private void summary1_Load(object sender, EventArgs e)
        {

        }
    }
}

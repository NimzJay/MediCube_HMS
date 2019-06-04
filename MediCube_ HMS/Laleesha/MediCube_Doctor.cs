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
    public partial class MediCube_Doctor : Form
    {
        public MediCube_Doctor()
        {
            InitializeComponent();
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            this.Hide();
            Doc_Details ps = new Doc_Details ();
            ps.Show();
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ps = new Form1();
            ps.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            this.Hide();   //moving from one window to another
            DocdetailsReport ss1 = new DocdetailsReport();
            ss1.Show();
        }
    }
}

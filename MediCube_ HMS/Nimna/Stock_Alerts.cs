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
    public partial class Stock_Alerts : UserControl
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hp\Desktop\MediCube_ HMS\DB\MediCube_DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        public Stock_Alerts()
        {
            InitializeComponent();
            fillGridDataView2();
        }

        void fillGridDataView2()
        {
            //load the set of records for the user entered parameters.
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("alertStock", sqlcon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl2 = new DataTable();
            sqlDa.Fill(dtbl2);
            dgvAlert.DataSource = dtbl2;

            sqlcon.Close();
        }

        private void Stock_Alerts_Load(object sender, EventArgs e)
        {
            fillGridDataView2();
        }

        private void reload_Click(object sender, EventArgs e)
        {
            fillGridDataView2();
        }
    }
}

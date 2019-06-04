namespace MediCube__HMS
{
    partial class DocdetailsReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.docReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.doctxt = new System.Windows.Forms.TextBox();
            this.docbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // docReport
            // 
            this.docReport.ActiveViewIndex = -1;
            this.docReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.docReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.docReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.docReport.Location = new System.Drawing.Point(0, 0);
            this.docReport.Name = "docReport";
            this.docReport.Size = new System.Drawing.Size(1084, 528);
            this.docReport.TabIndex = 0;
            this.docReport.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // doctxt
            // 
            this.doctxt.Location = new System.Drawing.Point(641, 12);
            this.doctxt.Name = "doctxt";
            this.doctxt.Size = new System.Drawing.Size(100, 22);
            this.doctxt.TabIndex = 1;
            // 
            // docbtn
            // 
            this.docbtn.Location = new System.Drawing.Point(783, 10);
            this.docbtn.Name = "docbtn";
            this.docbtn.Size = new System.Drawing.Size(75, 23);
            this.docbtn.TabIndex = 2;
            this.docbtn.Text = "SHOW";
            this.docbtn.UseVisualStyleBackColor = true;
            this.docbtn.Click += new System.EventHandler(this.docbtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(997, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = " BACK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DocdetailsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 528);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.docbtn);
            this.Controls.Add(this.doctxt);
            this.Controls.Add(this.docReport);
            this.Name = "DocdetailsReport";
            this.Text = "DocdetailsReport";
            this.Load += new System.EventHandler(this.DocdetailsReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer docReport;
        private System.Windows.Forms.TextBox doctxt;
        private System.Windows.Forms.Button docbtn;
        private System.Windows.Forms.Button button1;
    }
}
namespace MediCube__HMS
{
    partial class outPatienBillReport
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
            this.OutpatienBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.Otxt = new System.Windows.Forms.TextBox();
            this.obtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutpatienBill
            // 
            this.OutpatienBill.ActiveViewIndex = -1;
            this.OutpatienBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutpatienBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.OutpatienBill.Location = new System.Drawing.Point(307, 12);
            this.OutpatienBill.Name = "OutpatienBill";
            this.OutpatienBill.Size = new System.Drawing.Size(768, 390);
            this.OutpatienBill.TabIndex = 76;
            this.OutpatienBill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.OutpatienBill.Load += new System.EventHandler(this.OutpatienBill_Load);
            // 
            // Otxt
            // 
            this.Otxt.Location = new System.Drawing.Point(782, 21);
            this.Otxt.Name = "Otxt";
            this.Otxt.Size = new System.Drawing.Size(100, 22);
            this.Otxt.TabIndex = 77;
            // 
            // obtn
            // 
            this.obtn.Location = new System.Drawing.Point(898, 21);
            this.obtn.Name = "obtn";
            this.obtn.Size = new System.Drawing.Size(75, 23);
            this.obtn.TabIndex = 78;
            this.obtn.Text = "SHOW";
            this.obtn.UseVisualStyleBackColor = true;
            this.obtn.Click += new System.EventHandler(this.obtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(986, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 79;
            this.button1.Text = "BACK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // outPatienBillReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 414);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.obtn);
            this.Controls.Add(this.Otxt);
            this.Controls.Add(this.OutpatienBill);
            this.Name = "outPatienBillReport";
            this.Text = "outPatienBillReport";
            this.Load += new System.EventHandler(this.outPatienBillReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer OutpatienBill;
        private System.Windows.Forms.TextBox Otxt;
        private System.Windows.Forms.Button obtn;
        private System.Windows.Forms.Button button1;
    }
}
namespace Login
{
    partial class SalesReportWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesReportWindow));
            this.rctBoxReport = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rctBoxReport
            // 
            this.rctBoxReport.BackColor = System.Drawing.Color.White;
            this.rctBoxReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rctBoxReport.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rctBoxReport.Location = new System.Drawing.Point(0, 0);
            this.rctBoxReport.Name = "rctBoxReport";
            this.rctBoxReport.ReadOnly = true;
            this.rctBoxReport.Size = new System.Drawing.Size(607, 461);
            this.rctBoxReport.TabIndex = 0;
            this.rctBoxReport.Text = "";
            // 
            // SalesReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 461);
            this.Controls.Add(this.rctBoxReport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(623, 1000);
            this.MinimumSize = new System.Drawing.Size(623, 39);
            this.Name = "SalesReportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sales Report";
            this.Load += new System.EventHandler(this.SalesReportWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rctBoxReport;


    }
}
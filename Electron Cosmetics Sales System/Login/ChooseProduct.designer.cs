namespace Login
{
    partial class ChooseProductForm
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
            this.pricelbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ProdID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProductName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContactNotxt = new System.Windows.Forms.TextBox();
            this.CNumberlbl = new System.Windows.Forms.Label();
            this.CustomerTypetxt = new System.Windows.Forms.TextBox();
            this.CustomerNametxt = new System.Windows.Forms.TextBox();
            this.CustomerIDtxt = new System.Windows.Forms.TextBox();
            this.CompanycomboBox = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.CTypelbl = new System.Windows.Forms.Label();
            this.CNamelbl = new System.Windows.Forms.Label();
            this.CompNamelbl = new System.Windows.Forms.Label();
            this.CIDlbl = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.ProductIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.estidatelbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.paymethodComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.DeleteCartItemsbtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // pricelbl
            // 
            this.pricelbl.AutoSize = true;
            this.pricelbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pricelbl.Location = new System.Drawing.Point(589, 529);
            this.pricelbl.Name = "pricelbl";
            this.pricelbl.Size = new System.Drawing.Size(0, 16);
            this.pricelbl.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(514, 529);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 16);
            this.label3.TabIndex = 47;
            this.label3.Text = "Total Price :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 25);
            this.label1.TabIndex = 43;
            this.label1.Text = "Cart ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(344, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 25);
            this.label2.TabIndex = 42;
            this.label2.Text = "Available Products";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProdID,
            this.ProductName,
            this.Price,
            this.Quantity});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(349, 62);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(374, 197);
            this.listView1.TabIndex = 40;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ProdID
            // 
            this.ProdID.Text = "Product ID";
            this.ProdID.Width = 70;
            // 
            // ProductName
            // 
            this.ProductName.Text = "Name";
            this.ProductName.Width = 170;
            // 
            // Price
            // 
            this.Price.Text = "Price (RM)";
            this.Price.Width = 70;
            // 
            // Quantity
            // 
            this.Quantity.Text = "Quantity";
            // 
            // ContactNotxt
            // 
            this.ContactNotxt.Enabled = false;
            this.ContactNotxt.Location = new System.Drawing.Point(168, 243);
            this.ContactNotxt.Name = "ContactNotxt";
            this.ContactNotxt.Size = new System.Drawing.Size(121, 20);
            this.ContactNotxt.TabIndex = 59;
            // 
            // CNumberlbl
            // 
            this.CNumberlbl.AutoSize = true;
            this.CNumberlbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNumberlbl.Location = new System.Drawing.Point(52, 246);
            this.CNumberlbl.Name = "CNumberlbl";
            this.CNumberlbl.Size = new System.Drawing.Size(103, 16);
            this.CNumberlbl.TabIndex = 58;
            this.CNumberlbl.Text = "Contact Number:";
            // 
            // CustomerTypetxt
            // 
            this.CustomerTypetxt.Enabled = false;
            this.CustomerTypetxt.Location = new System.Drawing.Point(168, 206);
            this.CustomerTypetxt.Name = "CustomerTypetxt";
            this.CustomerTypetxt.Size = new System.Drawing.Size(121, 20);
            this.CustomerTypetxt.TabIndex = 57;
            // 
            // CustomerNametxt
            // 
            this.CustomerNametxt.Enabled = false;
            this.CustomerNametxt.Location = new System.Drawing.Point(168, 166);
            this.CustomerNametxt.Name = "CustomerNametxt";
            this.CustomerNametxt.Size = new System.Drawing.Size(121, 20);
            this.CustomerNametxt.TabIndex = 56;
            // 
            // CustomerIDtxt
            // 
            this.CustomerIDtxt.Enabled = false;
            this.CustomerIDtxt.Location = new System.Drawing.Point(168, 124);
            this.CustomerIDtxt.Name = "CustomerIDtxt";
            this.CustomerIDtxt.Size = new System.Drawing.Size(121, 20);
            this.CustomerIDtxt.TabIndex = 55;
            // 
            // CompanycomboBox
            // 
            this.CompanycomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CompanycomboBox.FormattingEnabled = true;
            this.CompanycomboBox.Location = new System.Drawing.Point(168, 84);
            this.CompanycomboBox.Name = "CompanycomboBox";
            this.CompanycomboBox.Size = new System.Drawing.Size(121, 21);
            this.CompanycomboBox.TabIndex = 54;
            this.CompanycomboBox.SelectedIndexChanged += new System.EventHandler(this.CompanycomboBox_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(50, 34);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(145, 25);
            this.label25.TabIndex = 53;
            this.label25.Text = "Customer Info";
            // 
            // CTypelbl
            // 
            this.CTypelbl.AutoSize = true;
            this.CTypelbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CTypelbl.Location = new System.Drawing.Point(52, 209);
            this.CTypelbl.Name = "CTypelbl";
            this.CTypelbl.Size = new System.Drawing.Size(91, 16);
            this.CTypelbl.TabIndex = 52;
            this.CTypelbl.Text = "Customer Type:";
            // 
            // CNamelbl
            // 
            this.CNamelbl.AutoSize = true;
            this.CNamelbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNamelbl.Location = new System.Drawing.Point(52, 169);
            this.CNamelbl.Name = "CNamelbl";
            this.CNamelbl.Size = new System.Drawing.Size(98, 16);
            this.CNamelbl.TabIndex = 51;
            this.CNamelbl.Text = "Customer Name:";
            // 
            // CompNamelbl
            // 
            this.CompNamelbl.AutoSize = true;
            this.CompNamelbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompNamelbl.Location = new System.Drawing.Point(52, 87);
            this.CompNamelbl.Name = "CompNamelbl";
            this.CompNamelbl.Size = new System.Drawing.Size(102, 16);
            this.CompNamelbl.TabIndex = 50;
            this.CompNamelbl.Text = "Company Name:";
            // 
            // CIDlbl
            // 
            this.CIDlbl.AutoSize = true;
            this.CIDlbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CIDlbl.Location = new System.Drawing.Point(52, 127);
            this.CIDlbl.Name = "CIDlbl";
            this.CIDlbl.Size = new System.Drawing.Size(76, 16);
            this.CIDlbl.TabIndex = 49;
            this.CIDlbl.Text = "Customer ID:";
            // 
            // dgvCart
            // 
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductIDColumn,
            this.ProductNameColumn,
            this.PriceColumn,
            this.QuantityColumn});
            this.dgvCart.Location = new System.Drawing.Point(77, 364);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(603, 150);
            this.dgvCart.TabIndex = 60;
            // 
            // ProductIDColumn
            // 
            this.ProductIDColumn.HeaderText = "Product ID";
            this.ProductIDColumn.Name = "ProductIDColumn";
            this.ProductIDColumn.ReadOnly = true;
            this.ProductIDColumn.Width = 80;
            // 
            // ProductNameColumn
            // 
            this.ProductNameColumn.HeaderText = "Name";
            this.ProductNameColumn.Name = "ProductNameColumn";
            this.ProductNameColumn.ReadOnly = true;
            this.ProductNameColumn.Width = 330;
            // 
            // PriceColumn
            // 
            this.PriceColumn.HeaderText = "Price (RM)";
            this.PriceColumn.Name = "PriceColumn";
            this.PriceColumn.ReadOnly = true;
            // 
            // QuantityColumn
            // 
            this.QuantityColumn.HeaderText = "Quantity";
            this.QuantityColumn.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40"});
            this.QuantityColumn.Name = "QuantityColumn";
            this.QuantityColumn.Width = 90;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(453, 550);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 61;
            this.label4.Text = "Estimated Arrival Date:";
            // 
            // estidatelbl
            // 
            this.estidatelbl.AutoSize = true;
            this.estidatelbl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.estidatelbl.Location = new System.Drawing.Point(589, 550);
            this.estidatelbl.Name = "estidatelbl";
            this.estidatelbl.Size = new System.Drawing.Size(0, 16);
            this.estidatelbl.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(52, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "Payment Method:";
            // 
            // paymethodComboBox
            // 
            this.paymethodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paymethodComboBox.FormattingEnabled = true;
            this.paymethodComboBox.Items.AddRange(new object[] {
            "Store Credit",
            "Cash",
            "Credit Card",
            "Debit Card",
            "Bank Transfer",
            "Cheque"});
            this.paymethodComboBox.Location = new System.Drawing.Point(168, 281);
            this.paymethodComboBox.Name = "paymethodComboBox";
            this.paymethodComboBox.Size = new System.Drawing.Size(121, 21);
            this.paymethodComboBox.TabIndex = 64;
            this.paymethodComboBox.SelectedIndexChanged += new System.EventHandler(this.paymethodComboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Coral;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(349, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 37);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "Add To Cart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DeleteCartItemsbtn
            // 
            this.DeleteCartItemsbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteCartItemsbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Coral;
            this.DeleteCartItemsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteCartItemsbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteCartItemsbtn.Location = new System.Drawing.Point(77, 529);
            this.DeleteCartItemsbtn.Name = "DeleteCartItemsbtn";
            this.DeleteCartItemsbtn.Size = new System.Drawing.Size(145, 37);
            this.DeleteCartItemsbtn.TabIndex = 65;
            this.DeleteCartItemsbtn.TabStop = false;
            this.DeleteCartItemsbtn.Text = "Remove From Cart";
            this.DeleteCartItemsbtn.UseVisualStyleBackColor = true;
            this.DeleteCartItemsbtn.Click += new System.EventHandler(this.DeleteCartItemsbtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Coral;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OrangeRed;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(286, 609);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(178, 54);
            this.button2.TabIndex = 66;
            this.button2.TabStop = false;
            this.button2.Text = "CONFIRM ORDER";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChooseProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(751, 702);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.DeleteCartItemsbtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.paymethodComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.estidatelbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.ContactNotxt);
            this.Controls.Add(this.CNumberlbl);
            this.Controls.Add(this.CustomerTypetxt);
            this.Controls.Add(this.CustomerNametxt);
            this.Controls.Add(this.CustomerIDtxt);
            this.Controls.Add(this.CompanycomboBox);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.CTypelbl);
            this.Controls.Add(this.CNamelbl);
            this.Controls.Add(this.CompNamelbl);
            this.Controls.Add(this.CIDlbl);
            this.Controls.Add(this.pricelbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChooseProductForm";
            this.Text = "Choose Products";
            this.Load += new System.EventHandler(this.ChooseProductForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pricelbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ProdID;
        private System.Windows.Forms.ColumnHeader ProductName;
        private System.Windows.Forms.ColumnHeader Price;
        private System.Windows.Forms.ColumnHeader Quantity;
        private System.Windows.Forms.TextBox ContactNotxt;
        private System.Windows.Forms.Label CNumberlbl;
        private System.Windows.Forms.TextBox CustomerTypetxt;
        private System.Windows.Forms.TextBox CustomerNametxt;
        private System.Windows.Forms.TextBox CustomerIDtxt;
        private System.Windows.Forms.ComboBox CompanycomboBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label CTypelbl;
        private System.Windows.Forms.Label CNamelbl;
        private System.Windows.Forms.Label CompNamelbl;
        private System.Windows.Forms.Label CIDlbl;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label estidatelbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox paymethodComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button DeleteCartItemsbtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn QuantityColumn;
    }
}
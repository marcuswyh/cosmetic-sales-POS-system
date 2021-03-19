using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace Login
{
    public partial class NewProductWindow : Form
    {

        private Product product = new Product();

        private string ProductID { get; set; }
        private SqlCeDataReader Read = null;

        public NewProductWindow(string type)
        {
            InitializeComponent();

            lblPageTitle.Text = "Add New Product";
            txtBoxName.Enabled = true;
            cmbBoxCategory.Enabled = true;
            btnAdd.Visible = true;
            btnReset.Visible = true;
            btnUpdate.Visible = false;
            
        }

        public NewProductWindow(string type, string productID) {
            InitializeComponent();

            ProductID = productID;
            lblPageTitle.Text = "Edit Product";
            txtBoxName.Enabled = false;
            cmbBoxCategory.Enabled = false;
            btnAdd.Visible = false;
            btnReset.Visible = false;
            btnUpdate.Visible = true;
            nupdownQuantity.Enabled = false;
            ProductDetails(ProductID);
        }

        private void ProductDetails(string productID) {
            Read = product.getProduct("One", productID,"");

            while(Read.Read()){
                txtBoxName.Text = Convert.ToString(Read["Name"]);
                cmbBoxCategory.SelectedItem = Convert.ToString(Read["Category"]);
                txtBoxSize.Text = Convert.ToString(Read["Size"]);
                txtPrice.Text = Convert.ToString(Read["Price"]);
                //nupdownQuantity.Value = Convert.ToInt16(Read["Quantity"]);
            }
            

        
        }


        private void NewProductWindow_Load(object sender, EventArgs e)
        {

            Read = product.ComboCategory();

            while (Read.Read())
            {
                
                cmbBoxCategory.Items.Add(Convert.ToString(Read["Category"]));
            }

            cmbBoxCategory.SelectedIndex = 0;
        }

        private void clearDetails() {
            cmbBoxCategory.SelectedIndex = 0;
            txtBoxName.Text = "";
            txtBoxSize.Text = "";
            txtPrice.Text = "";
            nupdownQuantity.Value = 0;
            
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string cat, productName, size, price = null;
            string caption;
            string msg;

            int qty = 0;
            StringBuilder errorMsg = new StringBuilder(50);
            size = txtBoxSize.Text;
            price = txtPrice.Text;
            cat = Convert.ToString(cmbBoxCategory.SelectedItem);
            productName = txtBoxName.Text;
            
            qty = (int) nupdownQuantity.Value;

           

            if (productName != "" && size != "" && price != "")
            {
                product.InsertNewProduct(cat, productName, size, price, qty);
                msg = string.Format("{0} Is Successfully Added",productName);
                caption = "New Product Added!";

                MessageBox.Show(msg, caption, MessageBoxButtons.OK,MessageBoxIcon.Information);
                clearDetails();
            }
            else {
                if (productName == "")
                    errorMsg.AppendLine("Product Name");
                if (size == "")
                    errorMsg.AppendLine("Product Size");
                if (price == "" )
                    errorMsg.AppendLine("Product Price");

                caption = "Missing Details!";
                msg = string.Format("Please Enter The Following Details:\n{0}", errorMsg);
                Console.WriteLine("Please Enter The Following:\n{0}", errorMsg);

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearDetails();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string size, price = null;
            size = txtBoxSize.Text;
            price = txtPrice.Text;

            product.UpdateProduct(size, price, ProductID);
            
        }
    }
}

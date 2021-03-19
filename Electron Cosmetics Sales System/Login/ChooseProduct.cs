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
    public partial class ChooseProductForm : Form
    {
        private Product products = new Product();
        private Customer cust = new Customer();
        private Order order = new Order();
        private Product_Order po = new Product_Order();
        private SqlCeDataReader read, readcart = null;

        private double totalprice = 0;
        private int EmpID { get; set; }

        public ChooseProductForm(int id)
        {
            InitializeComponent();
            EmpID = id;

        }

        private void ChooseProductForm_Load(object sender, EventArgs e)
        {

            //set listeners for datagridview's combobox event
            dgvCart.CellValueChanged += new DataGridViewCellEventHandler(dgvCart_CellValueChanged);
            dgvCart.CurrentCellDirtyStateChanged += new EventHandler(dgvCart_CurrentCellDirtyStateChanged);



            //to populate the list view
            read = products.getProduct("All","","");
            while (read.Read())
            {
                ListViewItem item = new ListViewItem(Convert.ToString(read["ProductID"]));
                item.SubItems.Add(Convert.ToString(read["Name"]));
                item.SubItems.Add(Convert.ToString(read["Price"]));
                item.SubItems.Add(Convert.ToString(read["Quantity"]));
                listView1.Items.Add(item);
            }

            //Assigning the customer's company names to the combo box
            read = cust.DisplayCustomers("", "");                                 
            while (read.Read())
            {
                CompanycomboBox.Items.Add(Convert.ToString(read["CompanyName"]));
            }

        }

        //to display customer info into textboxes after user has made combobox selection
        private void CompanycomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtdisplay(Convert.ToString(CompanycomboBox.SelectedItem));
        }

        //to display customer information into textboxes
        private void txtdisplay(string cat)
        {
            //Calling the method getCustomer which runs the query to obtain all the records
            read = cust.DisplayCustomers("", cat);  

            while (read.Read())
            {
                //assigning relevant customer info into textboxes
                CustomerIDtxt.Text = Convert.ToString(read["CID"]);
                CustomerNametxt.Text = Convert.ToString(read["FirstName"]) +" "+ Convert.ToString(read["LastName"]);
                ContactNotxt.Text = Convert.ToString(read["PhoneNo"]);
                CustomerTypetxt.Text = Convert.ToString(read["CustomerType"]);
            }
        }

        //to handle the "add to cart" button operations
        private void button1_Click(object sender, EventArgs e)
        {
            bool contain = false;
            string product = null;

            foreach (ListViewItem items in listView1.SelectedItems) {
                product = items.SubItems[0].Text;
            }

            foreach (DataGridViewRow row in dgvCart.Rows) {
                if (Convert.ToString(row.Cells[0].Value).Equals(product))
                {
                    MessageBox.Show("Selected product have already been added to cart!");
                    goto done;
                }
                else {
                    contain = true;
                }
            }

            if (contain) {
                Refreshdgvcart();

                //to display estimated order date
                DateTime today = DateTime.Today;
                DateTime date = today.AddDays(2);
                string Date = date.Date.ToString("dd/MM/yyyy");
                estidatelbl.Text = Date;
            }

            done:;
        }

        //to display selected products in the cart datagridview
        private void Refreshdgvcart() {
            //dgvCart.Rows.Clear();
            int count = 0;

            //loop through and display the checked items in the product listview
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                DataGridViewRow row = (DataGridViewRow)dgvCart.Rows[count].Clone();

                row.Cells[0].Value = item.SubItems[0].Text;
                row.Cells[1].Value = item.SubItems[1].Text;
                row.Cells[2].Value = item.SubItems[2].Text;

                dgvCart.Rows.Add(row);

                count = count + 1;
            }
        }

        //to handle the "Make Order" button operations
        private void button2_Click(object sender, EventArgs e)
        {
            //variables
            string date, cid, eid, orderstatus, paystatus, productid, orderid = null;
            string caption;
            string msg;
            bool cellstatus = true;
            int qty = 0;

            StringBuilder errorMsg = new StringBuilder(50);

            date = DateTime.Today.ToString("dd/MM/yyyy");
            orderstatus = "Pending";

            paystatus = "Paid";
            if (paymethodComboBox.Text == "Store Credit")
            {
                paystatus = "Not Paid";
            }

            eid = Convert.ToString(EmpID);
            cid = CustomerIDtxt.Text;

            //loop through the datagridview to ensure "Quantity" column is not empty
            for (int row = 0; row < dgvCart.Rows.Count - 1; row++)
            {
                DataGridViewRow r = dgvCart.Rows[row];
                for (int col = 0; col < dgvCart.Columns.Count; col++)
                {
                    if (Convert.ToString(r.Cells[col].Value) == "")
                    {
                        cellstatus = false;
                    }
                }
            }

            //to check if all relevant information is filled
            if (CompanycomboBox.Text != "" && dgvCart.Rows.Count > 1 && paymethodComboBox.Text != "" && pricelbl.Text != "" && cellstatus)
            {
                //to check if premium customer's credit limit is reached
                if (paymethodComboBox.Text == "Store Credit")
                {
                    read = cust.DisplayCustomers(CustomerIDtxt.Text);
                    double credit = 0;
                    double price = totalprice;

                    while (read.Read())
                    {
                        credit = Convert.ToDouble(read["CreditLimit"]);
                    }

                    if (price > credit)
                    {
                        MessageBox.Show("Customer's credit limit has been reached, please select other payment method.");
                        //goto done;
                    }

                }

                //to check if user selected product quantity exceeds stock quantity
                for (int row = 0; row < dgvCart.Rows.Count - 1; row++)
                {
                    DataGridViewRow r = dgvCart.Rows[row];
                    int cartq = Convert.ToInt32(r.Cells[3].Value);
                    read = products.getProduct("One", Convert.ToString(r.Cells[0].Value), "");

                    while (read.Read())
                    {
                        int q = Convert.ToInt32(read["Quantity"]);

                        //uncomment this part if you dont want order to go through if stock is less than 0

                        //if (q <= 0)
                        //{
                        //    MessageBox.Show(Convert.ToString(read["Name"]) + " is currently out of stock, we are sorry for the inconvenience.");
                        //    goto done;
                        //}

                        if (cartq > q)
                        {
                            MessageBox.Show("Stock for " + Convert.ToString(read["Name"]) + " is insufficient to fulfill order. Please make stock purchase for " + Convert.ToString(read["Name"]) + ".");
                        }
                    }

                }

                //adding order
                order.AddOrder(cid, eid, date, orderstatus, paystatus);

                //adding product_order
                readcart = order.lastrecord();
                while (readcart.Read())
                {
                    orderid = Convert.ToString(readcart["OrderID"]);
                }

                for (int row = 0; row < dgvCart.Rows.Count - 1; row++)
                {
                    DataGridViewRow r = dgvCart.Rows[row];

                    productid = Convert.ToString(r.Cells[0].Value);
                    qty = Convert.ToInt32(r.Cells[3].Value);

                    po.AddProductOrder(productid, orderid, qty);
                }

                //to update product stock quantity after an order is made
                for (int row = 0; row < dgvCart.Rows.Count - 1; row++)
                {
                    DataGridViewRow r = dgvCart.Rows[row];
                    read = products.getProduct("One", Convert.ToString(r.Cells[0].Value), "");
                    while (read.Read())
                    {
                        int stockquantity = Convert.ToInt32(read["Quantity"]);
                        int prodquantity = Convert.ToInt32(r.Cells[3].Value);

                        int newstockquantity = stockquantity - prodquantity;

                        if (newstockquantity < 0) {
                            newstockquantity = 0;
                        }

                        products.UpdateProduct(newstockquantity, Convert.ToString(r.Cells[0].Value));
                    }
                }

                //to update customer's credit limit amount after an order is made
                if (paymethodComboBox.Text == "Store Credit")
                {
                    double creditamt = 0, newcredlimit = 0;

                    read = cust.DisplayCustomers(CustomerIDtxt.Text);

                    while (read.Read()) {
                        creditamt = Convert.ToDouble(read["CreditLimit"]);
                        newcredlimit = creditamt - totalprice;

                        cust.UpdateCustomer(Convert.ToString(newcredlimit), Convert.ToString(CustomerIDtxt.Text));
                    }

                    
                }


                MessageBox.Show("Order Has Been Maid !", "New Order", MessageBoxButtons.OK, MessageBoxIcon.Information);


                read = order.lastrecord();
                while (read.Read())
                {
                    Receipt r = new Receipt(Convert.ToString(read["OrderID"]));
                    r.ShowDialog();
                    this.Close();
                }


            }
            //to inform user of unfilled information
            else
            {
                if (CompanycomboBox.Text == "")
                    errorMsg.AppendLine("Customer Information");
                if (dgvCart.Rows.Count < 2)
                    errorMsg.AppendLine("Products");
                if (paymethodComboBox.Text == "")
                    errorMsg.AppendLine("Payment Method");
                if (cellstatus == false)
                    errorMsg.AppendLine("Product Quantity in Cart");

                caption = "Missing Details!";
                msg = string.Format("Please Enter The Following Details:\n{0}", errorMsg);
                Console.WriteLine("Please Enter The Following:\n{0}", errorMsg);

                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //done:;


        }

        //to handle delete button of cart's operations
        private void DeleteCartItemsbtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dgvCart.SelectedRows)
                {
                    //remove selected items in cart 
                    dgvCart.Rows.RemoveAt(item.Index);
                }
            }
            catch (Exception s)
            {
                MessageBox.Show("Cannot delete empty row\n\n" +s.Source + "\n" + s.Message + "\n" + s.StackTrace);
            }
        }

        //to prevent store credit payment option is selected for normal customers
        private void paymethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paymethodComboBox.Text == "Store Credit" && CustomerTypetxt.Text == "Normal") {
                MessageBox.Show("The customer is normal customer, payment using store credit is not allowed.");
                paymethodComboBox.SelectedIndex = -1;
            }
        }

 //to update the total price of current items in the cart

        //event to check if cart cell have changed state
        void dgvCart_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvCart.IsCurrentCellDirty)
            {
                dgvCart.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //event to check if cart cell value have changed 
        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgvCart.Rows[e.RowIndex].Cells[3];
            if (cb.Value != null)
            {
                double amount = 0;

                //loop through the price column of cart and calculate total price
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    amount = (Convert.ToDouble(row.Cells[2].Value) * Convert.ToDouble(row.Cells[3].Value)) + amount;
                    pricelbl.Text = String.Format("{0:C}",Convert.ToDecimal(amount));
                    totalprice = amount;
                }
            }
        }


    }
}

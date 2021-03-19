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
    public partial class Sales : Form
    {
        private Product Connection = new Product();
        private Account acc = new Account();
        private LoginActivity la = new LoginActivity();
        private SalesReport sr = new SalesReport();
        private Customer cs = new Customer();
        private Order or = new Order();
        private Product_Order po = new Product_Order();



        private string ProductQTY { get; set; }
        private string Date{ get; set; }
        private static bool LoginStatus{ get; set; }
        private SqlCeDataReader Read,Products = null;
        private string[] LoginLogout = new string[2];
        public Sales()
        {  
            InitializeComponent();
            

        }

        //Connects to the database when the application is launched
        private void Main_Load(object sender, EventArgs e)
        {


            Connection.Connect();   //Establish Database Connection

            UnloadTab();            //To Unload All The Tabs Except For Login Tab

            //Set the title of the product listbox
            lblProductTitle.Text = string.Format("{0,-14}{1,-35}{2,-13}{3,-9}{4,-10}{5}", "Product ID","Product Name","Category","Size", "Price", "Quantity");

            //Set the title of the customer listbox
            lblTitleCustomer.Text = string.Format(" {0,-15}{1,-20}{2,-18}{3,-20}{4}", "Customer ID", "Company","Customer Type", "Contact Person", "Contact Num");

            //set the title of the employee listbox
            lblTitleEmployee.Text = string.Format("{0,-15}{1,-15}{2,-15}{3,-8}{4,-18}{5}", "Employee ID", "Account Type", "Name", "Sex", "Date of Birth", "Phone No");

            //set the title of the stock listbox
            lblAvailableStockTitle.Text = string.Format("{0,-5}{1,-36}{2}", "ID", "Name", "Quantity");
            
            //set the title of the stock to purchase listbox
            lblStockToPurchaseTitle.Text = string.Format("{0,-5}{1,-36}{2}", "ID", "Name", "Quantity");

            ComboBoxCategoryLoader();

            ShowOrder();

            //Set selected item for the comboboxes to be the first item
            cmBoxCategory.SelectedIndex = 0;
            cmbBoxCustomerType.SelectedIndex = 0;
            cmbBoxAccType.SelectedIndex = 0;
            cmbBoxStockLevel.SelectedIndex = 0;


            datePickerSalerReport.MaxDate = DateTime.Today;
        }



        //Disconnects the database connection when the application is closed
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.Disconnect();
        }
 




        //#############################################################################################################################
        //START OF CONTROLTAB SECTION
        //#############################################################################################################################

        //Method to create a custom Controltab design
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {    
                Graphics g = e.Graphics;
                Brush _textBrush;

                // Get the item from the collection.
                TabPage _tabPage = tabControl1.TabPages[e.Index];

                // Get the real bounds for the tab rectangle.
                Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

                if (e.State == DrawItemState.Selected)
                {

                    // Draw a different background color, and don't paint a focus rectangle.
                    _textBrush = new SolidBrush(Color.White);
                    g.FillRectangle(Brushes.Coral, e.Bounds);
                }
                else
                {
                    _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                    e.DrawBackground();
                }

                // Use our own font.
                Font _tabFont = new Font("Century Gothic", (float)14.0, FontStyle.Regular, GraphicsUnit.Pixel);

                // Draw string. Center the text.
                StringFormat _stringFlags = new StringFormat();
                _stringFlags.Alignment = StringAlignment.Center;
                _stringFlags.LineAlignment = StringAlignment.Center;
                g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
             
        }

        //Load tabs to the tab control depending on the user's account type
        private void LoadTabs() {
            string accType = acc.AccountType;                       //To get the account type of the user

            tabControl1.TabPages.Add(tabOrder);                     //Load the common tabs that all users can have access to
            tabControl1.TabPages.Add(tabCustomer);
            tabControl1.TabPages.Add(tabProduct);
            tabControl1.TabPages.Add(tabPurchaseProduct);

            //Check what account type does the user have & load the tabs depending on their account type
            if (accType == "Admin")                                 //When the user is an Admin
            {                                                       //The user get access to EmployeeManagement & SalesReport Tab
                tabControl1.TabPages.Add(tabEmployeeManagement);
                tabControl1.TabPages.Add(tabSalesReport);
            }else if (accType == "Manager")                         //When the user is a Manager
            {                                                       //The user only gets access to SalesReport Tab
                Console.WriteLine("Execute");
                tabControl1.TabPages.Add(tabSalesReport);
            }

            tabControl1.TabPages.Add(tabHelp);                      //Load the tab Help
        }

        //Method to unload all the tabs in the Controltab
        private void UnloadTab() {
            tabControl1.TabPages.Remove(tabEmployeeManagement);
            tabControl1.TabPages.Remove(tabSalesReport);
            tabControl1.TabPages.Remove(tabOrder);
            tabControl1.TabPages.Remove(tabCustomer);
            tabControl1.TabPages.Remove(tabProduct);
            tabControl1.TabPages.Remove(tabPurchaseProduct);
            tabControl1.TabPages.Remove(tabHelp);
        }

        //#############################################################################################################################
        //END OF CONTROLTAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF LOGIN TAB SECTION
        //#############################################################################################################################

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (acc.getAccount(txtBoxID.Text, txtBoxPass.Text))                     //To verify is the username & password correct
            {
                DateTime now = DateTime.Now;                                        //Get the current data & time of the system
              
                LoginLogout[0] = now.ToString("dd/MM/yyyy hh:mm:ss tt");            //Set the format of the datetime to Day/Month/Year Hour:Minute:Seconds

                LoadTabs();                                                         //Calling method LoadTab
                txtBoxID.Text = "";                                                 //Clear the textbox of userID & password
                txtBoxPass.Text = "";
                tabControl1.TabPages.Remove(tabLogin);                              //Remove access to login page,as the user has already successfully logged in
                tabControl1.SelectedTab = tabOrder;                                 //Make the selected tab to be the product tabe
                btnLogout.Visible = true;                                           //Set the logout button to be visible, as the user has successfully logged in
                this.ControlBox = false;
                
            }
            else
            {                                                                       //To Display massagebox to indicate the userID or password is invalid
                string msg = "Invalid Username OR Password!!";
                string caption = "Login Failure";

                MessageBoxButtons btns = MessageBoxButtons.RetryCancel;
                DialogResult results;

                
                results = MessageBox.Show(msg, caption, btns);                      //Messagebox to inform the login was not successful, as the password or username is incorrect

                if (results == System.Windows.Forms.DialogResult.Cancel)            //Exits the entire application if the cancel button is pressed
                {
                    this.Close();
                }
                else
                {
                    txtBoxID.Focus();                                               //Focus on the username textbox
                    txtBoxID.SelectAll();
                }

            }
        }


        //Clear textbox of ID & password when button 'clear' is pressed
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxID.Text = "";
            txtBoxPass.Text = "";
        }

        //Logout of the system when the 'Logout' button is clicked
        private void btnLogout_Click(object sender, EventArgs e)
        {

            string msg = "Are you sure you want to logout?";
            string caption = "Logout Confirmation";

            DialogResult results;

            results = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);  //Messagebox to confirm does the user wants to logout of the system

            if (results == System.Windows.Forms.DialogResult.Yes)                                       //Exits the entire application if the cancel button is pressed
            {
                DateTime now = DateTime.Now;                                                            //Get the current data & time of the system
              
                LoginLogout[1] = now.ToString("dd/MM/yyyy hh:mm:ss tt");                                //Set the format of the datetime to Day/Month/Year Hour:Minute:Seconds

                Console.WriteLine(acc.EmployeeID);
                Console.WriteLine(LoginLogout[1]);
                la.AddRecord(acc.EmployeeID, LoginLogout);                                              //Adds a new login & logout record to the loginActivity table


                btnLogout.Visible = false;                                                              //Set visibilty for logout button to false
                UnloadTab();                                                                            //Call method UnloadTab

                this.ControlBox = true;

                tabControl1.TabPages.Add(tabLogin);                                                     //Insert tabLogin to the tabcontrol collection
                tabControl1.SelectedTab = tabLogin;                                                     //Make the current tab to be tabLogin
                
            }
             
        }

        //Method to display the system's loginlog when the 'Login Activity' hyperlink is clicked
        private void linkLbl_LoginLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginRecordWindow LRW = new LoginRecordWindow();    // To display a new window that shows the login records of the system
            LRW.ShowDialog();
        }

        //#############################################################################################################################
        //END OF LOGIN TAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF PRODUCT TAB SECTION
        //#############################################################################################################################

        //Method to read all the types of makeup categories and load it to the combobox
        private void ComboBoxCategoryLoader(){
            //Assigning the categories of products to the combo box
            Read = Connection.ComboCategory();                                  
            while (Read.Read())
            {
                cmBoxCategory.Items.Add(Convert.ToString(Read["Category"]));
            }
        }

        //Calls method ShowProduct() when the selected item in the category has changed
        private void cmBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //Calls method ShowProduct() 
            ShowProducts(Convert.ToString(cmBoxCategory.SelectedItem));
        }


        //Access the Product table from the database and displays all the records
        private void ShowProducts(string cat) {
            Products = Connection.getProduct(cat,"", txtBoxProductSearch.Text);   //Calling the method getProducts which runs the query to obtain all the records
            listBoxProduct.Items.Clear();               //Clear the exisiting items in the listbox

            //Add the results of the query to the listbox
            while (Products.Read())
            {
                listBoxProduct.Items.Add(string.Format("{0,-5}\t\t{1,-40}{2,-15}{3,-10}{4,-15:C}{5}", Products["ProductID"], Products["Name"],
                    Products["Category"], Products["Size"], Convert.ToDecimal(Products["Price"]), Products["Quantity"]));
            }

            //Check is there any items in the listbox
            if (listBoxProduct.Items.Count != 0) {
                listBoxProduct.SetSelected(0, true);    //Set the selected item to be index 0
            }
        }

        //Calls the method ShowProduct() when the text in the textbox has change
        private void txtBoxProductSearch_TextChanged(object sender, EventArgs e)
        {
            ShowProducts(Convert.ToString(cmBoxCategory.SelectedItem));
        }

        //To add a new product to the system when 'Add' button is clicked
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            //Displays a new window where the user can fill in the details for a new product
            NewProductWindow npw = new NewProductWindow("Add");

            npw.ShowDialog();

            //Calls method ShowProduct() to reflect the changes made to the list of products
            ShowProducts("All");
        }

        //To edit the product selected when 'Edit' button is clicked
        private void btnEditProduct_Click(object sender, EventArgs e)
        {

            string productRecord = Convert.ToString(listBoxProduct.SelectedItem);
            string[] productID = productRecord.Split(' ');

            //Displays a new window where the user can edit the details of the product selected
            NewProductWindow npw = new NewProductWindow("Edit", productID[0]);
            npw.ShowDialog();

            //Calls method ShowProduct() to reflect the changes made to the list of products
            ShowProducts(Convert.ToString(cmBoxCategory.SelectedItem));            
            
        }

        //To delete a selected product when 'Delete' button is clicked
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            string productRecord = Convert.ToString(listBoxProduct.SelectedItem);
            string[] productID = productRecord.Split(' ');

            
            if (listBoxProduct.Items.Count != 0)
            {
                //MessabeBox to confirm is user want to delete the selected product
                DialogResult msgBoxResults = MessageBox.Show("Are you sure you want to permanently delete the selected product?", "Remove Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //If user clicks yes, then remove the product record from the database
                if (msgBoxResults == System.Windows.Forms.DialogResult.Yes)
                {
                    Connection.DeleteProduct(productID[0]);
                }

                //Calls method ShowProduct() to reflect the changes made to the list of products
                ShowProducts(Convert.ToString(cmBoxCategory.SelectedItem)); 
            }
            else
            {
                MessageBox.Show("No Product Selected !!", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Error);        //Message to show no product has been selected
            }

        }
        //#############################################################################################################################
        //END OF PRODUCT TAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF SALES REPORT TAB SECTION
        //#############################################################################################################################

        //Mehtod to re-load the sales report when the selected day is changed
        private void datePickerSalerReport_ValueChanged(object sender, EventArgs e)
        {
            Date = datePickerSalerReport.Value.Date.ToString("dd/MM/yyyy");         //Setting the format of the date to be Day/Month/Year
            LoadSalesReport();
        }

        //Method to load the sales report 
        private void LoadSalesReport() {
            //To show the number of orders made for the selected day
            txtTotalOrders.Text = Convert.ToString(sr.Report("Date", Date));
            //To show the total sales amount made for the selected day
            try
            {
                txtTotalSales.Text = string.Format("{0:C}", Convert.ToDecimal(sr.Report("Total Sale", Date)));
            }
            catch {
                txtTotalSales.Text = string.Format("{0:C}", 0M);
            }


            SqlCeDataReader results = null;

            
            
            //To fill in the detail of the product that is sold the most for the day
            results= sr.ReportBest("Best Product",Date);                                                                           
            txtBestSellingProductID.Text = "";
            txtBestSellingProductName.Text = "";
            txtBestSellingProductQTY.Text = "";

            while (results.Read())
            {
                txtBestSellingProductID.Text = Convert.ToString(results["ProductCode"]);
                txtBestSellingProductName.Text = Convert.ToString(results["Name"]);
                txtBestSellingProductQTY.Text = string.Format("{0} Units", Convert.ToString(results["Total"]));
            } 


            results = null;
            //To fill in the the details of the employee with the highest total sales
            results = sr.ReportBest("Top Employee", Date);

            txtEmployeeID.Text = "";
            txtEmployeeName.Text = "";
            txtEmployeeSales.Text = "";

            while (results.Read())
            {
                txtEmployeeID.Text = Convert.ToString(results["EmployeeID"]);
                txtEmployeeName.Text = Convert.ToString(results["Name"]);
                txtEmployeeSales.Text = string.Format("{0:C}", Convert.ToDecimal(results["NoofSales"]));
            }
               
            

            results = null;
            //To fill in the details of the customer with highest amount of purchase for the day
            results = sr.ReportBest("Top Customer", Date);

            txtCustomerID.Text = "";
            txtCustomerCompany.Text = "";
            txtCustomerSalesAmount.Text = "";

            while (results.Read())
            {
                txtCustomerID.Text = Convert.ToString(results["CID"]);
                txtCustomerCompany.Text = Convert.ToString(results["CompanyName"]);
                txtCustomerSalesAmount.Text = string.Format("{0:C}", Convert.ToDecimal(results["TotalSales"]));
            }
                        

        }

        
        //To produce a detailed sales report for the day when the button 'Sales Report' is clicked
        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            if(Convert.ToString(sr.Report("Date",Date)).Equals("0")){
                MessageBox.Show("No Sales Made Today", "Detailed Daily Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);        //Messagebox to show that there is no sales made for the selected day
            }else{
                SalesReportWindow srw = new SalesReportWindow(Date);                                                                           
                srw.ShowDialog();
            }
        }


        //#############################################################################################################################
        //END OF SALES REPORT TAB SECTION
        //#############################################################################################################################

        //#############################################################################################################################
        //START OF CUSTOMER TAB SECTION
        //#############################################################################################################################

        //Method to load all the customers from the database & displays it in a listbox
        private void ShowCustomers(string type) {
            listBoxCustomer.Items.Clear();                                                  //Clear all the current items in the listbox
            SqlCeDataReader customers = cs.DisplayCustomers(type, txtBoxSearch.Text);       //Calls the query to get all the customer
            string contactPerson;

            while (customers.Read()) {
                contactPerson = string.Format("{0} {1}", customers["FirstName"], customers["LastName"]);
                listBoxCustomer.Items.Add(string.Format(" {0,-17}{1,-23}{2,-21}{3,-23}{4}", customers["CID"],
                    customers["CompanyName"], customers["CustomerType"], contactPerson ,customers["PhoneNo"]));
                 
            }
            if (listBoxCustomer.Items.Count != 0) {
                listBoxCustomer.SetSelected(0, true);                                       //Sets the first item to be selected if the listbox is not empty
            } 
        }

        //Calls method ShowCustomers() when the selected item in the 'Account Type' combobox
        private void cmbBoxCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(Convert.ToString(cmbBoxCustomerType.SelectedItem));
            ShowCustomers(Convert.ToString(cmbBoxCustomerType.SelectedItem));
        }

        //Calls the method ShowCustomer() when the text in the textbox has change
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            ShowCustomers(Convert.ToString(cmbBoxCustomerType.SelectedItem));
        }

        //Delete the selected customer record when 'Delete' button is clicked
        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string customerRecord = Convert.ToString(listBoxCustomer.SelectedItem);     //Reads the selected item from the listbox as a string
            string[] customerID = customerRecord.Split(' ');                            //Split the strings into an array of strings, assuming the first array would be the customer's ID

            //Check is there any customer records in the listbox
            if (listBoxCustomer.Items.Count != 0)
            {
                //Warning to prompt if the user wants to the delete the selected record or not
                DialogResult msgBoxResults = MessageBox.Show("Are you sure you want to permanently delete the selected customer?", "Remove Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //Deletes the selected record if the 'yes' button is pressed
                if (msgBoxResults == System.Windows.Forms.DialogResult.Yes)
                {
                    cs.DeleteCustomer(customerID[1]);
                }
                ShowCustomers(Convert.ToString(cmbBoxCustomerType.SelectedIndex));      //Calls method ShowCustomers() to reflect on the changes made
            }
            else
            {
                MessageBox.Show("No Customer Selected !!", "Edit Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adds a new customer when the 'Add' button is clicked
        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            CustomerWindow cw = new CustomerWindow();
            cw.ShowDialog();
            cmbBoxCustomerType.SelectedIndex = 0;
            ShowCustomers(Convert.ToString(cmbBoxCustomerType.SelectedIndex));
        }

        //Edit a selected customer record when the 'Edit' button is clicked
        private void btnCustomerEdit_Click(object sender, EventArgs e)
        {

            string customerRecord = Convert.ToString(listBoxCustomer.SelectedItem);         //Reads the selected item from the listbox as a string
            string[] customerID = customerRecord.Split(' ');                                //Split the strings into an array of strings, assuming the first array would be the customer's ID
            
            //Check is there any customer records in the listbox
            if (listBoxCustomer.Items.Count != 0)
            {
                CustomerWindow cw = new CustomerWindow(acc.AccountType, "Edit", customerID[1]);
                cw.ShowDialog();
                ShowCustomers(Convert.ToString(cmbBoxCustomerType.SelectedIndex));
            }
            else {
                MessageBox.Show("No Customer Selected !!", "Edit Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
        //#############################################################################################################################
        //END OF CUSTOMER TAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF EMPLOYEE TAB SECTION
        //#############################################################################################################################

        //Method to load all the Employees from the database & displays it in a listbox
        private void showEmployee(string type) {
            SqlCeDataReader employees = acc.getEmployee(type, "",txtEmployeeSearch.Text);
            listBoxHR.Items.Clear();
            while (employees.Read()) {
                listBoxHR.Items.Add(string.Format("{0,-17}{1,-17}{2,-17}{3,-9}{4,-21}{5}", employees["EmployeeID"], employees["AccountType"], employees["Name"], employees["Sex"],
                    employees["DateofBirth"],employees["ContactNumber"])); 
            }

            //Sets the first item in the list to be selected
            if (listBoxHR.Items.Count != 0)
            {
                listBoxHR.SetSelected(0, true);
            }
        }

        //Adds a new employee when the 'Add' button is clicked
        private void btnEmployeeAdd_Click(object sender, EventArgs e)
        {
            NewEmployeeWindow newEmpWindow = new NewEmployeeWindow("Add");
            newEmpWindow.ShowDialog();
            showEmployee(Convert.ToString(cmbBoxAccType.SelectedItem));         //Calls method ShowEmployee() to reflect the changes made to the list
        }

        //Calls method ShowEmployee() when the selected item in the 'Employee Type' combobox is changed     
        private void cmbBoxAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            showEmployee(Convert.ToString(cmbBoxAccType.SelectedItem));
        }

        //Shows the detailed information of a selected employee when the listbox is double clicked
        private void listBoxHR_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxHR.Items.Count != 0) {
                string employeeRecord = Convert.ToString(listBoxHR.SelectedItem);       
                string[] employeeID = employeeRecord.Split(' ');

                Console.WriteLine(employeeID[0]);
                NewEmployeeWindow newEmpWindow = new NewEmployeeWindow("Details", employeeID[0]);

                newEmpWindow.ShowDialog();
            }
        }

        //Calls the method ShowEmployee() when the text in the textbox has change
        private void txtEmployeeSearch_TextChanged(object sender, EventArgs e)
        {
            showEmployee(Convert.ToString(cmbBoxAccType.SelectedItem));
        }

        //Deletes a Employee record when the 'Delete' button is clicked
        private void btnEmployeeDelete_Click(object sender, EventArgs e)
        {
            string employeeRecord = Convert.ToString(listBoxHR.SelectedItem);       //Reads the selected item from the listbox as a string
            string[] employeeID = employeeRecord.Split(' ');                        //Split the strings into an array of strings, assuming the first array would be the Employee's ID

            if (listBoxHR.Items.Count != 0)
            {
                //Message box to confirm does the user wants to delete the record of the selected Employee 
                DialogResult msgBoxResults = MessageBox.Show("Are you sure you want to permanently delete the selected Employee?", "Remove Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //Deletes the selected record when the 'Yes' button is pressed
                if (msgBoxResults == System.Windows.Forms.DialogResult.Yes)
                {
                    acc.DeleteEmployeeData(employeeID[0]);
                    showEmployee(Convert.ToString(cmbBoxAccType.SelectedItem));         //Calls method ShowEmployee() to reflect the changes made to the list
                }
            }
            else {
                MessageBox.Show("No Employee Selected !!", "Remove Employee", MessageBoxButtons.OK, MessageBoxIcon.Error);          
            }
        }

        //Edits the selected employee record when the 'Edit' button is clicked
        private void btnEmployeeEdit_Click(object sender, EventArgs e)
        {
            string employeeRecord = Convert.ToString(listBoxHR.SelectedItem);       //Reads the selected item from the listbox as a string
            string[] employeeID = employeeRecord.Split(' ');                        //Split the strings into an array of strings, assuming the first array would be the Employee's ID

            //To check is there any records in the listbox
            if (listBoxHR.Items.Count != 0)
            {
                NewEmployeeWindow newEmpWindow = new NewEmployeeWindow("Edit", employeeID[0]);

                newEmpWindow.ShowDialog();
                showEmployee(Convert.ToString(cmbBoxAccType.SelectedItem));
            }
            else {
                MessageBox.Show("No Employee Selected !!", "Edit Employee Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }    
        }
        //#############################################################################################################################
        //END OF EMPLOYEE TAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF PURCHASING ORDER TAB SECTION
        //#############################################################################################################################


        //Method to show the current stock level of the products.
        private void ShowStock(string type){

            SqlCeDataReader stock = null;

            switch (type) { 
                case "All":
                    stock = Connection.getProduct("Stock","", "10000");
                    break;
                case "Low Stock":
                    stock = Connection.getProduct("Stock", "", "20");
                    break;
                case "Out of Stock":
                    stock = Connection.getProduct("Stock", "", "0");
                    break; 
            }

            listBoxAvailableStock.Items.Clear();            //Clear all the existing items in the listbox

            while (stock.Read()) {
                listBoxAvailableStock.Items.Add(string.Format("{0,-5}{1,-38}{2}", stock["ProductID"], stock["Name"], stock["Quantity"]));
            }

            if (listBoxAvailableStock.Items.Count != 0) {   //If the listbox is not empty, then Set the selected item in the list to be the first 1
                listBoxAvailableStock.SelectedIndex = 0;
            }
        }

        //Calls method ShowStock() when the selected item in 'Stock Level' combobox changes
        private void cmbBoxStockLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowStock(Convert.ToString(cmbBoxStockLevel.SelectedItem));
        }

        //Fill out the details of the selected item from the comb
        private void listBoxAvailableStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stockRecord = Convert.ToString(listBoxAvailableStock.SelectedItem);          //Reads the selected item from the listbox as a string
            string[] stockID = stockRecord.Split(' ');                                          //Split the strings into an array of strings, assuming the first array would be the Product's ID

            SqlCeDataReader detail = Connection.getProduct("One", stockID[0], "");

            while (detail.Read()) {
                txtPurchaseProductID.Text = Convert.ToString(detail["ProductID"]);
                txtPurchaseProductName.Text = Convert.ToString(detail["Name"]);
                txtPurchaseProductCategory.Text = Convert.ToString(detail["Category"]);
            }
        }

        //Add selected stock to purchase to the cart when 'Add To Cart' button is pressed
        private void btnPurchaseAddtoCart_Click(object sender, EventArgs e)
        {
            bool contain = false;
            string[] listID = null;
            
            //To check is there a same product that is already in the list
            foreach (string it in listBoxStocktoPurchase.Items) {
                listID = it.Split(' ');
                if (listID[0].Equals(txtPurchaseProductID.Text)) {
                    contain = true;                                                 //Set variable contain to true if there is an existing record in the list
                }
            }
            if (contain){
                MessageBox.Show("Product Is Already Added to The Cart!!", "Purchase Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);      //Message to tell the user that the product is already added to the cart
            }
            else {
                listBoxStocktoPurchase.Items.Add(string.Format("{0,-5}{1,-38}{2}", txtPurchaseProductID.Text, txtPurchaseProductName.Text, nUpDownPurchaseQTY.Value));
                listBoxStocktoPurchase.SelectedIndex = 0;
            }
        }

        //Remove a product from the cart listbox when the 'Remove From Cart' button is clicked
        private void btnPurchaseRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (listBoxStocktoPurchase.Items.Count != 0) {
                listBoxStocktoPurchase.Items.RemoveAt(listBoxStocktoPurchase.SelectedIndex);
            }
        }

        //Purchase the stocks in the cart when the 'Purchase Stock' button is clicked
        private void btnPurchaseStock_Click(object sender, EventArgs e)
        {

            //Allows the user to purchase stock if the cart listbox is not empty
            if (listBoxStocktoPurchase.Items.Count != 0)
            {
                string[] listID = null;
                List<string> items = new List<string>();
                int totalQty = 0;

                //Add all the items in the listbox into a list
                foreach (string it in listBoxStocktoPurchase.Items)
                {
                    listID = it.Split(' ');
                    totalQty = Convert.ToUInt16(listID[listID.Length - 1]) + Convert.ToUInt16(Connection.getProduct(listID[0]));

                    items.Add(it);
                    Connection.UpdateProduct(totalQty, listID[0]);
                    totalQty = 0;
                    
                }

                MessageBox.Show("Stock Purchase Notice Sent!", "Stock Purchase", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PurchaseStock ps = new PurchaseStock(items);
                ps.ShowDialog();
                ShowStock(Convert.ToString(cmbBoxStockLevel.SelectedItem));
                ShowProducts(Convert.ToString(cmBoxCategory.SelectedItem));
                listBoxStocktoPurchase.Items.Clear();
                
            }
            else {
                MessageBox.Show("The Cart Is Empty!!", "Purchase Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //#############################################################################################################################
        //END OF PURCHASING ORDER TAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF HELP PAGE TAB SECTION
        //#############################################################################################################################
        
        //Shos a messagebox when 'Customer' button is clicked
        private void btnHelpCustomer_Click(object sender, EventArgs e)
        {
            string[] des = { "Operations for all types of employees:"," 1. View all existing Customers"," 2. Create a new Customer", " 3. Delete an existing Customer", " 4. Edit Customer Details",
                               "","Operations for Manager & Admin:", " 1. Edit Customer's credit limit", " 2. Change customer's type" };

            Help hp = new Help("Customer Management", des, "Employee, Manager, Admin");
            hp.ShowDialog();
        }


        //Shos a messagebox when 'Order' button is clicked
        private void btnHelpOrder_Click(object sender, EventArgs e)
        {
            string[] des = { "Operations that can be performed:", " 1. View all exisiting orders", " 2. Create new order", " 3. Edit order", " 4. Delete order",
                           " 5. Send payment notice to customer"};
            Help hp = new Help("Sales Order", des, "Employee, Manager, Admin");
            hp.ShowDialog();
        }

        //Shos a messagebox when 'Product' button is clicked
        private void btnHelpProduct_Click(object sender, EventArgs e)
        {
            string[] des = { "Operations that can be performed:", " 1. View all exisiting products", " 2. Add new product", " 3. Delete a product", " 4.Edit details of a product" };

            Help hp = new Help("Product", des, "Employee, Manager, Admin");
            hp.ShowDialog();
        }

        //Shos a messagebox when 'Purchase Stock' button is clicked
        private void btnHelpPurchaseStock_Click(object sender, EventArgs e)
        {
            string[] des = { "Operations that can be performed:", " 1. View products that out of stock or low in stock", " 2. Make an order to purchase more stock" };

            Help hp = new Help("Stock Purchasing", des, "Employee, Manager, Admin");
            hp.ShowDialog();
        }

        //Shos a messagebox when 'Employee' button is clicked
        private void btnHelpEmployee_Click(object sender, EventArgs e)
        {
            string[] des = { "Operations that can be performed:", " 1. View all existing employees", " 2. Add a new employee", " 3. Delete an exisiting employee", " 4. Edit employee details & account password"};

            Help hp = new Help("Employee Management", des, "Admin");
            hp.ShowDialog();
        }

        //Shos a messagebox when 'Sales Report' button is clicked
        private void btnHelpSalesReport_Click(object sender, EventArgs e)
        {
            string[] des = { "Operations that can be performed:", " 1. View the sales statistics for a particular day", " 2. The number of sales on a particular day", " 3. Total sales on a particular day",
                           " 4. The best selling product on a particular day", " 5. The employee with the best sales", " 6. Customer with the highest purchase", " 7. Detailed sales report on a particular day"};

            Help hp = new Help("Sales Report", des, "Admin, Manager");
            hp.ShowDialog();
        }
        //#############################################################################################################################
        //END OF HELP PAGE TAB SECTION
        //#############################################################################################################################


        //#############################################################################################################################
        //START OF ORDER PAGE TAB SECTION
        //#############################################################################################################################

        //Method to show all the orders made
        private void ShowOrder()
        {
            SqlCeDataReader readorder = null;

            listView1.Items.Clear();
            readorder = or.RetrievewithCompany();

            while (readorder.Read())
            {
                DateTime estiorder = DateTime.ParseExact(Convert.ToString(readorder["Date"]), "dd/MM/yyyy", null);
                DateTime date = estiorder.AddDays(2);
                string Date = date.Date.ToString("dd/MM/yyyy");

                ListViewItem item = new ListViewItem(Convert.ToString(readorder["OrderID"]));
                item.SubItems.Add(Convert.ToString(readorder["CompanyName"]));
                item.SubItems.Add(Convert.ToString(readorder["EmployeeID"]));
                item.SubItems.Add(Convert.ToString(readorder["Date"]));
                item.SubItems.Add(Convert.ToString(readorder["PaymentStatus"]));
                item.SubItems.Add(Convert.ToString(readorder["OrderStatus"]));
                item.SubItems.Add(Date);
                listView1.Items.Add(item);
            }
        }


        //Delete a selected order record when the 'Delete' button is clicked
        private void DeleteOrderbtn_Click(object sender, EventArgs e)
        {
            //Deletes the record if there is more than 0 records selected
            if (listView1.SelectedItems.Count > 0)
            {
                //Messagebox to confirm does the user want to delete the selected orders
                DialogResult msgBoxResults = MessageBox.Show("Are you sure you want to permanently delete the selected order(s)?", "Remove Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //Delete the record when the 'Yes' button is clicked
                if (msgBoxResults == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        listView1.Items.RemoveAt(item.Index);
                        po.DeleteProductOrder(item.SubItems[0].Text);
                        or.DeleteOrder(item.SubItems[0].Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose orders to be deleted.");
            }
            ShowOrder();    //Calls method ShowOrder() to reflect the changes made to the list of orders
        }

        //To Produce and send a receipt to the customer when 'Order Receipt' button is clicked
        private void orderReceiptbtn_Click(object sender, EventArgs e)
        {
            //To check is there any selected order records
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose orders to be displayed.");
            }
            else if (listView1.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {

                    int i = Convert.ToInt16(item.SubItems[0].Text);
                    DialogResult result = MessageBox.Show("Do You Want To Send This Payment Receipt To The Customer?", "Order Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == System.Windows.Forms.DialogResult.Yes) {
                        MessageBox.Show("Payment Receipt Sent!", "Order Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Receipt r = new Receipt(Convert.ToString(i));
                        r.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose only 1 order to be displayed.");
            }
        }

        // To send a payment notice to the customer if their order's payment status = "Not Paid"
        private void Paymentbtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose an order first.");
            }
            else if (listView1.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    if (item.SubItems[4].Text == "Not Paid")
                    {
                        SqlCeDataReader r = or.getCustomerEmail(Convert.ToInt16(item.SubItems[0].Text));

                        while (r.Read())
                        {
                            DialogResult msgBoxResults = MessageBox.Show("Do you want to send this payment notice to " + r["FirstName"] + " " + r["LastName"] + "'s email address: " + Convert.ToString(r["Email"]) + " ?", "Send Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (msgBoxResults == System.Windows.Forms.DialogResult.Yes)
                            {
                                MessageBox.Show("Order payment notice has been sent to " + r["FirstName"] + " " + r["LastName"] + "'s email address: " + Convert.ToString(r["Email"]));

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Customer have already paid for order.");       //Messagebox to show that the selected order has been paid and no notice would be sent to the customer
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose only 1 order to be displayed.");
            }
        }


        //Create a new order when 'New Order' button is clicked
        private void NewOrderbtn_Click(object sender, EventArgs e)
        {
            ChooseProductForm cpf = new ChooseProductForm(acc.EmployeeID);
            cpf.ShowDialog();
            ShowOrder();
            ShowProducts("All");        //Calls method ShowProducts() to reflect the changes made to the orders
        }

        //Edit a selected order record when the 'Edit' button is clicked
        private void EditOrderbtn_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose orders to be edited.");
            }
            else if (listView1.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    int i = Convert.ToInt16(item.SubItems[0].Text);
                    EditOrder editorder = new EditOrder(Convert.ToString(i));
                    editorder.ShowDialog();
                    ShowOrder();
                }
            }
            else
            {
                MessageBox.Show("Please choose only 1 order to be edited");
            }
        }
        //#############################################################################################################################
        //END OF ORDER PAGE TAB SECTION
        //#############################################################################################################################


    }
}

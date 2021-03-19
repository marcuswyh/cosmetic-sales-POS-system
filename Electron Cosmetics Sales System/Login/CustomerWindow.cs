using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Login
{
    public partial class CustomerWindow : Form
    {
        private Customer c = new Customer();
        private string AccountType{get;set;}
        private string OperationType { get; set; }
        private string CustomerID{get;set;}

        public CustomerWindow(){
            InitializeComponent();
        }

        public CustomerWindow(string accType, string type, string custID)
        {
            InitializeComponent();
            AccountType = accType;
            OperationType = type;
            CustomerID = custID;
        }

        private void CustomerWindow_Load(object sender, EventArgs e)
        {
            cmbBoxCustomerType.SelectedIndex = 0;
            cmbBoxState.SelectedIndex = 0;

            if (OperationType == "Edit")
            {
                if (AccountType == "Admin" || AccountType == "Manager")
                {
                    lblCreditLimit.Visible = true;
                    txtBoxCreditLimit.Visible = true;
                    
                }
                else {
                    cmbBoxCustomerType.Enabled = false;
                }
                btnUpdate.Visible = true;
                btnAdd.Visible = false;
                btnClear.Visible = false;
                lblTitle.Text = "Edit Customer";
                txtBoxCompany.Enabled = false;
                CustomerDetails();
            }
            else {
                lblTitle.Text = "New Customer";
            }
        }

        private void CustomerDetails() {
            SqlCeDataReader details = c.DisplayCustomers(CustomerID);

            while (details.Read()) {
                cmbBoxCustomerType.SelectedIndex = cmbBoxCustomerType.FindStringExact(Convert.ToString(details["CustomerType"]));
                cmbBoxState.SelectedIndex = cmbBoxState.FindStringExact(Convert.ToString(details["State"]));
                txtBoxCompany.Text = Convert.ToString(details["CompanyName"]);
                txtBoxCreditLimit.Text = Convert.ToString(details["CreditLimit"]);
                txtBoxFName.Text = Convert.ToString(details["FirstName"]);
                txtBoxLName.Text = Convert.ToString(details["LastName"]);
                txtBoxEmail.Text = Convert.ToString(details["Email"]);
                txtBoxPhoneNo.Text = Convert.ToString(details["PhoneNo"]);
                txtBoxAddr.Text = Convert.ToString(details["Address"]);
                txtCity.Text = Convert.ToString(details["City"]);
                txtZip.Text = Convert.ToString(details["Zip"]);
                
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void clearFields() {
            cmbBoxCustomerType.SelectedIndex = 0;
            cmbBoxState.SelectedIndex = 0;
            txtBoxCompany.Text = "";
            txtBoxAddr.Text = "";
            txtZip.Text = "";
            txtCity.Text = "";
            txtBoxFName.Text = "";
            txtBoxLName.Text = "";
            txtBoxEmail.Text = "";
            txtBoxPhoneNo.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            InsertUpdate();

        }

        private void InsertUpdate() {

            string company = txtBoxCompany.Text;
            string customertype = Convert.ToString(cmbBoxCustomerType.SelectedItem);
            string fName = txtBoxFName.Text;
            string lName = txtBoxLName.Text;
            string email = txtBoxEmail.Text;
            string phoneNo = txtBoxPhoneNo.Text;
            string addr = txtBoxAddr.Text;
            string city = txtCity.Text;
            string zip = txtZip.Text;
            string state = Convert.ToString(cmbBoxState.SelectedItem);
            string creditLimit = txtBoxCreditLimit.Text;

            StringBuilder errorMsg = new StringBuilder(50);

            if (company != "" && fName != "" && lName != "" && email != "" && phoneNo != "" && addr != "" && city != "" && zip != "")
            {
                if (OperationType == "Edit")
                {
                    if (customertype == "Normal")
                        creditLimit = "0";

                    c.UpdateCustomer(creditLimit, fName, lName, customertype, email, phoneNo, addr, city, zip, state, CustomerID);
                    MessageBox.Show("Customer Details Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else {
                    c.InsertNewCustomer(company, fName, lName, customertype, email, phoneNo, addr, city, zip, state);
                    MessageBox.Show("Successfully Added A New Customer", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }

                
            }
            else
            {

                if (company == "")
                    errorMsg.AppendLine("Company Name");
                if (fName == "")
                    errorMsg.AppendLine("First Name");
                if (lName == "")
                    errorMsg.AppendLine("Last Name");
                if (email == "")
                    errorMsg.AppendLine("Email");
                if (phoneNo == "")
                    errorMsg.AppendLine("Phone No.");
                if (addr == "")
                    errorMsg.AppendLine("Address");
                if (city == "")
                    errorMsg.AppendLine("City");
                if (zip == "")
                    errorMsg.AppendLine("Zip Code");

                string caption = "Missing Details!";
                string msg = string.Format("Please Enter The Following Details:\n\n{0}", errorMsg);


                MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            InsertUpdate();
        }

       

        

           
    }
}

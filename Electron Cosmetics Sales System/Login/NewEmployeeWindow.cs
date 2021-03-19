using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Globalization;

namespace Login
{
    public partial class NewEmployeeWindow : Form
    {
        private Account ep = new Account();

        private string EmployeeID { get; set; }
        private string OperationType { get; set; }
        private SqlCeDataReader Read = null;

 
        /*Add Data*/
         public NewEmployeeWindow(string operationType)
        {
            InitializeComponent();
            OperationType = operationType;

        }


        /*Edit Data*/
         public NewEmployeeWindow(string operationType, string employeeID)
         {
             InitializeComponent();
             OperationType = operationType;
             EmployeeID = employeeID;
         }

        /*Details*/
         private void EmployeeDetails()
         {
             Read = ep.getEmployee("One", EmployeeID,"");

             while (Read.Read())
             {
                 cmbBoxAccountType.SelectedItem = Convert.ToString(Read["AccountType"]);
                 txtName.Text = Convert.ToString(Read["Name"]);
                 cmbBoxGender.SelectedItem = Convert.ToString(Read["Sex"]);
                 dateTimePicker.Value = DateTime.ParseExact(Convert.ToString(Read["DateofBirth"]), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                 txtBoxContactNo.Text = Convert.ToString(Read["ContactNumber"]);
                 txtBoxAddr.Text = Convert.ToString(Read["Address"]);
                 txtBoxEmail.Text = Convert.ToString(Read["Email"]);
             }

         }


        private void NewEmployeeWindow_Load(object sender, EventArgs e)
        {
            cmbBoxAccountType.SelectedIndex = 0;
            dateTimePicker.MaxDate = DateTime.Now.AddYears(-15);

            string pageTitle = "";
            if (OperationType.Equals("Edit")) {
                pageTitle = "Edit Employee Details";

                txtName.Enabled = false;
                cmbBoxGender.Enabled = false;
                dateTimePicker.Enabled = false;


                btnUpdate.Visible = true;
                btnAdd.Visible = false;
                btnClear.Visible = false;

                EmployeeDetails();
            }
            else if (OperationType.Equals("Add")) {
                pageTitle = "Add New Employee";


                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnClear.Visible = true;
            }else if (OperationType.Equals("Details")){
                pageTitle = "Employee Details";

                txtBoxPassword.Visible = false;
                txtName.Enabled = false;
                cmbBoxGender.Enabled = false;
                dateTimePicker.Enabled = false;
                txtBoxAddr.Enabled = false;
                txtBoxContactNo.Enabled = false;
                txtBoxEmail.Enabled = false;
                cmbBoxAccountType.Enabled = false;
                lblPassword.Visible = false;
                txtPasswordVerify.Visible = false;
                lblConfirmPassword1.Visible = false;
                lblConfirmPassword2.Visible = false;

                btnUpdate.Visible = false;
                btnAdd.Visible = false;
                btnClear.Visible = false;

                EmployeeDetails();
            }

            lblPageTitle.Text = pageTitle;
        }

        private void clearDetails()
        {
            cmbBoxAccountType.SelectedIndex = 0;
            txtName.Text = "";
            cmbBoxGender.SelectedIndex = 0;
            txtBoxContactNo.Text = "";
            txtBoxAddr.Text = "";
            txtBoxEmail.Text = "";
            txtBoxPassword.Text = "";
            txtPasswordVerify.Text = "";

        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            string at, nm, sx, dob, number, ad, mail, pass, confirmPass;
            string caption;
            string msg;

            StringBuilder errorMsg = new StringBuilder(50);
            at = Convert.ToString(cmbBoxAccountType.SelectedItem);
            nm = txtName.Text;
            sx = Convert.ToString(cmbBoxGender.SelectedItem);
            
            dob = dateTimePicker.Value.Date.ToString("dd/MM/yyyy");
            number = txtBoxContactNo.Text;
            ad = txtBoxAddr.Text;
            mail = txtBoxEmail.Text;
            pass = txtBoxPassword.Text;
            confirmPass = txtPasswordVerify.Text;


            if (pass.Equals(confirmPass))
            {
                if (nm != "" && dob != "" && number != "" && ad != "" && mail != "" && pass != "")
                {
                    ep.InsertNewData(at, nm, sx, dob, number, ad, mail, pass);
                    msg = string.Format("Employee Is successfully added");
                    caption = "New Employee Data is Added!";

                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearDetails();
                }
                else
                {
                    if (nm == "")
                        errorMsg.AppendLine("Name");
                    if (dob == "")
                        errorMsg.AppendLine("Date Of Birth");
                    if (number == "")
                        errorMsg.AppendLine("Contact Number");
                    if (ad == "")
                        errorMsg.AppendLine("Address");
                    if (mail == "")
                        errorMsg.AppendLine("Email");
                    if (pass == "")
                        errorMsg.AppendLine("Password");

                    caption = "Missing Details!";
                    msg = string.Format("Please Enter Details:\n{0}", errorMsg);
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else {
                MessageBox.Show("Password Does Not Match, Please Re-enter your password!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtBoxPassword.Focus();
                txtBoxPassword.SelectAll();
                    
            }
         
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ad, mail, pass, number, confirmPass, accType;

            accType = Convert.ToString(cmbBoxAccountType.SelectedItem);
            number = txtBoxContactNo.Text;
            ad = txtBoxAddr.Text;
            mail = txtBoxEmail.Text;
            pass = txtBoxPassword.Text;
            confirmPass = txtPasswordVerify.Text;
            if (pass.Equals(confirmPass)) {
                ep.UpdateData(accType,number, ad, mail, pass, EmployeeID);
                MessageBox.Show("Account Details Updated!", "Employee Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Password Does Not Match, Please Re-enter your password!", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtBoxPassword.Focus();
                txtBoxPassword.SelectAll();

            }

            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearDetails();
        }

    }
}

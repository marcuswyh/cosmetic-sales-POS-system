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
    public partial class LoginRecordWindow : Form
    {
        LoginActivity la = new LoginActivity();
        
        public LoginRecordWindow()
        {
            InitializeComponent();
            
        }

        private void LoginRecord_Load(object sender, EventArgs e)
        {
            lblTitle.Text = string.Format("{0,-15} {1,-15} {2,-25} {3}", "Employee Name", "Employee ID", "Login Time", "Logout Time");
            SqlCeDataReader records = null;
            records = la.RetrieveRecord();      //Calls Method RetrieveRecord to obtain the records for the loginActitivy
                                                //The results is then assigned to variable records

            //Add each of the records into a list box
            while (records.Read()) {
                listBoxRecords.Items.Add(string.Format("{0,-15} {1,-15} {2,-25} {3}", records["Name"], records["EmployeeID"], records["LoginTime"], records["LogoutTime"]));
            }

        }
       
    }
}

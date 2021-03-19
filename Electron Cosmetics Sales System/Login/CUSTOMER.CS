using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace Login
{
    class Customer: DBConnection
    {
        //Overloaded method to get the customer records
        public SqlCeDataReader DisplayCustomers(string type, string search) { 
            
            //Retrieve customer's record based on their account type (Premium, Norma, All)
            string baseSQL = "SELECT CID, CompanyName, CustomerType,LastName,FirstName,PhoneNo FROM Customer";
            string SQL = "";
            switch (type) { 
                case "Premium":
                case "Normal":
                    SQL = string.Format("{0} WHERE CustomerType = '{1}' AND (CID LIKE '{2}' OR CompanyName LIKE '%{2}%')",baseSQL, type, search);
                    break;
                default:
                    SQL = string.Format("{0} WHERE (CID LIKE '{1}' OR CompanyName LIKE '%{1}%')  ", baseSQL, search);
                    break;
            
            }

            SqlCeCommand command = new SqlCeCommand(SQL, connection);
            return command.ExecuteReader();

        }

        //Overloaded method to get the customer records
        public SqlCeDataReader DisplayCustomers(string ID) {
            string SQL = string.Format("SELECT * FROM Customer WHERE CID ='{0}'", ID);
            SqlCeCommand command = new SqlCeCommand(SQL, connection);

            return command.ExecuteReader();
        }

        //Overloaded Method to update the customer's record
        public void UpdateCustomer(string creditLimit, string fName, string lName, string customerType, string email, string phoneNo, string addr, string city
            , string zip, string state, string custID) {

            SqlCeCommand command = new SqlCeCommand("UPDATE Customer SET CreditLimit = @crLimit, CustomerType = @cType, FirstName = @fName, LastName = @lName, Email = @email, PhoneNo = @phoneNo, Address = @addr, City = @city, Zip = @zip, State = @state WHERE CID = @ID ",connection);

            command.Parameters.Add("@cType", customerType);
            command.Parameters.Add("@crLimit", creditLimit);
            command.Parameters.Add("@fName", fName);
            command.Parameters.Add("@lName", lName);
            command.Parameters.Add("@email", email);
            command.Parameters.Add("@phoneNo", phoneNo);
            command.Parameters.Add("@addr", addr);
            command.Parameters.Add("@city", city);
            command.Parameters.Add("@zip", zip);
            command.Parameters.Add("@state", state);
            command.Parameters.Add("@ID", custID);

            command.ExecuteNonQuery();

        }

        //Overloaded method to update customer's credit limit
        public void UpdateCustomer(string credit, string cid) {
            SqlCeCommand command = new SqlCeCommand("UPDATE Customer SET CreditLimit = @credit WHERE CID = @cid", connection);

            command.Parameters.Add("@credit", credit);
            command.Parameters.Add("@cid", cid);

            command.ExecuteNonQuery();
            Console.WriteLine("Updated Credit!");
        }

        //Method to delete customer
        public void DeleteCustomer(string customerID) {
            SqlCeCommand command = new SqlCeCommand("DELETE FROM Customer WHERE CID = @CustomerID", connection);

            command.Parameters.Add("@CustomerID", customerID);

            command.ExecuteNonQuery();
        }

        //Method to insert new customer
        public void InsertNewCustomer(string company, string fName, string lName, string customerType, string email, string phoneNo, string addr, string city
            ,string zip, string state) {

            int creditLimit = 0;

            switch (customerType) { 
                case "Normal":
                    creditLimit = 0;
                    break;
                case "Premium":
                    creditLimit = 5000;
                    break;
            }

            SqlCeCommand command = new SqlCeCommand("INSERT INTO Customer(CustomerType,CompanyName,CreditLimit,FirstName,LastName,Email,PhoneNo,Address,City,Zip,State)" +
            "VALUES(@cType,@cName, @creditLimit, @fName,@lName,@email,@phoneNo, @addr,@city,@zip,@state)", connection);

            command.Parameters.Add("@cType", customerType);
            command.Parameters.Add("@cName", company);
            command.Parameters.Add("@creditLimit", creditLimit);
            command.Parameters.Add("@fName", fName);
            command.Parameters.Add("@lName", lName);
            command.Parameters.Add("@email", email);
            command.Parameters.Add("@phoneNo", phoneNo);
            command.Parameters.Add("@addr", addr);
            command.Parameters.Add("@city", city);
            command.Parameters.Add("@zip", zip);
            command.Parameters.Add("@state", state);

            command.ExecuteNonQuery();


        }

    }
}

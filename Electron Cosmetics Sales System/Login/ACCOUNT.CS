 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace Login
{
    public class Account : DBConnection
    {

        public String AccountType { get; private set; }
        public int EmployeeID { get; private set; }

        //Method to retrieve login crudentials
        public bool getAccount(string id, string pass)
        {
            bool found = false;

            try
            {
                SqlCeDataReader read = null;

                //SQL Query to retrieve the employee's details
                SqlCeCommand command = new SqlCeCommand("SELECT EmployeeID, Name, Password, AccountType FROM Account WHERE Name = @ID AND Password = @PASS;", connection);
                command.Parameters.Add("@ID", id);
                command.Parameters.Add("@PASS", pass);

                read = command.ExecuteReader();

                while (read.Read())
                {
                    found = true;
                    EmployeeID = Convert.ToInt16(read["EmployeeID"]);
                    Object ps = read["Password"];
                    Object ids = read["Name"];
                    AccountType = Convert.ToString(read["AccountType"]);
                }

                if (found)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed");
                return false;
            }

        }

        //Method to retrieve the employee's details
        public SqlCeDataReader getEmployee(string code, string employeeID, string search)
        {
            string SQL = null;
            string BaseSQL = "SELECT EmployeeID, AccountType, Name, Sex, DateofBirth, ContactNumber, Address, Email,Password FROM Account";
            try
            {
            	//Retrieve all the employee records from the database
                if (code.Equals("All"))
                {
                    SQL = string.Format("{0} WHERE EmployeeID LIKE '{1}' OR Name LIKE '%{1}%' ORDER BY AccountType", BaseSQL, search);
                }
                else if (code.Equals("One"))	//Retrieve a specific employee details
                {
                    SQL = string.Format("{0} WHERE EmployeeID = '{1}'", BaseSQL, employeeID);
                }
                else {							//Retrive all the employee records based on their Account Type
                    SQL = string.Format("{0} WHERE AccountType = '{1}' AND EmployeeID LIKE '{2}' OR Name LIKE '%{2}%'", BaseSQL, code,search);
                }
                

                SqlCeCommand cmd = new SqlCeCommand(SQL, connection);
                return cmd.ExecuteReader();
            }
            catch (SqlCeException e)
            {
                
                return null;
            }
        }


        
        //Method to insert a new employee record the the Account Table
        public void InsertNewData(string at, string nm, string sx, string dob, string contact, string ad, string em, string pass)
        {

            SqlCeCommand command = new SqlCeCommand("INSERT INTO Account(AccountType,Name,Sex,DateofBirth,ContactNumber,Address,Email,Password)" +
            "VALUES(@AccountType,@Name,@Sex,@DateofBirth,@ContactNumber,@Address,@Email,@Password);", connection);


            command.Parameters.Add("@AccountType", at);
            command.Parameters.Add("@Name", nm);
            command.Parameters.Add("@Sex", sx);
            command.Parameters.Add("@DateofBirth", dob);
            command.Parameters.Add("@ContactNumber", contact);
            command.Parameters.Add("@Address", ad);
            command.Parameters.Add("@Email", em);
            command.Parameters.Add("@Password", pass);

            command.ExecuteNonQuery();

        }

        //Method to update an employee's details
        public void UpdateData(string acType,string number, string ad, string em, string pass, string empID)
        {
            SqlCeCommand cmd = new SqlCeCommand("UPDATE Account SET AccountType =  @AccType, ContactNumber = @ContactNumber, Address = @Address, Email = @Email, Password = @Password WHERE EmployeeID = @ID", connection);

            cmd.Parameters.Add("@AccType", acType);
            cmd.Parameters.Add("@ContactNumber", number);
            cmd.Parameters.Add("@Address", ad);
            cmd.Parameters.Add("@Email", em);
            cmd.Parameters.Add("@Password", pass);
            cmd.Parameters.Add("@ID", empID);

            cmd.ExecuteNonQuery();
        }


        //Method to delete a employee's record
        public void DeleteEmployeeData(string Name)
        {
            SqlCeCommand command = new SqlCeCommand("DELETE FROM Account WHERE EmployeeID = @Name", connection);

            command.Parameters.Add("@Name", Name);

            command.ExecuteNonQuery();
        }
    }
}

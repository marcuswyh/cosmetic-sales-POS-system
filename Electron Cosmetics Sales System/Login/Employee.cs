using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Windows.Forms;


namespace Login
{
    public class Employee: DBConnection
    {
        
        
        
        public SqlCeDataReader getEmployee(string code, string employeeID)
        {
            string SQL = null;
            string BaseSQL = "SELECT EmployeeID, AccountType, Name, Sex, DatefBirth, CotactNumber, Address, Email FROM Account";
            try
            {
                if (code.Equals("All"))
                {
                    SQL = string.Format("{0} ORDER BY AccountType", BaseSQL);
                }
                else if (code.Equals("One"))
                {
                    SQL = string.Format("{0} WHERE EmployeeID = '{1}'",BaseSQL,employeeID);
                }

                SqlCeCommand cmd = new SqlCeCommand(SQL, connection);
                return cmd.ExecuteReader();
            }catch(SqlCeException e)
            {
                MessageBox.Show(e.Source + "\n" + e.Message + "\n" + e.StackTrace);
                return null;
            }
        }




        /*OK*/
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
            Console.WriteLine("New Product Added");
        }

        public void UpdateData(string sx, string dob, string number, String ad, String em, String pass )
        {
            SqlCeCommand cmd = new SqlCeCommand("ÜPDATE Account SET ContactNumber = @ContactNumber, Address = @Address, Email = @Email, Password = @Password");

            cmd.Parameters.Add("@ContactNumber", number);
            cmd.Parameters.Add("@Address", ad);
            cmd.Parameters.Add("@Email", em);
            cmd.Parameters.Add("@Password", pass);

            cmd.ExecuteNonQuery();
            Console.WriteLine("New Employee Data Added");
        }


        /*Done*/
        public void DeleteEmployeeData(string Name)
        {
            SqlCeCommand command = new SqlCeCommand("DELETE FROM Account WHERE Name = @Name", connection);


            command.Parameters.Add("@Name", Name);

            command.ExecuteNonQuery();
            Console.WriteLine("Deleted Selected Employee Data Details");
        }
 }
}

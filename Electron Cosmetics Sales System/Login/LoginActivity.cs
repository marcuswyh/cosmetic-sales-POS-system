using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace Login
{
    

    public class LoginActivity: DBConnection
    {

        //Method to retrieve the 10 latest loginActivity record
        public SqlCeDataReader RetrieveRecord()
        {      
            SqlCeCommand query = new SqlCeCommand("SELECT TOP(10) A.EmployeeID, A.Name, LA.LoginTime, LA.LogoutTime FROM LoginActivity AS LA, Account AS A WHERE A.EmployeeID = LA.EmployeeID ORDER BY LA.LoginCount DESC;", connection);
            
            return query.ExecuteReader();

        }

        //Insert a new login record the LoginActivity table
        public void AddRecord(int empID, string[] dateTime) {

            
            SqlCeCommand command = new SqlCeCommand("INSERT INTO LoginActivity(EmployeeID, LoginTime, LogoutTime)VALUES(@EID,@LINTime,@LOUTTime);", connection);


            command.Parameters.Add("@EID", empID);
            command.Parameters.Add("@LINTime", dateTime[0]);
            command.Parameters.Add("@LOUTTime", dateTime[1]);
            
            command.ExecuteNonQuery();
            Console.WriteLine("Success!");

            //START OF CHECKING, REMOVE WHEN SUBMISSION
            SqlCeDataReader read = null;
            command = new SqlCeCommand("SELECT * FROM LoginActivity;", connection);

            read = command.ExecuteReader();
            while (read.Read()) {
                Console.WriteLine("No: {0}\nEMP: {1}\nTime In: {2}\nTime Out: {3}", read["LoginCount"], read["EmployeeID"], read["LoginTime"], read["LogoutTime"]);
            }
            //END OF CHECKING, REMOVE WHEN SUBMISSION
        }

        //Method to count the exisiting number records in the system
        private int CountingRecord() {
            try {
                //SqlCeDataReader read = null;

                SqlCeCommand command = new SqlCeCommand("", connection);

                int val = Convert.ToInt16(command.ExecuteScalar());

                return val;
                
            }
            catch(Exception e) {
                return 0;
            }
            
        }
    }
}

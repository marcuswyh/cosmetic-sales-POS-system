using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;


namespace Login
{
    public class DBConnection
    {
        protected static SqlCeConnection connection;
        

        public DBConnection() { }

        //To Close the database connection
        public void Disconnect() {
            connection.Close();
            Console.WriteLine("Connection Closed !!");
        }

        //To open the database connection
        public string Connect() {
            try
            {
                //connection = new SqlCeConnection("Data Source="+ System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database.sdf"));

                connection = new SqlCeConnection(@"Data Source=|DataDirectory|\Database.sdf");
                
                connection.Open();
                Console.WriteLine("OPEN DATABASE !!!");
                return "Connected";
            }
            catch (SqlCeException e)
            {
                Console.WriteLine("Not Connected");
                connection = null;
                return "Not Connected";
            }
        
        }



 

       

    }
}

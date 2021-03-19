using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Windows.Forms;

namespace Login
{
    class Order: DBConnection
    {
        //Method to add new sales order into the SalesOrder table
        public void AddOrder(string cid, string eid, string date, string status, string paystatus)
        {

            SqlCeCommand command = new SqlCeCommand("INSERT INTO SalesOrder(CustomerID, EmployeeID, Date, OrderStatus,PaymentStatus)" +
            "VALUES(@CustomerID, @EmployeeID, @Date, @OrderStatus, @PaymentStatus);", connection);


            command.Parameters.Add("@CustomerID", cid);
            command.Parameters.Add("@EmployeeID", eid);
            command.Parameters.Add("@Date", date);
            command.Parameters.Add("@OrderStatus", status);
            command.Parameters.Add("@PaymentStatus", paystatus);

            command.ExecuteNonQuery();
            Console.WriteLine("New Order Added");
        }

        //Method to update a SalesOrder record
        public void UpdateOrder(string orderstatus, string paystatus, string orderid)
        {
            SqlCeCommand command = new SqlCeCommand("UPDATE SalesOrder SET PaymentStatus = @paystatus, OrderStatus = @orderstatus WHERE OrderID = @orderid", connection);

            command.Parameters.Add("@orderstatus", orderstatus);
            command.Parameters.Add("@paystatus", paystatus);
            command.Parameters.Add("@orderid", orderid);

            command.ExecuteNonQuery();
            Console.WriteLine("Updated Order!");
        }

        //Method to delete a SalesOrder record
        public void DeleteOrder(string orderID)
        {
            SqlCeCommand command = new SqlCeCommand("DELETE FROM SalesOrder WHERE OrderID = @OrderID", connection);

            command.Parameters.Add("@OrderID", orderID);

            command.ExecuteNonQuery();
            Console.WriteLine("Deleted Order!");
        }

        //Method to retrieve SalesOrder record
        public SqlCeDataReader Retrieve()
        {

            SqlCeDataReader read = null;

            SqlCeCommand command = new SqlCeCommand("SELECT * FROM SalesOrder;", connection);
            read = command.ExecuteReader();

            return read;
        }

        //Overloaded Method to retrieve SalesOrder record based on companyName
        public SqlCeDataReader RetrievewithCompany()
        {

            SqlCeDataReader read = null;

            SqlCeCommand command = new SqlCeCommand("SELECT so.*, cs.CompanyName FROM SalesOrder so, Customer cs WHERE so.CustomerID = cs.CID;", connection);
            read = command.ExecuteReader();

            return read;
        }

        //Overloaded Method to retrieve SalesOrder record based on companyName
        public SqlCeDataReader RetrievewithCompany(string orderid)
        {
            string sql = string.Format("SELECT so.*, cs.CompanyName FROM SalesOrder so, Customer cs WHERE so.CustomerID = cs.CID AND so.OrderID = {0}", orderid);
            SqlCeCommand command = new SqlCeCommand(sql, connection);
            return command.ExecuteReader();
        }

        //Method to retrieve the latest SalesOrder record
        public SqlCeDataReader lastrecord()
        {
            SqlCeDataReader read = null;

            SqlCeCommand command = new SqlCeCommand("SELECT TOP(1) * FROM SalesOrder ORDER BY OrderID DESC;", connection);
            read = command.ExecuteReader();

            return read;
        }

        public SqlCeDataReader getsingleorder(int orderid)
        {
            string sql = string.Format("SELECT * FROM SalesOrder WHERE OrderID = {0}", orderid);
            SqlCeCommand command = new SqlCeCommand(sql, connection);
            return command.ExecuteReader();
        }

        public SqlCeDataReader retrievereceiptquery(int orderid)
        {
            string sql = string.Format("SELECT so.*, cu.CompanyName, cu.[FirstName], cu.LastName, ac.Name FROM SalesOrder so, Customer cu, Account ac WHERE so.CustomerID = cu.CID AND so.EmployeeID = ac.EmployeeID AND so.OrderID = {0}", orderid);
            SqlCeCommand command = new SqlCeCommand(sql, connection);
            return command.ExecuteReader();
        }

        public SqlCeDataReader retrievereceiptlist(int orderid)
        {
            string sql = string.Format("SELECT po.*, p.Name, p.Price FROM Product_Order po, Product p WHERE po.ProductCode = p.ProductID AND po.OrderNum = {0}", orderid);
            SqlCeCommand command = new SqlCeCommand(sql, connection);
            return command.ExecuteReader();
        }

        public SqlCeDataReader getCustomerEmail(int orderid)
        {
            string sql = string.Format("SELECT so.CustomerID, cu.Email, cu.[FirstName], cu.LastName FROM SalesOrder so, Customer cu WHERE so.CustomerID = cu.CID AND so.OrderID = {0}", orderid);
            SqlCeCommand command = new SqlCeCommand(sql, connection);
            return command.ExecuteReader();
        }
    }
}

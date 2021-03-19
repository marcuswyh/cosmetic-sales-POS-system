using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Windows.Forms;

namespace Login
{
    class Product_Order:DBConnection
    {
        //Method to add new record into the Product_Order table
        public void AddProductOrder(string pid, string oid, int qty)
        {

            SqlCeCommand command = new SqlCeCommand("INSERT INTO Product_Order(ProductCode,OrderNum,Quantity)" +
            "VALUES(@pid, @oid, @qty);", connection);


            command.Parameters.Add("@pid", pid);
            command.Parameters.Add("@oid", oid);
            command.Parameters.Add("@qty", qty);

            command.ExecuteNonQuery();
            Console.WriteLine("New Product Order Added");
        }

        //Method to delete a record from Product_Order table
        public void DeleteProductOrder(string orderID)
        {
            SqlCeCommand command = new SqlCeCommand("DELETE FROM Product_Order WHERE OrderNum = @OrderID", connection);


            command.Parameters.Add("@OrderID", orderID);

            command.ExecuteNonQuery();
            Console.WriteLine("Deleted Order!");
        }

        //Method to retrieve records from Product_Order table
        public SqlCeDataReader Retrieve()
        {

            SqlCeDataReader read = null;

            SqlCeCommand command = new SqlCeCommand("SELECT * FROM Product_Order;", connection);
            read = command.ExecuteReader();

            return read;
        }

        public SqlCeDataReader RetrieveOrderPrice(string orderid)
        {
            string sql = string.Format("SELECT po.*, p.Price FROM Product_Order po, Product p WHERE OrderNum = {0}", orderid);

            SqlCeCommand command = new SqlCeCommand(sql, connection);

            return command.ExecuteReader();
        }
    }
}

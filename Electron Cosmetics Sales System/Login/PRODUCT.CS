using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Windows.Forms;

namespace Login
{
    public class Product : DBConnection
    {
        //To retrieve products from Product Table
        public SqlCeDataReader getProduct(string code, string productID,string search)
        {
            string SQL = null;
            string BaseSQL = "SELECT ProductID, Category,Name,Size,Price,Quantity FROM product";
            try
            {

                if (code.Equals("All"))
                {
                    SQL = string.Format("{0} WHERE (ProductID LIKE '{1}' OR Name LIKE '%{1}%') ORDER BY Category ", BaseSQL, search);
                }
                else if (code.Equals("One")) {
                    SQL = string.Format("{0} WHERE ProductID = '{1}' AND (ProductID LIKE '{2}' OR Name LIKE '%{2}%')", BaseSQL, productID,search);
                }else if (code.Equals("Stock")){
                    SQL = string.Format("{0} WHERE Quantity <= {1} ORDER BY Quantity", BaseSQL, search);
                }
                else
                {
                    SQL = string.Format("{0} WHERE Category = '{1}' AND (ProductID LIKE '{2}' OR Name LIKE '%{2}%')", BaseSQL, code,search);
                }

                

                SqlCeCommand command = new SqlCeCommand(SQL,connection);

                return command.ExecuteReader();

            }
            catch (SqlCeException e)
            {
                MessageBox.Show(e.Source + "\n" + e.Message + "\n" + e.StackTrace);
                return null;
            }

        }

        //Overloaded method to get product record from Product table based on ProductID
        public Object getProduct(string id) {
            SqlCeCommand command = new SqlCeCommand("SELECT Quantity FROM Product WHERE ProductID = @Product", connection);
            command.Parameters.Add("@Product", id);

            return command.ExecuteScalar();
        }

        //Method to retrieve the kinds of make up category
        public SqlCeDataReader ComboCategory()
        { 
            SqlCeCommand command = new SqlCeCommand("SELECT Category FROM Product GROUP BY Category;", connection);

            return command.ExecuteReader();
        }

        //Method to add new record into the Product table
        public void InsertNewProduct(string cat, string nm, string size, string price, int quantity) {

            SqlCeCommand command = new SqlCeCommand("INSERT INTO Product(Category,Name, Size, Price, Quantity)"+ 
            "VALUES(@Category, @Name, @Size, @Price, @Quantity);", connection);


            command.Parameters.Add("@Category", cat);
            command.Parameters.Add("@Name", nm);
            command.Parameters.Add("@Size", size);
            command.Parameters.Add("@Price", price);
            command.Parameters.Add("@Quantity", quantity);

            command.ExecuteNonQuery();
            Console.WriteLine("New Product Added");
        }

        //Overloaded Method to update a record in the Product table
        public void UpdateProduct(string size, string price, string productID) {
            SqlCeCommand command = new SqlCeCommand("UPDATE Product SET Size = @Size, Price = @Price WHERE ProductID = @ProductID", connection);

            command.Parameters.Add("@Size", size);
            command.Parameters.Add("@Price", price);
            command.Parameters.Add("@ProductID", productID);

            command.ExecuteNonQuery();
            Console.WriteLine("Updated Product!");
        }

        //Overloaded method to update a product's quantity 
        public void UpdateProduct(int qty, string pid) {
            SqlCeCommand command = new SqlCeCommand("UPDATE Product SET Quantity = @Qty WHERE ProductID = @ProductID", connection);
            command.Parameters.Add("@Qty", qty);
            command.Parameters.Add("@ProductID", pid);
            command.ExecuteNonQuery();
        }

        //Method to delete a product record from the Product table
        public void DeleteProduct(string productID) {
            SqlCeCommand command = new SqlCeCommand("DELETE FROM Product WHERE ProductID = @ProductID", connection);

            command.Parameters.Add("@ProductID", productID);

            command.ExecuteNonQuery();
            
        }
    }
}

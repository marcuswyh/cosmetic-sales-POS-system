using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace Login
{
    class SalesReport : DBConnection
    {
        
        private string Date, SQL;
       

        //Method to get the number of sales and total sales amount for a day
        public Object Report(string type, string date) {
            Date = date;
            
            switch (type) { 
                case "Date":
                    SQL = string.Format("SELECT COUNT({0}) AS Count FROM SalesOrder WHERE Date LIKE '{1}%'", type, Date);
                    break;
                case "Total Sale":
                    SQL = string.Format("SELECT SUM(P.Price * PO.Quantity) AS TotalSale FROM Product_Order AS PO," 
                        +"Product AS P, SalesOrder AS SO WHERE (PO.ProductCode = P.ProductID) AND (SO.OrderID = PO.OrderNum) AND SO.Date LIKE '{0}%'", Date);
                    break;
            
            }
           SqlCeCommand command = new SqlCeCommand(SQL, connection);
            
           return command.ExecuteScalar();        
        }

        //Method to find the Best selling product, Employee with the highest sales and customer with the highest sales
        public SqlCeDataReader ReportBest(string type, string date) {
            Date = date;


            switch (type) { 
                case "Best Product":
                    SQL = string.Format("SELECT TOP(1) PO.ProductCode,P.Name, SUM(PO.Quantity)AS Total FROM Product_Order AS PO, Product AS P, SalesOrder AS SO WHERE (PO.ProductCode = P.ProductID) AND (SO.OrderID = PO.OrderNum) AND SO.Date LIKE '{0}%'GROUP BY PO.ProductCode, P.Name ORDER BY Total DESC",Date);
                    break;
                case "Top Employee":
                    SQL = string.Format("SELECT TOP(1) E.EmployeeID, E.Name, SUM(P.Price * PO.Quantity) AS NoofSales FROM Account AS E, SalesOrder AS SO, Product AS P, Product_Order AS PO WHERE (P.ProductID = PO.ProductCode)AND (PO.OrderNum = SO.OrderID)AND (E.EmployeeID = SO.EmployeeID) AND SO.Date LIKE '{0}%' GROUP BY E.EmployeeID, E.Name ORDER BY NoofSales DESC",Date);
                    break;
                case "Top Customer":
                    SQL = string.Format("SELECT TOP(1) C.CID, C.CompanyName, SUM(P.Price * PO.Quantity) AS TotalSales FROM Customer AS C, Product AS P, Product_Order AS PO, SalesOrder AS SO WHERE (C.CID = SO.CustomerID) AND (P.ProductID = PO.ProductCode) AND (SO.OrderID = PO.OrderNUM) AND (SO.Date LIKE '{0}%') GROUP BY C.CID, C.CompanyName ORDER BY TotalSales DESC", Date);
                    break;
            }

            SqlCeCommand command = new SqlCeCommand(SQL, connection);
            return command.ExecuteReader();
            
        }

        
        public SqlCeDataReader CustomerSales(string date) {
            Date = date;

            SQL = string.Format("SELECT	SO.OrderID FROM  SalesOrder AS SO WHERE SO.Date LIKE '{0}%';", Date);

            SqlCeCommand command = new SqlCeCommand(SQL, connection);

            return command.ExecuteReader();
            
        }

        //Overloaded Method to retrieve the details for each SalesOrder record
        public SqlCeDataReader OrderDetail(string orderID, string date) {
            Date = date;

            SQL = string.Format("SELECT C.CID, C.CompanyName,C.PhoneNo, C.Address, C.City, C.Zip, C.State, SO.Date, SO.OrderID, PO.ProductCode, P.Name,P.Price, PO.Quantity, (P.Price * PO.Quantity) AS Total  FROM Customer AS C, Product AS P, Product_Order AS PO, SalesOrder AS SO WHERE (C.CID = SO.CustomerID) AND (SO.OrderID = PO.OrderNum) AND (PO.ProductCode = P.ProductID) AND (SO.Date LIKE '{0}%') AND SO.OrderID = '{1}'", Date, orderID);

            SqlCeCommand command = new SqlCeCommand(SQL, connection);

            return command.ExecuteReader();
        }

        //Overloaded method to retrieve the details for each SalesOrder record
        public SqlCeDataReader OrderDetail(string orderID)
        {
            

            SQL = string.Format("SELECT C.CompanyName,C.PhoneNo, C.Address, C.City, C.Zip, C.State, SO.Date, SO.OrderID, PO.ProductCode, P.Name,P.Price, PO.Quantity, (P.Price * PO.Quantity) AS Total  FROM Customer AS C, Product AS P, Product_Order AS PO, SalesOrder AS SO WHERE (C.CID = SO.CustomerID) AND (SO.OrderID = PO.OrderNum) AND (PO.ProductCode = P.ProductID) AND SO.OrderID = '{0}'", orderID);

            SqlCeCommand command = new SqlCeCommand(SQL, connection);

            return command.ExecuteReader();
        }


        

        
    }
}

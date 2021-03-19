using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace Login
{
    public partial class EditOrder : Form
    {
        private string OrderID { get; set; }
        private Order order = new Order();
        private Product_Order po = new Product_Order();
        private Customer cu = new Customer();

        
        public EditOrder(string orderid)
        {
            InitializeComponent();

            OrderID = orderid;

            getdetails(OrderID);
        }

        private void getdetails(string orderid)
        {
            SqlCeDataReader or = order.RetrievewithCompany(Convert.ToString(orderid));

            while (or.Read())
            {
                orderidtxt.Text = orderid;
                employeeidtxt.Text = Convert.ToString(or["EmployeeID"]);
                companytxt.Text = Convert.ToString(or["CompanyName"]);
                orderdatetxt.Text = Convert.ToString(or["Date"]);
                paystatuscombobox.Text = Convert.ToString(or["PaymentStatus"]);
                orderstatuscombobox.Text = Convert.ToString(or["OrderStatus"]);
            }
        }

      

        private void updateorderbtn_Click_1(object sender, EventArgs e)
        {
            string paystat = paystatuscombobox.Text;
            string orderstat = orderstatuscombobox.Text;
            string id = orderidtxt.Text;
            double newcredlimit = 0;
            double qty, price = 0;

            order.UpdateOrder(orderstat, paystat, id);

            if (paystat == "Paid")
            {
                SqlCeDataReader read = po.RetrieveOrderPrice(id);

                while (read.Read())
                {
                    qty = Convert.ToDouble(read["Quantity"]);
                    price = Convert.ToDouble(read["Price"]);

                    newcredlimit = newcredlimit + (qty * price);
                }

                read = order.getsingleorder(Convert.ToInt32(id));

                while (read.Read())
                {
                    string custid = Convert.ToString(read["CustomerID"]);

                    cu.UpdateCustomer(Convert.ToString(newcredlimit), custid);
                }

            }

            MessageBox.Show("Order updated!");
            this.Close();
        }
    }
}

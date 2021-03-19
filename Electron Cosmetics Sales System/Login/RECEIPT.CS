using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Login
{
    public partial class Receipt : Form
    {

        private string OrderID { get; set; }
        private SalesReport sr = new SalesReport();
        private decimal TotalPayable { get; set; }

        public Receipt(string id)
        {
            InitializeComponent();
            OrderID = id;
            TotalPayable = 0M;
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            SqlCeDataReader order = sr.OrderDetail(OrderID);
            Console.WriteLine(OrderID);

            while(order.Read()){
                
                lblCompanyName.Text = Convert.ToString(order["CompanyName"]);
                lblOrderNum.Text = OrderID;
                lblInvoiceDate.Text = Convert.ToString(order["Date"]);
                lblAddr.Text = Convert.ToString(order["Address"]);
                lblZip.Text = Convert.ToString(order["Zip"]);
                lblCity.Text = Convert.ToString(order["City"]);
                lblState.Text = Convert.ToString(order["State"]);

                lblContactNo.Text = Convert.ToString(order["PhoneNo"]);

                TotalPayable += Convert.ToDecimal(order["Total"]);

                rTxtBoxItems.AppendText(string.Format("{0,-13}{1,-36}{2,-20:C}{3}{4,24:C}\n", order["ProductCode"], order["Name"], order["Price"], order["Quantity"], order["Total"] ));
            
            }

            rTxtBoxItems.AppendText(string.Format("\n\n{0,79}{1,16:C}", "Total incl GST", TotalPayable));
        }
    }
}

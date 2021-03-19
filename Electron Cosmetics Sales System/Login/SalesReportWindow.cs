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
    public partial class SalesReportWindow : Form
    {
        SalesReport sr = new SalesReport();
        private String Date { get; set; }

        public SalesReportWindow(string dt)
        {
            InitializeComponent();
            Date = dt;
        }

        private void SalesReportWindow_Load(object sender, EventArgs e)
        {
            List<List<String>> report = new List<List<String>>();


            SqlCeDataReader OrderResults, IndividualOrders;
            OrderResults = sr.CustomerSales(Date);
            bool first = true;
            decimal totalSale = 0, productTotal = 0;
            

            while(OrderResults.Read()){
                String OrderId = Convert.ToString(OrderResults["OrderID"]);
                IndividualOrders = sr.OrderDetail(OrderId,Date);
                while (IndividualOrders.Read()) {
                    if (first == true) {
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Bold);
                        rctBoxReport.AppendText(String.Format("Order ID    : "));
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Regular);
                        rctBoxReport.AppendText(Convert.ToString(IndividualOrders["OrderID"]));
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Bold);
                        rctBoxReport.AppendText(String.Format("\nCustomer ID : "));
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Regular);
                        rctBoxReport.AppendText(Convert.ToString(IndividualOrders["CID"]));
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Bold);
                        rctBoxReport.AppendText(String.Format("\nCompany Name: "));
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Regular);
                        rctBoxReport.AppendText(Convert.ToString(IndividualOrders["CompanyName"]));
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Bold);
                        rctBoxReport.AppendText(string.Format("\n\n{0,-15} {1,-35} {2,-10:C} {3,-8} {4:C}\n", "Product ID", "Name", "Price", "Qty", "Total"));
                        rctBoxReport.AppendText("_______________________________________________________________________________\n");
                        rctBoxReport.SelectionFont = new Font(rctBoxReport.Font, FontStyle.Regular);
                        first = false;
                    }
                    productTotal = Convert.ToDecimal(IndividualOrders["Total"]);
                    totalSale += productTotal;

                    rctBoxReport.AppendText(String.Format("{0,-15} {1,-35} {2,-10:C} {3,-8} {4:C}\n", IndividualOrders["ProductCode"],
                        IndividualOrders["Name"], Convert.ToDecimal(IndividualOrders["Price"]), IndividualOrders["Quantity"], productTotal ));
               
                }

                rctBoxReport.AppendText("_______________________________________________________________________________\n");
                rctBoxReport.AppendText(string.Format("{0,68} {1,10:C}", "Total", totalSale));
                totalSale = 0M;
                rctBoxReport.AppendText("\n\n\n\n\n");
                
                first = true;
            }

        }
    }
}

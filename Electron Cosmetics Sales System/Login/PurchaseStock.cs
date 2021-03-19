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
    public partial class PurchaseStock : Form
    {
        private static DateTime now = DateTime.Now;
        private List<string> Products = new List<string>();
        Product pd = new Product();

        public PurchaseStock(List<string> list)
        {
            InitializeComponent();
            Products = list;
           
        }

        private void PurchaseStock_Load(object sender, EventArgs e)
        {
            lblOrderDate.Text = now.ToString("dd/MM/yyyy");

            string[] id = null;
            decimal unitPrice = 0M;
            decimal unitPriceQuantity = 0M;
            decimal total = 0M;
            SqlCeDataReader result;
            lblTitle.Text = string.Format(" {0,-5}{1,-36}{2,-19}{3,-15}{4}", "ID", "Name", "Quantity", "Unit Price", "Total");

            foreach (string it in Products) {
                id = it.Split(' ');

                result = pd.getProduct("One", id[0], "");
                while (result.Read()) {
                    unitPrice = Convert.ToDecimal(result["Price"]);
                    unitPrice = unitPrice * 0.6M;
                }
                unitPriceQuantity = unitPrice * Convert.ToDecimal((id[id.Length - 1]));
                total += unitPriceQuantity;

                rTxtBoxOrder.AppendText(string.Format(" {0,-60}{1,-15:C}{2:C}\n", it, unitPrice, unitPriceQuantity));

                unitPrice = 0M;
                unitPriceQuantity = 0M;

            }
            rTxtBoxOrder.AppendText(string.Format("_____________________________________________________________________________________"));
            rTxtBoxOrder.AppendText(string.Format("\n{0,70}{1,15:C}","Total", total));
        }
    }
}

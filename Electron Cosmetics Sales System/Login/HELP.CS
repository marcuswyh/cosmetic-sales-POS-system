using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class Help : Form
    {
        private string Title { get; set; }
        private string[] Description { get; set; }
        private string Access { get; set; }

        public Help(string title, string[] description, string access)
        {
            InitializeComponent();

            Title = title;
            Description = description;
            Access = access;
        }

        private void Help_Load(object sender, EventArgs e)
        {
            lblPageTitle.Text = Title;
            lblAccessible.Text = Access;

            foreach (string st in Description) { 
                rTxtBoxDiscription.AppendText(string.Format("{0}\n",st));
            }
        }
    }
}

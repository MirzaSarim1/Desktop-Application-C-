using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class Form11 : Form
    {
        string supplierid;
        public Form11()
        {
            InitializeComponent();
            supplierid = "";
        }
        public Form11(string supplierid)
        {
            InitializeComponent();
            this.supplierid = supplierid;
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

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
    public partial class Form2 : Form
    {
        public List<Product> Products;
        public List<Supplier> Suppliers;
        public Form2()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        public Form2(List<Product> Products,List<Supplier> Suppliers)
        {
            InitializeComponent();
            this.Products = Products;
            this.Suppliers = Suppliers;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f3 = new Form3(Products,Suppliers);
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f4 = new Form4(Products, Suppliers);
            f4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f5 = new Form5(Products, Suppliers);
            f5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f6 = new Form6(Products, Suppliers);
            f6.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f7 = new Form7(Products,Suppliers);
            f7.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f8 = new Form8(Products, Suppliers);
            f8.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f9 = new Form9(Products, Suppliers);
            f9.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f10 = new Form10(Products, Suppliers);
            f10.Show();
        }
    }
}

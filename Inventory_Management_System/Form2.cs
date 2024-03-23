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

        public Form2()
        {
            InitializeComponent();
            Products = new List<Product>();
        }

        public Form2(List<Product> Products)
        {
            InitializeComponent();
            this.Products = Products;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f3 = new Form3(Products);
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f4 = new Form4(Products);
            f4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f5 = new Form5(Products);
            f5.Show();
        }
    }
}

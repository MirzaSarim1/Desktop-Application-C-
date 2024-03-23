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
    public partial class Form8 : Form
    {
        public List<Product> Products;
        public List<Supplier> Suppliers;
        public Form8()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        public Form8(List<Product> Products, List<Supplier> Suppliers)
        {
            InitializeComponent();
            this.Products = Products;
            this.Suppliers = Suppliers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> SearchedProducts = new List<Product>();
            string search = textBox1.Text;
            int search_reorder = -1;
            bool number = false;
            if (int.TryParse(textBox1.Text, out search_reorder))
            {
                // if user enter any number it assign to search_reorder we use it to compare with Reorder_Level
                number = true;
            }
            //int search_count = 0;


            for (int i = 0; i < Products.Count(); i++)
            {
                if (search == Products[i].get_Name() || search == Products[i].get_Category() || search == Products[i].get_SupplierID())
                {
                    SearchedProducts.Add(Products[i]);
                }
                else if (number && search_reorder == Products[i].get_ReorderLevel())
                {
                    SearchedProducts.Add(Products[i]);
                }
            }
            dataGridView1.Rows.Clear();
            Functions.add_data_to_grid(SearchedProducts, dataGridView1);
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f2 = new Form2(Products, Suppliers);
            f2.Show();
        }
    }
}

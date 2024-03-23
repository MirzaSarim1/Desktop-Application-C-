﻿using System;
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
    public partial class Form5 : Form
    {
        public List<Product> Products;
        public List<Supplier> Suppliers;
        public Form5()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        public Form5(List<Product> Products, List<Supplier> Suppliers)
        {
            InitializeComponent();
            this.Products = Products;
            this.Suppliers = Suppliers;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f2 = new Form2(Products,Suppliers);
            f2.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Functions.add_data_to_grid(Products, dataGridView1);
        }
    }
}

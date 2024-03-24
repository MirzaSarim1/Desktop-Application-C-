using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class Form9 : Form
    {
        OracleConnection con;
        Product Products_var = new Product();
        Supplier Suppliers_var = new Supplier();
        int U_id;

        //Functions functionHandler;
        public List<Product> Products;
        public List<Supplier> Suppliers;
        public Form9()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }
        public Form9(List<Product> Products, List<Supplier> Suppliers)
        {
            InitializeComponent();
            this.Products = Products;
            this.Suppliers = Suppliers;
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            Functions.add_data_to_grid(Products, dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f2 = new Form2(Products, Suppliers);
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";

            int id = Convert.ToInt32(textBox1.Text);
            string sqlQuery = "SELECT * FROM Products Where ProductID = :id";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        con.Open();
                        cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            name.Text = reader["ProductName"].ToString();
                            textBox2.Text = reader["Cost"].ToString();
                            textBox3.Text = reader["StockQuantity"].ToString();
                            textBox4.Text = reader["SupplierID"].ToString();
                            textBox5.Text = reader["Category"].ToString();
                            textBox6.Text = reader["reorderLevel"].ToString();

                        }
                        else
                        {
                            MessageBox.Show("No Record Found!");
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                    // Handle the exception
                }
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            Products_var.set_ID(id);
            Products_var.set_Name(name.Text);
            Products_var.set_cost(Convert.ToDouble(textBox2.Text));
            Products_var.set_SupplierID(textBox4.Text);
            Products_var.set_StockQuantity(Convert.ToInt32(textBox3.Text));
            Products_var.set_Category(textBox5.Text);
            Products_var.set_ReorderLevel(Convert.ToInt32(textBox6.Text));



            //MessageBox.Show(Convert.ToString(Products_var.get_ReorderLevel()));
            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    string sqlQuery = "UPDATE Products SET ProductName = :name, Cost = :cost, StockQuantity = :quantity, ReorderLevel = :reorderlevel, Category = :Category, SupplierID = :supp_id WHERE ProductID = :id";

                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        con.Open();
                        cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = Products_var.get_Name();
                        cmd.Parameters.Add("cost", OracleDbType.Double).Value = Products_var.get_cost();
                        cmd.Parameters.Add("quantity", OracleDbType.Int32).Value = Products_var.get_StockQuantity();
                        cmd.Parameters.Add("id", OracleDbType.Int32).Value = Products_var.get_ID();
                        cmd.Parameters.Add("supp_id", OracleDbType.Varchar2).Value = Products_var.get_SupplierID();
                        cmd.Parameters.Add("reorderlevel", OracleDbType.Int32).Value = Products_var.get_ReorderLevel();
                        cmd.Parameters.Add("Category", OracleDbType.Varchar2).Value = Products_var.get_Category();
                        cmd.ExecuteNonQuery();
                        Functions.UpdateProductById(Products, Products_var, Products_var.get_ID());
                        dataGridView1.Rows.Clear();
                        Functions.add_data_to_grid(Products, dataGridView1);

                        MessageBox.Show("Product Details Updated Successfully!");
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                    // Handle the exception
                }
            }
        }
    }
}

using Oracle.ManagedDataAccess.Client;
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
    public partial class Form3 : Form
    {
        private static int id;
        public List<Product> Products;
        public List<Supplier> Suppliers;
        public Form3()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        public Form3(List<Product> Products, List<Supplier> Suppliers)
        {
            InitializeComponent();
            this.Products = Products;
            this.Suppliers = Suppliers;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int id = 0; // Declare id variable

            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
            //using (OracleConnection con = new OracleConnection(connectionString))
            //{
            //    try
            //    {
            //        con.Open();
            //        string sqlQueryCount = "SELECT COUNT(*) FROM Products";

            //        using (OracleCommand cmd = new OracleCommand(sqlQueryCount, con))
            //        {
            //            id = Convert.ToInt32(cmd.ExecuteScalar()) + 1; // Retrieve total rows count and increment for the next product
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: " + ex.Message, "Error");
            //    }
            //}

            //..........................................................................................
            int maxid = 0;

            // Execute SQL query to get the maximum ProductID from the Products table
            string sqlQueryCount = "SELECT MAX(ProductID) FROM Products";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (OracleCommand cmd = new OracleCommand(sqlQueryCount, con))
                    {
                        maxid = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                    // Handle the exception
                }
            }

            int id = maxid + 1; // Initialize variable to hold the missing ProductID

            // Check for missing ProductID within the range of 1 to maxid
            for (int i = 1; i <= maxid; i++)
            {
                // Execute SQL query to check if the product ID exists in the Products table
                string sqlQueryCheck = "SELECT COUNT(*) FROM Products WHERE ProductID = :productId";
                using (OracleConnection con2 = new OracleConnection(connectionString))
                {
                    try
                    {
                        con2.Open();
                        using (OracleCommand cmd = new OracleCommand(sqlQueryCheck, con2))
                        {
                            cmd.Parameters.Add("productId", OracleDbType.Int32).Value = i;
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count == 0)
                            {
                                id = i; // Assign the missing ProductID
                                break; // Exit loop if missing ProductID is found
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                        // Handle the exception
                    }
                }
            }

            //..........................................................................................

            // Read product details from textboxes
            string Name = textBox1.Text;
            double cost = Convert.ToDouble(textBox2.Text);
            int stockQuantity = Convert.ToInt32(textBox3.Text);
            string suppID = textBox4.Text;
            string category = textBox5.Text;
            int reorderLevel = Convert.ToInt32(textBox6.Text);

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)
                || string.IsNullOrEmpty(suppID) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Enter Values in All fields");
            }
            else
            {
                if (!Functions.SupplierExists(suppID)) // Check if the supplier exists
                {
                    MessageBox.Show("No supplier available with this SupplierID.");
                }
                else
                {
                    using (OracleConnection con = new OracleConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            string sqlQueryInsert = "INSERT INTO Products (ProductID, ProductName, Cost, StockQuantity, SupplierID, Category, ReorderLevel) " +
                                "VALUES (:id, :Name, :Cost, :StockQuantity, :SuppID, :Category, :ReorderLevel)";

                            using (OracleCommand cmd = new OracleCommand(sqlQueryInsert, con))
                            {
                                // Add parameters
                                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id; // Use the incremented id for the next product
                                cmd.Parameters.Add("Name", OracleDbType.Varchar2).Value = Name;
                                cmd.Parameters.Add("Cost", OracleDbType.Double).Value = cost;
                                cmd.Parameters.Add("StockQuantity", OracleDbType.Int32).Value = stockQuantity;
                                cmd.Parameters.Add("SuppID", OracleDbType.Varchar2).Value = suppID;
                                cmd.Parameters.Add("Category", OracleDbType.Varchar2).Value = category;
                                cmd.Parameters.Add("ReorderLevel", OracleDbType.Int32).Value = reorderLevel;

                                cmd.ExecuteNonQuery(); // Execute the query
                                Product newProduct= new Product();
                                newProduct.set_ID(id);
                                newProduct.set_Name(Name);
                                newProduct.set_cost(cost);
                                newProduct.set_StockQuantity(stockQuantity);
                                newProduct.set_SupplierID(suppID);
                                newProduct.set_Category(category);
                                newProduct.set_ReorderLevel(reorderLevel);
                                Products.Insert(id - 1, newProduct);
                                MessageBox.Show("Product Added");
                            }
                        }
                        catch (OracleException ex)
                        {
                            if (ex.Number == 1) // ORA-00001: unique constraint violated
                            {
                                MessageBox.Show("A product with the same name already exists.");
                            }
                            else
                            {
                                MessageBox.Show("Error: " + ex.Message, "Error");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error");
                        }
                    }
                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f2 = new Form2(Products,Suppliers);
            f2.Show();
        }
    }
}

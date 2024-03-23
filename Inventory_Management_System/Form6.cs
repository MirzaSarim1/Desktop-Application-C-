using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory_Management_System
{
    public partial class Form6 : Form
    {
        public List<Product> Products;
        public List<Supplier> Suppliers;
        int place;
        public Form6()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        public Form6(List<Product> Products, List<Supplier> Suppliers)
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

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlQueryInsert = "Select COUNT(*) From Supplier";

                    using (OracleCommand cmd = new OracleCommand(sqlQueryInsert, con))
                    {
                        // Add parameters

                        place = Convert.ToInt32(cmd.ExecuteScalar()); // Execute the query


                    }
                }
                catch (OracleException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }


            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string id = textBox1.Text;
                string name = textBox2.Text;
                string location = textBox3.Text;

                try
                {
                    con.Open();
                    string sqlQueryInsert = "INSERT INTO Supplier (SupplierID, SupplierName, Location) " +
                        "VALUES (:Supp_id, :Supp_Name, :Location)";

                    using (OracleCommand cmd = new OracleCommand(sqlQueryInsert, con))
                    {
                        // Add parameters
                        cmd.Parameters.Add("Supp_id", OracleDbType.Varchar2).Value = id; 
                        cmd.Parameters.Add("Supp_Name", OracleDbType.Varchar2).Value = name;
                        cmd.Parameters.Add("Location", OracleDbType.Varchar2).Value = location;

                        cmd.ExecuteNonQuery(); // Execute the query

                        Supplier newSupplier = new Supplier();
                        newSupplier.set_SupplierID(id);
                        newSupplier.set_Name(name);
                        newSupplier.set_Location(location);
                        Suppliers.Insert(place, newSupplier);
                        MessageBox.Show("Supplier Added");
                    }
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 1) // ORA-00001: unique constraint violated
                    {
                        MessageBox.Show("A Supplier with the same ID already exists.");
                    }
                    else
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }
                }
            }

        }
    }
}

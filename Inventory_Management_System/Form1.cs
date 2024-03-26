using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Inventory_Management_System
{
    public partial class Form1 : Form
    {

        OracleConnection con;
        Product Products_var = new Product();
        Supplier Suppliers_var = new Supplier();
        int login_as = 0;
        string supplierid = "";

        //Functions functionHandler;
        public List<Product> Products;
        public List<Supplier> Suppliers;
        public Form1()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (login_as == 0)
            {
                MessageBox.Show("Must select Login As option");
            }
            else if(login_as == 1)
            {
                string enteredUsername = textBox1.Text;
                string enteredPassword = Functions.Md5Encry(textBox2.Text);
                if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
                {
                    MessageBox.Show("Please enter both Username and Password.");
                    return;
                }
                string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string sqlQuery = "SELECT COUNT(*) FROM userlogin WHERE Username = :username AND Password = :password";

                        using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                        {
                            cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = enteredUsername;
                            cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = enteredPassword;

                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (count > 0)
                            {
                                List<Product> Products = Products_var.LoadProducts();
                                List<Supplier> Suppliers = Suppliers_var.LoadSuppliers();
                                MessageBox.Show("Login");
                                Form f2 = new Form2(Products, Suppliers);
                                this.Hide();
                                f2.Show();
                            }
                            else
                            {
                                MessageBox.Show("Wrong username or password.");
                                //string hashedPassword = Functions.Md5Encry(textBox2.Text);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }
                }
            }
            else
            {
                string enteredUsername = textBox1.Text;
                string enteredPassword = Functions.Md5Encry(textBox2.Text);
                if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
                {
                    MessageBox.Show("Please enter both Username and Password.");
                    return;
                }
                string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        string sqlQuery = "SELECT COUNT(*) FROM Supplier WHERE SupplierID = :SupplierID AND Password = :password";

                        using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                        {
                            cmd.Parameters.Add("SupplierID", OracleDbType.Varchar2).Value = enteredUsername;
                            cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = enteredPassword;

                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                            if (count > 0)
                            {
                                supplierid = enteredUsername;
                                MessageBox.Show("Login");
                                Form f11 = new Form11(supplierid);
                                this.Hide();
                                f11.Show();
                            }
                            else
                            {
                                MessageBox.Show("Wrong username or password.");
                                //string hashedPassword = Functions.Md5Encry(textBox2.Text);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error");
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            login_as = 1;
            label5.Text = "User";
            label1.Text = "Username";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            login_as= 2;
            label5.Text = "Supplier";
            label1.Text = "Supplier ID";
        }
    }
}

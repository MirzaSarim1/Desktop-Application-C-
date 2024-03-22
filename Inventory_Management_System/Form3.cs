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
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
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
                    string sqlQuery = "SELECT COUNT(*) FROM Products";

                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        //cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = enteredUsername;
                        //cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = enteredPassword;

                        id = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }
            string Name = textBox1.Text;
            double cost = Convert.ToDouble(textBox2.Text);
            int stockQunatity = Convert.ToInt32(textBox3.Text);
            string suppID = textBox4.Text;
            string category = textBox5.Text;
            int reoderLevel = Convert.ToInt32(textBox6.Text);
            if (!(string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text)
                && string.IsNullOrEmpty(suppID) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(textBox6.Text)))
            {
                
            }
            else
            {
                MessageBox.Show("Enter Values in All fields");
            }
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO Products (ProductID, ProductName, Cost, StockQuantity, SupplierID, Category, ReorderLevel) " +
                        "VALUES (:id, :Name, :Cost, :StockQuantity, :SuppID, :Category, :reorderLvl);";

                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = id;
                        cmd.Parameters.Add("Cost", OracleDbType.Varchar2).Value = cost;
                        cmd.Parameters.Add("StockQuantity", OracleDbType.Varchar2).Value = stockQunatity;
                        cmd.Parameters.Add("SuppID", OracleDbType.Varchar2).Value = suppID;
                        cmd.Parameters.Add("Category", OracleDbType.Varchar2).Value = category;
                        cmd.Parameters.Add("reorderLvl", OracleDbType.Varchar2).Value = reoderLevel;
                        cmd.ExecuteScalar();
                        con.Close();

                        Application.Exit();

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

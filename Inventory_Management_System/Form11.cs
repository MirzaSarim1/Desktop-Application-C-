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
    public partial class Form11 : Form
    {
        string supplierid;
        Order order_var = new Order();
        public List<Product> Products;
        public List<Order> Orders;
        public Form11()
        {
            InitializeComponent();
            supplierid = "";
            Orders = new List<Order>();
            Products = new List<Product>();

        }
        public Form11(string supplierid, List<Product> products)
        {
            InitializeComponent();
            this.supplierid = supplierid;
            this.Products = products;
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            Orders = order_var.LoadOrder();
            Functions.add_data_to_grid_order(Orders, this.supplierid, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Accept") 
            {
                int id,StokADD = 0;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductId"].Value);
                string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    //MessageBox.Show(Convert.ToString(Products.Count()));
                    for (int i=0; i< Products.Count(); i++)
                    {
                        if (Products[i].get_ID() == id)
                        {
                            StokADD = Products[i].get_StockQuantity() + 100;
                            Products[i].set_StockQuantity(StokADD);
                        }
                    }
                    string sqlQuery = "UPDATE Products Set StockQuantity = :StokADD WHERE ProductID = :id AND SupplierID = :supp";

                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        try
                        {
                            cmd.Parameters.Add("StokADD", OracleDbType.Int32).Value = StokADD;
                            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                            cmd.Parameters.Add("supp", OracleDbType.Varchar2).Value = this.supplierid;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Order Accept SuccessFully!");

                            string sqlQuery1 = "Delete From SupplierProducts WHERE ProductID = :id AND SupplierID = :supp";
                            using (OracleCommand cmd1 = new OracleCommand(sqlQuery1, con))
                            {
                                try
                                {
                                    cmd1.Parameters.Add("id", OracleDbType.Int32).Value = id;
                                    cmd1.Parameters.Add("supp", OracleDbType.Varchar2).Value = this.supplierid;
                                    cmd1.ExecuteNonQuery();
                                    dataGridView1.Rows.Clear();
                                    Orders = order_var.LoadOrder();
                                    Functions.add_data_to_grid_order(Orders, this.supplierid, dataGridView1);
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error: " + ex.Message);
                                }
                            }

                            dataGridView1.Rows.Clear();
                            Orders = order_var.LoadOrder();
                            Functions.add_data_to_grid_order(Orders, this.supplierid, dataGridView1);
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        con.Close();
                    }
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Decline")
            {
                int id;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductId"].Value);
                string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";

                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "Delete From SupplierProducts WHERE ProductID = :id AND SupplierID = :supp";

                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        try
                        {
                            cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;
                            cmd.Parameters.Add("supp", OracleDbType.Varchar2).Value = this.supplierid;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Order Declined SuccessFully!");
                            dataGridView1.Rows.Clear();
                            Orders = order_var.LoadOrder();
                            Functions.add_data_to_grid_order(Orders, this.supplierid, dataGridView1);
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}

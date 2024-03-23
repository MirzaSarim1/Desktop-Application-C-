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

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> SearchedProducts = new List<Product>();
            int search = Convert.ToInt32(textBox1.Text);
            //int search_reorder = -1;

            //int search_count = 0;


            for (int i = 0; i < Products.Count(); i++)
            {
                if (search == Products[i].get_ID())
                {
                    SearchedProducts.Add(Products[i]);
                }
            }
            dataGridView1.Rows.Clear();
            Functions.add_data_to_grid(SearchedProducts, dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
               
                string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        if(MessageBox.Show(string.Format("Do you Want to Delete This Row?" , row.Cells["Product_ID"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string sqlQuery = "Delete FROM Products WHERE ProductID = :ID ";
                            using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                            {
                                cmd.Parameters.Add("ID", OracleDbType.Varchar2).Value = row.Cells["Product_ID"].Value;

                                cmd.ExecuteNonQuery();
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                                Products.RemoveAt(e.RowIndex);
                                con.Close();

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
                MessageBox.Show("You can not Delete An Empty Row!");
            }

        }
    }
}

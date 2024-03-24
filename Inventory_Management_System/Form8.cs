using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

            List<Supplier> SearchedSupplier = new List<Supplier>();
            string search = textBox1.Text;
            //int search_reorder = -1;

            //int search_count = 0;


            for (int i = 0; i < Suppliers.Count(); i++)
            {
                if (search == Suppliers[i].get_Id())
                {
                    SearchedSupplier.Add(Suppliers[i]);
                }
            }
            dataGridView1.Rows.Clear();
            Functions.add_data_to_grid_supp(SearchedSupplier, dataGridView1);
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Functions.add_data_to_grid_supp(Suppliers, dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f2 = new Form2(Products, Suppliers);
            f2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        if (MessageBox.Show(string.Format("Do you Want to Delete This Row?", row.Cells["Supplier_ID"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string sqlQuery = "Select Count(*) From Products Where SupplierID = :ID";
                            using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                            {
                                int count;
                                cmd.Parameters.Add("ID", OracleDbType.Varchar2).Value = row.Cells["Supplier_ID"].Value;
                                count = Convert.ToInt32(cmd.ExecuteScalar());
                                if(count > 0)
                                {
                                    string sqlQuery2 = "UPDATE Products SET SupplierID = '' WHERE SupplierID = :suppid";
                                    using (OracleCommand cmd1 = new OracleCommand(sqlQuery2, con))
                                    {
                                        cmd1.Parameters.Add("suppid", OracleDbType.Varchar2).Value = row.Cells["Supplier_ID"].Value;

                                        cmd1.ExecuteNonQuery();
                                        con.Close();

                                    }
                                }
                                con.Close();

                            }
                            con.Open();
                            string sqlQuery1 = "Delete FROM Supplier WHERE SupplierID = :ID ";
                            string Supplier_id = (string)row.Cells["Supplier_ID"].Value;
                            // In upper line saving value before delete to pass to function whiich clear 
                            // SupplierID from List
                            using (OracleCommand cmd = new OracleCommand(sqlQuery1, con))
                            {
                                
                                cmd.Parameters.Add("ID", OracleDbType.Varchar2).Value =  row.Cells["Supplier_ID"].Value;

                                cmd.ExecuteNonQuery();
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                                con.Close();
                            }

                            Functions.RemoveSupplierById(Suppliers, Supplier_id);
                            
                            for (int i=0;i<Products.Count();i++)
                            {
                                Products[i].ClearSupplierID(Supplier_id);//Deleting from list
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

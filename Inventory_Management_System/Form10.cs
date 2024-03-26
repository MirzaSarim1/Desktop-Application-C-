using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class Form10 : Form
    {
        public List<Product> Products;
        public List<Supplier> Suppliers;
        double Total_Cost = 0.0;
        List<Purchase> Log = new List<Purchase>();
        OracleConnection con;
        public Form10()
        {
            InitializeComponent();
            Products = new List<Product>();
            Suppliers = new List<Supplier>();
        }

        public Form10(List<Product> Products, List<Supplier> Suppliers)
        {
            InitializeComponent();
            this.Products = Products;
            this.Suppliers = Suppliers;
        }

        private void Form10_Load(object sender, EventArgs e)
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
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Must enter product id or product name");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Must enter quantity");
            }
            else if (Convert.ToInt32(textBox3.Text) <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero");
            }
            else
            {
                int id = -1;
                if (textBox1 == null)
                {
                    id = Convert.ToInt32(textBox1.Text);
                }

                string name = Convert.ToString(textBox2.Text);
                for (int i = 0; i < Products.Count(); i++)
                {
                    if (Products[i].get_ID() == id || Products[i].get_Name() == name)
                    {
                        if (!(Products[i].get_StockQuantity() >= Convert.ToInt32(textBox3.Text)))
                        {
                            MessageBox.Show("Required Quantity Not Available Right Now!");
                            textBox3.Clear();
                            return;
                        }
                    }
                }

                int P_id = 0;
                if (textBox1.Text != "")
                {
                    P_id = Convert.ToInt32(textBox1.Text);
                }
                string P_name = textBox2.Text;
                int P_quantity = Convert.ToInt32(textBox3.Text);
                bool available = false;
                double P_cost = 0, Per_Item_Cost = 0.0;
                int pId = 0;
                for (int i = 0; i < Products.Count() ; i++)
                {
                    if (Products[i].get_ID() == P_id || Products[i].get_Name() == P_name)
                    {
                        P_cost = Products[i].get_cost();
                        P_name = Products[i].get_Name(); 
                        pId = Products[i].get_ID();
                        available= true;
                        break;
                    }
                }

                if (!available)
                {
                    MessageBox.Show("Product with this id or name not available");
                }
                else
                {
                    Purchase item= new Purchase();
                    Per_Item_Cost = P_cost * P_quantity;
                    item.set_P_ID(pId);
                    item.set_ID(Log.Count() + 1);
                    item.set_Name(P_name);
                    item.set_Quantity(P_quantity);
                    item.set_cost(P_cost);
                    item.set_Per_Item_Cost(Per_Item_Cost);
                    //....................
                    bool already_added = false;
                    int index_already_added = -1;

                    for (int i = 0; i < Log.Count() ; i++)
                    {
                        if (Log[i].get_Name() == item.get_Name())
                        {
                            already_added= true;
                            index_already_added = i;
                            break;
                        }
                    }
                    if (already_added)
                    {
                        Log[index_already_added].set_Quantity(Log[index_already_added].get_Quantity() + item.get_Quantity());
                        Log[index_already_added].set_Per_Item_Cost(Log[index_already_added].get_Per_Item_Cost() + item.get_Per_Item_Cost());
                    }
                    //.....................
                    else
                    {
                        Log.Add(item);
                    }
                    dataGridView1.Rows.Clear();
                    for(int i = 0; i < Log.Count; i++)
                    {
                        Functions.Purchase_item(Log[i], dataGridView1);
                    }
                    Total_Cost += P_cost * P_quantity;
                    label5.Text = Total_Cost.ToString();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();




                }
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int diff;
            bool flag = false;
            for (int i = 0;i < Log.Count() ;i++)
            {
                for (int j=0; j< Products.Count() ;j++) 
                {
                    if ((Log[i].get_P_id() == Products[j].get_ID() || Log[i].get_Name() == Products[j].get_Name()) && Products[j].get_StockQuantity() > Log[i].get_Quantity())
                    {
                        flag = true;
                        diff = Products[j].get_StockQuantity() - Log[i].get_Quantity();
                        string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
                        
                        using (OracleConnection con = new OracleConnection(connectionString))
                        {
                            try
                            {
                                con.Open();
                                string sqlQuery = "UPDATE Products Set StockQuantity = :diff WHERE PRODUCTID = :id";

                                using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                                {
                                    cmd.Parameters.Add("diff", OracleDbType.Int32).Value = diff;
                                    cmd.Parameters.Add("id", OracleDbType.Int32).Value = Products[j].get_ID();

                                    cmd.ExecuteNonQuery();
                                    Functions.Update_Stocks(Products[j], diff);
                                    con.Close();
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
            if(!flag)
            {
                MessageBox.Show("Cart Is Empty! Add Products First!");
            }
            else
            {
                MessageBox.Show("Order Confirmed! Thank You For Shopping Here :)");
                this.Hide();
                Form f2 = new Form2(Products, Suppliers);
                f2.Show();
            }
        }
    }
}

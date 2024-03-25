using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    internal static class Functions
    {
        internal static string Md5Encry(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static bool SupplierExists(string supplierId)
        {
            bool exists = false;

            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlQuery = "SELECT COUNT(*) FROM Supplier WHERE SupplierID = :supplierId";
                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        cmd.Parameters.Add("supplierId", OracleDbType.Varchar2).Value = supplierId;
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        exists = count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error");
                }
            }

            return exists;
        }

        public static void add_data_to_grid(List<Product> Products, DataGridView grid)
        {
            for (int i = 0; i < Products.Count(); i++)
            {
                grid.Rows.Add(Products[i].get_ID(), Products[i].get_Name(), Products[i].get_cost(), Products[i].get_StockQuantity(), Products[i].get_SupplierID(), Products[i].get_Category(), Products[i].get_ReorderLevel());
            }
        }
        public static void add_data_to_grid_supp(List<Supplier> suppliers, DataGridView grid)
        {
            for (int i = 0; i < suppliers.Count(); i++)
            {
                grid.Rows.Add(suppliers[i].get_Id(), suppliers[i].get_Name(), suppliers[i].get_location());
            }
        }

        public static void RemoveProductById(List<Product> products, int id)
        {
            products.RemoveAll(product => product.get_ID() == id);
        }

        public static void RemoveSupplierById(List<Supplier> suppliers, string id)
        {
            suppliers.RemoveAll(supplier => supplier.get_Id() == id);
        }

        public static void UpdateProductById(List<Product> products, Product product, int id)
        {
            for (int i = 0; i< products.Count(); i++)
            {
                if (products[i].get_ID() == id)
                {
                    products[i].set_Name(product.get_Name());
                    products[i].set_cost(product.get_cost());
                    products[i].set_Category(product.get_Category());
                    products[i].set_StockQuantity(product.get_StockQuantity());
                    products[i].set_ReorderLevel(product.get_ReorderLevel());
                    products[i].set_SupplierID(product.get_SupplierID());
                }
            }
        }

        public static void Purchase_item(Purchase item, DataGridView grid)
        {
            grid.Rows.Add(item.get_ID(),item.get_Name(),item.get_cost(),item.get_Quantity(),item.get_Per_Item_Cost());
        }

    }
}

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

    }
}

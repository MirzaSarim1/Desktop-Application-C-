using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Product
    {
        int ID, StockQuantity, ReorderLevel;
        string Name, SupplierID, Category;
        Double cost;

        public
            Product()
        {
            Name = string.Empty;
            SupplierID = string.Empty;
            Category = string.Empty;
            ID = 0;
            StockQuantity = 0;
            ReorderLevel = 0;
            cost = 0.0;
        }

        // Getter..........................................

        public
            string get_Name()
        { return Name; }

        public
            string get_SupplierID()
        { return SupplierID; }

        public
            string get_Category()
        { return Category; }

        public
            int get_ID()
        { return ID; }

        public
            int get_StockQuantity()
        { return StockQuantity; }

        public
            int get_ReorderLevel()
        { return ReorderLevel; }

        public
            double get_cost()
        { return cost; }

        //.................................................

        //.........................SETTER.....................

        public
            void set_Name(string Name)
        { this.Name = Name; }

        public
            void set_SupplierID(string SupplierID)
        { this.SupplierID = SupplierID; }

        public
            void set_Category(string Category)
        { this.Category = Category; }

        public
            void set_ID(int ID)
        { this.ID = ID; }

        public
            void set_StockQuantity(int StockQuantity)
        { this.StockQuantity = StockQuantity; }


        public
            void set_ReorderLevel(int ReorderLevel)
        { this.ReorderLevel = ReorderLevel; }

        public
            void set_cost(double cost)
        { this.cost = cost; }

        //....................................................

        public List<Product> LoadProducts()
        {
            List<Product> products = new List<Product>();
            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Products ORDER BY ProductID ASC";

                using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                {
                    try
                    {
                        con.Open();
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product();
                                product.ID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                                product.Name = reader.GetString(reader.GetOrdinal("ProductName"));
                                product.cost = reader.GetDouble(reader.GetOrdinal("Cost"));
                                product.StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"));
                                product.SupplierID = reader.GetString(reader.GetOrdinal("SupplierID"));
                                product.Category = reader.GetString(reader.GetOrdinal("Category"));
                                product.ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel"));

                                products.Add(product);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return products;
        }
    }
}

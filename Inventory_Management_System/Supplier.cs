using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Supplier
    {
        string Id, Name, Location;

        public
            Supplier()
        {
            Name = string.Empty;
            Id = string.Empty;
            Location = string.Empty;
        }

        // Getter..........................................

        public
            string get_Name()
        { return Name; }

        public
            string get_Id()
        { return Id; }

        public
            string get_location()
        { return Location; }


        //.................................................

        //.........................SETTER.....................

        public
            void set_Name(string Name)
        { this.Name = Name; }

        public
            void set_SupplierID(string Id)
        { this.Id = Id; }

        public
            void set_Location(string Location)
        { this.Location = Location; }


        //....................................................

        public List<Supplier> LoadSuppliers()
        {
            List<Supplier> Suppliers = new List<Supplier>();
            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Supplier ORDER BY SupplierID ASC";

                using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                {
                    try
                    {
                        con.Open();
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Supplier supplier = new Supplier();
                                supplier.Id = reader.GetString(reader.GetOrdinal("SupplierID"));
                                supplier.Name = reader.GetString(reader.GetOrdinal("SupplierName"));
                                supplier.Location = reader.GetString(reader.GetOrdinal("Location"));

                                Suppliers.Add(supplier);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return Suppliers;
        }
    }
}

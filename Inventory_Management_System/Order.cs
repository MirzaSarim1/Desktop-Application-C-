using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory_Management_System
{
    public class Order
    {
        int p_id, quantity;
        string SupplierId;
        public
            Order()
        {
            p_id = 0;
            quantity = 0;
            SupplierId = "";
        }

        // Getters

        public
            int get_PId()
        { return this.p_id; }

        public
            int get_quantity()
        { return this.quantity; }

        public
            string get_Sid()
        { return this.SupplierId; }

        // setters


        public
            void set_PID(int Id)
        { this.p_id = Id; }

        public
            void set_qunatity(int q)
        { this.quantity = q; }

        public
            void set_sid(string id)
        { this.SupplierId = id; }


        public List<Order> LoadOrder()
        {
            List<Order> orders = new List<Order>();
            string connectionString = @"DATA SOURCE = localhost:1521/XE; USER ID=Inventory_System; PASSWORD=12345";

            using (OracleConnection con = new OracleConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM SupplierProducts ORDER BY ProductID ASC";

                using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                {
                    try
                    {
                        con.Open();
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.set_PID(reader.GetInt32(reader.GetOrdinal("ProductID")));
                                order.set_qunatity(50);  // default order quantity
                                order.set_sid(reader.GetString(reader.GetOrdinal("SupplierId")));
                                //MessageBox.Show(Convert.ToString(order.get_PId()));
                                orders.Add(order);
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return orders;
        }
    }


}

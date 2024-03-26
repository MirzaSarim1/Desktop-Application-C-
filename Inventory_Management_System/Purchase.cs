using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    internal class Purchase
    {
        int ID, Quantity, P_id;
        string Name;
        Double cost, Per_Item_Cost;

        public
            Purchase()
        {
            Name = string.Empty;
            ID = 0;
            P_id = 0;
            Quantity = 0;
            cost = 0.0;
            Per_Item_Cost = 0.0;
        }

        //.................GETTER..............

        public
            string get_Name()
        { return Name; }

        public
            int get_ID()
        { return ID; }

        public
        int get_P_id()
        { return ID; }

        public
            int get_Quantity()
        { return Quantity; }

        public
            double get_cost()
        { return cost; }

        public
            double get_Per_Item_Cost()
        { return Per_Item_Cost; }

        //.....................................

        //.................SETTER..............
        public
            void set_Name(string Name)
        { this.Name = Name; }

        public
            void set_ID(int ID)
        { this.ID = ID; }


        public
            void set_P_ID(int ID)
        { this.ID = ID; }

        public
            void set_Quantity(int Quantity)
        { this.Quantity = Quantity; }

        public
            void set_cost(double cost)
        { this.cost = cost; }

        public
            void set_Per_Item_Cost(double Per_Item_Cost)
        { this.Per_Item_Cost = Per_Item_Cost; }
        //.....................................
    }
}

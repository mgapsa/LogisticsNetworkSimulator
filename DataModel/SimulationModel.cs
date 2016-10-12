using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class SimulationModel
    {
        //connection list can be splitted in order to optimize
        public List<Connection> Connections { get; set; }
        public List<Buyer> Buyers { get; set; }
        public List<Shop> Shops { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Order> Orders { get; set; }

        public SimulationModel()
        {
            Connections = new List<Connection>();
            Buyers = new List<Buyer>();
            Shops = new List<Shop>();
            Suppliers = new List<Supplier>();
            Orders = new List<Order>();
        }
        
    }
}

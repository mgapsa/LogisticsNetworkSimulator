using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class SimulationModel
    {
        public Project Project { get; set; }
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

        public SimulationModel(Project project)
        {
            this.Project = project;
            Connections = new List<Connection>();
            Buyers = new List<Buyer>();
            Shops = new List<Shop>();
            Suppliers = new List<Supplier>();
            Orders = new List<Order>();
        }

        public List<int> GetListOfBuyersIds()
        {
            List<int> idList = new List<int>();
            foreach (Buyer buyer in Buyers)
            {
                if (buyer.Id != 0)
                {
                    idList.Add(buyer.Id);
                }
            }
            return idList;
        }

        public List<int> GetListOfShopsIds()
        {
            List<int> idList = new List<int>();
            foreach (Shop shop in Shops)
            {
                if (shop.Id != 0)
                {
                    idList.Add(shop.Id);
                }
            }
            return idList;
        }

        public List<int> GetListOfSuppliersIds()
        {
            List<int> idList = new List<int>();
            foreach (Supplier supplier in Suppliers)
            {
                if (supplier.Id != 0)
                {
                    idList.Add(supplier.Id);
                }
            }
            return idList;
        }

        public List<int> GetListOfConnectionsIds()
        {
            List<int> idList = new List<int>();
            foreach (Connection connection in Connections)
            {
                if (connection.Id != 0)
                {
                    idList.Add(connection.Id);
                }
            }
            return idList;
        }

    }
}

using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsNetworkSimulator.SimulationEventsHelper
{
    public class NewOrderSupplierToShopEventArgs : EventArgs
    {
        public Supplier Supplier { get; set; }
        public Shop Shop { get; set; }
        public List<Connection> Connections { get; set; }
        public int I { get; set; }

        public NewOrderSupplierToShopEventArgs(Supplier supplier, Shop shop, List<Connection> connections, int i)
        {
            this.Supplier = supplier;
            this.Shop = shop;
            this.Connections = connections;
            this.I = i;
        }

    }
}

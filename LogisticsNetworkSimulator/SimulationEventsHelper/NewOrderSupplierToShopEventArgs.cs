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
        public Connection Connection { get; set; }

        public NewOrderSupplierToShopEventArgs(Supplier supplier, Shop shop, Connection connection)
        {
            this.Supplier = supplier;
            this.Shop = shop;
            this.Connection = connection;
        }

    }
}

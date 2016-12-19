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
        public int I { get; set; }
        public DateTime Time { get; set; }

        public NewOrderSupplierToShopEventArgs(Supplier supplier, Shop shop, Connection connection, int i, DateTime time)
        {
            this.Supplier = supplier;
            this.Shop = shop;
            this.Connection = connection;
            this.I = i;
            this.Time = time;
        }

    }
}

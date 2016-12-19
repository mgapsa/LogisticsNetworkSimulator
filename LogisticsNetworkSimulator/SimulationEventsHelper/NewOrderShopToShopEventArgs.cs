using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsNetworkSimulator.SimulationEventsHelper
{
    public class NewOrderShopToShopEventArgs : EventArgs
    {
        public Shop ShopA { get; set; }
        public Shop ShopB { get; set; }
        public Connection Connection { get; set; }
        public int I { get; set; }
        public DateTime Time { get; set; }


        public NewOrderShopToShopEventArgs(Shop shopA, Shop shopB, Connection connection, int i, DateTime time)
        {
            this.ShopA = shopA;
            this.ShopB = shopB;
            this.Connection = connection;
            this.I = i;
            this.Time = time;
        }
    }
}

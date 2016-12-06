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
        public List<Connection> Connections { get; set; }
        public int I { get; set; }


        public NewOrderShopToShopEventArgs(Shop shopA, Shop shopB, List<Connection> connections, int i)
        {
            this.ShopA = shopA;
            this.ShopB = shopB;
            this.Connections = connections;
            this.I = i;
        }
    }
}

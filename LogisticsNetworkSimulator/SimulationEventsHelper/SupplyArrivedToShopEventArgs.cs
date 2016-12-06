using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsNetworkSimulator.SimulationEventsHelper
{
    public class SupplyArrivedToShopEventArgs : EventArgs
    {
        public Shop Shop { get; set; }
        public int I { get; set; }

        public SupplyArrivedToShopEventArgs(Shop shop, int i)
        {
            this.Shop = shop;
            this.I = i;
        }
    }
}

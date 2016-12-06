using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsNetworkSimulator.SimulationEventsHelper
{
    public class NewOrderShopToBuyerEventArgs : EventArgs
    {
        public Shop Shop { get; set; }
        public Buyer Buyer { get; set; }

        public NewOrderShopToBuyerEventArgs(Shop shop, Buyer buyer)
        {
            this.Shop = shop;
            this.Buyer = buyer;
        }

    }
}

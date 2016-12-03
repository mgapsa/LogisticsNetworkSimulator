using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsNetworkSimulator.SimulationEventsHelper
{
    public class Delegates
    {
        //We define a method signature that tells the listeners how ther custom listener should look like
        public delegate void NewOrderShopToBuyerEventHandler(object sender, NewOrderShopToBuyerEventArgs args);

        public delegate void NewOrderShopToShopEventHandler(object sender, NewOrderShopToShopEventArgs args);

        public delegate void NewOrderSupplierToShopEventHandler(object sender, NewOrderSupplierToShopEventArgs args);

        public delegate void SupplyArrivedToShopEventHandler(object sender, SupplyArrivedToShopEventArgs args);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsNetworkSimulator.SimulationEventsHelper
{
    public class SimulationEventHandler
    {
        public SimulationEventHandler(SimulationUI simulation)
        {
            //set event handlers to event found in simulationUI
            simulation.NewOrderSupplierToShop += simulation_NewOrderSupplierToShop;
            simulation.NewOrderShopToShop += simulation_NewOrderShopToShop;
            simulation.NewOrderShopToBuyer += simulation_NewOrderShopToBuyer;
            simulation.SupplyArrivedToShop += simulation_SupplyArrivedToShop;
        }

        void simulation_NewOrderShopToBuyer(object sender, NewOrderShopToBuyerEventArgs args)
        {
     
        }

        void simulation_NewOrderShopToShop(object sender, NewOrderShopToShopEventArgs args)
        {

        }

        void simulation_NewOrderSupplierToShop(object sender, NewOrderSupplierToShopEventArgs args)
        {

        }

        void simulation_SupplyArrivedToShop(object sender, SupplyArrivedToShopEventArgs args)
        {

        }

    }
}

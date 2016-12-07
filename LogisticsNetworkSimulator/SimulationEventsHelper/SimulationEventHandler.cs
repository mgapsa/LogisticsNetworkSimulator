using DataModel;
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
            Console.WriteLine("Shop To buyer");
            if(args.Shop.GraphData[args.I] >= args.Buyer.NextOrderAmount)
            {
                args.Shop.GraphData[args.I] -= args.Buyer.NextOrderAmount;
                args.Buyer.GraphData[args.I] = args.Buyer.NextOrderAmount;
            }
            else
            {
                throw new ShopException(ShopException.LACKOFSUUPLY);
            }
        }

        void simulation_NewOrderShopToShop(object sender, NewOrderShopToShopEventArgs args)
        {
            Console.WriteLine("Shop To Shop");
        }

        void simulation_NewOrderSupplierToShop(object sender, NewOrderSupplierToShopEventArgs args)
        {
            //ustawic lastordertime
            Console.WriteLine("Supplier To buyer");
            double orderAmount = 100;
            DateTime orderDate = new DateTime(2016, 12, 12, 12, 9, 0);
            switch(args.Shop.Option)
            {
                case EnumTypes.ShopOptions.rS:                    
                    args.Shop.OrdersList.Add(new Order(orderAmount, orderDate));

                    args.Supplier.GraphData[args.I] += orderAmount;
                    break;
                case EnumTypes.ShopOptions.rsS:
                    break;
                case EnumTypes.ShopOptions.sq:
                    break;
            }
        }

        void simulation_SupplyArrivedToShop(object sender, SupplyArrivedToShopEventArgs args)
        {
            Console.WriteLine("ORDER");

        }

    }
}

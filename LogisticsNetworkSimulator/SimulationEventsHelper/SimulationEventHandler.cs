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
            if(args.Shop.GraphData[args.I] >= args.Buyer.NextOrderAmount)
            {
                args.Shop.GraphData[args.I] -= args.Buyer.NextOrderAmount;
                args.Buyer.GraphData[args.I] = args.Buyer.NextOrderAmount;

                Console.WriteLine(args.Time.ToString() + "Shop To buyer" + args.Buyer.NextOrderAmount.ToString());
            }
            else
            {
                throw new ShopException(ShopException.LACKOFSUUPLY);
            }
        }

        void simulation_NewOrderShopToShop(object sender, NewOrderShopToShopEventArgs args)
        {
            if(args.ShopB.LastOrderAmount == -1 || args.ShopB.LastOrderTime != args.Time)
            {
                //generate order like that so that others can use it
                args.ShopB.GenerateOrder(args.Time, args.I);
            }

            Random rnd = new Random();
            int rand = rnd.Next(Convert.ToInt32(args.Connection.MinDelay), Convert.ToInt32(args.Connection.MaxDelay));
            DateTime orderTime = args.Time.AddHours(rand);
            Double orderAmount = (args.Connection.Usage * args.ShopB.LastOrderAmount) / 100;

            if(args.ShopA.GraphData[args.I] > orderAmount)
            {
                args.ShopA.GraphData[args.I] -= orderAmount;

                Order order = new Order(orderAmount, orderTime);
                args.ShopB.OrdersList.Add(order);

                Console.WriteLine(args.Time.ToString() + "Shop To Shop" + order.Amount.ToString() + "   " + order.ArrivalTime.ToString());
            }
            else
            {
                throw new ShopException(ShopException.LACKOFSUUPLY);
            }


        }

        void simulation_NewOrderSupplierToShop(object sender, NewOrderSupplierToShopEventArgs args)
        {
            if (args.Shop.LastOrderAmount == -1 || args.Shop.LastOrderTime != args.Time)
            {
                //generate order like that so that others can use it
                args.Shop.GenerateOrder(args.Time, args.I);
            }

            Random rnd = new Random();
            int rand = rnd.Next(Convert.ToInt32(args.Connection.MinDelay), Convert.ToInt32(args.Connection.MaxDelay) + 1);
            DateTime orderTime = args.Time.AddHours(rand);
            Double orderAmount = (args.Connection.Usage * args.Shop.LastOrderAmount) / 100;

            args.Supplier.GraphData[args.I] += orderAmount;
            Order order = new Order(orderAmount, orderTime);
            args.Shop.OrdersList.Add(order);

            Console.WriteLine(args.Time.ToString() + "Supplier To Shop" + order.Amount.ToString() + "   " + order.ArrivalTime.ToString());

        }

        void simulation_SupplyArrivedToShop(object sender, SupplyArrivedToShopEventArgs args)
        {
            Shop shop = args.Shop;
            bool flag = true;
            while(flag)
            {
                flag = false;
                foreach (Order order in shop.OrdersList)
                {
                    if(order.ArrivalTime <= args.Time)
                    {
                        Console.WriteLine(args.Time.ToString() + "Order arrived" + order.Amount.ToString() + "   " + order.ArrivalTime.ToString());
                        shop.GraphData[args.I] += order.Amount;
                        shop.OrdersList.Remove(order);
                        flag = true;
                        break;
                    }
                }
            }


        }

    }
}

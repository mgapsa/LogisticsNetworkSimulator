using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Shop : Actor
    {
        public EnumTypes.ShopOptions Option { get; set; }

        public List<Order> OrdersList { get; set; }

        public double InitialAmount { get; set; }
        public double CurrentAmount { get; set; }

        public double Policy_sq_s { get; set; }
        public double Policy_sq_q { get; set; }

        public double Policy_rS_r { get; set; }
        public double Policy_rS_S { get; set; }

        public double Policy_rsS_r { get; set; }
        public double Policy_rsS_s { get; set; }
        public double Policy_rsS_Sbig { get; set; }

        public int MyProperty { get; set; }

        public DateTime LastOrderTime { get; set; }
        public double LastOrderAmount { get; set; }

        public Shop()
        {
            //TODO form preferences?
            this.Option = EnumTypes.ShopOptions.sq;

            this.LastOrderAmount = -1;

            this.InitialAmount = 1000;

            this.Policy_sq_s = 700;
            this.Policy_sq_q = 100;

            this.Policy_rS_r = 60;
            this.Policy_rS_S = 1000;

            this.Policy_rsS_r = 60;
            this.Policy_rsS_s = 700;
            this.Policy_rsS_Sbig = 1000;

            OrdersList = new List<Order>();
        }

        //copy constructor
        public Shop(Shop shop) :base(shop)
        {
            this.Option = shop.Option;

            this.InitialAmount = shop.InitialAmount;

            this.Policy_sq_s = shop.Policy_sq_s;
            this.Policy_sq_q = shop.Policy_sq_q;

            this.Policy_rS_r = shop.Policy_rS_r;
            this.Policy_rS_S = shop.Policy_rS_S;

            this.Policy_rsS_r = shop.Policy_rsS_r;
            this.Policy_rsS_s = shop.Policy_rsS_s;
            this.Policy_rsS_Sbig = shop.Policy_rsS_Sbig;

            OrdersList = new List<Order>();
        }

        public bool MakeOrder(DateTime currentTime, int i)
        {
            //specia if to fasten it
            if (currentTime == LastOrderTime)
            {
                return true;
            }

            //if first time, it is first loop, it is 0 minute - if I change logic of simulation to add one minute imm
            if(LastOrderTime == new DateTime())
            {
                LastOrderTime = currentTime;
            }
            switch(Option)
            {
                case EnumTypes.ShopOptions.rS:
                    if(currentTime == LastOrderTime ||  (currentTime - LastOrderTime).TotalMinutes >= this.Policy_rS_r)
                    {
                        if(ProductShortage(i, this.Policy_rS_S, false))
                            return true;
                    }
                    break;
                case EnumTypes.ShopOptions.rsS:
                    if(currentTime == LastOrderTime || (currentTime - LastOrderTime).TotalMinutes >= this.Policy_rsS_r)
                    {
                        if(ProductShortage(i, this.Policy_rsS_s, true))
                        {
                            return true;
                        }
                    }
                    break;
                case EnumTypes.ShopOptions.sq:
                    if(ProductShortage(i,this.Policy_sq_s, true))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private bool ProductShortage(int i, double s, bool equal)
        {
            //TODO sum orders
            double sumOfIncomingOrders = 0;
            foreach (Order order in OrdersList)
            {
                sumOfIncomingOrders += order.Amount;
            }
            if(equal)
            {
                if (GraphData[i] + sumOfIncomingOrders <= s)
                    return true;
            }
            else
            {
                if (GraphData[i] + sumOfIncomingOrders < s)
                    return true;
            }
            return false;
        }

        public void GenerateOrder(DateTime currentTime, int i)
        {
            double sumOfIncomingOrders = 0;
            foreach (Order order in OrdersList)
            {
                sumOfIncomingOrders += order.Amount;
            }

            LastOrderTime = currentTime;

            switch(this.Option)
            {
                case EnumTypes.ShopOptions.rS:
                    this.LastOrderAmount = this.Policy_rS_S - this.GraphData[i] - sumOfIncomingOrders;
                    break;
                case EnumTypes.ShopOptions.rsS:
                    this.LastOrderAmount = this.Policy_rsS_Sbig - this.GraphData[i] - sumOfIncomingOrders;
                    break;
                case EnumTypes.ShopOptions.sq:
                    this.LastOrderAmount = this.Policy_sq_q;
                    break;
            }
        }

        public bool OrderArrived(DateTime currentTime)
        {
            foreach(Order order in OrdersList)
            {
                if (order.ArrivalTime <= currentTime)
                    return true;
            }
            return false;
        }

        public override void SetDataSize(int size)
        {
            base.SetDataSize(size);
            this.GraphData[0] = InitialAmount;
        }
    }
}

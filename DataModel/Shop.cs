using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Shop : Actor
    {
        //ile chce mieć, taka wartosc bazowa
        public double BasicAmount { get; set; }
        //minimalna dopuszczalna
        public double MinAmount { get; set; }
        public double InitialAmount { get; set; }
        public double StorageCost { get; set; }

        public Shop()
        {
            //TODO form preferences?
            this.BasicAmount = 100;
            this.MinAmount = 80;
            this.InitialAmount = 100;
            this.StorageCost = 10;
        }

        //copy constructor
        public Shop(Shop shop) :base(shop)
        {
            this.BasicAmount = shop.BasicAmount;
            this.MinAmount = shop.MinAmount;
            this.InitialAmount = shop.InitialAmount;
            this.StorageCost = shop.StorageCost;
        }
    }
}

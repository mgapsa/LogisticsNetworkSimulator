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

        public double InitialAmount { get; set; }

        public double Policy_sq_s { get; set; }
        public double Policy_sq_q { get; set; }

        public double Policy_rS_r { get; set; }
        public double Policy_rS_S { get; set; }

        public double Policy_rsS_r { get; set; }
        public double Policy_rsS_s { get; set; }
        public double Policy_rsS_Sbig { get; set; }

        public Shop()
        {
            //TODO form preferences?
            this.Option = EnumTypes.ShopOptions.sq;

            this.InitialAmount = 100;

            this.Policy_sq_s = 900;
            this.Policy_sq_q = 100;

            this.Policy_rS_r = 60;
            this.Policy_rS_S = 1000;

            this.Policy_rsS_r = 60;
            this.Policy_rsS_s = 900;
            this.Policy_rsS_Sbig = 1000;
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
        }
    }
}

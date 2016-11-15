using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class EnumTypes
    {
        public enum ConnectionTypes
        {
            SupplierToShop = 1,
            ShopToShop,
            ShopToBuyer
        };

        public enum UserControlTypes
        {
            BuyerUserControl = 1,
            ShopUserControl,
            SupplierUserControl
        };

        public enum BuyerOptions
        {

        };
    }
}

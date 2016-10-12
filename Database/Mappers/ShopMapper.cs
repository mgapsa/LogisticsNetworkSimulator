using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Dapper.FluentMap.Mapping;

namespace Database.Mappers
{
    public class ShopMapper : EntityMap<Shop>
    {
        public ShopMapper()
        {
            Map(x => x.Id).ToColumn("shop_id");
            Map(x => x.ProjectId).ToColumn("shop_project_id");
            Map(x => x.X).ToColumn("shop_x");
            Map(x => x.Y).ToColumn("shop_y");
            Map(x => x.BasicAmount).ToColumn("shop_basic_amount");
            Map(x => x.InitialAmount).ToColumn("shop_initial_value");
            Map(x => x.MinAmount).ToColumn("shop_min_amount");
            Map(x => x.StorageCost).ToColumn("shop_storage_cost");
        }
    }
}

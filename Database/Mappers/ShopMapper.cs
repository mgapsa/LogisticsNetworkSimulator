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
            Map(x => x.InitialAmount).ToColumn("shop_initial_value");
            Map(x => x.Option).ToColumn("shop_policy");

            Map(x => x.Policy_sq_s).ToColumn("shop_policy_sq_s");
            Map(x => x.Policy_sq_q).ToColumn("shop_policy_sq_q");

            Map(x => x.Policy_rS_r).ToColumn("shop_policy_rs_r");
            Map(x => x.Policy_rS_S).ToColumn("shop_policy_rs_s");

            Map(x => x.Policy_rsS_r).ToColumn("shop_policy_rss_r");
            Map(x => x.Policy_rsS_s).ToColumn("shop_policy_rss_s");
            Map(x => x.Policy_rsS_Sbig).ToColumn("shop_policy_rss_bigs");
        }
    }
}

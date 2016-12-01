using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Dapper.FluentMap.Mapping;

namespace Database.Mappers
{
    public class BuyerMapper : EntityMap<Buyer>
    {
        public BuyerMapper()
        {
            Map(x => x.Id).ToColumn("buyer_id");
            Map(x => x.ProjectId).ToColumn("buyer_project_id");
            Map(x => x.X).ToColumn("buyer_x");
            Map(x => x.Y).ToColumn("buyer_y");
            Map(x => x.OptionA).ToColumn("buyer_option_a");
            Map(x => x.OptionB).ToColumn("buyer_option_b");
            Map(x => x.Amount).ToColumn("buyer_amount");
            Map(x => x.MinAmount).ToColumn("buyer_min_amount");
            Map(x => x.MaxAmount).ToColumn("buyer_max_amount");
            Map(x => x.Lambda).ToColumn("buyer_lambda");
            Map(x => x.MeanOptionA).ToColumn("buyer_mean_option_a");
            Map(x => x.DeviationOptionA).ToColumn("buyer_deviation_option_a");
            Map(x => x.Minutes).ToColumn("buyer_minutes");
            Map(x => x.MeanOptionB).ToColumn("buyer_mean_option_b");
            Map(x => x.DeviationOptionB).ToColumn("buyer_deviation_option_b");
            
        }
    }
}

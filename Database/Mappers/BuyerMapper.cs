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
            Map(x => x.ParamA).ToColumn("buyer_param_a");
            Map(x => x.ParamB).ToColumn("buyer_param_b");
            Map(x => x.ParamC).ToColumn("buyer_param_c");
            Map(x => x.ParamD).ToColumn("buyer_param_d");
            Map(x => x.ParamE).ToColumn("buyer_param_e");
            Map(x => x.ParamF).ToColumn("buyer_param_f");
        }
    }
}

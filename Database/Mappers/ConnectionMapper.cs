using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Dapper.FluentMap.Mapping;


namespace Database.Mappers
{
    public class ConnectionMapper : EntityMap<Connection>
    {
        public ConnectionMapper()
        {
            Map(x => x.Id).ToColumn("connection_id");
            Map(x => x.ProjectId).ToColumn("connection_project_id");
            Map(x => x.ActorAId).ToColumn("connection_actor_a_id");
            Map(x => x.ActorBId).ToColumn("connection_actor_b_id");
            Map(x => x.ConnectionType).ToColumn("connection_connection_type");
            Map(x => x.MaxDelay).ToColumn("connection_max_delay");
            Map(x => x.MinDelay).ToColumn("connection_min_delay");
            Map(x => x.TransportCost).ToColumn("connection_order_cost");
            Map(x => x.Usage).ToColumn("connection_usage");
        }
    }
}

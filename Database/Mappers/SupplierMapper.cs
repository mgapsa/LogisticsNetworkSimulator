using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Dapper.FluentMap.Mapping;

namespace Database.Mappers
{
    public class SupplierMapper : EntityMap<Supplier>
    {
        public SupplierMapper()
        {
            Map(x => x.Id).ToColumn("supplier_id");
            Map(x => x.ProjectId).ToColumn("supplier_project_id");
            Map(x => x.X).ToColumn("supplier_x");
            Map(x => x.Y).ToColumn("supplier_y");
        }
    }
}

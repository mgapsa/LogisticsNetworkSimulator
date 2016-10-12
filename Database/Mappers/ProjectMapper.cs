using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Dapper.FluentMap.Mapping;

namespace Database.Mappers
{
    public class ProjectMapper : EntityMap<Project>
    {
        public ProjectMapper()
        {
            Map(x => x.Id).ToColumn("project_id");
            Map(x => x.Name).ToColumn("project_name");
            Map(x => x.Description).ToColumn("project_description");
        }
    }
}

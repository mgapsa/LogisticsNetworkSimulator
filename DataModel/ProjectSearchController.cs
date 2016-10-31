using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ProjectSearchController
    {
        public IEnumerable<Project> Filter(List<Project> projectList, string name)
        {
            return projectList.Where(p => p.Name.Contains(name));
        }
    }
}

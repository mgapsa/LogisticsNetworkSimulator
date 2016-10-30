using Database;
using Database.Brokers;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProjectService
    {
        public int AddProject(Project project)
        {
            using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            {
                return new ProjectBroker().Save(connection, project);
            }
        }

        public List<Project> GetAllProjects()
        {
            using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            {
                return new ProjectBroker().GetAll(connection).ToList();
            }
        }

        public int DeleteProject(Project project)
        {
            using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            {
                return new ProjectBroker().Delete(connection,project.Id);
            }
        }
    }
}

using DataModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Database.Brokers
{
    public class ProjectBroker : IBroker<Project>
    {
        public Project Get(IDbConnection connection, int searchId)
        {
            return connection.Query<Project>(
            "SELECT * FROM project WHERE project_id = @id",
            new { id = searchId }).FirstOrDefault();
        }

        public IEnumerable<Project> GetAll(IDbConnection connection)
        {
            return connection.Query<Project>(
            "SELECT * FROM project").ToList();
        }

        public int Insert(IDbConnection connection, Project item)
        {
            const string query =
              "INSERT INTO project (project_name, project_description) " +
              "VALUES (@Name, @Description)";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Update(IDbConnection connection, Project item)
        {
            const string query = "UPDATE project " +
                     "SET project_name = @Name, " +
                     "project_description = @Description " +
                     "WHERE project_id = @Id";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Save(IDbConnection connection, Project item)
        {
            if(item.Id == 0)
            {
                return Insert(connection, item);
            }
            else
            {
                return Update(connection, item);
            }
        }

        public int Delete(IDbConnection connection, int deleteId)
        {
            const string query = "DELETE FROM project " +
                 "WHERE project_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

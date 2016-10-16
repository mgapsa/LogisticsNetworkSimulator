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
    public class SupplierBroker : IBroker<Supplier>
    {
        public IEnumerable<Supplier> GetAllFromProject(IDbConnection connection, int projectId)
        {
            return connection.Query<Supplier>(
            "SELECT * FROM supplier WHERE supplier_project_id = @id", new { id = projectId }).ToList();
        }

        public int Insert(IDbConnection connection, Supplier item, IDbTransaction transaction = null)
        {
            const string query =
              "INSERT INTO supplier (supplier_project_id, supplier_x, supplier_y) " +
              "VALUES (@ProjectId, @X, @Y)";

            int rowsAffected;
            if (transaction != null)
            {
                rowsAffected = connection.Execute(query, item, transaction);
            }
            else
            {
                rowsAffected = connection.Execute(query, item);
            }

            item.Id = connection.Query<int>(
            "SELECT supplier_id FROM supplier WHERE supplier_project_id = @projectId and supplier_x = @x and supplier_y = @y",
            new { projectId = item.ProjectId, x = item.X, y = item.Y }).FirstOrDefault();

            return rowsAffected;
        }

        public int Update(IDbConnection connection, Supplier item, IDbTransaction transaction = null)
        {
            const string query = "UPDATE supplier " +
                     "SET supplier_x = @X, " +
                     "supplier_y = @Y " +
                     "WHERE supplier_id = @Id";

            int rowsAffected;
            if (transaction != null)
            {
                rowsAffected = connection.Execute(query, item, transaction);
            }
            else
            {
                rowsAffected = connection.Execute(query, item);
            }

            return rowsAffected;
        }

        public int Save(IDbConnection connection, Supplier item, IDbTransaction transaction = null)
        {
            if (item.Id == 0)
            {
                return Insert(connection, item, transaction);
            }
            else
            {
                return Update(connection, item, transaction);
            }
        }

        public int RemoveNotExistingFromProject(IDbConnection connection, int projectId, List<int> idList)
        {
            const string query = "DELETE FROM supplier " +
            "WHERE supplier_project_id = @projectId AND supplier_id NOT IN @ids";
            int rowsAffected = connection.Execute(query, new { projectId = projectId, ids = idList });
            return rowsAffected;
        }

        public Supplier Get(IDbConnection connection, int searchId)
        {
            return connection.Query<Supplier>(
            "SELECT * FROM supplier WHERE supplier_id = @id",
            new { id = searchId }).FirstOrDefault();
        }

        public IEnumerable<Supplier> GetAll(IDbConnection connection)
        {
            return connection.Query<Supplier>(
            "SELECT * FROM supplier").ToList();
        }

        public int Delete(IDbConnection connection, int deleteId)
        {
            const string query = "DELETE FROM supplier " +
                 "WHERE supplier_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

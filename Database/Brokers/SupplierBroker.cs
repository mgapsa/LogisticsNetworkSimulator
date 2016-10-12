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

        public int Insert(IDbConnection connection, Supplier item)
        {
            const string query =
              "INSERT INTO supplier (supplier_project_id, supplier_x, supplier_y) " +
              "VALUES (@ProjectId, @X, @Y)";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Update(IDbConnection connection, Supplier item)
        {
            const string query = "UPDATE supplier " +
                     "SET supplier_x = @X, " +
                     "SET supplier_y = @Y " +
                     "WHERE supplier_id = @Id";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Save(IDbConnection connection, Supplier item)
        {
            if (item.Id == 0)
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
            const string query = "DELETE FROM supplier " +
                 "WHERE supplier_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

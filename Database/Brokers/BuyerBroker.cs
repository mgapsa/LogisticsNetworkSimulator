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
    public class BuyerBroker : IBroker<Buyer>
    {
        public IEnumerable<Buyer> GetAllFromProject(IDbConnection connection, int projectId)
        {
            return connection.Query<Buyer>(
            "SELECT * FROM buyer WHERE buyer_project_id = @id", new { id = projectId}).ToList();
        }

        public int RemoveNotExistingFromProject(IDbConnection connection, int projectId, List<int> idList)
        {
            const string query = "DELETE FROM buyer " +
            "WHERE buyer_project_id = @projectId AND buyer_id NOT IN @ids";
            int rowsAffected = connection.Execute(query, new { projectId = projectId, ids = idList });
            return rowsAffected;
        }

        public int Insert(IDbConnection connection, Buyer item, IDbTransaction transaction = null)
        {
            const string query =
              "INSERT INTO buyer (buyer_project_id, buyer_x, buyer_y, buyer_option_a, buyer_option_b, buyer_param_a, buyer_param_b, buyer_param_c, buyer_param_d, buyer_param_e, buyer_param_f) " +
              "VALUES (@ProjectId, @X, @Y, @OptionA, @OptionB, @ParamA, @ParamB, @ParamC, @ParamD, @ParamE, @ParamF)";

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
            "SELECT buyer_id FROM buyer WHERE buyer_project_id = @projectId and buyer_x = @x and buyer_y = @y",
            new { projectId = item.ProjectId, x = item.X, y = item.Y }).FirstOrDefault();

            return rowsAffected;
        }

        public int Update(IDbConnection connection, Buyer item, IDbTransaction transaction = null)
        {
            const string query = "UPDATE buyer " +
                     "SET buyer_x = @X, " +
                     "buyer_y = @Y, " +
                     "buyer_option_a = @OptionA, " +
                     "buyer_option_b = @OptionB, " +
                     "buyer_param_a = @ParamA, " +
                     "buyer_param_b = @ParamB, " +
                     "buyer_param_c = @ParamC, " +
                     "buyer_param_d = @ParamD, " +
                     "buyer_param_e = @ParamE, " +
                     "buyer_param_f = @ParamF " +
                     "WHERE buyer_id = @Id";

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

        public int Save(IDbConnection connection, Buyer item, IDbTransaction transaction = null)
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

        public Buyer Get(IDbConnection connection, int searchId)
        {
            return connection.Query<Buyer>(
            "SELECT * FROM buyer WHERE buyer_id = @id",
            new { id = searchId }).FirstOrDefault();
        }

        public IEnumerable<Buyer> GetAll(IDbConnection connection)
        {
            return connection.Query<Buyer>(
            "SELECT * FROM buyer").ToList();
        }

        public int Delete(IDbConnection connection, int deleteId)
        {
            const string query = "DELETE FROM buyer " +
                 "WHERE buyer_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

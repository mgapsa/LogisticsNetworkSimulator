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
    public class ShopBroker : IBroker<Shop>
    {
        public IEnumerable<Shop> GetAllFromProject(IDbConnection connection, int projectId)
        {
            return connection.Query<Shop>(
            "SELECT * FROM shop WHERE shop_project_id = @id", new { id = projectId }).ToList();
        }

        public int Insert(IDbConnection connection, Shop item, IDbTransaction transaction = null)
        {
            const string query =
              "INSERT INTO shop (shop_project_id, shop_x, shop_y, shop_initial_value, shop_policy, shop_policy_sq_s, shop_policy_sq_q, shop_policy_rs_r, shop_policy_rs_s, shop_policy_rss_r, shop_policy_rss_s, shop_policy_rss_bigs ) " +
              "VALUES (@ProjectId, @X, @Y, @InitialAmount, @Option, @Policy_sq_s, @Policy_sq_q, @Policy_rS_r, @Policy_rS_S, @Policy_rsS_r, @Policy_rsS_s, @Policy_rsS_Sbig)";

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
            "SELECT shop_id FROM shop WHERE shop_project_id = @projectId and shop_x = @x and shop_y = @y",
            new { projectId = item.ProjectId, x = item.X, y = item.Y }).FirstOrDefault();

            return rowsAffected;
        }

        public int Update(IDbConnection connection, Shop item, IDbTransaction transaction = null)
        {
            const string query = "UPDATE shop " +
                     "SET shop_x = @X, " +
                     "shop_y = @Y, " +
                     "shop_initial_value = @InitialAmount, " +
                     "shop_policy = @Option, " +
                     "shop_policy_sq_s = @Policy_sq_s, " +
                     "shop_policy_sq_q = @Policy_sq_q, " +
                     "shop_policy_rs_r = @Policy_rS_r, " +
                     "shop_policy_rs_s = @Policy_rS_S, " +
                     "shop_policy_rss_r = @Policy_rsS_r, " +
                     "shop_policy_rss_s = @Policy_rsS_s, " +
                     "shop_policy_rss_bigs = @Policy_rsS_Sbig " +
                     "WHERE shop_id = @Id";

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

        public int Save(IDbConnection connection, Shop item, IDbTransaction transaction = null)
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
            const string query = "DELETE FROM shop " +
            "WHERE shop_project_id = @projectId AND shop_id NOT IN @ids";
            int rowsAffected = connection.Execute(query, new { projectId = projectId, ids = idList });
            return rowsAffected;
        }

        public Shop Get(IDbConnection connection, int searchId)
        {
            return connection.Query<Shop>(
            "SELECT * FROM shop WHERE shop_id = @id",
            new { id = searchId }).FirstOrDefault();
        }

        public IEnumerable<Shop> GetAll(IDbConnection connection)
        {
            return connection.Query<Shop>(
            "SELECT * FROM shop").ToList();
        }

        public int Delete(IDbConnection connection, int deleteId)
        {
            const string query = "DELETE FROM shop " +
                 "WHERE shop_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

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

        public int Insert(IDbConnection connection, Shop item)
        {
            const string query =
              "INSERT INTO shop (shop_project_id, shop_x, shop_y, shop_basic_amount, shop_min_amount, shop_storage_cost, shop_initial_value) " +
              "VALUES (@ProjectId, @X, @Y, @BasicAmount, @MinAmount, @StorageCost, @InitialAmount)";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Update(IDbConnection connection, Shop item)
        {
            const string query = "UPDATE shop " +
                     "SET shop_x = @X, " +
                     "SET shop_y = @Y, " +
                     "SET shop_basic_amount = @BasicAmount, " +
                     "SET shop_min_amount = @MinAmount, " +
                     "SET shop_storage_cost = @StorageCost, " +
                     "SET shop_initial_value = @InitialValue " +
                     "WHERE shop_id = @Id";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Save(IDbConnection connection, Shop item)
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
            const string query = "DELETE FROM shop " +
                 "WHERE shop_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

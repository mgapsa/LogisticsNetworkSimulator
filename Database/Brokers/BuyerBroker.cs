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

        public int Insert(IDbConnection connection, Buyer item)
        {
            const string query =
              "INSERT INTO buyer (buyer_project_id, buyer_x, buyer_y, buyer_option_a, buyer_option_b, buyer_param_a, buyer_param_b, buyer_param_c, buyer_param_d, buyer_param_e, buyer_param_f) " +
              "VALUES (@ProjectId, @X, @Y, @OptionA, @OptionB, @ParamA, @ParamB, @ParamC, @ParamD, @ParamE, @ParamF)";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Update(IDbConnection connection, Buyer item)
        {
            const string query = "UPDATE buyer " +
                     "SET buyer_x = @X, " +
                     "SET buyer_y = @Y, " +
                     "SET buyer_option_a = @OptionA, " +
                     "SET buyer_option_b = @OptionB, " +
                     "SET buyer_param_a = @ParamA, " +
                     "SET buyer_param_b = @ParamB, " +
                     "SET buyer_param_c = @ParamC, " +
                     "SET buyer_param_d = @ParamD, " +
                     "SET buyer_param_e = @ParamE, " +
                     "SET buyer_param_f = @ParamF " +
                     "WHERE buyer_id = @Id";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Save(IDbConnection connection, Buyer item)
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
            const string query = "DELETE FROM buyer " +
                 "WHERE buyer_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

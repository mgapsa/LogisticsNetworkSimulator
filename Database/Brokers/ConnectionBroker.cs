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
    public class ConnectionBroker : IBroker<Connection>
    {
        public Connection Get(IDbConnection connection, int searchId)
        {
            return connection.Query<Connection>(
            "SELECT * FROM connection WHERE connection_id = @id",
            new { id = searchId }).FirstOrDefault();
        }

        public IEnumerable<Connection> GetAll(IDbConnection connection)
        {
            return connection.Query<Connection>(
            "SELECT * FROM connection").ToList();
        }

        public int Insert(IDbConnection connection, Connection item)
        {
            const string query =
              "INSERT INTO connection (connection_project_id, connection_actor_a_id, connection_actor_b_id, connection_connection_type ,connection_transport_cost, connection_usage, connection_min_delay, connection_max_delay) " +
              "VALUES (@ProjectId, @ActorAId, @ActorBId, @ConnectionType, @TransportCost, @Usage, @MinDelay, @MaxDelay)";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Update(IDbConnection connection, Connection item)
        {
            const string query = "UPDATE connection " +
                     "SET connection_project_id = @ProjectId, " +
                     "connection_actor_a_id = @ActorAId, " +
                     "connection_actor_b_id = @ActorBId, " +
                     "connection_connection_type = @ConnectionType, " +
                     "connection_transport_cost = @TransportCost, " +
                     "connection_usage = @Usage, " +
                     "connection_min_delay= @MinDelay, " +
                     "connection_max_delay = @MaxDelay " +
                     "WHERE connection_id = @Id";
            int rowsAffected = connection.Execute(query, item);
            return rowsAffected;
        }

        public int Save(IDbConnection connection, Connection item)
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
            const string query = "DELETE FROM connection " +
                 "WHERE connection_id = @id";
            int rowsAffected = connection.Execute(query, new { id = deleteId });
            return rowsAffected;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Brokers
{
    public interface IBroker<T>
    {
        T Get(IDbConnection connection, int searchId);

        IEnumerable<T> GetAll(IDbConnection connection);

        int Insert(IDbConnection connection, T item);

        int Update(IDbConnection connection, T item);

        int Save(IDbConnection connection, T item);

        int Delete(IDbConnection connection, int deleteId);
    }
}

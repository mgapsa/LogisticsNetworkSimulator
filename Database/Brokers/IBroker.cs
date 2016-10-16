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

        int Insert(IDbConnection connection, T item, IDbTransaction transaction);

        int Update(IDbConnection connection, T item, IDbTransaction transaction);

        int Save(IDbConnection connection, T item, IDbTransaction transaction);

        int Delete(IDbConnection connection, int deleteId);
    }
}

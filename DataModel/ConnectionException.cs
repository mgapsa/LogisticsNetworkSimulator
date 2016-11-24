using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ConnectionException : Exception
    {
        public static string ILLEGALCONNECTIONFROMBUYER = "Cannot create any connection from buyer!";
        public static string ILLEGALCONNECTIONTOSUPPLIER = "Cannot create any connection to supplier!";
        public static string ILLEGALCONNECTIONSUPPLIERBUYER = "Cannot create connection from supplier to buyer";
        public static string CONNECTIONALREADYEXISTS = "Connection already exists";

        public ConnectionException()
        {

        }

        public ConnectionException(string message) : base(message)
        {

        }

        public ConnectionException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

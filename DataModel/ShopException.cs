using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ShopException : Exception
    {
        public static string LACKOFSUUPLY = "Shop lacks supplies!";

        public ShopException()
        {

        }

        public ShopException(string message) : base(message)
        {

        }

        public ShopException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

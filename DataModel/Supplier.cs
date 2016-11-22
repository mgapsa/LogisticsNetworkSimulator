using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Supplier : Actor
    {
        public Supplier()
        {

        }

        //copy constructor
        public Supplier(Supplier supplier) : base(supplier)
        {

        }
    }
}
